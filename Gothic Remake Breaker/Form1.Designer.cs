namespace Gothic_Remake_Breaker
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                Gothic_Remake_Breaker.Form1.UnregisterHotkey();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelPlateCount = new System.Windows.Forms.Label();
            this.panelPlateCountChecks = new System.Windows.Forms.FlowLayoutPanel();
            this.chkPlateCount2 = new System.Windows.Forms.CheckBox();
            this.chkPlateCount3 = new System.Windows.Forms.CheckBox();
            this.chkPlateCount4 = new System.Windows.Forms.CheckBox();
            this.chkPlateCount5 = new System.Windows.Forms.CheckBox();
            this.chkPlateCount6 = new System.Windows.Forms.CheckBox();
            this.chkPlateCount7 = new System.Windows.Forms.CheckBox();
            this.labelStartPositions = new System.Windows.Forms.Label();
            this.panelPlates = new System.Windows.Forms.FlowLayoutPanel();
            this.panelEffects = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxDelaySettings = new System.Windows.Forms.GroupBox();
            this._nudMoveDelay = new System.Windows.Forms.NumericUpDown();
            this.labelMoveDelay = new System.Windows.Forms.Label();
            this._nudPlateSwitchDelay = new System.Windows.Forms.NumericUpDown();
            this.labelPlateSwitchDelay = new System.Windows.Forms.Label();
            this._chkEnableHotkey = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxOntop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelPlateCountChecks.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxDelaySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudMoveDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudPlateSwitchDelay)).BeginInit();
            this.SuspendLayout();
            //
            // labelPlateCount
            //
            this.labelPlateCount.AutoSize = true;
            this.labelPlateCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlateCount.Location = new System.Drawing.Point(12, 10);
            this.labelPlateCount.Name = "labelPlateCount";
            this.labelPlateCount.Size = new System.Drawing.Size(148, 17);
            this.labelPlateCount.TabIndex = 0;
            this.labelPlateCount.Text = "Количество пластин:";
            //
            // panelPlateCountChecks
            //
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount2);
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount3);
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount4);
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount5);
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount6);
            this.panelPlateCountChecks.Controls.Add(this.chkPlateCount7);
            this.panelPlateCountChecks.Location = new System.Drawing.Point(161, 10);
            this.panelPlateCountChecks.Name = "panelPlateCountChecks";
            this.panelPlateCountChecks.Size = new System.Drawing.Size(230, 25);
            this.panelPlateCountChecks.TabIndex = 1;
            //
            // chkPlateCount2
            //
            this.chkPlateCount2.AutoSize = true;
            this.chkPlateCount2.Checked = true;
            this.chkPlateCount2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlateCount2.Location = new System.Drawing.Point(3, 3);
            this.chkPlateCount2.Name = "chkPlateCount2";
            this.chkPlateCount2.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount2.TabIndex = 0;
            this.chkPlateCount2.Tag = "2";
            this.chkPlateCount2.Text = "2";
            this.chkPlateCount2.UseVisualStyleBackColor = true;
            this.chkPlateCount2.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // chkPlateCount3
            //
            this.chkPlateCount3.AutoSize = true;
            this.chkPlateCount3.Location = new System.Drawing.Point(41, 3);
            this.chkPlateCount3.Name = "chkPlateCount3";
            this.chkPlateCount3.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount3.TabIndex = 1;
            this.chkPlateCount3.Tag = "3";
            this.chkPlateCount3.Text = "3";
            this.chkPlateCount3.UseVisualStyleBackColor = true;
            this.chkPlateCount3.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // chkPlateCount4
            //
            this.chkPlateCount4.AutoSize = true;
            this.chkPlateCount4.Location = new System.Drawing.Point(79, 3);
            this.chkPlateCount4.Name = "chkPlateCount4";
            this.chkPlateCount4.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount4.TabIndex = 2;
            this.chkPlateCount4.Tag = "4";
            this.chkPlateCount4.Text = "4";
            this.chkPlateCount4.UseVisualStyleBackColor = true;
            this.chkPlateCount4.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // chkPlateCount5
            //
            this.chkPlateCount5.AutoSize = true;
            this.chkPlateCount5.Location = new System.Drawing.Point(117, 3);
            this.chkPlateCount5.Name = "chkPlateCount5";
            this.chkPlateCount5.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount5.TabIndex = 3;
            this.chkPlateCount5.Tag = "5";
            this.chkPlateCount5.Text = "5";
            this.chkPlateCount5.UseVisualStyleBackColor = true;
            this.chkPlateCount5.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // chkPlateCount6
            //
            this.chkPlateCount6.AutoSize = true;
            this.chkPlateCount6.Location = new System.Drawing.Point(155, 3);
            this.chkPlateCount6.Name = "chkPlateCount6";
            this.chkPlateCount6.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount6.TabIndex = 4;
            this.chkPlateCount6.Tag = "6";
            this.chkPlateCount6.Text = "6";
            this.chkPlateCount6.UseVisualStyleBackColor = true;
            this.chkPlateCount6.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // chkPlateCount7
            //
            this.chkPlateCount7.AutoSize = true;
            this.chkPlateCount7.Location = new System.Drawing.Point(193, 3);
            this.chkPlateCount7.Name = "chkPlateCount7";
            this.chkPlateCount7.Size = new System.Drawing.Size(32, 17);
            this.chkPlateCount7.TabIndex = 5;
            this.chkPlateCount7.Tag = "7";
            this.chkPlateCount7.Text = "7";
            this.chkPlateCount7.UseVisualStyleBackColor = true;
            this.chkPlateCount7.CheckedChanged += new System.EventHandler(this.plateCountCheck_CheckedChanged);
            //
            // labelStartPositions
            //
            this.labelStartPositions.AutoSize = true;
            this.labelStartPositions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStartPositions.Location = new System.Drawing.Point(17, 61);
            this.labelStartPositions.Name = "labelStartPositions";
            this.labelStartPositions.Size = new System.Drawing.Size(292, 17);
            this.labelStartPositions.TabIndex = 2;
            this.labelStartPositions.Text = "Начальная позиция для каждой пластины:";
            //
            // panelPlates
            //
            this.panelPlates.AutoScroll = true;
            this.panelPlates.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelPlates.Location = new System.Drawing.Point(5, 10);
            this.panelPlates.Name = "panelPlates";
            this.panelPlates.Size = new System.Drawing.Size(341, 248);
            this.panelPlates.TabIndex = 3;
            //
            // panelEffects
            //
            this.panelEffects.AutoScroll = true;
            this.panelEffects.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelEffects.Location = new System.Drawing.Point(4, 11);
            this.panelEffects.Name = "panelEffects";
            this.panelEffects.Size = new System.Drawing.Size(621, 247);
            this.panelEffects.TabIndex = 5;
            //
            // buttonSolve
            //
            this.buttonSolve.Location = new System.Drawing.Point(15, 356);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(75, 23);
            this.buttonSolve.TabIndex = 6;
            this.buttonSolve.Text = "Решить";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            //
            // textBoxResult
            //
            this.textBoxResult.Location = new System.Drawing.Point(15, 386);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(226, 217);
            this.textBoxResult.TabIndex = 8;
            this.textBoxResult.WordWrap = false;
            //
            // groupBox1
            //
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.panelEffects);
            this.groupBox1.Location = new System.Drawing.Point(370, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(630, 262);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            //
            // groupBox2
            //
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.panelPlates);
            this.groupBox2.Location = new System.Drawing.Point(15, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 262);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            //
            // groupBoxDelaySettings
            //
            this.groupBoxDelaySettings.Controls.Add(this._nudMoveDelay);
            this.groupBoxDelaySettings.Controls.Add(this.labelMoveDelay);
            this.groupBoxDelaySettings.Controls.Add(this._nudPlateSwitchDelay);
            this.groupBoxDelaySettings.Controls.Add(this.labelPlateSwitchDelay);
            this.groupBoxDelaySettings.Location = new System.Drawing.Point(278, 386);
            this.groupBoxDelaySettings.Name = "groupBoxDelaySettings";
            this.groupBoxDelaySettings.Size = new System.Drawing.Size(441, 78);
            this.groupBoxDelaySettings.TabIndex = 11;
            this.groupBoxDelaySettings.TabStop = false;
            //
            // _nudMoveDelay
            //
            this._nudMoveDelay.Location = new System.Drawing.Point(365, 46);
            this._nudMoveDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this._nudMoveDelay.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._nudMoveDelay.Name = "_nudMoveDelay";
            this._nudMoveDelay.Size = new System.Drawing.Size(64, 20);
            this._nudMoveDelay.TabIndex = 2;
            this._nudMoveDelay.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            //
            // labelMoveDelay
            //
            this.labelMoveDelay.AutoSize = true;
            this.labelMoveDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMoveDelay.Location = new System.Drawing.Point(13, 46);
            this.labelMoveDelay.Name = "labelMoveDelay";
            this.labelMoveDelay.Size = new System.Drawing.Size(250, 17);
            this.labelMoveDelay.TabIndex = 3;
            this.labelMoveDelay.Text = "Задержка движения пластины (A/D):";
            //
            // _nudPlateSwitchDelay
            //
            this._nudPlateSwitchDelay.Location = new System.Drawing.Point(365, 20);
            this._nudPlateSwitchDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this._nudPlateSwitchDelay.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._nudPlateSwitchDelay.Name = "_nudPlateSwitchDelay";
            this._nudPlateSwitchDelay.Size = new System.Drawing.Size(64, 20);
            this._nudPlateSwitchDelay.TabIndex = 0;
            this._nudPlateSwitchDelay.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            //
            // labelPlateSwitchDelay
            //
            this.labelPlateSwitchDelay.AutoSize = true;
            this.labelPlateSwitchDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlateSwitchDelay.Location = new System.Drawing.Point(13, 20);
            this.labelPlateSwitchDelay.Name = "labelPlateSwitchDelay";
            this.labelPlateSwitchDelay.Size = new System.Drawing.Size(346, 17);
            this.labelPlateSwitchDelay.TabIndex = 1;
            this.labelPlateSwitchDelay.Text = "Задержка переключения между пластинами (W/S):";
            //
            // _chkEnableHotkey
            //
            this._chkEnableHotkey.AutoSize = true;
            this._chkEnableHotkey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._chkEnableHotkey.Location = new System.Drawing.Point(294, 533);
            this._chkEnableHotkey.Name = "_chkEnableHotkey";
            this._chkEnableHotkey.Size = new System.Drawing.Size(287, 29);
            this._chkEnableHotkey.TabIndex = 4;
            this._chkEnableHotkey.Text = "Отслеживание нажатия F4";
            this._chkEnableHotkey.UseVisualStyleBackColor = true;
            //
            // buttonClear
            //
            this.buttonClear.Location = new System.Drawing.Point(116, 356);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Сброс";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            //
            // checkBoxOntop
            //
            this.checkBoxOntop.AutoSize = true;
            this.checkBoxOntop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxOntop.Location = new System.Drawing.Point(294, 479);
            this.checkBoxOntop.Name = "checkBoxOntop";
            this.checkBoxOntop.Size = new System.Drawing.Size(141, 21);
            this.checkBoxOntop.TabIndex = 13;
            this.checkBoxOntop.Text = "Поверх всех окон";
            this.checkBoxOntop.UseVisualStyleBackColor = true;
            this.checkBoxOntop.CheckedChanged += new System.EventHandler(this.checkBoxOntop_CheckedChanged);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(371, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "левые чекбоксы = противоположное двжение, правые чекбоксы = движение совпадает";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(371, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Эффекты:";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 615);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxOntop);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this._chkEnableHotkey);
            this.Controls.Add(this.groupBoxDelaySettings);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.labelStartPositions);
            this.Controls.Add(this.panelPlateCountChecks);
            this.Controls.Add(this.labelPlateCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Gothic Remake Breaker";
            this.panelPlateCountChecks.ResumeLayout(false);
            this.panelPlateCountChecks.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBoxDelaySettings.ResumeLayout(false);
            this.groupBoxDelaySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudMoveDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudPlateSwitchDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlateCount;
        private System.Windows.Forms.FlowLayoutPanel panelPlateCountChecks;
        private System.Windows.Forms.CheckBox chkPlateCount2;
        private System.Windows.Forms.CheckBox chkPlateCount3;
        private System.Windows.Forms.CheckBox chkPlateCount4;
        private System.Windows.Forms.CheckBox chkPlateCount5;
        private System.Windows.Forms.CheckBox chkPlateCount6;
        private System.Windows.Forms.CheckBox chkPlateCount7;
        private System.Windows.Forms.Label labelStartPositions;
        private System.Windows.Forms.FlowLayoutPanel panelPlates;
        private System.Windows.Forms.FlowLayoutPanel panelEffects;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxDelaySettings;
        private System.Windows.Forms.CheckBox _chkEnableHotkey;
        private System.Windows.Forms.Label labelPlateSwitchDelay;
        private System.Windows.Forms.NumericUpDown _nudPlateSwitchDelay;
        private System.Windows.Forms.Label labelMoveDelay;
        private System.Windows.Forms.NumericUpDown _nudMoveDelay;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxOntop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
