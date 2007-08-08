/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 11-02-2007
 * Time: 14:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WinFormsClient
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.HateButton = new System.Windows.Forms.Button();
			this.LoveButton = new System.Windows.Forms.Button();
			this.SkipButton = new System.Windows.Forms.Button();
			this.TuneInButton = new System.Windows.Forms.Button();
			this.RadioStation = new System.Windows.Forms.ComboBox();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generatePlaylistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.legalIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.StatuspictureBox = new System.Windows.Forms.PictureBox();
			this.StatusBar = new System.Windows.Forms.ProgressBar();
			this.groupBox1.SuspendLayout();
			this.MainMenu.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StatuspictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.HateButton);
			this.groupBox1.Controls.Add(this.LoveButton);
			this.groupBox1.Controls.Add(this.SkipButton);
			this.groupBox1.Controls.Add(this.TuneInButton);
			this.groupBox1.Controls.Add(this.RadioStation);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(338, 77);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Radio station";
			// 
			// HateButton
			// 
			this.HateButton.Enabled = false;
			this.HateButton.Location = new System.Drawing.Point(249, 46);
			this.HateButton.Name = "HateButton";
			this.HateButton.Size = new System.Drawing.Size(75, 23);
			this.HateButton.TabIndex = 4;
			this.HateButton.Text = "H&ate";
			this.HateButton.UseVisualStyleBackColor = true;
			this.HateButton.Click += new System.EventHandler(this.HateButtonClick);
			// 
			// LoveButton
			// 
			this.LoveButton.Enabled = false;
			this.LoveButton.Location = new System.Drawing.Point(168, 46);
			this.LoveButton.Name = "LoveButton";
			this.LoveButton.Size = new System.Drawing.Size(75, 23);
			this.LoveButton.TabIndex = 3;
			this.LoveButton.Text = "&Love";
			this.LoveButton.UseVisualStyleBackColor = true;
			this.LoveButton.Click += new System.EventHandler(this.LoveButtonClick);
			// 
			// SkipButton
			// 
			this.SkipButton.Enabled = false;
			this.SkipButton.Location = new System.Drawing.Point(87, 46);
			this.SkipButton.Name = "SkipButton";
			this.SkipButton.Size = new System.Drawing.Size(75, 23);
			this.SkipButton.TabIndex = 2;
			this.SkipButton.Text = "&Skip";
			this.SkipButton.UseVisualStyleBackColor = true;
			this.SkipButton.Click += new System.EventHandler(this.SkipButtonClick);
			// 
			// TuneInButton
			// 
			this.TuneInButton.Enabled = false;
			this.TuneInButton.Location = new System.Drawing.Point(6, 46);
			this.TuneInButton.Name = "TuneInButton";
			this.TuneInButton.Size = new System.Drawing.Size(75, 23);
			this.TuneInButton.TabIndex = 1;
			this.TuneInButton.Text = "&Tune in";
			this.TuneInButton.UseVisualStyleBackColor = true;
			this.TuneInButton.Click += new System.EventHandler(this.TuneInButtonClick);
			// 
			// RadioStation
			// 
			this.RadioStation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.RadioStation.Enabled = false;
			this.RadioStation.FormattingEnabled = true;
			this.RadioStation.Items.AddRange(new object[] {
									"lastfm://globaltags/rock",
									"lastfm://globaltags/indie",
									"lastfm://globaltags/alternative",
									"lastfm://globaltags/seen%20live",
									"lastfm://globaltags/metal",
									"lastfm://globaltags/electronic",
									"lastfm://globaltags/pop",
									"lastfm://globaltags/punk",
									"lastfm://globaltags/indie%20rock",
									"lastfm://globaltags/classic%20rock",
			                        "lastfm://artist/Rammstein/similarartists",
			                        "lastfm://user/HansMustermann/personal"});
			this.RadioStation.Location = new System.Drawing.Point(6, 19);
			this.RadioStation.Name = "RadioStation";
			this.RadioStation.Size = new System.Drawing.Size(326, 21);
			this.RadioStation.TabIndex = 0;
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(362, 24);
			this.MainMenu.TabIndex = 1;
			this.MainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.settingsToolStripMenuItem,
									this.generatePlaylistsToolStripMenuItem,
									this.toolStripSeparator2,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.settingsToolStripMenuItem.Text = "&Settings";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItemClick);
			// 
			// generatePlaylistsToolStripMenuItem
			// 
			this.generatePlaylistsToolStripMenuItem.Name = "generatePlaylistsToolStripMenuItem";
			this.generatePlaylistsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.generatePlaylistsToolStripMenuItem.Text = "&Generate playlists";
			this.generatePlaylistsToolStripMenuItem.Click += new System.EventHandler(this.GeneratePlaylistsToolStripMenuItemClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.legalIssuesToolStripMenuItem,
									this.onlineHelpToolStripMenuItem,
									this.toolStripSeparator1,
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// legalIssuesToolStripMenuItem
			// 
			this.legalIssuesToolStripMenuItem.Name = "legalIssuesToolStripMenuItem";
			this.legalIssuesToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.legalIssuesToolStripMenuItem.Text = "&Legal issues";
			this.legalIssuesToolStripMenuItem.Click += new System.EventHandler(this.LegalIssuesToolStripMenuItemClick);
			// 
			// onlineHelpToolStripMenuItem
			// 
			this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
			this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.onlineHelpToolStripMenuItem.Text = "Online &Help";
			this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.OnlineHelpToolStripMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.StatusLabel);
			this.groupBox2.Controls.Add(this.StatuspictureBox);
			this.groupBox2.Controls.Add(this.StatusBar);
			this.groupBox2.Location = new System.Drawing.Point(12, 110);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(338, 194);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Currently recording";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.StatusLabel.Location = new System.Drawing.Point(6, 19);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(180, 143);
			this.StatusLabel.TabIndex = 2;
			this.StatusLabel.Text = "Not recording...";
			// 
			// StatuspictureBox
			// 
			this.StatuspictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.StatuspictureBox.Location = new System.Drawing.Point(192, 19);
			this.StatuspictureBox.Name = "StatuspictureBox";
			this.StatuspictureBox.Size = new System.Drawing.Size(140, 140);
			this.StatuspictureBox.TabIndex = 1;
			this.StatuspictureBox.TabStop = false;
			// 
			// StatusBar
			// 
			this.StatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.StatusBar.Location = new System.Drawing.Point(6, 165);
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(326, 23);
			this.StatusBar.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(362, 323);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.MainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MainMenu;
			this.MinimumSize = new System.Drawing.Size(370, 350);
			this.Name = "MainForm";
			this.Text = "TheLastRipper";
			this.groupBox1.ResumeLayout(false);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.StatuspictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.Button HateButton;
		private System.Windows.Forms.Button LoveButton;
		private System.Windows.Forms.Button SkipButton;
		private System.Windows.Forms.Button TuneInButton;
		private System.Windows.Forms.ComboBox RadioStation;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.PictureBox StatuspictureBox;
		private System.Windows.Forms.ProgressBar StatusBar;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem legalIssuesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem generatePlaylistsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
