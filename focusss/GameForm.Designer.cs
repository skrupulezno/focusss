namespace focusss
{
    partial class GameForm
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
            this.lblRound = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRound
            // 
            this.lblRound.AutoSize = true;
            this.lblRound.Location = new System.Drawing.Point(783, 68);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(10, 13);
            this.lblRound.TabIndex = 0;
            this.lblRound.Text = "-";
            // 
            // panelCards
            // 
            this.panelCards.Location = new System.Drawing.Point(12, 12);
            this.panelCards.Name = "panelCards";
            this.panelCards.Size = new System.Drawing.Size(728, 460);
            this.panelCards.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(894, 582);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 81);
            this.button1.TabIndex = 2;
            this.button1.Text = "Настройки";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(783, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "-";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 684);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.lblRound);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}