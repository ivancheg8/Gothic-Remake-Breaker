using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gothic_Remake_Breaker
{
    public partial class Form1 : Form
    {
        private const int MAX_PLATES = 7;

        private FlowLayoutPanel[] _platePanels = new FlowLayoutPanel[MAX_PLATES];
        private TableLayoutPanel[] _effectRows = new TableLayoutPanel[MAX_PLATES];
        private CheckBox[,] _sameCheckBoxes = new CheckBox[MAX_PLATES, MAX_PLATES];
        private CheckBox[,] _oppositeCheckBoxes = new CheckBox[MAX_PLATES, MAX_PLATES];
        private System.Windows.Forms.Button _buttonClear;

        private int _plateSwitchDelayMs = 150;
        private int _moveDelayMs = 150;
        private const int StartDelayMs = 300;

        public int PlateSwitchDelayMs => _plateSwitchDelayMs;
        public int MoveDelayMs => _moveDelayMs;

        private void ApplyDelayFromUI()
        {
            if (_nudPlateSwitchDelay != null)
                _plateSwitchDelayMs = Math.Max(0, (int)_nudPlateSwitchDelay.Value);
            if (_nudMoveDelay != null)
                _moveDelayMs = Math.Max(0, (int)_nudMoveDelay.Value);
        }

        private void SyncUIFromDelays()
        {
            if (_nudPlateSwitchDelay != null)
                _nudPlateSwitchDelay.Value = _plateSwitchDelayMs;
            if (_nudMoveDelay != null)
                _nudMoveDelay.Value = _moveDelayMs;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        private const uint INPUT_KEYBOARD = 1;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern ushort MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 1;
        private const uint MOD_NOREPEAT = 0x4000;
        private const int WM_HOTKEY = 0x0312;

        public static void UnregisterHotkey()
        {
            if (_formHandle != IntPtr.Zero)
            {
                UnregisterHotKey(_formHandle, HOTKEY_ID);
            }
        }

        private static IntPtr _formHandle = IntPtr.Zero;

        private int UiToInternal(int uiVal) => uiVal - 4;
        private int InternalToUi(int internalVal) => internalVal + 4;

        public Form1()
        {
            InitializeComponent();
            CreateAllControlsOnce();
            GeneratePlateControls(2);
            GenerateEffectControls(2);
            SyncUIFromDelays();
            RegisterGlobalHotkey();
        }

        private void RegisterGlobalHotkey()
        {
            _formHandle = this.Handle;
            bool ok = RegisterHotKey(_formHandle, HOTKEY_ID, MOD_NOREPEAT, 0x73);
            if (!ok)
            {
                int err = Marshal.GetLastWin32Error();
                System.Diagnostics.Debug.WriteLine($"RegisterHotKey failed! Error: {err}");
                MessageBox.Show(
                    $"Не удалось зарегистрировать глобальный хоткей F4.\n" +
                    $"Код ошибки: {err}\n\n" +
                    "Возможно, F4 уже занят другой программой.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                if (_chkEnableHotkey != null && _chkEnableHotkey.Checked)
                    StartAutoPlay();
                return;
            }
            base.WndProc(ref m);
        }

        private void checkBoxOntop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBoxOntop.Checked;
        }

        private void CreateAllControlsOnce()
        {
            panelPlates.SuspendLayout();
            panelEffects.SuspendLayout();

            for (int i = MAX_PLATES - 1; i >= 0; i--)
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

            for (int src = MAX_PLATES - 1; src >= 0; src--)
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

                for (int tgt = 0; tgt < MAX_PLATES; tgt++)
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

                for (int tgt = 0; tgt < MAX_PLATES; tgt++)
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

                row.Controls.Add(oppositePanel, 0, 0);
                row.Controls.Add(plateLabel, 1, 0);
                row.Controls.Add(samePanel, 2, 0);

                _effectRows[src] = row;
                panelEffects.Controls.Add(row);
            }

            for (int i = 2; i < MAX_PLATES; i++)
            {
                _platePanels[i].Visible = false;
                _effectRows[i].Visible = false;
            }

            panelPlates.ResumeLayout();
            panelEffects.ResumeLayout();
        }

        private void plateCountCheck_CheckedChanged(object sender, EventArgs e)
        {
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            int count = GetSelectedPlateCount();
            count = Math.Min(count, MAX_PLATES); // Ограничение

            for (int i = 0; i < count; i++)
            {
                var panel = _platePanels[i] as FlowLayoutPanel;
                if (panel == null) continue;

                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is RadioButton rb && rb.Text == "4")
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }

            for (int src = 0; src < count; src++)
            {
                for (int tgt = 0; tgt < count; tgt++)
                {
                    if (src != tgt)
                    {
                        _sameCheckBoxes[src, tgt].Checked = false;
                        _oppositeCheckBoxes[src, tgt].Checked = false;
                    }
                }
            }

            // Сбрасываем все скрытые чекбоксы
            ClearHiddenCheckboxes(count);
        }

        private void ClearHiddenCheckboxes(int visibleCount)
        {
            for (int src = 0; src < MAX_PLATES; src++)
            {
                for (int tgt = 0; tgt < MAX_PLATES; tgt++)
                {
                    if (src >= visibleCount || tgt >= visibleCount || src == tgt)
                    {
                        _sameCheckBoxes[src, tgt].Checked = false;
                        _oppositeCheckBoxes[src, tgt].Checked = false;
                    }
                }
            }
        }

        private int GetSelectedPlateCount()
        {
            foreach (Control ctrl in panelPlateCountChecks.Controls)
            {
                if (ctrl is CheckBox chk && chk.Checked)
                {
                    if (chk.Tag != null && int.TryParse(chk.Tag.ToString(), out int count))
                    {
                        return Math.Max(1, Math.Min(count, MAX_PLATES)); // Ограничение диапазона
                    }
                }
            }
            return 2; // Значение по умолчанию
        }

        private void GeneratePlateControls(int plateCount)
        {
            plateCount = Math.Max(1, Math.Min(plateCount, MAX_PLATES)); // Ограничение

            panelPlates.SuspendLayout();
            panelPlates.Controls.Clear();

            for (int i = plateCount - 1; i >= 0; i--)
            {
                _platePanels[i].Visible = true;
                panelPlates.Controls.Add(_platePanels[i]);
            }
            panelPlates.ResumeLayout();
        }

        private void GenerateEffectControls(int plateCount)
        {
            plateCount = Math.Max(1, Math.Min(plateCount, MAX_PLATES)); // Ограничение

            panelEffects.SuspendLayout();
            panelEffects.Controls.Clear();

            for (int i = plateCount - 1; i >= 0; i--)
            {
                _effectRows[i].Visible = true;
                panelEffects.Controls.Add(_effectRows[i]);
            }

            for (int src = 0; src < MAX_PLATES; src++)
            {
                for (int tgt = 0; tgt < MAX_PLATES; tgt++)
                {
                    bool show = (src < plateCount && tgt < plateCount && src != tgt);
                    _sameCheckBoxes[src, tgt].Visible = show;
                    _oppositeCheckBoxes[src, tgt].Visible = show;

                    // Отключаем невидимые чекбоксы
                    _sameCheckBoxes[src, tgt].Enabled = show;
                    _oppositeCheckBoxes[src, tgt].Enabled = show;
                }
            }
            panelEffects.ResumeLayout();
        }

        private void EffectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Автоматически снимаем отметку, если чекбокс стал невидимым
            if (sender is CheckBox cb && !cb.Visible && cb.Checked)
            {
                cb.Checked = false;
            }
        }

        private void rbPlate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private int[] GetStartPositions()
        {
            int count = GetSelectedPlateCount();
            int[] positions = new int[count];

            for (int i = 0; i < count; i++)
            {
                var panel = _platePanels[i] as FlowLayoutPanel;
                if (panel == null) continue;

                foreach (Control ctrl in panel.Controls)
                {
                    if (ctrl is RadioButton rb && rb.Checked)
                    {
                        if (int.TryParse(rb.Text, out int uiVal))
                        {
                            positions[i] = UiToInternal(uiVal);
                        }
                        break;
                    }
                }
            }
            return positions;
        }

        private void ReadEffects(LockConfig config)
        {
            for (int src = 0; src < config.PlateCount; src++)
            {
                for (int tgt = 0; tgt < config.PlateCount; tgt++)
                {
                    config.Effects[src, tgt] = EffectType.None;
                }
            }

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
                                // ПРОВЕРКА ГРАНИЦ
                                if (tag.Source < config.PlateCount && tag.Target < config.PlateCount && check.Checked)
                                {
                                    config.Effects[tag.Source, tag.Target] = tag.Type;
                                }
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
                                // ПРОВЕРКА ГРАНИЦ
                                if (tag.Source < config.PlateCount && tag.Target < config.PlateCount)
                                {
                                    check.Checked = (config.Effects[tag.Source, tag.Target] == tag.Type);
                                }
                                else
                                {
                                    check.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при решении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private struct ParsedMove
        {
            public int PlateIndex;
            public Direction Direction;
            public int Count;
        }

        private static List<ParsedMove> ParseResult(string text, int maxPlates)
        {
            var moves = new List<ParsedMove>();
            if (string.IsNullOrWhiteSpace(text)) return moves;

            foreach (var line in text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var trimmed = line.Trim();
                var plateMatch = System.Text.RegularExpressions.Regex.Match(trimmed, @"Пластина\s+(\d+)");
                if (!plateMatch.Success) continue;

                if (!int.TryParse(plateMatch.Groups[1].Value, out int plate)) continue;

                // ПРОВЕРКА ГРАНИЦ
                if (plate < 1 || plate > maxPlates) continue;

                Direction dir = Direction.Left;
                if (trimmed.Contains("Вправо")) dir = Direction.Right;

                int count = 1;
                var countMatch = System.Text.RegularExpressions.Regex.Match(trimmed, @"x(\d+)");
                if (countMatch.Success && int.TryParse(countMatch.Groups[1].Value, out int parsedCount))
                    count = Math.Max(1, parsedCount);

                moves.Add(new ParsedMove { PlateIndex = plate, Direction = dir, Count = count });
            }
            return moves;
        }

        private static void SendVirtualKey(byte vk)
        {
            try
            {
                INPUT[] inputs = new INPUT[2];

                inputs[0].type = INPUT_KEYBOARD;
                inputs[0].u.ki = new KEYBDINPUT
                {
                    wVk = vk,
                    wScan = MapVirtualKey(vk, 0),
                    dwFlags = 0,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                };

                inputs[1].type = INPUT_KEYBOARD;
                inputs[1].u.ki = new KEYBDINPUT
                {
                    wVk = vk,
                    wScan = MapVirtualKey(vk, 0),
                    dwFlags = KEYEVENTF_KEYUP,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                };

                uint sent = SendInput(2, inputs, Marshal.SizeOf<INPUT>());

                if (sent == 0)
                {
                    int error = Marshal.GetLastWin32Error();
                    System.Diagnostics.Debug.WriteLine($"SendInput failed! Error code: {error}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SendVirtualKey exception: {ex.Message}");
            }
        }

        private void StartAutoPlay()
        {
            try
            {
                ApplyDelayFromUI();

                string resultText = textBoxResult.Text;
                if (string.IsNullOrWhiteSpace(resultText) || !resultText.Contains("Решено"))
                {
                    MessageBox.Show("Сначала решите замок!", "Авто-игра", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maxPlates = GetSelectedPlateCount();
                var moves = ParseResult(resultText, maxPlates);
                if (moves.Count == 0)
                {
                    MessageBox.Show("Не удалось распознать ходы в результате.", "Авто-игра", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var worker = new Thread(() => ExecuteAutoPlay(moves, maxPlates))
                {
                    IsBackground = true
                };
                worker.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска авто-игры: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteAutoPlay(List<ParsedMove> moves, int maxPlates)
        {
            try
            {
                const byte VK_R = 0x52;
                const byte VK_W = 0x57;
                const byte VK_S = 0x53;
                const byte VK_A = 0x41;
                const byte VK_D = 0x44;

                int currentPlate = 1;

                Thread.Sleep(StartDelayMs);

                SendVirtualKey(VK_R);
                Thread.Sleep(PlateSwitchDelayMs);

                foreach (var move in moves)
                {
                    // ПРОВЕРКА ГРАНИЦ
                    if (move.PlateIndex < 1 || move.PlateIndex > maxPlates) continue;

                    int maxIterations = maxPlates * 2; // Защита от бесконечного цикла
                    int iterations = 0;

                    while (currentPlate < move.PlateIndex && iterations < maxIterations)
                    {
                        SendVirtualKey(VK_W);
                        Thread.Sleep(PlateSwitchDelayMs);
                        currentPlate++;
                        iterations++;
                    }

                    iterations = 0;
                    while (currentPlate > move.PlateIndex && iterations < maxIterations)
                    {
                        SendVirtualKey(VK_S);
                        Thread.Sleep(PlateSwitchDelayMs);
                        currentPlate--;
                        iterations++;
                    }

                    byte moveKey = move.Direction == Direction.Left ? VK_A : VK_D;
                    for (int i = 0; i < move.Count; i++)
                    {
                        SendVirtualKey(moveKey);
                        Thread.Sleep(MoveDelayMs);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExecuteAutoPlay exception: {ex.Message}");
                // Опционально: показать сообщение пользователю
                // this.Invoke((Action)(() => MessageBox.Show($"Ошибка авто-игры: {ex.Message}")));
            }
        }
    }

    public enum EffectType { None, Same, Opposite }
    public enum Direction { Left, Right }

    public class LockConfig
    {
        public int PlateCount { get; set; }
        public int[] StartPositions { get; set; }
        public EffectType[,] Effects { get; set; }
    }

    public class MoveGroup
    {
        public int PlateIndex { get; set; }
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
        private const int TIMEOUT_MS = 15000;

        public SolverResult Solve(LockConfig config, bool fewerSwitches = true)
        {
            if (fewerSwitches)
                return SolveFewerPlateSwitches(config);
            else
                return SolveShortestMoves(config);
        }

        private int[] ApplyMove(int[] current, int plateIndex, Direction dir, LockConfig config)
        {
            int delta = dir == Direction.Left ? 1 : -1;
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

            foreach (var val in next)
            {
                if (val < MIN_POS || val > MAX_POS) return null;
            }
            return next;
        }

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

                        for (int count = 1; ; count++)
                        {
                            chainValues = ApplyMove(chainValues, plate, dir, config);
                            if (chainValues == null) break;

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
                sb.AppendLine($"{(i + 1).ToString().PadLeft(2)}. Пластина {m.PlateIndex + 1} -> {dir}{countStr}");
            }

            return sb.ToString();
        }
    }

    internal class EffectTag
    {
        public int Source;
        public int Target;
        public EffectType Type;
    }
}