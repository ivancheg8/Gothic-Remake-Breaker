namespace Gothic_Remake_Breaker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.labelEffectsTitle = new System.Windows.Forms.Label();
            this.panelEffects = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelPlateCountChecks.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPlateCount
            // 
            this.labelPlateCount.AutoSize = true;
            this.labelPlateCount.Location = new System.Drawing.Point(12, 15);
            this.labelPlateCount.Name = "labelPlateCount";
            this.labelPlateCount.Size = new System.Drawing.Size(113, 13);
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
            this.panelPlateCountChecks.Location = new System.Drawing.Point(131, 10);
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
            this.labelStartPositions.Location = new System.Drawing.Point(12, 50);
            this.labelStartPositions.Name = "labelStartPositions";
            this.labelStartPositions.Size = new System.Drawing.Size(278, 13);
            this.labelStartPositions.TabIndex = 2;
            this.labelStartPositions.Text = "Выберите начальную позицию для каждой пластины:";
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
            // labelEffectsTitle
            // 
            this.labelEffectsTitle.AutoSize = true;
            this.labelEffectsTitle.Location = new System.Drawing.Point(390, 50);
            this.labelEffectsTitle.Name = "labelEffectsTitle";
            this.labelEffectsTitle.Size = new System.Drawing.Size(392, 26);
            this.labelEffectsTitle.TabIndex = 4;
            this.labelEffectsTitle.Text = "Эффекты:\r\n← левые чекбоксы = Opposite (влево), правые чекбоксы → = Same (вправо)";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 615);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.labelEffectsTitle);
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
        private System.Windows.Forms.Label labelEffectsTitle;
        private System.Windows.Forms.FlowLayoutPanel panelEffects;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
