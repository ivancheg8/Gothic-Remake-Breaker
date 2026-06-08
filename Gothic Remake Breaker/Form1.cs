using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gothic_Remake_Breaker
{
    public partial class Form1 : Form
    {
        // Кэш контролов — создаются один раз при загрузке для макс. количества пластин
        private FlowLayoutPanel[] _platePanels = new FlowLayoutPanel[7];

        // Кэш строк эффектов (TableLayoutPanel) — по одной на каждый источник 0..6
        private TableLayoutPanel[] _effectRows = new TableLayoutPanel[7];

        // Кэш чекбоксов эффектов: [src, tgt] → Same / Opposite
        private CheckBox[,] _sameCheckBoxes = new CheckBox[7, 7];
        private CheckBox[,] _oppositeCheckBoxes = new CheckBox[7, 7];

        // Конвертация: UI-значение (1..7) ↔ внутреннее (-3..3), где "4" = 0 (центр)
        private int UiToInternal(int uiVal) => uiVal - 4;
        private int InternalToUi(int internalVal) => internalVal + 4;

        public Form1()
        {
            InitializeComponent();
            // Создаём все контролы один раз для макс. количества (7 пластин)
            CreateAllControlsOnce();
            // Показываем начальные пластины и эффекты (2 по умолчанию)
            GeneratePlateControls(2);
            GenerateEffectControls(2);
        }

        // Создаёт ВСЕ контролы один раз для макс. количества пластин (7)
        // Последовательность в panelPlates: Plate 1 (снизу) → Plate 7 (сверху)
        private void CreateAllControlsOnce()
        {
            panelPlates.SuspendLayout();
            panelEffects.SuspendLayout();

            // ПЛАСТИНЫ: добавляем от 7 к 1, чтобы Plate 1 оказалась внизу
            for (int i = 6; i >= 0; i--)  // <-- было: for (int i = 0; i < 7; i++)
            {
                var platePanel = new FlowLayoutPanel();
                platePanel.AutoSize = false;
                platePanel.Size = new System.Drawing.Size(333, 28);
                platePanel.FlowDirection = FlowDirection.LeftToRight;
                platePanel.WrapContents = false;

                var plateLabel = new Label
                {
                    Text = $"Plate {i + 1}",
                    AutoSize = true,
                    Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold),
                    Margin = new Padding(0, 0, 6, 0)
                };
                platePanel.Controls.Add(plateLabel);

                string[] posLabels = { "1", "2", "3", "4", "5", "6", "7" };
                for (int p = 0; p < posLabels.Length; p++)
                {
                    RadioButton rb = new RadioButton();
                    rb.Text = posLabels[p];
                    rb.AutoSize = true;
                    rb.Location = new System.Drawing.Point(5 + p * 40, 5);
                    rb.Tag = i;
                    if (p == 3) rb.Checked = true;
                    rb.CheckedChanged += rbPlate_CheckedChanged;
                    platePanel.Controls.Add(rb);
                }

                _platePanels[i] = platePanel;
                panelPlates.Controls.Add(platePanel);
            }

            // ЭФФЕКТЫ: добавляем строки от Plate 7 к Plate 1
            for (int src = 6; src >= 0; src--)  // <-- было: for (int src = 0; src < 7; src++)
            {
                var row = new TableLayoutPanel();
                row.AutoSize = true;
                row.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                row.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                row.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                row.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var samePanel = new FlowLayoutPanel();
                samePanel.FlowDirection = FlowDirection.LeftToRight;
                samePanel.WrapContents = false;
                samePanel.AutoSize = true;
                samePanel.Padding = new Padding(0, 0, 4, 0);

                for (int tgt = 0; tgt < 7; tgt++)
                {
                    var cb = new CheckBox
                    {
                        Text = (tgt + 1).ToString(),
                        AutoSize = true,
                        Enabled = (tgt != src),
                        Tag = new EffectTag { Source = src, Target = tgt, Type = EffectType.Same }
                    };
                    _sameCheckBoxes[src, tgt] = cb;
                    cb.CheckedChanged += EffectCheckBox_CheckedChanged;
                    samePanel.Controls.Add(cb);
                }

                var plateLabel = new Label
                {
                    Text = $"Plate {src + 1}",
                    AutoSize = true,
                    Font = new System.Drawing.Font(this.Font, System.Drawing.FontStyle.Bold),
                    Margin = new Padding(6, 0, 6, 0)
                };

                var oppositePanel = new FlowLayoutPanel();
                oppositePanel.FlowDirection = FlowDirection.LeftToRight;
                oppositePanel.WrapContents = false;
                oppositePanel.AutoSize = true;
                oppositePanel.Padding = new Padding(4, 0, 0, 0);

                for (int tgt = 0; tgt < 7; tgt++)
                {
                    var cb = new CheckBox
                    {
                        Text = (tgt + 1).ToString(),
                        AutoSize = true,
                        Enabled = (tgt != src),
                        Tag = new EffectTag { Source = src, Target = tgt, Type = EffectType.Opposite }
                    };
                    _oppositeCheckBoxes[src, tgt] = cb;
                    cb.CheckedChanged += EffectCheckBox_CheckedChanged;
                    oppositePanel.Controls.Add(cb);
                }

                // ВАЖНО: порядок колонок в TableLayoutPanel должен соответствовать описанию
                // "слева от надписи Plate N — Opposite, справа — Same"
                row.Controls.Add(oppositePanel, 0, 0);
                row.Controls.Add(plateLabel, 1, 0);
                row.Controls.Add(samePanel, 2, 0);

                _effectRows[src] = row;
                panelEffects.Controls.Add(row);
            }

            // Скрываем всё, что за пределами начального значения (2)
            for (int i = 2; i < 7; i++)
            {
                _platePanels[i].Visible = false;
                _effectRows[i].Visible = false;
            }

            panelPlates.ResumeLayout();
            panelEffects.ResumeLayout();
        }

        private void plateCountCheck_CheckedChanged(object sender, EventArgs e)
        {
            // Только один чекбокс может быть активен (как RadioButton)
            if (sender is CheckBox current && current.Checked)
            {
                foreach (Control ctrl in panelPlateCountChecks.Controls)
                {
                    if (ctrl is CheckBox chk && chk != current)
                        chk.Checked = false;
                }
            }

            int count = GetSelectedPlateCount();
            if (count > 0)
            {
                GeneratePlateControls(count);
                GenerateEffectControls(count);
            }
        }

        private int GetSelectedPlateCount()
        {
            foreach (Control ctrl in panelPlateCountChecks.Controls)
            {
                if (ctrl is CheckBox chk && chk.Checked)
                {
                    return int.Parse(chk.Tag.ToString());
                }
            }
            return 7; // fallback
        }

        private void GeneratePlateControls(int plateCount)
        {
            panelPlates.SuspendLayout();
            panelPlates.Controls.Clear();

            // Добавляем панели в обратном порядке, чтобы Plate 1 оказалась внизу
            for (int i = plateCount - 1; i >= 0; i--)
            {
                _platePanels[i].Visible = true; // Явно показываем
                panelPlates.Controls.Add(_platePanels[i]);
            }
            panelPlates.ResumeLayout();
        }

        private void GenerateEffectControls(int plateCount)
        {
            panelEffects.SuspendLayout();
            panelEffects.Controls.Clear();

            // Добавляем строки в обратном порядке, чтобы Plate 1 оказалась внизу
            for (int i = plateCount - 1; i >= 0; i--)
            {
                _effectRows[i].Visible = true; // Явно показываем
                panelEffects.Controls.Add(_effectRows[i]);
            }

            // Внутри каждой видимой строки показываем только чекбоксы для tgt < plateCount
            for (int src = 0; src < plateCount; src++)
            {
                for (int tgt = 0; tgt < 7; tgt++)
                {
                    bool show = (tgt < plateCount);
                    _sameCheckBoxes[src, tgt].Visible = show;
                    _oppositeCheckBoxes[src, tgt].Visible = show;
                }
            }
            panelEffects.ResumeLayout();
        }

        private void EffectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Эффекты применяются мгновенно — просто обновляем состояние
        }

        private void rbPlate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private int[] GetStartPositions()
        {
            int count = GetSelectedPlateCount();
            int[] positions = new int[count];
            // _platePanels[i] соответствует Plate (i+1) напрямую
            for (int i = 0; i < count; i++)
            {
                var panel = _platePanels[i] as FlowLayoutPanel;
                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is RadioButton rb && rb.Checked)
                    {
                        positions[i] = UiToInternal(int.Parse(rb.Text));
                        break;
                    }
                }
            }
            return positions;
        }

        private void ReadEffects(LockConfig config)
        {
            // Сбрасываем все в None
            for (int src = 0; src < config.PlateCount; src++)
            {
                for (int tgt = 0; tgt < config.PlateCount; tgt++)
                {
                    config.Effects[src, tgt] = EffectType.None;
                }
            }

            // Считываем напрямую из кэша _effectRows по индексу источника
            for (int src = 0; src < config.PlateCount; src++)
            {
                var row = _effectRows[src];
                foreach (Control child in row.Controls)
                {
                    if (child is FlowLayoutPanel fp)
                    {
                        foreach (Control cb in fp.Controls)
                        {
                            if (cb is CheckBox check && check.Tag is EffectTag tag)
                            {
                                if (check.Checked)
                                    config.Effects[tag.Source, tag.Target] = tag.Type;
                            }
                        }
                    }
                }
            }
        }

        private void SetEffectsFromConfig(LockConfig config)
        {
            for (int src = 0; src < config.PlateCount; src++)
            {
                var row = _effectRows[src];
                foreach (Control child in row.Controls)
                {
                    if (child is FlowLayoutPanel fp)
                    {
                        foreach (Control cb in fp.Controls)
                        {
                            if (cb is CheckBox check && check.Tag is EffectTag tag)
                            {
                                check.Checked = (config.Effects[tag.Source, tag.Target] == tag.Type);
                            }
                        }
                    }
                }
            }
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            int count = GetSelectedPlateCount();
            var config = new LockConfig
            {
                PlateCount = count,
                StartPositions = GetStartPositions(),
                Effects = new EffectType[count, count]
            };

            ReadEffects(config);

            var solver = new LockpickSolver();
            var result = solver.Solve(config);
            textBoxResult.Text = solver.FormatSolution(result, "Результат");
        }
    }

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

    // Тег для чекбоксов эффектов
    internal class EffectTag
    {
        public int Source;   // пластина-источник (0-based)
        public int Target;   // пластина-цель (0-based)
        public EffectType Type; // Same или Opposite
    }
}
