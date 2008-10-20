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
			this.MusicPathLabel = new System.Windows.Forms.Label();
			this.MusicPathTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.PatternLabel = new System.Windows.Forms.Label();
			this.FilenamePattern = new System.Windows.Forms.TextBox();
			this.AfterRipLabel = new System.Windows.Forms.Label();
			this.AfterRipTextBox = new System.Windows.Forms.TextBox();
			this.NewSongCommandLabel = new System.Windows.Forms.Label();
			this.NewSongCommandTextBox = new System.Windows.Forms.TextBox();
			this.CommentLabel = new System.Windows.Forms.Label();
			this.CommentTextBox = new System.Windows.Forms.TextBox();
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
			this.PortLabel = new System.Windows.Forms.Label();
			this.PortTextBox = new System.Windows.Forms.TextBox();
			this.PortComment = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.SaveModeLabel = new System.Windows.Forms.Label();
			this.SaveModeCombo = new System.Windows.Forms.ComboBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.BrowseButton2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.ExcludeNewMusicCheckBox = new System.Windows.Forms.CheckBox();
			this.ExcludeExistingMusicCheckBox = new System.Windows.Forms.CheckBox();
			this.QuarantinePathTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.ExcludeFileTextBox = new System.Windows.Forms.TextBox();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.LoginGroupBox.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.NetworkGroupBox.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
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
			this.LoginGroupBox.Location = new System.Drawing.Point(-4, 0);
			this.LoginGroupBox.Name = "LoginGroupBox";
			this.LoginGroupBox.Size = new System.Drawing.Size(430, 165);
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
			this.LoginButton.Location = new System.Drawing.Point(12, 200);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 4;
			this.LoginButton.Text = "&Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
			// 
			// MusicPathLabel
			// 
			this.MusicPathLabel.Location = new System.Drawing.Point(0, 0);
			this.MusicPathLabel.Name = "MusicPathLabel";
			this.MusicPathLabel.Size = new System.Drawing.Size(81, 19);
			this.MusicPathLabel.TabIndex = 1;
			this.MusicPathLabel.Text = "Music directory";
			// 
			// MusicPathTextBox
			// 
			this.MusicPathTextBox.Location = new System.Drawing.Point(3, 22);
			this.MusicPathTextBox.Name = "MusicPathTextBox";
			this.MusicPathTextBox.Size = new System.Drawing.Size(333, 20);
			this.MusicPathTextBox.TabIndex = 1;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(345, 22);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(63, 20);
			this.BrowseButton.TabIndex = 2;
			this.BrowseButton.Text = "&Browse";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButtonClick);
			// 
			// PatternLabel
			// 
			this.PatternLabel.Location = new System.Drawing.Point(0, 78);
			this.PatternLabel.Name = "PatternLabel";
			this.PatternLabel.Size = new System.Drawing.Size(89, 19);
			this.PatternLabel.TabIndex = 3;
			this.PatternLabel.Text = "Filename Pattern";
			// 
			// FilenamePattern
			// 
			this.FilenamePattern.Location = new System.Drawing.Point(95, 75);
			this.FilenamePattern.Name = "FilenamePattern";
			this.FilenamePattern.Size = new System.Drawing.Size(241, 20);
			this.FilenamePattern.TabIndex = 3;
			// 
			// AfterRipLabel
			// 
			this.AfterRipLabel.Location = new System.Drawing.Point(0, 21);
			this.AfterRipLabel.Name = "AfterRipLabel";
			this.AfterRipLabel.Size = new System.Drawing.Size(114, 17);
			this.AfterRipLabel.TabIndex = 2;
			this.AfterRipLabel.Text = "After Rip Command";
			// 
			// AfterRipTextBox
			// 
			this.AfterRipTextBox.Location = new System.Drawing.Point(6, 41);
			this.AfterRipTextBox.Name = "AfterRipTextBox";
			this.AfterRipTextBox.Size = new System.Drawing.Size(333, 20);
			this.AfterRipTextBox.TabIndex = 1;
			// 
			// NewSongCommandLabel
			// 
			this.NewSongCommandLabel.Location = new System.Drawing.Point(0, 79);
			this.NewSongCommandLabel.Name = "NewSongCommandLabel";
			this.NewSongCommandLabel.Size = new System.Drawing.Size(114, 17);
			this.NewSongCommandLabel.TabIndex = 4;
			this.NewSongCommandLabel.Text = "New Song Command";
			// 
			// NewSongCommandTextBox
			// 
			this.NewSongCommandTextBox.Location = new System.Drawing.Point(6, 99);
			this.NewSongCommandTextBox.Name = "NewSongCommandTextBox";
			this.NewSongCommandTextBox.Size = new System.Drawing.Size(333, 20);
			this.NewSongCommandTextBox.TabIndex = 3;
			// 
			// CommentLabel
			// 
			this.CommentLabel.Location = new System.Drawing.Point(0, 104);
			this.CommentLabel.Name = "CommentLabel";
			this.CommentLabel.Size = new System.Drawing.Size(89, 17);
			this.CommentLabel.TabIndex = 5;
			this.CommentLabel.Text = "ID3 Comment";
			// 
			// CommentTextBox
			// 
			this.CommentTextBox.Location = new System.Drawing.Point(95, 101);
			this.CommentTextBox.Name = "CommentTextBox";
			this.CommentTextBox.Size = new System.Drawing.Size(241, 20);
			this.CommentTextBox.TabIndex = 6;
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
			this.groupBox3.Size = new System.Drawing.Size(416, 155);
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
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.SMILCheckBox);
			this.groupBox4.Controls.Add(this.PLSCheckBox);
			this.groupBox4.Controls.Add(this.M3UCheckBox);
			this.groupBox4.Location = new System.Drawing.Point(286, 19);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(130, 107);
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
			this.OKbutton.Location = new System.Drawing.Point(93, 200);
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
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(430, 182);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.LoginGroupBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(422, 156);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Login";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.NetworkGroupBox);
			this.tabPage2.Controls.Add(this.PortLabel);
			this.tabPage2.Controls.Add(this.PortTextBox);
			this.tabPage2.Controls.Add(this.PortComment);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(422, 156);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Network";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// PortLabel
			// 
			this.PortLabel.Location = new System.Drawing.Point(6, 97);
			this.PortLabel.Name = "PortLabel";
			this.PortLabel.Size = new System.Drawing.Size(100, 23);
			this.PortLabel.TabIndex = 8;
			this.PortLabel.Text = "Port Number:";
			// 
			// PortTextBox
			// 
			this.PortTextBox.Location = new System.Drawing.Point(112, 97);
			this.PortTextBox.Name = "PortTextBox";
			this.PortTextBox.Size = new System.Drawing.Size(51, 20);
			this.PortTextBox.TabIndex = 8;
			// 
			// PortComment
			// 
			this.PortComment.Location = new System.Drawing.Point(169, 91);
			this.PortComment.Name = "PortComment";
			this.PortComment.Size = new System.Drawing.Size(239, 47);
			this.PortComment.TabIndex = 8;
			this.PortComment.Text = "Listen to the song you are ripping opening the \"http://127.0.0.1:PortNumber\" address with your fa" +
			"vourite music player.";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.MusicPathLabel);
			this.tabPage3.Controls.Add(this.MusicPathTextBox);
			this.tabPage3.Controls.Add(this.BrowseButton);
			this.tabPage3.Controls.Add(this.SaveModeLabel);
			this.tabPage3.Controls.Add(this.SaveModeCombo);
			this.tabPage3.Controls.Add(this.PatternLabel);
			this.tabPage3.Controls.Add(this.FilenamePattern);
			this.tabPage3.Controls.Add(this.CommentLabel);
			this.tabPage3.Controls.Add(this.CommentTextBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(422, 156);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Storage";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// SaveModeLabel
			// 
			this.SaveModeLabel.Location = new System.Drawing.Point(0, 51);
			this.SaveModeLabel.Name = "SaveModeLabel";
			this.SaveModeLabel.Size = new System.Drawing.Size(89, 20);
			this.SaveModeLabel.TabIndex = 5;
			this.SaveModeLabel.Text = "Save Mode:";
			// 
			// SaveModeCombo
			// 
			this.SaveModeCombo.Items.AddRange(new object[] {
									"Save directly to disc",
									"Buffer in memory and after save to disc"});
			this.SaveModeCombo.Location = new System.Drawing.Point(95, 48);
			this.SaveModeCombo.Name = "SaveModeCombo";
			this.SaveModeCombo.Size = new System.Drawing.Size(241, 21);
			this.SaveModeCombo.TabIndex = 2;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox3);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(422, 156);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Playlist";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.BrowseButton2);
			this.tabPage5.Controls.Add(this.button1);
			this.tabPage5.Controls.Add(this.ExcludeNewMusicCheckBox);
			this.tabPage5.Controls.Add(this.ExcludeExistingMusicCheckBox);
			this.tabPage5.Controls.Add(this.QuarantinePathTextBox);
			this.tabPage5.Controls.Add(this.label6);
			this.tabPage5.Controls.Add(this.label8);
			this.tabPage5.Controls.Add(this.ExcludeFileTextBox);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(422, 156);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Advanced";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// BrowseButton2
			// 
			this.BrowseButton2.Location = new System.Drawing.Point(345, 71);
			this.BrowseButton2.Name = "BrowseButton2";
			this.BrowseButton2.Size = new System.Drawing.Size(63, 20);
			this.BrowseButton2.TabIndex = 2;
			this.BrowseButton2.Text = "&Browse";
			this.BrowseButton2.UseVisualStyleBackColor = true;
			this.BrowseButton2.Click += new System.EventHandler(this.BrowseButton2Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(345, 26);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(63, 20);
			this.button1.TabIndex = 4;
			this.button1.Text = "&Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// ExcludeNewMusicCheckBox
			// 
			this.ExcludeNewMusicCheckBox.Location = new System.Drawing.Point(6, 126);
			this.ExcludeNewMusicCheckBox.Name = "ExcludeNewMusicCheckBox";
			this.ExcludeNewMusicCheckBox.Size = new System.Drawing.Size(407, 24);
			this.ExcludeNewMusicCheckBox.TabIndex = 8;
			this.ExcludeNewMusicCheckBox.Text = "Skip new music (no artist directory in music or quarantine directory)";
			this.ExcludeNewMusicCheckBox.UseVisualStyleBackColor = true;
			// 
			// ExcludeExistingMusicCheckBox
			// 
			this.ExcludeExistingMusicCheckBox.Location = new System.Drawing.Point(6, 98);
			this.ExcludeExistingMusicCheckBox.Name = "ExcludeExistingMusicCheckBox";
			this.ExcludeExistingMusicCheckBox.Size = new System.Drawing.Size(407, 24);
			this.ExcludeExistingMusicCheckBox.TabIndex = 9;
			this.ExcludeExistingMusicCheckBox.Text = "Skip existing music (in music  or quarantine directory).";
			this.ExcludeExistingMusicCheckBox.UseVisualStyleBackColor = true;
			// 
			// QuarantinePathTextBox
			// 
			this.QuarantinePathTextBox.Location = new System.Drawing.Point(6, 72);
			this.QuarantinePathTextBox.Name = "QuarantinePathTextBox";
			this.QuarantinePathTextBox.Size = new System.Drawing.Size(333, 20);
			this.QuarantinePathTextBox.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(333, 20);
			this.label6.TabIndex = 5;
			this.label6.Text = "Exclude file (CR/LF separated artists; UTF-8)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 49);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(333, 20);
			this.label8.TabIndex = 13;
			this.label8.Text = "Quarantine directory for new music";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ExcludeFileTextBox
			// 
			this.ExcludeFileTextBox.Location = new System.Drawing.Point(6, 26);
			this.ExcludeFileTextBox.Name = "ExcludeFileTextBox";
			this.ExcludeFileTextBox.Size = new System.Drawing.Size(333, 20);
			this.ExcludeFileTextBox.TabIndex = 3;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.AfterRipLabel);
			this.tabPage6.Controls.Add(this.AfterRipTextBox);
			this.tabPage6.Controls.Add(this.NewSongCommandLabel);
			this.tabPage6.Controls.Add(this.NewSongCommandTextBox);
			this.tabPage6.Location = new System.Drawing.Point(4, 22);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(422, 156);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Commands";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.FileName = "exclude.txt";
			// 
			// Preferences
			// 
			this.AcceptButton = this.OKbutton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(452, 229);
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
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.NetworkGroupBox.ResumeLayout(false);
			this.NetworkGroupBox.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.tabPage6.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label8;
		public System.Windows.Forms.CheckBox ExcludeExistingMusicCheckBox;
		private System.Windows.Forms.Button BrowseButton2;
		public System.Windows.Forms.TextBox QuarantinePathTextBox;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		public System.Windows.Forms.CheckBox ExcludeNewMusicCheckBox;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox ExcludeFileTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabPage6;
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
		public System.Windows.Forms.TextBox FilenamePattern;
		public System.Windows.Forms.TextBox AfterRipTextBox;
		public System.Windows.Forms.TextBox NewSongCommandTextBox;
		private System.Windows.Forms.Label CommentLabel;
		public System.Windows.Forms.TextBox CommentTextBox;
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
		private System.Windows.Forms.Label MusicPathLabel;
		private System.Windows.Forms.Label PatternLabel;
		private System.Windows.Forms.Label AfterRipLabel;
		private System.Windows.Forms.Label NewSongCommandLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label SaveModeLabel;
		public System.Windows.Forms.ComboBox SaveModeCombo;
		private System.Windows.Forms.Label PortLabel;
		public System.Windows.Forms.TextBox PortTextBox;
		private System.Windows.Forms.Label PortComment;
	}
}
