namespace summoner
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
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            WindowsLB = new ListBox();
            ScreensLB = new ListBox();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(12, 283);
            button1.Name = "button1";
            button1.Size = new Size(506, 25);
            button1.TabIndex = 0;
            button1.Text = "Summon Window";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ShowWindowBtn_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 3;
            label1.Text = "Programs";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(463, 9);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 4;
            label2.Text = "Monitors";
            // 
            // WindowsLB
            // 
            WindowsLB.FormattingEnabled = true;
            WindowsLB.Location = new Point(12, 27);
            WindowsLB.Name = "WindowsLB";
            WindowsLB.Size = new Size(250, 244);
            WindowsLB.TabIndex = 5;
            // 
            // ScreensLB
            // 
            ScreensLB.FormattingEnabled = true;
            ScreensLB.Location = new Point(268, 27);
            ScreensLB.Name = "ScreensLB";
            ScreensLB.Size = new Size(250, 244);
            ScreensLB.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(137, 1);
            button2.Name = "button2";
            button2.Size = new Size(91, 23);
            button2.TabIndex = 7;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(137, 1);
            button3.Name = "button3";
            button3.Size = new Size(91, 23);
            button3.TabIndex = 7;
            button3.Text = "Refresh";
            button3.UseVisualStyleBackColor = true;
            button3.Click += RefreshLists;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 320);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(ScreensLB);
            Controls.Add(WindowsLB);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Summoner";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private ListBox WindowsLB;
        private ListBox ScreensLB;
        private Button button2;
        private Button button3;
    }
}
