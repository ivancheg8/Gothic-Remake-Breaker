using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gothic_Remake_Breaker
{
    public enum EffectType { None, Same, Opposite }
    public enum Direction { Left, Right }

    public class LockConfig
    {
        public int PlateCount { get; set; }
        public int[] StartPositions { get; set; }
        // Множество [source, target]. Как движение source влияет на target.
        public EffectType[,] Effects { get; set; }
    }

    public class MoveGroup
    {
        public int PlateIndex { get; set; } // 0-based
        public Direction Direction { get; set; }
        public int Count { get; set; }
    }

    public class SolverResult
    {
        public bool Success { get; set; }
        public bool Timeout { get; set; }
        public List<MoveGroup> Moves { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class LockpickSolver
    {
        private const int MIN_POS = -3;
        private const int MAX_POS = 3;
        private const int TIMEOUT_MS = 15000; // 15 секунд, как в оригинале

        public SolverResult Solve(LockConfig config, bool fewerSwitches = true)
        {
            if (fewerSwitches)
                return SolveFewerPlateSwitches(config);
            else
                return SolveShortestMoves(config);
        }

        // Применяем одно движение к массиву состояний
        private int[] ApplyMove(int[] current, int plateIndex, Direction dir, LockConfig config)
        {
            int delta = dir == Direction.Left ? 1 : -1; // В оригинале Left = +1, Right = -1
            int[] next = new int[config.PlateCount];
            Array.Copy(current, next, config.PlateCount);

            next[plateIndex] += delta;

            for (int target = 0; target < config.PlateCount; target++)
            {
                if (target == plateIndex) continue;
                var effect = config.Effects[plateIndex, target];

                if (effect == EffectType.Same) next[target] += delta;
                else if (effect == EffectType.Opposite) next[target] -= delta;
            }

            // Проверка границ: если хоть одна пластина вылезла за -3..+3, ход невозможен
            foreach (var val in next)
            {
                if (val < MIN_POS || val > MAX_POS) return null;
            }
            return next;
        }

        // Быстрое хеширование состояния в одно число (битовый сдвиг)
        // Значения от -3 до 3. Прибавляем 3, получаем 0..6 (занимает 3 бита).
        // 7 пластин * 3 бита = 21 бит, идеально помещается в int.
        private int GetStateKey(int[] values)
        {
            int key = 0;
            for (int i = 0; i < values.Length; i++)
            {
                key |= (values[i] + 3) << (i * 3);
            }
            return key;
        }

        private bool IsGoal(int[] values)
        {
            return values.All(v => v == 0);
        }

        private List<MoveGroup> BuildSolution(int goalIndex, List<int[]> states, List<(int prev, MoveGroup move)> parents)
        {
            var moves = new List<MoveGroup>();
            int index = goalIndex;

            while (index != -1)
            {
                var parent = parents[index];
                if (parent.move != null) moves.Add(parent.move);
                index = parent.prev;
            }

            moves.Reverse();
            return moves;
        }

        private SolverResult SolveShortestMoves(LockConfig config)
        {
            var start = config.StartPositions;
            if (IsGoal(start)) return new SolverResult { Success = true, Moves = new List<MoveGroup>() };

            var states = new List<int[]> { start };
            var parents = new List<(int prev, MoveGroup move)> { (-1, null) };
            var queue = new Queue<int>();
            queue.Enqueue(0);

            var seen = new Dictionary<int, int> { { GetStateKey(start), 0 } };
            var sw = Stopwatch.StartNew();

            while (queue.Count > 0)
            {
                if (sw.ElapsedMilliseconds > TIMEOUT_MS)
                    return new SolverResult { Timeout = true, ErrorMessage = "Превышено время ожидания." };

                int currentIndex = queue.Dequeue();
                var currentValues = states[currentIndex];

                for (int plate = 0; plate < config.PlateCount; plate++)
                {
                    foreach (var dir in new[] { Direction.Left, Direction.Right })
                    {
                        var nextValues = ApplyMove(currentValues, plate, dir, config);
                        if (nextValues == null) continue;

                        int key = GetStateKey(nextValues);
                        if (seen.ContainsKey(key)) continue;

                        int nextIndex = states.Count;
                        seen[key] = nextIndex;
                        states.Add(nextValues);
                        parents.Add((currentIndex, new MoveGroup { PlateIndex = plate, Direction = dir, Count = 1 }));

                        if (IsGoal(nextValues))
                            return new SolverResult { Success = true, Moves = BuildSolution(nextIndex, states, parents) };

                        queue.Enqueue(nextIndex);
                    }
                }
            }
            return new SolverResult { Success = false, ErrorMessage = "Решение не найдено." };
        }

        private SolverResult SolveFewerPlateSwitches(LockConfig config)
        {
            var start = config.StartPositions;
            if (IsGoal(start)) return new SolverResult { Success = true, Moves = new List<MoveGroup>() };

            var states = new List<int[]> { start };
            var parents = new List<(int prev, MoveGroup move)> { (-1, null) };
            var queue = new Queue<int>();
            queue.Enqueue(0);

            var seen = new Dictionary<int, int> { { GetStateKey(start), 0 } };
            var sw = Stopwatch.StartNew();

            while (queue.Count > 0)
            {
                if (sw.ElapsedMilliseconds > TIMEOUT_MS)
                    return new SolverResult { Timeout = true, ErrorMessage = "Превышено время ожидания." };

                int currentIndex = queue.Dequeue();
                var currentValues = states[currentIndex];

                for (int plate = 0; plate < config.PlateCount; plate++)
                {
                    foreach (var dir in new[] { Direction.Left, Direction.Right })
                    {
                        int[] chainValues = currentValues;

                        // Пытаемся сделать серию ходов одной пластиной в одну сторону
                        for (int count = 1; ; count++)
                        {
                            chainValues = ApplyMove(chainValues, plate, dir, config);
                            if (chainValues == null) break; // Уперлись в границу

                            int key = GetStateKey(chainValues);
                            if (seen.ContainsKey(key)) continue;

                            int nextIndex = states.Count;
                            seen[key] = nextIndex;
                            states.Add(chainValues);
                            parents.Add((currentIndex, new MoveGroup
                            {
                                PlateIndex = plate,
                                Direction = dir,
                                Count = count
                            }));

                            if (IsGoal(chainValues))
                                return new SolverResult { Success = true, Moves = BuildSolution(nextIndex, states, parents) };

                            queue.Enqueue(nextIndex);
                        }
                    }
                }
            }
            return new SolverResult { Success = false, ErrorMessage = "Решение не найдено." };
        }

        // Форматирование ответа для вывода в UI
        public string FormatSolution(SolverResult result, string lockName = "")
        {
            if (result.Timeout) return $"Ошибка: {result.ErrorMessage}";
            if (!result.Success) return $"Ошибка: {result.ErrorMessage}";
            if (result.Moves.Count == 0) return "Замок уже открыт (все пластины в центре).";

            var sb = new StringBuilder();
            string prefix = string.IsNullOrEmpty(lockName) ? "" : $"{lockName} — ";
            sb.AppendLine($"{prefix}Решено за {result.Moves.Sum(m => m.Count)} ходов.");
            sb.AppendLine();

            for (int i = 0; i < result.Moves.Count; i++)
            {
                var m = result.Moves[i];
                string dir = m.Direction == Direction.Left ? "Влево" : "Вправо";
                string countStr = m.Count > 1 ? $" x{m.Count}" : "";
                // В UI показываем пластины с 1, как в оригинале
                sb.AppendLine($"{(i + 1).ToString().PadLeft(2)}. Пластина {m.PlateIndex + 1} -> {dir}{countStr}");
            }

            return sb.ToString();
        }
    }
}