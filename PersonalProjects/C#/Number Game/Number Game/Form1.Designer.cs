namespace Number_Game
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
            this.uxComputerLabel = new System.Windows.Forms.Label();
            this.uxPlayerLabel = new System.Windows.Forms.Label();
            this.uxEasyButton = new System.Windows.Forms.RadioButton();
            this.uxMediumButton = new System.Windows.Forms.RadioButton();
            this.uxHardButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // uxComputerLabel
            // 
            this.uxComputerLabel.AutoSize = true;
            this.uxComputerLabel.Location = new System.Drawing.Point(357, 88);
            this.uxComputerLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.uxComputerLabel.Name = "uxComputerLabel";
            this.uxComputerLabel.Size = new System.Drawing.Size(111, 44);
            this.uxComputerLabel.TabIndex = 0;
            this.uxComputerLabel.Text = "Enter";
            // 
            // uxPlayerLabel
            // 
            this.uxPlayerLabel.AutoSize = true;
            this.uxPlayerLabel.Location = new System.Drawing.Point(66, 88);
            this.uxPlayerLabel.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.uxPlayerLabel.Name = "uxPlayerLabel";
            this.uxPlayerLabel.Size = new System.Drawing.Size(117, 44);
            this.uxPlayerLabel.TabIndex = 1;
            this.uxPlayerLabel.Text = "Press";
            // 
            // uxEasyButton
            // 
            this.uxEasyButton.AutoSize = true;
            this.uxEasyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxEasyButton.Location = new System.Drawing.Point(24, 25);
            this.uxEasyButton.Name = "uxEasyButton";
            this.uxEasyButton.Size = new System.Drawing.Size(77, 29);
            this.uxEasyButton.TabIndex = 2;
            this.uxEasyButton.TabStop = true;
            this.uxEasyButton.Text = "Easy";
            this.uxEasyButton.UseVisualStyleBackColor = true;
            this.uxEasyButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            // 
            // uxMediumButton
            // 
            this.uxMediumButton.AutoSize = true;
            this.uxMediumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxMediumButton.Location = new System.Drawing.Point(220, 25);
            this.uxMediumButton.Name = "uxMediumButton";
            this.uxMediumButton.Size = new System.Drawing.Size(103, 29);
            this.uxMediumButton.TabIndex = 3;
            this.uxMediumButton.TabStop = true;
            this.uxMediumButton.Text = "Medium";
            this.uxMediumButton.UseVisualStyleBackColor = true;
            this.uxMediumButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            // 
            // uxHardButton
            // 
            this.uxHardButton.AutoSize = true;
            this.uxHardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHardButton.Location = new System.Drawing.Point(451, 25);
            this.uxHardButton.Name = "uxHardButton";
            this.uxHardButton.Size = new System.Drawing.Size(75, 29);
            this.uxHardButton.TabIndex = 4;
            this.uxHardButton.TabStop = true;
            this.uxHardButton.Text = "Hard";
            this.uxHardButton.UseVisualStyleBackColor = true;
            this.uxHardButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(22F, 42F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 236);
            this.Controls.Add(this.uxHardButton);
            this.Controls.Add(this.uxMediumButton);
            this.Controls.Add(this.uxEasyButton);
            this.Controls.Add(this.uxPlayerLabel);
            this.Controls.Add(this.uxComputerLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "Form1";
            this.Text = "Number Game";
//            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uxComputerLabel;
        private System.Windows.Forms.Label uxPlayerLabel;
        private System.Windows.Forms.RadioButton uxEasyButton;
        private System.Windows.Forms.RadioButton uxMediumButton;
        private System.Windows.Forms.RadioButton uxHardButton;
    }
}

