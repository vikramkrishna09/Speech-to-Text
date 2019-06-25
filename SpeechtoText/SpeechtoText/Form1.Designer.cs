namespace SpeechtoText
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
            this.Feed = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Feed2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Feed
            // 
            this.Feed.BackColor = System.Drawing.SystemColors.Highlight;
            this.Feed.Location = new System.Drawing.Point(30, 25);
            this.Feed.Multiline = true;
            this.Feed.Name = "Feed";
            this.Feed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Feed.Size = new System.Drawing.Size(1158, 513);
            this.Feed.TabIndex = 0;
            this.Feed.TextChanged += new System.EventHandler(this.Feed_TextChanged);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 582);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(226, 83);
            this.Start.TabIndex = 1;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Startbutton);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(866, 582);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(226, 83);
            this.Stop.TabIndex = 2;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Endbutton);
            // 
            // Feed2
            // 
            this.Feed2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Feed2.Location = new System.Drawing.Point(266, 582);
            this.Feed2.Multiline = true;
            this.Feed2.Name = "Feed2";
            this.Feed2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Feed2.Size = new System.Drawing.Size(594, 83);
            this.Feed2.TabIndex = 3;
            this.Feed2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Feed2_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 703);
            this.Controls.Add(this.Feed2);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Feed);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Feed;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.TextBox Feed2;
    }
}

