namespace ProcessPlanner
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.queueListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.currentProcessorStateLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statisticsLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.startExperimentButton = new System.Windows.Forms.Button();
            this.processorTactModelTimer = new System.Windows.Forms.Timer(this.components);
            this.stopExperimentButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.currentVideocardStateLabel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.currentMemoryStateLabel = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // queueListBox
            // 
            this.queueListBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queueListBox.FormattingEnabled = true;
            this.queueListBox.ItemHeight = 19;
            this.queueListBox.Location = new System.Drawing.Point(12, 97);
            this.queueListBox.Name = "queueListBox";
            this.queueListBox.Size = new System.Drawing.Size(246, 403);
            this.queueListBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 355);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Очередь процессов";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.currentProcessorStateLabel);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(287, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 84);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Процессор";
            // 
            // currentProcessorStateLabel
            // 
            this.currentProcessorStateLabel.AutoSize = true;
            this.currentProcessorStateLabel.Location = new System.Drawing.Point(7, 20);
            this.currentProcessorStateLabel.Name = "currentProcessorStateLabel";
            this.currentProcessorStateLabel.Size = new System.Drawing.Size(0, 19);
            this.currentProcessorStateLabel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.statisticsLabel);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(535, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 355);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статистика";
            // 
            // statisticsLabel
            // 
            this.statisticsLabel.AutoSize = true;
            this.statisticsLabel.Location = new System.Drawing.Point(6, 19);
            this.statisticsLabel.Name = "statisticsLabel";
            this.statisticsLabel.Size = new System.Drawing.Size(0, 19);
            this.statisticsLabel.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 3;
            // 
            // startExperimentButton
            // 
            this.startExperimentButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startExperimentButton.Location = new System.Drawing.Point(358, 28);
            this.startExperimentButton.Name = "startExperimentButton";
            this.startExperimentButton.Size = new System.Drawing.Size(170, 55);
            this.startExperimentButton.TabIndex = 4;
            this.startExperimentButton.Text = "Начать эксперимент";
            this.startExperimentButton.UseVisualStyleBackColor = true;
            this.startExperimentButton.Click += new System.EventHandler(this.startExperimentButton_Click);
            // 
            // processorTactModelTimer
            // 
            this.processorTactModelTimer.Interval = 20;
            this.processorTactModelTimer.Tick += new System.EventHandler(this.processorTactModelTimer_Tick);
            // 
            // stopExperimentButton
            // 
            this.stopExperimentButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stopExperimentButton.Location = new System.Drawing.Point(630, 28);
            this.stopExperimentButton.Name = "stopExperimentButton";
            this.stopExperimentButton.Size = new System.Drawing.Size(115, 55);
            this.stopExperimentButton.TabIndex = 5;
            this.stopExperimentButton.Text = "Stop";
            this.stopExperimentButton.UseVisualStyleBackColor = true;
            this.stopExperimentButton.Click += new System.EventHandler(this.stopExperimentButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.currentVideocardStateLabel);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(287, 238);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(241, 86);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Videocard";
            // 
            // currentVideocardStateLabel
            // 
            this.currentVideocardStateLabel.AutoSize = true;
            this.currentVideocardStateLabel.Location = new System.Drawing.Point(9, 26);
            this.currentVideocardStateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentVideocardStateLabel.Name = "currentVideocardStateLabel";
            this.currentVideocardStateLabel.Size = new System.Drawing.Size(0, 19);
            this.currentVideocardStateLabel.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.currentMemoryStateLabel);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(289, 353);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Size = new System.Drawing.Size(241, 149);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Memory";
            // 
            // currentMemoryStateLabel
            // 
            this.currentMemoryStateLabel.AutoSize = true;
            this.currentMemoryStateLabel.Location = new System.Drawing.Point(9, 23);
            this.currentMemoryStateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentMemoryStateLabel.Name = "currentMemoryStateLabel";
            this.currentMemoryStateLabel.Size = new System.Drawing.Size(0, 19);
            this.currentMemoryStateLabel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 533);
            this.Controls.Add(this.queueListBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.stopExperimentButton);
            this.Controls.Add(this.startExperimentButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox queueListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label currentProcessorStateLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label statisticsLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button startExperimentButton;
        private System.Windows.Forms.Timer processorTactModelTimer;
        private System.Windows.Forms.Button stopExperimentButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label currentVideocardStateLabel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label currentMemoryStateLabel;
    }
}

