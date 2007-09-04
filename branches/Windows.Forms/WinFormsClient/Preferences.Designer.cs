/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WinFormsClient
{
	partial class Preferences
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.LoginGroupBox = new System.Windows.Forms.GroupBox();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.SavePasswordCheckBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.UserNameTextBox = new System.Windows.Forms.TextBox();
			this.LoginButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.MusicPathTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.AllMixedCheckBox = new System.Windows.Forms.CheckBox();
			this.WeeklyTrackChartMixedCheckBox = new System.Windows.Forms.CheckBox();
			this.RecentlyLovedMixedCheckBox = new System.Windows.Forms.CheckBox();
			this.TopTracksMixedCheckBox = new System.Windows.Forms.CheckBox();
			this.WeeklyTrackChartCheckBox = new System.Windows.Forms.CheckBox();
			this.RecentlyLovedCheckBox = new System.Windows.Forms.CheckBox();
			this.TopTracksCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.SMILCheckBox = new System.Windows.Forms.CheckBox();
			this.PLSCheckBox = new System.Windows.Forms.CheckBox();
			this.M3UCheckBox = new System.Windows.Forms.CheckBox();
			this.OKbutton = new System.Windows.Forms.Button();
			this.NetworkGroupBox = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ProxyAddressTextBox = new System.Windows.Forms.TextBox();
			this.ProxyPasswordTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ProxyUsernameTextBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.LoginGroupBox.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.NetworkGroupBox.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			// 
			// FolderBrowserDialog
			// 
			this.FolderBrowserDialog.Description = "Select the root folder of your music library. All recorded music files will be sa" +
			"ved to this folder, sorted in /artist/album/number.mp3";
			// 
			// LoginGroupBox
			// 
			this.LoginGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.LoginGroupBox.Controls.Add(this.PasswordTextBox);
			this.LoginGroupBox.Controls.Add(this.SavePasswordCheckBox);
			this.LoginGroupBox.Controls.Add(this.label2);
			this.LoginGroupBox.Controls.Add(this.label1);
			this.LoginGroupBox.Controls.Add(this.UserNameTextBox);
			this.LoginGroupBox.Enabled = false;
			this.LoginGroupBox.Location = new System.Drawing.Point(0, 0);
			this.LoginGroupBox.Name = "LoginGroupBox";
			this.LoginGroupBox.Size = new System.Drawing.Size(419, 107);
			this.LoginGroupBox.TabIndex = 0;
			this.LoginGroupBox.TabStop = false;
			this.LoginGroupBox.Text = "Login";
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Location = new System.Drawing.Point(112, 45);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.PasswordChar = '*';
			this.PasswordTextBox.Size = new System.Drawing.Size(296, 20);
			this.PasswordTextBox.TabIndex = 2;
			// 
			// SavePasswordCheckBox
			// 
			this.SavePasswordCheckBox.Checked = true;
			this.SavePasswordCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SavePasswordCheckBox.Location = new System.Drawing.Point(112, 72);
			this.SavePasswordCheckBox.Name = "SavePasswordCheckBox";
			this.SavePasswordCheckBox.Size = new System.Drawing.Size(104, 24);
			this.SavePasswordCheckBox.TabIndex = 3;
			this.SavePasswordCheckBox.Text = "Save password";
			this.SavePasswordCheckBox.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Username:";
			// 
			// UserNameTextBox
			// 
			this.UserNameTextBox.Location = new System.Drawing.Point(112, 19);
			this.UserNameTextBox.Name = "UserNameTextBox";
			this.UserNameTextBox.Size = new System.Drawing.Size(296, 20);
			this.UserNameTextBox.TabIndex = 1;
			this.UserNameTextBox.TextChanged += new System.EventHandler(this.UserNameTextBoxTextChanged);
			// 
			// LoginButton
			// 
			this.LoginButton.Location = new System.Drawing.Point(12, 205);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 4;
			this.LoginButton.Text = "&Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.MusicPathTextBox);
			this.groupBox2.Controls.Add(this.BrowseButton);
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(419, 54);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Music directory";
			// 
			// MusicPathTextBox
			// 
			this.MusicPathTextBox.Location = new System.Drawing.Point(6, 22);
			this.MusicPathTextBox.Name = "MusicPathTextBox";
			this.MusicPathTextBox.Size = new System.Drawing.Size(321, 20);
			this.MusicPathTextBox.TabIndex = 1;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(333, 19);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseButton.TabIndex = 2;
			this.BrowseButton.Text = "&Browse";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButtonClick);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.AllMixedCheckBox);
			this.groupBox3.Controls.Add(this.WeeklyTrackChartMixedCheckBox);
			this.groupBox3.Controls.Add(this.RecentlyLovedMixedCheckBox);
			this.groupBox3.Controls.Add(this.TopTracksMixedCheckBox);
			this.groupBox3.Controls.Add(this.WeeklyTrackChartCheckBox);
			this.groupBox3.Controls.Add(this.RecentlyLovedCheckBox);
			this.groupBox3.Controls.Add(this.TopTracksCheckBox);
			this.groupBox3.Controls.Add(this.groupBox4);
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(414, 142);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Playlist settings";
			// 
			// AllMixedCheckBox
			// 
			this.AllMixedCheckBox.Checked = true;
			this.AllMixedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.AllMixedCheckBox.Location = new System.Drawing.Point(128, 109);
			this.AllMixedCheckBox.Name = "AllMixedCheckBox";
			this.AllMixedCheckBox.Size = new System.Drawing.Size(104, 24);
			this.AllMixedCheckBox.TabIndex = 7;
			this.AllMixedCheckBox.Text = "All lists mixed";
			this.AllMixedCheckBox.UseVisualStyleBackColor = true;
			// 
			// WeeklyTrackChartMixedCheckBox
			// 
			this.WeeklyTrackChartMixedCheckBox.Checked = true;
			this.WeeklyTrackChartMixedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.WeeklyTrackChartMixedCheckBox.Location = new System.Drawing.Point(128, 79);
			this.WeeklyTrackChartMixedCheckBox.Name = "WeeklyTrackChartMixedCheckBox";
			this.WeeklyTrackChartMixedCheckBox.Size = new System.Drawing.Size(146, 24);
			this.WeeklyTrackChartMixedCheckBox.TabIndex = 6;
			this.WeeklyTrackChartMixedCheckBox.Text = "Weekly track chart mixed";
			this.WeeklyTrackChartMixedCheckBox.UseVisualStyleBackColor = true;
			// 
			// RecentlyLovedMixedCheckBox
			// 
			this.RecentlyLovedMixedCheckBox.Checked = true;
			this.RecentlyLovedMixedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.RecentlyLovedMixedCheckBox.Location = new System.Drawing.Point(128, 49);
			this.RecentlyLovedMixedCheckBox.Name = "RecentlyLovedMixedCheckBox";
			this.RecentlyLovedMixedCheckBox.Size = new System.Drawing.Size(122, 24);
			this.RecentlyLovedMixedCheckBox.TabIndex = 5;
			this.RecentlyLovedMixedCheckBox.Text = "Recently loved mixed";
			this.RecentlyLovedMixedCheckBox.UseVisualStyleBackColor = true;
			// 
			// TopTracksMixedCheckBox
			// 
			this.TopTracksMixedCheckBox.Checked = true;
			this.TopTracksMixedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.TopTracksMixedCheckBox.Location = new System.Drawing.Point(128, 19);
			this.TopTracksMixedCheckBox.Name = "TopTracksMixedCheckBox";
			this.TopTracksMixedCheckBox.Size = new System.Drawing.Size(138, 24);
			this.TopTracksMixedCheckBox.TabIndex = 4;
			this.TopTracksMixedCheckBox.Text = "Top tracks mixed";
			this.TopTracksMixedCheckBox.UseVisualStyleBackColor = true;
			// 
			// WeeklyTrackChartCheckBox
			// 
			this.WeeklyTrackChartCheckBox.Checked = true;
			this.WeeklyTrackChartCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.WeeklyTrackChartCheckBox.Location = new System.Drawing.Point(6, 79);
			this.WeeklyTrackChartCheckBox.Name = "WeeklyTrackChartCheckBox";
			this.WeeklyTrackChartCheckBox.Size = new System.Drawing.Size(116, 24);
			this.WeeklyTrackChartCheckBox.TabIndex = 3;
			this.WeeklyTrackChartCheckBox.Text = "Weekly track chart";
			this.WeeklyTrackChartCheckBox.UseVisualStyleBackColor = true;
			// 
			// RecentlyLovedCheckBox
			// 
			this.RecentlyLovedCheckBox.Checked = true;
			this.RecentlyLovedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.RecentlyLovedCheckBox.Location = new System.Drawing.Point(6, 49);
			this.RecentlyLovedCheckBox.Name = "RecentlyLovedCheckBox";
			this.RecentlyLovedCheckBox.Size = new System.Drawing.Size(104, 24);
			this.RecentlyLovedCheckBox.TabIndex = 2;
			this.RecentlyLovedCheckBox.Text = "Recently loved";
			this.RecentlyLovedCheckBox.UseVisualStyleBackColor = true;
			// 
			// TopTracksCheckBox
			// 
			this.TopTracksCheckBox.Checked = true;
			this.TopTracksCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.TopTracksCheckBox.Location = new System.Drawing.Point(6, 19);
			this.TopTracksCheckBox.Name = "TopTracksCheckBox";
			this.TopTracksCheckBox.Size = new System.Drawing.Size(104, 24);
			this.TopTracksCheckBox.TabIndex = 1;
			this.TopTracksCheckBox.Text = "Top tracks";
			this.TopTracksCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.SMILCheckBox);
			this.groupBox4.Controls.Add(this.PLSCheckBox);
			this.groupBox4.Controls.Add(this.M3UCheckBox);
			this.groupBox4.Location = new System.Drawing.Point(280, 19);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(128, 107);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Format";
			// 
			// SMILCheckBox
			// 
			this.SMILCheckBox.Checked = true;
			this.SMILCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SMILCheckBox.Location = new System.Drawing.Point(6, 77);
			this.SMILCheckBox.Name = "SMILCheckBox";
			this.SMILCheckBox.Size = new System.Drawing.Size(104, 24);
			this.SMILCheckBox.TabIndex = 10;
			this.SMILCheckBox.Text = ".smil";
			this.SMILCheckBox.UseVisualStyleBackColor = true;
			// 
			// PLSCheckBox
			// 
			this.PLSCheckBox.Checked = true;
			this.PLSCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.PLSCheckBox.Location = new System.Drawing.Point(6, 49);
			this.PLSCheckBox.Name = "PLSCheckBox";
			this.PLSCheckBox.Size = new System.Drawing.Size(104, 24);
			this.PLSCheckBox.TabIndex = 9;
			this.PLSCheckBox.Text = ".pls";
			this.PLSCheckBox.UseVisualStyleBackColor = true;
			// 
			// M3UCheckBox
			// 
			this.M3UCheckBox.Checked = true;
			this.M3UCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.M3UCheckBox.Location = new System.Drawing.Point(6, 19);
			this.M3UCheckBox.Name = "M3UCheckBox";
			this.M3UCheckBox.Size = new System.Drawing.Size(104, 24);
			this.M3UCheckBox.TabIndex = 8;
			this.M3UCheckBox.Text = ".m3u";
			this.M3UCheckBox.UseVisualStyleBackColor = true;
			// 
			// OKbutton
			// 
			this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.OKbutton.Location = new System.Drawing.Point(93, 205);
			this.OKbutton.Name = "OKbutton";
			this.OKbutton.Size = new System.Drawing.Size(75, 23);
			this.OKbutton.TabIndex = 5;
			this.OKbutton.Text = "&Ok";
			this.OKbutton.UseVisualStyleBackColor = true;
			// 
			// NetworkGroupBox
			// 
			this.NetworkGroupBox.Controls.Add(this.label5);
			this.NetworkGroupBox.Controls.Add(this.ProxyAddressTextBox);
			this.NetworkGroupBox.Controls.Add(this.ProxyPasswordTextBox);
			this.NetworkGroupBox.Controls.Add(this.label3);
			this.NetworkGroupBox.Controls.Add(this.label4);
			this.NetworkGroupBox.Controls.Add(this.ProxyUsernameTextBox);
			this.NetworkGroupBox.Location = new System.Drawing.Point(0, 0);
			this.NetworkGroupBox.Name = "NetworkGroupBox";
			this.NetworkGroupBox.Size = new System.Drawing.Size(419, 88);
			this.NetworkGroupBox.TabIndex = 5;
			this.NetworkGroupBox.TabStop = false;
			this.NetworkGroupBox.Text = "Proxy Settings";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 12;
			this.label5.Text = "proxy host:";
			// 
			// ProxyAddressTextBox
			// 
			this.ProxyAddressTextBox.Location = new System.Drawing.Point(112, 13);
			this.ProxyAddressTextBox.Name = "ProxyAddressTextBox";
			this.ProxyAddressTextBox.Size = new System.Drawing.Size(296, 20);
			this.ProxyAddressTextBox.TabIndex = 7;
			// 
			// ProxyPasswordTextBox
			// 
			this.ProxyPasswordTextBox.Location = new System.Drawing.Point(112, 59);
			this.ProxyPasswordTextBox.Name = "ProxyPasswordTextBox";
			this.ProxyPasswordTextBox.PasswordChar = '*';
			this.ProxyPasswordTextBox.Size = new System.Drawing.Size(296, 20);
			this.ProxyPasswordTextBox.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Password:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Username:";
			// 
			// ProxyUsernameTextBox
			// 
			this.ProxyUsernameTextBox.Location = new System.Drawing.Point(112, 36);
			this.ProxyUsernameTextBox.Name = "ProxyUsernameTextBox";
			this.ProxyUsernameTextBox.Size = new System.Drawing.Size(296, 20);
			this.ProxyUsernameTextBox.TabIndex = 8;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(430, 187);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.LoginGroupBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(422, 161);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Login";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.NetworkGroupBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(422, 161);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Network";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(422, 161);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Storage";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox3);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(422, 161);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Playlist";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// Preferences
			// 
			this.AcceptButton = this.OKbutton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 237);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.OKbutton);
			this.Controls.Add(this.LoginButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Preferences";
			this.Text = "Preferences";
			this.LoginGroupBox.ResumeLayout(false);
			this.LoginGroupBox.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.NetworkGroupBox.ResumeLayout(false);
			this.NetworkGroupBox.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.GroupBox NetworkGroupBox;
		public System.Windows.Forms.TextBox ProxyUsernameTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox ProxyPasswordTextBox;
		public System.Windows.Forms.TextBox ProxyAddressTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox LoginGroupBox;
		public System.Windows.Forms.TextBox PasswordTextBox;
		public System.Windows.Forms.TextBox UserNameTextBox;
		public System.Windows.Forms.CheckBox SavePasswordCheckBox;
		private System.Windows.Forms.Button LoginButton;
		public System.Windows.Forms.TextBox MusicPathTextBox;
		private System.Windows.Forms.Button BrowseButton;
		public System.Windows.Forms.CheckBox SMILCheckBox;
		public System.Windows.Forms.CheckBox PLSCheckBox;
		public System.Windows.Forms.CheckBox M3UCheckBox;
		public System.Windows.Forms.CheckBox AllMixedCheckBox;
		public System.Windows.Forms.CheckBox WeeklyTrackChartMixedCheckBox;
		public System.Windows.Forms.CheckBox RecentlyLovedMixedCheckBox;
		public System.Windows.Forms.CheckBox TopTracksMixedCheckBox;
		public System.Windows.Forms.CheckBox WeeklyTrackChartCheckBox;
		public System.Windows.Forms.CheckBox RecentlyLovedCheckBox;
		public System.Windows.Forms.CheckBox TopTracksCheckBox;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
		private System.Windows.Forms.Button OKbutton;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}
