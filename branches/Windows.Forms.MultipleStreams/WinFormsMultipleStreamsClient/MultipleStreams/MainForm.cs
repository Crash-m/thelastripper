using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsMultipleStreamsClient
{
    public partial class MainForm : Form
    {
        //Counter for open tabpages
        private int tabPageCounter = 1;

        //true if the program is currenty ripping
        private bool startedStreaming = false;

        public MainForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Adds databinding from all passed windows.form objects to user1
        /// </summary>
        /// <param name="user1">The User the objects should be bound to</param>
        /// <param name="usernameTextBox"></param>
        /// <param name="passwordTextBox"></param>
        /// <param name="port"></param>
        /// <param name="stationText"></param>
        /// <param name="radioGenre"></param>
        /// <param name="ipBox"></param>
        private void AddDataBinding(User user1, TextBox usernameTextBox, MaskedTextBox passwordTextBox, NumericUpDown port
            , TextBox stationText, ComboBox radioGenre, TextBox ipBox)
        {
            usernameTextBox.DataBindings.Add("Text", user1, "Username", true, DataSourceUpdateMode.OnPropertyChanged);
            passwordTextBox.DataBindings.Add("Text", user1, "Password", true, DataSourceUpdateMode.OnPropertyChanged);
            port.DataBindings.Add("Value", user1, "Port", true, DataSourceUpdateMode.OnPropertyChanged);
            stationText.DataBindings.Add("Text", user1, "StationText", true, DataSourceUpdateMode.OnPropertyChanged);
            radioGenre.DataBindings.Add("SelectedItem", user1, "RadioGenre", true, DataSourceUpdateMode.OnPropertyChanged);
            ipBox.DataBindings.Add("Text", user1, "IpAdress", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Adds a new tabpage and binds its objects to the passed user
        /// If user is null just adds an empty new tabpage
        /// </summary>
        /// <param name="user1">User you want to bind the objects to</param>
        private void AddTabPage(User user1)
        {
            tabPageCounter++;
            tabControlUsers.TabPages.Insert(tabControlUsers.TabPages.Count - 1, "User" + tabPageCounter);

            #region initializeComponents
            Label TabpageLabel1 = new Label();
            Label TabpageLabel2 = new Label();
            Label Tabpagelabel3 = new Label();
            Label Tabpagelabel4 = new Label();
            Label Tabpagelabel5 = new Label();
            MaskedTextBox TabpagePasswordTextbox = new MaskedTextBox();
            NumericUpDown TabpagePortUpDown = new NumericUpDown();
            ComboBox TabpageRadioGenreComboBox = new ComboBox();
            TextBox TabpageStationTextTextBox = new TextBox();
            TextBox TabpageUsernameTextbox = new TextBox();

            TextBox TabpageIpTextBox = new System.Windows.Forms.TextBox();
            Label labelip = new Label();




            this.toolTip1.SetToolTip(TabpagePasswordTextbox, "This property will only be updated if you restart ripping");
            this.toolTip1.SetToolTip(TabpagePortUpDown, "This property will only be updated if you restart ripping");
            this.toolTip1.SetToolTip(TabpageRadioGenreComboBox, "This property will only be updated if you restart ripping");
            this.toolTip1.SetToolTip(TabpageStationTextTextBox, "This property will only be updated if you restart ripping");
            this.toolTip1.SetToolTip(TabpageUsernameTextbox, "This property will only be updated if you restart ripping");
            this.toolTip1.SetToolTip(TabpageIpTextBox, "This property will only be updated if you restart ripping");


           

            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageLabel1);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(labelip);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageIpTextBox);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageLabel2);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(Tabpagelabel3);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(Tabpagelabel4);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(Tabpagelabel5);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpagePasswordTextbox);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpagePortUpDown);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageRadioGenreComboBox);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageStationTextTextBox);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Controls.Add(TabpageUsernameTextbox);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Location = new System.Drawing.Point(4, 22);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Padding = new System.Windows.Forms.Padding(3);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].Size = new System.Drawing.Size(744, 54);
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].TabIndex = 0;
            this.tabControlUsers.TabPages[tabControlUsers.TabPages.Count - 2].UseVisualStyleBackColor = true;


            // 
            // ipTextBox
            // 
            TabpageIpTextBox.Location = new System.Drawing.Point(286, 61);
            TabpageIpTextBox.Name = "ipTextBox";
            TabpageIpTextBox.Size = new System.Drawing.Size(120, 20);
            TabpageIpTextBox.TabIndex = 6;
            // 
            // label1
            // 
            labelip.AutoSize = true;
            labelip.Location = new System.Drawing.Point(318, 46);
            labelip.Name = "label1";
            labelip.Size = new System.Drawing.Size(62, 13);
            labelip.TabIndex = 11;
            labelip.Text = "Listening IP";
            // 
            // Tabpagelabel5
            // 
            Tabpagelabel5.AutoSize = true;
            Tabpagelabel5.Location = new System.Drawing.Point(182, 46);
            Tabpagelabel5.Name = "Tabpagelabel5";
            Tabpagelabel5.Size = new System.Drawing.Size(67, 13);
            Tabpagelabel5.TabIndex = 9;
            Tabpagelabel5.Text = "Radio Genre";
            // 
            // Tabpagelabel4
            // 
            Tabpagelabel4.AutoSize = true;
            Tabpagelabel4.Location = new System.Drawing.Point(328, 7);
            Tabpagelabel4.Name = "Tabpagelabel4";
            Tabpagelabel4.Size = new System.Drawing.Size(26, 13);
            Tabpagelabel4.TabIndex = 8;
            Tabpagelabel4.Text = "Port";
            // 
            // TabpageUsernameTextbox
            // 
            TabpageUsernameTextbox.Location = new System.Drawing.Point(10, 22);
            TabpageUsernameTextbox.Name = "TabpageUsernameTextbox";
            TabpageUsernameTextbox.Size = new System.Drawing.Size(138, 20);
            TabpageUsernameTextbox.TabIndex = 0;
            // 
            // TabpageLabel1
            // 
            TabpageLabel1.AutoSize = true;
            TabpageLabel1.Location = new System.Drawing.Point(48, 7);
            TabpageLabel1.Name = "TabpageLabel1";
            TabpageLabel1.Size = new System.Drawing.Size(55, 13);
            TabpageLabel1.TabIndex = 1;
            TabpageLabel1.Text = "Username";
            // 
            // TabpagePasswordTextbox
            // 
            TabpagePasswordTextbox.Location = new System.Drawing.Point(154, 22);
            TabpagePasswordTextbox.Name = "TabpagePasswordTextbox";
            TabpagePasswordTextbox.PasswordChar = '*';
            TabpagePasswordTextbox.Size = new System.Drawing.Size(126, 20);
            TabpagePasswordTextbox.TabIndex = 1;
            // 
            // TabpageLabel2
            // 
            TabpageLabel2.AutoSize = true;
            TabpageLabel2.Location = new System.Drawing.Point(182, 7);
            TabpageLabel2.Name = "TabpageLabel2";
            TabpageLabel2.Size = new System.Drawing.Size(53, 13);
            TabpageLabel2.TabIndex = 10;
            TabpageLabel2.Text = "Password";
            // 
            // TabpageStationTextTextBox
            // 
            TabpageStationTextTextBox.Location = new System.Drawing.Point(10, 61);
            TabpageStationTextTextBox.Name = "TabpageStationTextTextBox";
            TabpageStationTextTextBox.Size = new System.Drawing.Size(138, 20);
            TabpageStationTextTextBox.TabIndex = 2;
            // 
            // Tabpagelabel3
            // 
            Tabpagelabel3.AutoSize = true;
            Tabpagelabel3.Location = new System.Drawing.Point(48, 46);
            Tabpagelabel3.Name = "Tabpagelabel3";
            Tabpagelabel3.Size = new System.Drawing.Size(64, 13);
            Tabpagelabel3.TabIndex = 5;
            Tabpagelabel3.Text = "Station Text";
            // 
            // TabpageRadioGenreComboBox
            // 
            TabpageRadioGenreComboBox.FormattingEnabled = true;
            TabpageRadioGenreComboBox.Items.AddRange(new object[] {
            "Artist",
            "Tag",
            "Group",
            "Personal",
            "Playlist",
            "Loved",
            "Neighbourhood",
            "Recommendations"});
            TabpageRadioGenreComboBox.Location = new System.Drawing.Point(154, 60);
            TabpageRadioGenreComboBox.Name = "TabpageRadioGenreComboBox";
            TabpageRadioGenreComboBox.Size = new System.Drawing.Size(126, 21);
            TabpageRadioGenreComboBox.TabIndex = 3;
            TabpageRadioGenreComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            
            // 
            // TabpagePortUpDown
            // 
            TabpagePortUpDown.Location = new System.Drawing.Point(286, 22);
            TabpagePortUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            TabpagePortUpDown.Name = "TabpagePortUpDown";
            TabpagePortUpDown.Size = new System.Drawing.Size(120, 20);
            TabpagePortUpDown.TabIndex = 4;
            TabpagePortUpDown.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            #endregion

            //Creates an new User if no old one had been loaded
            if (user1 == null)
            {
                user1 = new User();
                ((MultipleStreamSettings)this.multipleStreamsSerializableBindingSource.DataSource).Users.Insert(0,user1);
                user1.Port = user1.Port - tabPageCounter + 1;

             
            }
            
            //Adds data binding from all the Windows.Form elements passed to user1
            AddDataBinding(user1, TabpageUsernameTextbox, TabpagePasswordTextbox, TabpagePortUpDown,
         TabpageStationTextTextBox, TabpageRadioGenreComboBox, TabpageIpTextBox);
         


        }
        
        /// <summary>
        /// Event of the start Button, thats starts ripping with all songs
        /// </summary>
        /// <param name="sender">start Button</param>
        /// <param name="e">Click Event</param>
        private void start_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Method that starts ripping with all 
        /// </summary>
        private void Start()
        {

            Save();

            if (!startedStreaming)
            {
                startedStreaming = true;
                int counter = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.Count - 1;

                foreach (User user in ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users)
                {

                    CreateForm(counter, user);
                    counter--;

                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Your already ripping, you realy want to restart ripping with the current settings(Currently ripping songs will be aborted)", "Info", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    int counter = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.Count - 1;
                    foreach (User user in ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users)
                    {
                        if (user.Form != null)
                        {
                            if (!user.Form.IsDisposed)
                            {
                                user.Form.RefreshManagerAndTuneIn();

        
                                
                            }
                        }
                        else
                        {
                            CreateForm(counter, user);
                        }
                        
                        counter--;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a stream form adds it to a tabPage and subscribes the OnNewSong EventHandler
        /// </summary>
        /// <param name="counter">index of the tabpage</param>
        /// <param name="user">user this form belongs to</param>
        private void CreateForm(int tabpageIndex, User user)
        {
            StreamForm form = new StreamForm(((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource), user);
            form.TopLevel = false;
            form.Parent = this.tabControlUsers.TabPages[tabpageIndex];
            form.Text = user.Username;
            form.Height = this.tabControlUsers.TabPages[tabpageIndex].Height - 83;
            form.Width = this.tabControlUsers.TabPages[tabpageIndex].Width;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Top = 83;
            form.Show();
            user.Form = form;
            form.Manager.OnNewSong += new EventHandler(Manager_OnNewSong);
            dataGridView1.Rows.Add(new object[] { "Skip", user.Username, null, null, null  });
           
        }

        /// <summary>
        /// Method that gets called if a new song starts
        /// </summary>
        /// <param name="sender">LastManager</param>
        /// <param name="e">MetaInfo</param>
        void Manager_OnNewSong(object sender, EventArgs e)
        {

            if (this.InvokeRequired)
            {
                //Invoke this method and it's arguments to the correct thread.
                this.Invoke(new System.EventHandler(this.Manager_OnNewSong), new System.Object[] { sender, e });
                //Return this method to avoid executing the logic on the wrong thread.
                return;
            }
            //Get some information
            LibLastRip.LastManager manager = (LibLastRip.LastManager)sender;
            LibLastRip.MetaInfo info = (LibLastRip.MetaInfo)e;

            //Set the overview datagridviewcells correctly
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value == manager.UserName)
                {
                    row.Cells[2].Value = info.Station;
                    row.Cells[3].Value = info.Track;
                    row.Cells[4].Value = info.Artist;
                }
            }
            
        }

        /// <summary>
        /// Event for changing the lockerCheckBox to enable the groupbox
        /// </summary>
        /// <param name="sender">checkbox</param>
        /// <param name="e"></param>
        private void LockercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxLocker.Enabled = LockercheckBox.Checked;
        }

        /// <summary>
        /// Method that gets called if the SelectedTabPage Changes,
        /// if the Tabpage Selected is +, add another tabpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlUsers.SelectedTab.Text == "+")
            {
                AddTabPage(null);
                tabControlUsers.SelectedIndex = tabControlUsers.TabPages.Count - 2;
            }
        }

        /// <summary>
        /// Method that gets called after the form is showed
        /// some loading is done after the form is shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultipleStreams_Shown(object sender, EventArgs e)
        {
            multipleStreamsSerializableBindingSource.DataSource = new MultipleStreamSettings();

                if (((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Load() != null)
                {
                    multipleStreamsSerializableBindingSource.DataSource = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Load();
                    tabPageCounter--;
                    for (int i = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.Count - 1; i >= 0; i--)
                    {
                        User user = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users[i];
                        AddTabPage(user);
                    }
                    tabControlUsers.TabPages.RemoveAt(0);
                }
                else
                {
                    User user1 = new User();
                    AddDataBinding(user1, tabpageUsernameTextbox, tabpagePasswordTextbox,
                        tabpagePortUpDown, tabpageStationTextTextBox, tabpageRadioGenreComboBox, ipTextBox);
                    ((MultipleStreamSettings)this.multipleStreamsSerializableBindingSource.DataSource).Users.Add(user1);
                }
            
        }

        /// <summary>
        /// Closes selected tabpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeSelectedTabpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.RemoveAt(
                ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.Count - 1 - tabControlUsers.SelectedIndex);
            this.tabControlUsers.TabPages.RemoveAt(tabControlUsers.SelectedIndex);
        }

        /// <summary>
        /// Starts recording with all users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Starts recording with the user of the selected tabpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startSelectedUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            int counter = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users.Count - this.tabControlUsers.SelectedIndex - 1;
            User user = ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users[counter];
            if (!startedStreaming)
            {
                startedStreaming = true;
                CreateForm(this.tabControlUsers.SelectedIndex, user);
            }
            else
            {
                    
                if (user.Form != null)
                {
                    if (!user.Form.IsDisposed)
                    {
                        user.Form.RefreshManagerAndTuneIn();
                  
                    }
                }
                else
                {
                    CreateForm(this.tabControlUsers.SelectedIndex, user);
                }   
                    
            }

            
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Save();
        }

        /// <summary>
        /// Saves the settings if autoSave is enabled
        /// </summary>
        private void Save()
        {
            if (checkBoxAutoSave.Checked)
            {
                ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Save();
            }
        }

        /// <summary>
        /// Shows the tabpage of the user, which row has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int counter = 0;
            foreach (User user in ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users)
            {
                if (user.Username == dataGridView1.Rows[e.RowIndex].Cells[1].Value)
                {
                    tabControlMain.SelectedIndex = 0;
                    tabControlUsers.SelectedIndex = tabControlUsers.TabPages.Count-counter -2;
                    break;
                }
                counter++;
            }

        }
        
        /// <summary>
        /// Skips the currently recording track of the selected user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (User user in ((MultipleStreamSettings)multipleStreamsSerializableBindingSource.DataSource).Users)
                {
                    if (user.Username == dataGridView1.Rows[e.RowIndex].Cells[1].Value)
                    {
                        user.Form.SkipSong();
                        break;
                    }
                }
            }
        }



    }
}
