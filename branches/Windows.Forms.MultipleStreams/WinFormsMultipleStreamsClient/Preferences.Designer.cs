/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WinFormsMultipleStreamsClient
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
            this.label9 = new System.Windows.Forms.Label();
            this.ProxyEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ProxyAddressTextBox = new System.Windows.Forms.TextBox();
            this.ProxyPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ProxyUsernameTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ListeningGroupBox = new System.Windows.Forms.GroupBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.PortComment = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PatternLabel = new System.Windows.Forms.Label();
            this.FilenamePattern = new System.Windows.Forms.TextBox();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.CommentTextBox = new System.Windows.Forms.TextBox();
            this.SaveModeLabel = new System.Windows.Forms.Label();
            this.SaveModeCombo = new System.Windows.Forms.ComboBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.MusicPathTextBox = new System.Windows.Forms.TextBox();
            this.MusicPathLabel = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.HealthTextBox = new System.Windows.Forms.TextBox();
            this.HealthCheckBox = new System.Windows.Forms.CheckBox();
            this.BrowseButton2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ExcludeNewMusicCheckBox = new System.Windows.Forms.CheckBox();
            this.ExcludeExistingMusicCheckBox = new System.Windows.Forms.CheckBox();
            this.QuarantinePathTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ExcludeFileTextBox = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NewSongCommandLabel = new System.Windows.Forms.Label();
            this.NewSongCommandTextBox = new System.Windows.Forms.TextBox();
            this.AfterRipLabel = new System.Windows.Forms.Label();
            this.AfterRipTextBox = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.LockercheckBox = new System.Windows.Forms.CheckBox();
            this.groupBoxLocker = new System.Windows.Forms.GroupBox();
            this.LockerPasswordTextBox = new System.Windows.Forms.MaskedTextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.LockerEmailtextBox = new System.Windows.Forms.TextBox();
            this.CreateLockerbutton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoginGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.NetworkGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.ListeningGroupBox.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBoxLocker.SuspendLayout();
            this.SuspendLayout();
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.Description = "Select the root folder of your music library. All recorded music files will be sa" +
                "ved to this folder, sorted in /artist/album/number.mp3";
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.PasswordTextBox);
            this.LoginGroupBox.Controls.Add(this.SavePasswordCheckBox);
            this.LoginGroupBox.Controls.Add(this.label2);
            this.LoginGroupBox.Controls.Add(this.label1);
            this.LoginGroupBox.Controls.Add(this.UserNameTextBox);
            this.LoginGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginGroupBox.Enabled = false;
            this.LoginGroupBox.Location = new System.Drawing.Point(3, 3);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(416, 177);
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
            this.LoginButton.Location = new System.Drawing.Point(12, 227);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "&Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButtonClick);
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
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 177);
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
            this.groupBox4.Location = new System.Drawing.Point(280, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(130, 129);
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
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(93, 227);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 5;
            this.OKbutton.Text = "&Ok";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // NetworkGroupBox
            // 
            this.NetworkGroupBox.Controls.Add(this.label9);
            this.NetworkGroupBox.Controls.Add(this.ProxyEnabledCheckBox);
            this.NetworkGroupBox.Controls.Add(this.label5);
            this.NetworkGroupBox.Controls.Add(this.ProxyAddressTextBox);
            this.NetworkGroupBox.Controls.Add(this.ProxyPasswordTextBox);
            this.NetworkGroupBox.Controls.Add(this.label3);
            this.NetworkGroupBox.Controls.Add(this.label4);
            this.NetworkGroupBox.Controls.Add(this.ProxyUsernameTextBox);
            this.NetworkGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.NetworkGroupBox.Location = new System.Drawing.Point(3, 3);
            this.NetworkGroupBox.Name = "NetworkGroupBox";
            this.NetworkGroupBox.Size = new System.Drawing.Size(416, 120);
            this.NetworkGroupBox.TabIndex = 5;
            this.NetworkGroupBox.TabStop = false;
            this.NetworkGroupBox.Text = "Proxy Settings";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Proxy enabled:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProxyEnabledCheckBox
            // 
            this.ProxyEnabledCheckBox.Location = new System.Drawing.Point(94, 19);
            this.ProxyEnabledCheckBox.Name = "ProxyEnabledCheckBox";
            this.ProxyEnabledCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProxyEnabledCheckBox.Size = new System.Drawing.Size(15, 20);
            this.ProxyEnabledCheckBox.TabIndex = 13;
            this.ProxyEnabledCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ProxyEnabledCheckBox.UseVisualStyleBackColor = true;
            this.ProxyEnabledCheckBox.CheckedChanged += new System.EventHandler(this.CheckBoxProxyEnabledCheckedChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Proxy host:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProxyAddressTextBox
            // 
            this.ProxyAddressTextBox.Location = new System.Drawing.Point(94, 40);
            this.ProxyAddressTextBox.Name = "ProxyAddressTextBox";
            this.ProxyAddressTextBox.Size = new System.Drawing.Size(316, 20);
            this.ProxyAddressTextBox.TabIndex = 7;
            // 
            // ProxyPasswordTextBox
            // 
            this.ProxyPasswordTextBox.Location = new System.Drawing.Point(94, 91);
            this.ProxyPasswordTextBox.Name = "ProxyPasswordTextBox";
            this.ProxyPasswordTextBox.PasswordChar = '*';
            this.ProxyPasswordTextBox.Size = new System.Drawing.Size(316, 20);
            this.ProxyPasswordTextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Username:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProxyUsernameTextBox
            // 
            this.ProxyUsernameTextBox.Location = new System.Drawing.Point(94, 66);
            this.ProxyUsernameTextBox.Name = "ProxyUsernameTextBox";
            this.ProxyUsernameTextBox.Size = new System.Drawing.Size(316, 20);
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
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 209);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LoginGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(422, 183);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Login";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ListeningGroupBox);
            this.tabPage2.Controls.Add(this.NetworkGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(422, 183);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Network";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ListeningGroupBox
            // 
            this.ListeningGroupBox.Controls.Add(this.PortLabel);
            this.ListeningGroupBox.Controls.Add(this.PortTextBox);
            this.ListeningGroupBox.Controls.Add(this.PortComment);
            this.ListeningGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ListeningGroupBox.Location = new System.Drawing.Point(3, 129);
            this.ListeningGroupBox.Name = "ListeningGroupBox";
            this.ListeningGroupBox.Size = new System.Drawing.Size(416, 51);
            this.ListeningGroupBox.TabIndex = 9;
            this.ListeningGroupBox.TabStop = false;
            this.ListeningGroupBox.Text = "Listening Settings";
            // 
            // PortLabel
            // 
            this.PortLabel.Location = new System.Drawing.Point(6, 14);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(83, 20);
            this.PortLabel.TabIndex = 11;
            this.PortLabel.Text = "Port Number:";
            this.PortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(94, 14);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(51, 20);
            this.PortTextBox.TabIndex = 10;
            // 
            // PortComment
            // 
            this.PortComment.Location = new System.Drawing.Point(151, 8);
            this.PortComment.Name = "PortComment";
            this.PortComment.Size = new System.Drawing.Size(256, 40);
            this.PortComment.TabIndex = 9;
            this.PortComment.Text = "Listen to the song you are ripping opening the \"http://127.0.0.1:PortNumber\" addr" +
                "ess with your favourite music player.";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(422, 183);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Storage";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PatternLabel);
            this.groupBox1.Controls.Add(this.FilenamePattern);
            this.groupBox1.Controls.Add(this.CommentLabel);
            this.groupBox1.Controls.Add(this.CommentTextBox);
            this.groupBox1.Controls.Add(this.SaveModeLabel);
            this.groupBox1.Controls.Add(this.SaveModeCombo);
            this.groupBox1.Controls.Add(this.BrowseButton);
            this.groupBox1.Controls.Add(this.MusicPathTextBox);
            this.groupBox1.Controls.Add(this.MusicPathLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 177);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // PatternLabel
            // 
            this.PatternLabel.Location = new System.Drawing.Point(6, 114);
            this.PatternLabel.Name = "PatternLabel";
            this.PatternLabel.Size = new System.Drawing.Size(89, 19);
            this.PatternLabel.TabIndex = 9;
            this.PatternLabel.Text = "Filename Pattern";
            // 
            // FilenamePattern
            // 
            this.FilenamePattern.Location = new System.Drawing.Point(101, 114);
            this.FilenamePattern.Name = "FilenamePattern";
            this.FilenamePattern.Size = new System.Drawing.Size(302, 20);
            this.FilenamePattern.TabIndex = 8;
            // 
            // CommentLabel
            // 
            this.CommentLabel.Location = new System.Drawing.Point(6, 143);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(89, 17);
            this.CommentLabel.TabIndex = 10;
            this.CommentLabel.Text = "ID3 Comment";
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Location = new System.Drawing.Point(101, 140);
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.Size = new System.Drawing.Size(302, 20);
            this.CommentTextBox.TabIndex = 11;
            // 
            // SaveModeLabel
            // 
            this.SaveModeLabel.Location = new System.Drawing.Point(6, 87);
            this.SaveModeLabel.Name = "SaveModeLabel";
            this.SaveModeLabel.Size = new System.Drawing.Size(89, 20);
            this.SaveModeLabel.TabIndex = 7;
            this.SaveModeLabel.Text = "Save Mode:";
            // 
            // SaveModeCombo
            // 
            this.SaveModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SaveModeCombo.Items.AddRange(new object[] {
            "Save directly to disc",
            "Buffer in memory and after save to disc"});
            this.SaveModeCombo.Location = new System.Drawing.Point(101, 87);
            this.SaveModeCombo.Name = "SaveModeCombo";
            this.SaveModeCombo.Size = new System.Drawing.Size(302, 21);
            this.SaveModeCombo.TabIndex = 6;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(340, 12);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(63, 20);
            this.BrowseButton.TabIndex = 4;
            this.BrowseButton.Text = "&Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // MusicPathTextBox
            // 
            this.MusicPathTextBox.Location = new System.Drawing.Point(6, 38);
            this.MusicPathTextBox.Name = "MusicPathTextBox";
            this.MusicPathTextBox.Size = new System.Drawing.Size(397, 20);
            this.MusicPathTextBox.TabIndex = 3;
            // 
            // MusicPathLabel
            // 
            this.MusicPathLabel.Location = new System.Drawing.Point(6, 16);
            this.MusicPathLabel.Name = "MusicPathLabel";
            this.MusicPathLabel.Size = new System.Drawing.Size(81, 19);
            this.MusicPathLabel.TabIndex = 2;
            this.MusicPathLabel.Text = "Music directory";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(422, 183);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Playlist";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.HealthTextBox);
            this.tabPage5.Controls.Add(this.HealthCheckBox);
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
            this.tabPage5.Size = new System.Drawing.Size(422, 183);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Advanced";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(345, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "46718";
            // 
            // HealthTextBox
            // 
            this.HealthTextBox.Location = new System.Drawing.Point(272, 155);
            this.HealthTextBox.Name = "HealthTextBox";
            this.HealthTextBox.Size = new System.Drawing.Size(67, 20);
            this.HealthTextBox.TabIndex = 15;
            this.HealthTextBox.Text = "46718";
            this.HealthTextBox.TextChanged += new System.EventHandler(this.HealthTextBoxTextChanged);
            // 
            // HealthCheckBox
            // 
            this.HealthCheckBox.Location = new System.Drawing.Point(6, 153);
            this.HealthCheckBox.Name = "HealthCheckBox";
            this.HealthCheckBox.Size = new System.Drawing.Size(260, 24);
            this.HealthCheckBox.TabIndex = 14;
            this.HealthCheckBox.Text = "rename possibly damaged files with health below";
            this.HealthCheckBox.UseVisualStyleBackColor = true;
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
            this.ExcludeNewMusicCheckBox.CheckedChanged += new System.EventHandler(this.ExcludeNewMusicCheckBoxCheckedChanged);
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
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(422, 183);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Commands";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NewSongCommandLabel);
            this.groupBox2.Controls.Add(this.NewSongCommandTextBox);
            this.groupBox2.Controls.Add(this.AfterRipLabel);
            this.groupBox2.Controls.Add(this.AfterRipTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 177);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // NewSongCommandLabel
            // 
            this.NewSongCommandLabel.Location = new System.Drawing.Point(6, 71);
            this.NewSongCommandLabel.Name = "NewSongCommandLabel";
            this.NewSongCommandLabel.Size = new System.Drawing.Size(398, 17);
            this.NewSongCommandLabel.TabIndex = 6;
            this.NewSongCommandLabel.Text = "New Song Command";
            // 
            // NewSongCommandTextBox
            // 
            this.NewSongCommandTextBox.Location = new System.Drawing.Point(6, 91);
            this.NewSongCommandTextBox.Name = "NewSongCommandTextBox";
            this.NewSongCommandTextBox.Size = new System.Drawing.Size(398, 20);
            this.NewSongCommandTextBox.TabIndex = 5;
            // 
            // AfterRipLabel
            // 
            this.AfterRipLabel.Location = new System.Drawing.Point(6, 16);
            this.AfterRipLabel.Name = "AfterRipLabel";
            this.AfterRipLabel.Size = new System.Drawing.Size(114, 17);
            this.AfterRipLabel.TabIndex = 4;
            this.AfterRipLabel.Text = "After Rip Command";
            // 
            // AfterRipTextBox
            // 
            this.AfterRipTextBox.Location = new System.Drawing.Point(6, 36);
            this.AfterRipTextBox.Name = "AfterRipTextBox";
            this.AfterRipTextBox.Size = new System.Drawing.Size(398, 20);
            this.AfterRipTextBox.TabIndex = 3;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.LockercheckBox);
            this.tabPage7.Controls.Add(this.groupBoxLocker);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(422, 183);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "MP3tunes";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // LockercheckBox
            // 
            this.LockercheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LockercheckBox.Location = new System.Drawing.Point(12, 6);
            this.LockercheckBox.Name = "LockercheckBox";
            this.LockercheckBox.Size = new System.Drawing.Size(404, 24);
            this.LockercheckBox.TabIndex = 1;
            this.LockercheckBox.Text = "Upload recorded tracks to music locker at MP3tunes";
            this.LockercheckBox.UseVisualStyleBackColor = true;
            this.LockercheckBox.CheckedChanged += new System.EventHandler(this.LockercheckBoxCheckedChanged);
            // 
            // groupBoxLocker
            // 
            this.groupBoxLocker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLocker.Controls.Add(this.LockerPasswordTextBox);
            this.groupBoxLocker.Controls.Add(this.labelPassword);
            this.groupBoxLocker.Controls.Add(this.emailLabel);
            this.groupBoxLocker.Controls.Add(this.LockerEmailtextBox);
            this.groupBoxLocker.Controls.Add(this.CreateLockerbutton);
            this.groupBoxLocker.Enabled = false;
            this.groupBoxLocker.Location = new System.Drawing.Point(6, 36);
            this.groupBoxLocker.Name = "groupBoxLocker";
            this.groupBoxLocker.Size = new System.Drawing.Size(410, 114);
            this.groupBoxLocker.TabIndex = 0;
            this.groupBoxLocker.TabStop = false;
            this.groupBoxLocker.Text = "MP3tunes login";
            // 
            // LockerPasswordTextBox
            // 
            this.LockerPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LockerPasswordTextBox.Location = new System.Drawing.Point(88, 45);
            this.LockerPasswordTextBox.Name = "LockerPasswordTextBox";
            this.LockerPasswordTextBox.Size = new System.Drawing.Size(316, 20);
            this.LockerPasswordTextBox.TabIndex = 5;
            this.LockerPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(6, 48);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(76, 23);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Password:";
            // 
            // emailLabel
            // 
            this.emailLabel.Location = new System.Drawing.Point(6, 22);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(76, 17);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "E-mail:";
            // 
            // LockerEmailtextBox
            // 
            this.LockerEmailtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LockerEmailtextBox.Location = new System.Drawing.Point(88, 19);
            this.LockerEmailtextBox.Name = "LockerEmailtextBox";
            this.LockerEmailtextBox.Size = new System.Drawing.Size(316, 20);
            this.LockerEmailtextBox.TabIndex = 2;
            // 
            // CreateLockerbutton
            // 
            this.CreateLockerbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateLockerbutton.Location = new System.Drawing.Point(239, 71);
            this.CreateLockerbutton.Name = "CreateLockerbutton";
            this.CreateLockerbutton.Size = new System.Drawing.Size(165, 23);
            this.CreateLockerbutton.TabIndex = 0;
            this.CreateLockerbutton.Text = "Create new MP3tunes account";
            this.CreateLockerbutton.UseVisualStyleBackColor = true;
            this.CreateLockerbutton.Click += new System.EventHandler(this.CreateLockerbuttonClick);
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
            this.ClientSize = new System.Drawing.Size(452, 262);
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
            this.ListeningGroupBox.ResumeLayout(false);
            this.ListeningGroupBox.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.groupBoxLocker.ResumeLayout(false);
            this.groupBoxLocker.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label label9;
		public System.Windows.Forms.CheckBox ProxyEnabledCheckBox;
		private System.Windows.Forms.Button CreateLockerbutton;
		public System.Windows.Forms.TextBox LockerEmailtextBox;
		private System.Windows.Forms.Label emailLabel;
		private System.Windows.Forms.Label labelPassword;
		public System.Windows.Forms.MaskedTextBox LockerPasswordTextBox;
		private System.Windows.Forms.GroupBox groupBoxLocker;
		public System.Windows.Forms.CheckBox LockercheckBox;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.CheckBox HealthCheckBox;
		public System.Windows.Forms.TextBox HealthTextBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox ListeningGroupBox;
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
		
		void CheckBoxProxyEnabledCheckedChanged(object sender, System.EventArgs e)
		{
			
		}
	}
}
