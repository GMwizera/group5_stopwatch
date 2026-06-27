namespace Timer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            Pause = new Button();
            Resume = new Button();
            Reset = new Button();
            Stop = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // Start
            // 
            Start.Location = new Point(266, 157);
            Start.Name = "Start";
            Start.Size = new Size(121, 72);
            Start.TabIndex = 0;
            Start.Text = "Start";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // Pause
            // 
            Pause.Location = new Point(136, 157);
            Pause.Name = "Pause";
            Pause.Size = new Size(111, 72);
            Pause.TabIndex = 1;
            Pause.Text = "Pause";
            Pause.UseVisualStyleBackColor = true;
            Pause.Click += Pause_Click;
            // 
            // Resume
            // 
            Resume.Location = new Point(407, 157);
            Resume.Name = "Resume";
            Resume.Size = new Size(111, 72);
            Resume.TabIndex = 2;
            Resume.Text = "Resume";
            Resume.UseVisualStyleBackColor = true;
            Resume.Click += Resume_Click;
            // 
            // Reset
            // 
            Reset.Location = new Point(12, 157);
            Reset.Name = "Reset";
            Reset.Size = new Size(111, 72);
            Reset.TabIndex = 3;
            Reset.Text = "Reset";
            Reset.UseVisualStyleBackColor = true;
            Reset.Click += Reset_Click;
            // 
            // Stop
            // 
            Stop.Location = new Point(545, 157);
            Stop.Name = "Stop";
            Stop.Size = new Size(111, 72);
            Stop.TabIndex = 4;
            Stop.Text = "Stop";
            Stop.UseVisualStyleBackColor = true;
            Stop.Click += Stop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Algerian", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(156, 38);
            label1.Name = "label1";
            label1.Size = new Size(362, 89);
            label1.TabIndex = 5;
            label1.Text = "00:00:00";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(682, 253);
            Controls.Add(label1);
            Controls.Add(Stop);
            Controls.Add(Reset);
            Controls.Add(Resume);
            Controls.Add(Pause);
            Controls.Add(Start);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Start;
        private Button Pause;
        private Button Resume;
        private Button Reset;
        private Button Stop;
        private Label label1;
    }
}
