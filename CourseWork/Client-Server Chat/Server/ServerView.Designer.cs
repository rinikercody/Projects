namespace FinalProject_T10_A_Server
{
    partial class ServerView
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
            this.UserListBox = new System.Windows.Forms.ListBox();
            this.ContactListBox = new System.Windows.Forms.ListBox();
            this.ChannelListBox = new System.Windows.Forms.ListBox();
            this.ChannelUsersList = new System.Windows.Forms.ListBox();
            this.UserListLabel = new System.Windows.Forms.Label();
            this.ContactListLabel = new System.Windows.Forms.Label();
            this.ChannelListLabel = new System.Windows.Forms.Label();
            this.ChannelUsersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserListBox
            // 
            this.UserListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserListBox.FormattingEnabled = true;
            this.UserListBox.Location = new System.Drawing.Point(12, 32);
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.Size = new System.Drawing.Size(270, 238);
            this.UserListBox.Sorted = true;
            this.UserListBox.TabIndex = 0;
            // 
            // ContactListBox
            // 
            this.ContactListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContactListBox.FormattingEnabled = true;
            this.ContactListBox.Location = new System.Drawing.Point(302, 32);
            this.ContactListBox.Name = "ContactListBox";
            this.ContactListBox.Size = new System.Drawing.Size(270, 238);
            this.ContactListBox.TabIndex = 1;
            // 
            // ChannelListBox
            // 
            this.ChannelListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelListBox.FormattingEnabled = true;
            this.ChannelListBox.Location = new System.Drawing.Point(12, 311);
            this.ChannelListBox.Name = "ChannelListBox";
            this.ChannelListBox.Size = new System.Drawing.Size(270, 238);
            this.ChannelListBox.TabIndex = 2;
            // 
            // ChannelUsersList
            // 
            this.ChannelUsersList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelUsersList.FormattingEnabled = true;
            this.ChannelUsersList.Location = new System.Drawing.Point(302, 311);
            this.ChannelUsersList.Name = "ChannelUsersList";
            this.ChannelUsersList.Size = new System.Drawing.Size(270, 238);
            this.ChannelUsersList.TabIndex = 3;
            // 
            // UserListLabel
            // 
            this.UserListLabel.AutoSize = true;
            this.UserListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.UserListLabel.Location = new System.Drawing.Point(107, 9);
            this.UserListLabel.Name = "UserListLabel";
            this.UserListLabel.Size = new System.Drawing.Size(51, 20);
            this.UserListLabel.TabIndex = 4;
            this.UserListLabel.Text = "Users";
            // 
            // ContactListLabel
            // 
            this.ContactListLabel.AutoSize = true;
            this.ContactListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ContactListLabel.Location = new System.Drawing.Point(378, 9);
            this.ContactListLabel.Name = "ContactListLabel";
            this.ContactListLabel.Size = new System.Drawing.Size(122, 20);
            this.ContactListLabel.TabIndex = 5;
            this.ContactListLabel.Text = "User\'s Contacts";
            // 
            // ChannelListLabel
            // 
            this.ChannelListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ChannelListLabel.AutoSize = true;
            this.ChannelListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ChannelListLabel.Location = new System.Drawing.Point(107, 288);
            this.ChannelListLabel.Name = "ChannelListLabel";
            this.ChannelListLabel.Size = new System.Drawing.Size(76, 20);
            this.ChannelListLabel.TabIndex = 6;
            this.ChannelListLabel.Text = "Channels";
            // 
            // ChannelUsersLabel
            // 
            this.ChannelUsersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ChannelUsersLabel.AutoSize = true;
            this.ChannelUsersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ChannelUsersLabel.Location = new System.Drawing.Point(370, 288);
            this.ChannelUsersLabel.Name = "ChannelUsersLabel";
            this.ChannelUsersLabel.Size = new System.Drawing.Size(130, 20);
            this.ChannelUsersLabel.TabIndex = 7;
            this.ChannelUsersLabel.Text = "Users in Channel";
            // 
            // ServerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.ChannelUsersLabel);
            this.Controls.Add(this.ChannelListLabel);
            this.Controls.Add(this.ContactListLabel);
            this.Controls.Add(this.UserListLabel);
            this.Controls.Add(this.ChannelUsersList);
            this.Controls.Add(this.ChannelListBox);
            this.Controls.Add(this.ContactListBox);
            this.Controls.Add(this.UserListBox);
            this.Name = "ServerView";
            this.Text = "Chat Server View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox UserListBox;
        private System.Windows.Forms.ListBox ContactListBox;
        private System.Windows.Forms.ListBox ChannelListBox;
        private System.Windows.Forms.ListBox ChannelUsersList;
        private System.Windows.Forms.Label UserListLabel;
        private System.Windows.Forms.Label ContactListLabel;
        private System.Windows.Forms.Label ChannelListLabel;
        private System.Windows.Forms.Label ChannelUsersLabel;
    }
}