namespace FakeSQLInformationGenerator
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
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.uxStartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "txt";
            // 
            // uxStartButton
            // 
            this.uxStartButton.Location = new System.Drawing.Point(13, 13);
            this.uxStartButton.Name = "uxStartButton";
            this.uxStartButton.Size = new System.Drawing.Size(460, 77);
            this.uxStartButton.TabIndex = 0;
            this.uxStartButton.Text = "Generate Data";
            this.uxStartButton.UseVisualStyleBackColor = true;
            this.uxStartButton.Click += new System.EventHandler(this.uxStartButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 112);
            this.Controls.Add(this.uxStartButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;
        private System.Windows.Forms.Button uxStartButton;
    }
}

