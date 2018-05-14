namespace MoneyManager
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
            this.uxListView = new System.Windows.Forms.ListView();
            this.AmountColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uxAmountBox = new System.Windows.Forms.TextBox();
            this.uxDescriptionBox = new System.Windows.Forms.TextBox();
            this.uxAddButton = new System.Windows.Forms.Button();
            this.uxTotalLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uxSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxDescriptionLabel = new System.Windows.Forms.Label();
            this.uxAmountLabel = new System.Windows.Forms.Label();
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.uxTotalAmountLabel = new System.Windows.Forms.Label();
            this.uxPrintDialog = new System.Windows.Forms.PrintDialog();
            this.uxPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.uxPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // uxListView
            // 
            this.uxListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AmountColumn,
            this.DescriptionColumn});
            this.uxListView.GridLines = true;
            this.uxListView.Location = new System.Drawing.Point(12, 70);
            this.uxListView.Name = "uxListView";
            this.uxListView.Size = new System.Drawing.Size(775, 352);
            this.uxListView.TabIndex = 0;
            this.uxListView.UseCompatibleStateImageBehavior = false;
            this.uxListView.View = System.Windows.Forms.View.Details;
            // 
            // AmountColumn
            // 
            this.AmountColumn.Text = "Amount";
            this.AmountColumn.Width = 93;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Text = "Description";
            this.DescriptionColumn.Width = 684;
            // 
            // uxAmountBox
            // 
            this.uxAmountBox.Location = new System.Drawing.Point(62, 27);
            this.uxAmountBox.Name = "uxAmountBox";
            this.uxAmountBox.Size = new System.Drawing.Size(136, 20);
            this.uxAmountBox.TabIndex = 1;
            this.uxAmountBox.TextChanged += new System.EventHandler(this.uxAmountBox_TextChanged);
            this.uxAmountBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_KeyDown);
            // 
            // uxDescriptionBox
            // 
            this.uxDescriptionBox.Location = new System.Drawing.Point(270, 27);
            this.uxDescriptionBox.Name = "uxDescriptionBox";
            this.uxDescriptionBox.Size = new System.Drawing.Size(422, 20);
            this.uxDescriptionBox.TabIndex = 2;
            this.uxDescriptionBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_KeyDown);
            // 
            // uxAddButton
            // 
            this.uxAddButton.Enabled = false;
            this.uxAddButton.Location = new System.Drawing.Point(712, 27);
            this.uxAddButton.Name = "uxAddButton";
            this.uxAddButton.Size = new System.Drawing.Size(75, 23);
            this.uxAddButton.TabIndex = 3;
            this.uxAddButton.Text = "Add";
            this.uxAddButton.UseVisualStyleBackColor = true;
            this.uxAddButton.Click += new System.EventHandler(this.uxAddButton_Click);
            // 
            // uxTotalLabel
            // 
            this.uxTotalLabel.AutoSize = true;
            this.uxTotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTotalLabel.Location = new System.Drawing.Point(13, 425);
            this.uxTotalLabel.Name = "uxTotalLabel";
            this.uxTotalLabel.Size = new System.Drawing.Size(57, 20);
            this.uxTotalLabel.TabIndex = 4;
            this.uxTotalLabel.Text = "Total $";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxSaveAsMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uxSaveAsMenuItem
            // 
            this.uxSaveAsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxOpenMenuItem,
            this.uxSaveMenuItem,
            this.saveAsToolStripMenuItem});
            this.uxSaveAsMenuItem.Name = "uxSaveAsMenuItem";
            this.uxSaveAsMenuItem.Size = new System.Drawing.Size(37, 20);
            this.uxSaveAsMenuItem.Text = "File";
            // 
            // uxOpenMenuItem
            // 
            this.uxOpenMenuItem.Name = "uxOpenMenuItem";
            this.uxOpenMenuItem.Size = new System.Drawing.Size(114, 22);
            this.uxOpenMenuItem.Text = "Open";
            this.uxOpenMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // uxSaveMenuItem
            // 
            this.uxSaveMenuItem.Name = "uxSaveMenuItem";
            this.uxSaveMenuItem.Size = new System.Drawing.Size(114, 22);
            this.uxSaveMenuItem.Text = "Save";
            this.uxSaveMenuItem.Click += new System.EventHandler(this.uxSaveMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // uxDescriptionLabel
            // 
            this.uxDescriptionLabel.AutoSize = true;
            this.uxDescriptionLabel.Location = new System.Drawing.Point(204, 30);
            this.uxDescriptionLabel.Name = "uxDescriptionLabel";
            this.uxDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.uxDescriptionLabel.TabIndex = 6;
            this.uxDescriptionLabel.Text = "Description";
            // 
            // uxAmountLabel
            // 
            this.uxAmountLabel.AutoSize = true;
            this.uxAmountLabel.Location = new System.Drawing.Point(9, 30);
            this.uxAmountLabel.Name = "uxAmountLabel";
            this.uxAmountLabel.Size = new System.Drawing.Size(43, 13);
            this.uxAmountLabel.TabIndex = 7;
            this.uxAmountLabel.Text = "Amount";
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "txt";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // uxTotalAmountLabel
            // 
            this.uxTotalAmountLabel.AutoSize = true;
            this.uxTotalAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxTotalAmountLabel.Location = new System.Drawing.Point(67, 425);
            this.uxTotalAmountLabel.Name = "uxTotalAmountLabel";
            this.uxTotalAmountLabel.Size = new System.Drawing.Size(18, 20);
            this.uxTotalAmountLabel.TabIndex = 8;
            this.uxTotalAmountLabel.Text = "0";
            // 
            // uxPrintDialog
            // 
            this.uxPrintDialog.UseEXDialog = true;
            // 
            // uxPrintPreview
            // 
            this.uxPrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.uxPrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.uxPrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.uxPrintPreview.Enabled = true;
            this.uxPrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("uxPrintPreview.Icon")));
            this.uxPrintPreview.Name = "uxPrintPreview";
            this.uxPrintPreview.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.uxTotalAmountLabel);
            this.Controls.Add(this.uxAmountLabel);
            this.Controls.Add(this.uxDescriptionLabel);
            this.Controls.Add(this.uxTotalLabel);
            this.Controls.Add(this.uxAddButton);
            this.Controls.Add(this.uxDescriptionBox);
            this.Controls.Add(this.uxAmountBox);
            this.Controls.Add(this.uxListView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Money Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView uxListView;
        private System.Windows.Forms.ColumnHeader AmountColumn;
        private System.Windows.Forms.ColumnHeader DescriptionColumn;
        private System.Windows.Forms.TextBox uxAmountBox;
        private System.Windows.Forms.TextBox uxDescriptionBox;
        private System.Windows.Forms.Button uxAddButton;
        private System.Windows.Forms.Label uxTotalLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uxSaveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxOpenMenuItem;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog;
        private System.Windows.Forms.Label uxDescriptionLabel;
        private System.Windows.Forms.Label uxAmountLabel;
        private System.Windows.Forms.ToolStripMenuItem uxSaveMenuItem;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Label uxTotalAmountLabel;
        private System.Windows.Forms.PrintDialog uxPrintDialog;
        private System.Drawing.Printing.PrintDocument uxPrintDocument;
        private System.Windows.Forms.PrintPreviewDialog uxPrintPreview;
    }
}

