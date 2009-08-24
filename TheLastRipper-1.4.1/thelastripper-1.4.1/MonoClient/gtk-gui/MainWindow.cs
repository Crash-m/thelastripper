// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.UIManager UIManager;
    
    private Gtk.Action File;
    
    private Gtk.Action Preferences;
    
    private Gtk.Action Exit;
    
    private Gtk.Action GeneratePlaylist;
    
    private Gtk.Action Help;
    
    private Gtk.Action OnlineHelp;
    
    private Gtk.Action About;
    
    private Gtk.Action LegalNotice;
    
    private Gtk.VBox vbox4;
    
    private Gtk.MenuBar menubar4;
    
    private Gtk.Frame frame5;
    
    private Gtk.Alignment GtkAlignment1;
    
    private Gtk.VBox vbox6;
    
    private Gtk.HBox StationBox;
    
    private Gtk.ComboBox StationType;
    
    private Gtk.Entry LastFMStationEntry;
    
    private Gtk.HBox hbox2;
    
    private Gtk.Button ConnectButton;
    
    private Gtk.Button SkipButton;
    
    private Gtk.Button LoveButton;
    
    private Gtk.Button BanButton;
    
    private Gtk.Label GtkLabel6;
    
    private Gtk.Frame frame6;
    
    private Gtk.Alignment GtkAlignment5;
    
    private Gtk.VBox vbox8;
    
    private Gtk.HBox hbox5;
    
    private Gtk.Label StatusLabel;
    
    private Gtk.Image CoverBox;
    
    private Gtk.ProgressBar SongProgressBar;
    
    private Gtk.Label GtkLabel5;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.UIManager = new Gtk.UIManager();
        Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
        this.File = new Gtk.Action("File", Mono.Unix.Catalog.GetString("_File"), null, null);
        this.File.ShortLabel = "File";
        w1.Add(this.File, null);
        this.Preferences = new Gtk.Action("Preferences", Mono.Unix.Catalog.GetString("_Preferences"), null, "gtk-preferences");
        this.Preferences.ShortLabel = "Preferences";
        w1.Add(this.Preferences, null);
        this.Exit = new Gtk.Action("Exit", Mono.Unix.Catalog.GetString("Exit"), null, "gtk-quit");
        this.Exit.ShortLabel = "Exit";
        w1.Add(this.Exit, null);
        this.GeneratePlaylist = new Gtk.Action("GeneratePlaylist", Mono.Unix.Catalog.GetString("Generate playli_st"), Mono.Unix.Catalog.GetString("Saves playlist as defined in perferences"), "gtk-refresh");
        this.GeneratePlaylist.ShortLabel = "Generate playlist";
        w1.Add(this.GeneratePlaylist, "<Control><Mod2>s");
        this.Help = new Gtk.Action("Help", Mono.Unix.Catalog.GetString("_Help"), "", null);
        this.Help.ShortLabel = "Help";
        w1.Add(this.Help, null);
        this.OnlineHelp = new Gtk.Action("OnlineHelp", Mono.Unix.Catalog.GetString("Online _help"), Mono.Unix.Catalog.GetString("Find help online"), "gtk-help");
        this.OnlineHelp.ShortLabel = "Online help";
        w1.Add(this.OnlineHelp, "<Mod2>F11");
        this.About = new Gtk.Action("About", Mono.Unix.Catalog.GetString("_About"), null, "gtk-about");
        this.About.ShortLabel = "About";
        w1.Add(this.About, null);
        this.LegalNotice = new Gtk.Action("LegalNotice", Mono.Unix.Catalog.GetString("_Legal notice"), null, "gnome-stock-attach");
        this.LegalNotice.ShortLabel = "Legal notice";
        w1.Add(this.LegalNotice, null);
        this.UIManager.InsertActionGroup(w1, 0);
        this.AddAccelGroup(this.UIManager.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("TheLastRipper");
        this.Icon = Gdk.Pixbuf.LoadFromResource("logo.png");
        this.WindowPosition = ((Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox4 = new Gtk.VBox();
        this.vbox4.Name = "vbox4";
        // Container child vbox4.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><menubar name='menubar4'><menu name='File' action='File'><menuitem name='Preferences' action='Preferences'/><menuitem name='GeneratePlaylist' action='GeneratePlaylist'/><separator/><menuitem name='Exit' action='Exit'/></menu><menu name='Help' action='Help'><menuitem name='LegalNotice' action='LegalNotice'/><menuitem name='OnlineHelp' action='OnlineHelp'/><separator/><menuitem name='About' action='About'/></menu></menubar></ui>");
        this.menubar4 = ((Gtk.MenuBar)(this.UIManager.GetWidget("/menubar4")));
        this.menubar4.Name = "menubar4";
        this.vbox4.Add(this.menubar4);
        Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox4[this.menubar4]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child vbox4.Gtk.Box+BoxChild
        this.frame5 = new Gtk.Frame();
        this.frame5.Name = "frame5";
        this.frame5.ShadowType = ((Gtk.ShadowType)(0));
        // Container child frame5.Gtk.Container+ContainerChild
        this.GtkAlignment1 = new Gtk.Alignment(0F, 0F, 1F, 1F);
        this.GtkAlignment1.Name = "GtkAlignment1";
        this.GtkAlignment1.LeftPadding = ((uint)(12));
        // Container child GtkAlignment1.Gtk.Container+ContainerChild
        this.vbox6 = new Gtk.VBox();
        this.vbox6.Name = "vbox6";
        // Container child vbox6.Gtk.Box+BoxChild
        this.StationBox = new Gtk.HBox();
        this.StationBox.Sensitive = false;
        this.StationBox.Name = "StationBox";
        this.StationBox.Spacing = 6;
        // Container child StationBox.Gtk.Box+BoxChild
        this.StationType = Gtk.ComboBox.NewText();
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Tag:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Similar artist:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Playlist:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Personal:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Loved:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Recommended:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Neighbours:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("Group:"));
        this.StationType.AppendText(Mono.Unix.Catalog.GetString("lastfm://"));
        this.StationType.Name = "StationType";
        this.StationType.Active = 0;
        this.StationBox.Add(this.StationType);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.StationBox[this.StationType]));
        w3.Position = 0;
        w3.Expand = false;
        w3.Fill = false;
        // Container child StationBox.Gtk.Box+BoxChild
        this.LastFMStationEntry = new Gtk.Entry();
        this.LastFMStationEntry.CanFocus = true;
        this.LastFMStationEntry.Name = "LastFMStationEntry";
        this.LastFMStationEntry.Text = Mono.Unix.Catalog.GetString("rock");
        this.LastFMStationEntry.IsEditable = true;
        this.LastFMStationEntry.InvisibleChar = '●';
        this.StationBox.Add(this.LastFMStationEntry);
        Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.StationBox[this.LastFMStationEntry]));
        w4.Position = 1;
        this.vbox6.Add(this.StationBox);
        Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox6[this.StationBox]));
        w5.Position = 0;
        w5.Expand = false;
        w5.Fill = false;
        // Container child vbox6.Gtk.Box+BoxChild
        this.hbox2 = new Gtk.HBox();
        this.hbox2.Name = "hbox2";
        this.hbox2.Spacing = 6;
        // Container child hbox2.Gtk.Box+BoxChild
        this.ConnectButton = new Gtk.Button();
        this.ConnectButton.Sensitive = false;
        this.ConnectButton.CanFocus = true;
        this.ConnectButton.Name = "ConnectButton";
        // Container child ConnectButton.Gtk.Container+ContainerChild
        Gtk.Alignment w6 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w7 = new Gtk.HBox();
        w7.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w8 = new Gtk.Image();
        w8.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_media-play", Gtk.IconSize.Menu, 16);
        w7.Add(w8);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w10 = new Gtk.Label();
        w10.LabelProp = Mono.Unix.Catalog.GetString("Tune in");
        w7.Add(w10);
        w6.Add(w7);
        this.ConnectButton.Add(w6);
        this.hbox2.Add(this.ConnectButton);
        Gtk.Box.BoxChild w14 = ((Gtk.Box.BoxChild)(this.hbox2[this.ConnectButton]));
        w14.Position = 0;
        w14.Expand = false;
        w14.Fill = false;
        // Container child hbox2.Gtk.Box+BoxChild
        this.SkipButton = new Gtk.Button();
        this.SkipButton.Sensitive = false;
        this.SkipButton.CanFocus = true;
        this.SkipButton.Name = "SkipButton";
        this.SkipButton.UseUnderline = true;
        // Container child SkipButton.Gtk.Container+ContainerChild
        Gtk.Alignment w15 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w16 = new Gtk.HBox();
        w16.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w17 = new Gtk.Image();
        w17.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_media-next", Gtk.IconSize.Menu, 16);
        w16.Add(w17);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w19 = new Gtk.Label();
        w19.LabelProp = Mono.Unix.Catalog.GetString("Skip");
        w19.UseUnderline = true;
        w16.Add(w19);
        w15.Add(w16);
        this.SkipButton.Add(w15);
        this.hbox2.Add(this.SkipButton);
        Gtk.Box.BoxChild w23 = ((Gtk.Box.BoxChild)(this.hbox2[this.SkipButton]));
        w23.Position = 1;
        w23.Expand = false;
        w23.Fill = false;
        // Container child hbox2.Gtk.Box+BoxChild
        this.LoveButton = new Gtk.Button();
        this.LoveButton.Sensitive = false;
        this.LoveButton.CanFocus = true;
        this.LoveButton.Name = "LoveButton";
        // Container child LoveButton.Gtk.Container+ContainerChild
        Gtk.Alignment w24 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w25 = new Gtk.HBox();
        w25.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w26 = new Gtk.Image();
        w26.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_3d-favourites", Gtk.IconSize.Menu, 16);
        w25.Add(w26);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w28 = new Gtk.Label();
        w28.LabelProp = Mono.Unix.Catalog.GetString("Love");
        w25.Add(w28);
        w24.Add(w25);
        this.LoveButton.Add(w24);
        this.hbox2.Add(this.LoveButton);
        Gtk.Box.BoxChild w32 = ((Gtk.Box.BoxChild)(this.hbox2[this.LoveButton]));
        w32.Position = 2;
        w32.Expand = false;
        w32.Fill = false;
        // Container child hbox2.Gtk.Box+BoxChild
        this.BanButton = new Gtk.Button();
        this.BanButton.Sensitive = false;
        this.BanButton.CanFocus = true;
        this.BanButton.Name = "BanButton";
        // Container child BanButton.Gtk.Container+ContainerChild
        Gtk.Alignment w33 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w34 = new Gtk.HBox();
        w34.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w35 = new Gtk.Image();
        w35.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-remove", Gtk.IconSize.Menu, 16);
        w34.Add(w35);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w37 = new Gtk.Label();
        w37.LabelProp = Mono.Unix.Catalog.GetString("Ban");
        w34.Add(w37);
        w33.Add(w34);
        this.BanButton.Add(w33);
        this.hbox2.Add(this.BanButton);
        Gtk.Box.BoxChild w41 = ((Gtk.Box.BoxChild)(this.hbox2[this.BanButton]));
        w41.Position = 3;
        w41.Expand = false;
        w41.Fill = false;
        this.vbox6.Add(this.hbox2);
        Gtk.Box.BoxChild w42 = ((Gtk.Box.BoxChild)(this.vbox6[this.hbox2]));
        w42.PackType = ((Gtk.PackType)(1));
        w42.Position = 1;
        w42.Expand = false;
        this.GtkAlignment1.Add(this.vbox6);
        this.frame5.Add(this.GtkAlignment1);
        this.GtkLabel6 = new Gtk.Label();
        this.GtkLabel6.Name = "GtkLabel6";
        this.GtkLabel6.LabelProp = Mono.Unix.Catalog.GetString("<b>Last.FM Station</b>");
        this.GtkLabel6.UseMarkup = true;
        this.frame5.LabelWidget = this.GtkLabel6;
        this.vbox4.Add(this.frame5);
        Gtk.Box.BoxChild w45 = ((Gtk.Box.BoxChild)(this.vbox4[this.frame5]));
        w45.Position = 1;
        w45.Expand = false;
        // Container child vbox4.Gtk.Box+BoxChild
        this.frame6 = new Gtk.Frame();
        this.frame6.Name = "frame6";
        this.frame6.ShadowType = ((Gtk.ShadowType)(0));
        // Container child frame6.Gtk.Container+ContainerChild
        this.GtkAlignment5 = new Gtk.Alignment(0F, 0F, 1F, 1F);
        this.GtkAlignment5.Name = "GtkAlignment5";
        this.GtkAlignment5.LeftPadding = ((uint)(12));
        // Container child GtkAlignment5.Gtk.Container+ContainerChild
        this.vbox8 = new Gtk.VBox();
        this.vbox8.Name = "vbox8";
        // Container child vbox8.Gtk.Box+BoxChild
        this.hbox5 = new Gtk.HBox();
        this.hbox5.Name = "hbox5";
        // Container child hbox5.Gtk.Box+BoxChild
        this.StatusLabel = new Gtk.Label();
        this.StatusLabel.Name = "StatusLabel";
        this.StatusLabel.Xalign = 0F;
        this.StatusLabel.Yalign = 0F;
        this.StatusLabel.LabelProp = "Not Recording";
        this.StatusLabel.UseMarkup = true;
        this.hbox5.Add(this.StatusLabel);
        Gtk.Box.BoxChild w46 = ((Gtk.Box.BoxChild)(this.hbox5[this.StatusLabel]));
        w46.Position = 0;
        // Container child hbox5.Gtk.Box+BoxChild
        this.CoverBox = new Gtk.Image();
        this.CoverBox.Name = "CoverBox";
        this.CoverBox.Yalign = 0F;
        this.CoverBox.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-cdrom", Gtk.IconSize.Dialog, 48);
        this.hbox5.Add(this.CoverBox);
        Gtk.Box.BoxChild w47 = ((Gtk.Box.BoxChild)(this.hbox5[this.CoverBox]));
        w47.Position = 1;
        w47.Expand = false;
        w47.Fill = false;
        this.vbox8.Add(this.hbox5);
        Gtk.Box.BoxChild w48 = ((Gtk.Box.BoxChild)(this.vbox8[this.hbox5]));
        w48.Position = 0;
        // Container child vbox8.Gtk.Box+BoxChild
        this.SongProgressBar = new Gtk.ProgressBar();
        this.SongProgressBar.Name = "SongProgressBar";
        this.vbox8.Add(this.SongProgressBar);
        Gtk.Box.BoxChild w49 = ((Gtk.Box.BoxChild)(this.vbox8[this.SongProgressBar]));
        w49.Position = 1;
        w49.Expand = false;
        this.GtkAlignment5.Add(this.vbox8);
        this.frame6.Add(this.GtkAlignment5);
        this.GtkLabel5 = new Gtk.Label();
        this.GtkLabel5.Name = "GtkLabel5";
        this.GtkLabel5.LabelProp = Mono.Unix.Catalog.GetString("<b>Currently recording</b>");
        this.GtkLabel5.UseMarkup = true;
        this.frame6.LabelWidget = this.GtkLabel5;
        this.vbox4.Add(this.frame6);
        Gtk.Box.BoxChild w52 = ((Gtk.Box.BoxChild)(this.vbox4[this.frame6]));
        w52.Position = 2;
        this.Add(this.vbox4);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 498;
        this.DefaultHeight = 349;
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.Preferences.Activated += new System.EventHandler(this.OnPreferencesActivated);
        this.Exit.Activated += new System.EventHandler(this.OnExitActivated);
        this.GeneratePlaylist.Activated += new System.EventHandler(this.OnGeneratePlaylistClick);
        this.OnlineHelp.Activated += new System.EventHandler(this.OnOnlineHelpActivated);
        this.About.Activated += new System.EventHandler(this.OnAboutActivated);
        this.LegalNotice.Activated += new System.EventHandler(this.OnLegalNoticeActivated);
        this.StationType.Changed += new System.EventHandler(this.OnStationTypeChanged);
        this.ConnectButton.Clicked += new System.EventHandler(this.OnConnectButtonClicked);
        this.SkipButton.Clicked += new System.EventHandler(this.OnSkipButtonClicked);
        this.LoveButton.Clicked += new System.EventHandler(this.OnLoveButtonClicked);
        this.BanButton.Clicked += new System.EventHandler(this.OnBanButtonClicked);
    }
}