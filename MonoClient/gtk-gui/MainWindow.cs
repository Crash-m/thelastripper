// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
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
    
    private Gtk.ComboBoxEntry LastFMStationEntry;
    
    private Gtk.HBox hbox1;
    
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
        Gtk.UIManager w1 = new Gtk.UIManager();
        Gtk.ActionGroup w2 = new Gtk.ActionGroup("Default");
        this.File = new Gtk.Action("File", Mono.Unix.Catalog.GetString("_File"), null, null);
        this.File.ShortLabel = "File";
        w2.Add(this.File, null);
        this.Preferences = new Gtk.Action("Preferences", Mono.Unix.Catalog.GetString("_Preferences"), null, "gtk-preferences");
        this.Preferences.ShortLabel = "Preferences";
        w2.Add(this.Preferences, null);
        this.Exit = new Gtk.Action("Exit", Mono.Unix.Catalog.GetString("Exit"), null, "gtk-quit");
        this.Exit.ShortLabel = "Exit";
        w2.Add(this.Exit, null);
        this.GeneratePlaylist = new Gtk.Action("GeneratePlaylist", Mono.Unix.Catalog.GetString("Generate playli_st"), Mono.Unix.Catalog.GetString("Saves playlist as defined in perferences"), "gtk-refresh");
        this.GeneratePlaylist.ShortLabel = "Generate playlist";
        w2.Add(this.GeneratePlaylist, "<Control><Mod2>s");
        this.Help = new Gtk.Action("Help", Mono.Unix.Catalog.GetString("_Help"), "", null);
        this.Help.ShortLabel = "Help";
        w2.Add(this.Help, null);
        this.OnlineHelp = new Gtk.Action("OnlineHelp", Mono.Unix.Catalog.GetString("Online _help"), Mono.Unix.Catalog.GetString("Find help online"), "gtk-help");
        this.OnlineHelp.ShortLabel = "Online help";
        w2.Add(this.OnlineHelp, "<Mod2>F11");
        this.About = new Gtk.Action("About", Mono.Unix.Catalog.GetString("_About"), null, "gtk-about");
        this.About.ShortLabel = "About";
        w2.Add(this.About, null);
        this.LegalNotice = new Gtk.Action("LegalNotice", Mono.Unix.Catalog.GetString("_Legal notice"), null, "gnome-stock-attach");
        this.LegalNotice.ShortLabel = "Legal notice";
        w2.Add(this.LegalNotice, null);
        w1.InsertActionGroup(w2, 0);
        this.AddAccelGroup(w1.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("TheLastRipper");
        this.Icon = Gdk.Pixbuf.LoadFromResource("logo.png");
        this.WindowPosition = ((Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox4 = new Gtk.VBox();
        this.vbox4.Name = "vbox4";
        // Container child vbox4.Gtk.Box+BoxChild
        w1.AddUiFromString("<ui><menubar name='menubar4'><menu action='File'><menuitem action='Preferences'/><menuitem action='GeneratePlaylist'/><separator/><menuitem action='Exit'/></menu><menu action='Help'><menuitem action='LegalNotice'/><menuitem action='OnlineHelp'/><separator/><menuitem action='About'/></menu></menubar></ui>");
        this.menubar4 = ((Gtk.MenuBar)(w1.GetWidget("/menubar4")));
        this.menubar4.Name = "menubar4";
        this.vbox4.Add(this.menubar4);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox4[this.menubar4]));
        w3.Position = 0;
        w3.Expand = false;
        w3.Fill = false;
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
        this.LastFMStationEntry = Gtk.ComboBoxEntry.NewText();
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/rock"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/indie"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/alternative"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/seen%20live"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/metal"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/electronic"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/pop"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/punk"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/indie%20rock"));
        this.LastFMStationEntry.AppendText(Mono.Unix.Catalog.GetString("lastfm://globaltags/classic%20rock"));
        this.LastFMStationEntry.Sensitive = false;
        this.LastFMStationEntry.Name = "LastFMStationEntry";
        this.vbox6.Add(this.LastFMStationEntry);
        Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox6[this.LastFMStationEntry]));
        w4.Position = 0;
        w4.Expand = false;
        // Container child vbox6.Gtk.Box+BoxChild
        this.hbox1 = new Gtk.HBox();
        this.hbox1.Name = "hbox1";
        // Container child hbox1.Gtk.Box+BoxChild
        this.ConnectButton = new Gtk.Button();
        this.ConnectButton.Sensitive = false;
        this.ConnectButton.CanFocus = true;
        this.ConnectButton.Name = "ConnectButton";
        // Container child ConnectButton.Gtk.Container+ContainerChild
        Gtk.Alignment w5 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w6 = new Gtk.HBox();
        w6.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w7 = new Gtk.Image();
        w7.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_media-play", Gtk.IconSize.Menu, 16);
        w6.Add(w7);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w9 = new Gtk.Label();
        w9.LabelProp = Mono.Unix.Catalog.GetString("Tune in");
        w6.Add(w9);
        w5.Add(w6);
        this.ConnectButton.Add(w5);
        this.hbox1.Add(this.ConnectButton);
        Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.hbox1[this.ConnectButton]));
        w13.Position = 0;
        w13.Expand = false;
        w13.Fill = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.SkipButton = new Gtk.Button();
        this.SkipButton.Sensitive = false;
        this.SkipButton.CanFocus = true;
        this.SkipButton.Name = "SkipButton";
        this.SkipButton.UseUnderline = true;
        // Container child SkipButton.Gtk.Container+ContainerChild
        Gtk.Alignment w14 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w15 = new Gtk.HBox();
        w15.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w16 = new Gtk.Image();
        w16.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_media-next", Gtk.IconSize.Menu, 16);
        w15.Add(w16);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w18 = new Gtk.Label();
        w18.LabelProp = Mono.Unix.Catalog.GetString("Skip");
        w18.UseUnderline = true;
        w15.Add(w18);
        w14.Add(w15);
        this.SkipButton.Add(w14);
        this.hbox1.Add(this.SkipButton);
        Gtk.Box.BoxChild w22 = ((Gtk.Box.BoxChild)(this.hbox1[this.SkipButton]));
        w22.Position = 1;
        w22.Expand = false;
        w22.Fill = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.LoveButton = new Gtk.Button();
        this.LoveButton.Sensitive = false;
        this.LoveButton.CanFocus = true;
        this.LoveButton.Name = "LoveButton";
        // Container child LoveButton.Gtk.Container+ContainerChild
        Gtk.Alignment w23 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w24 = new Gtk.HBox();
        w24.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w25 = new Gtk.Image();
        w25.Pixbuf = Stetic.IconLoader.LoadIcon(this, "stock_3d-favourites", Gtk.IconSize.Menu, 16);
        w24.Add(w25);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w27 = new Gtk.Label();
        w27.LabelProp = Mono.Unix.Catalog.GetString("Love");
        w24.Add(w27);
        w23.Add(w24);
        this.LoveButton.Add(w23);
        this.hbox1.Add(this.LoveButton);
        Gtk.Box.BoxChild w31 = ((Gtk.Box.BoxChild)(this.hbox1[this.LoveButton]));
        w31.Position = 2;
        w31.Expand = false;
        w31.Fill = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.BanButton = new Gtk.Button();
        this.BanButton.Sensitive = false;
        this.BanButton.CanFocus = true;
        this.BanButton.Name = "BanButton";
        // Container child BanButton.Gtk.Container+ContainerChild
        Gtk.Alignment w32 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
        // Container child GtkAlignment.Gtk.Container+ContainerChild
        Gtk.HBox w33 = new Gtk.HBox();
        w33.Spacing = 2;
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Image w34 = new Gtk.Image();
        w34.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-remove", Gtk.IconSize.Menu, 16);
        w33.Add(w34);
        // Container child GtkHBox.Gtk.Container+ContainerChild
        Gtk.Label w36 = new Gtk.Label();
        w36.LabelProp = Mono.Unix.Catalog.GetString("Ban");
        w33.Add(w36);
        w32.Add(w33);
        this.BanButton.Add(w32);
        this.hbox1.Add(this.BanButton);
        Gtk.Box.BoxChild w40 = ((Gtk.Box.BoxChild)(this.hbox1[this.BanButton]));
        w40.Position = 3;
        w40.Expand = false;
        w40.Fill = false;
        this.vbox6.Add(this.hbox1);
        Gtk.Box.BoxChild w41 = ((Gtk.Box.BoxChild)(this.vbox6[this.hbox1]));
        w41.PackType = ((Gtk.PackType)(1));
        w41.Position = 1;
        w41.Expand = false;
        this.GtkAlignment1.Add(this.vbox6);
        this.frame5.Add(this.GtkAlignment1);
        this.GtkLabel6 = new Gtk.Label();
        this.GtkLabel6.Name = "GtkLabel6";
        this.GtkLabel6.LabelProp = Mono.Unix.Catalog.GetString("<b>Last.FM Station</b>");
        this.GtkLabel6.UseMarkup = true;
        this.frame5.LabelWidget = this.GtkLabel6;
        this.vbox4.Add(this.frame5);
        Gtk.Box.BoxChild w44 = ((Gtk.Box.BoxChild)(this.vbox4[this.frame5]));
        w44.Position = 1;
        w44.Expand = false;
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
        this.StatusLabel.LabelProp = "Not Recording";
        this.StatusLabel.UseMarkup = true;
        this.hbox5.Add(this.StatusLabel);
        Gtk.Box.BoxChild w45 = ((Gtk.Box.BoxChild)(this.hbox5[this.StatusLabel]));
        w45.Position = 0;
        // Container child hbox5.Gtk.Box+BoxChild
        this.CoverBox = new Gtk.Image();
        this.CoverBox.Name = "CoverBox";
        this.CoverBox.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-cdrom", Gtk.IconSize.Menu, 16);
        this.hbox5.Add(this.CoverBox);
        Gtk.Box.BoxChild w46 = ((Gtk.Box.BoxChild)(this.hbox5[this.CoverBox]));
        w46.Position = 1;
        w46.Expand = false;
        w46.Fill = false;
        this.vbox8.Add(this.hbox5);
        Gtk.Box.BoxChild w47 = ((Gtk.Box.BoxChild)(this.vbox8[this.hbox5]));
        w47.Position = 0;
        w47.Expand = false;
        // Container child vbox8.Gtk.Box+BoxChild
        this.SongProgressBar = new Gtk.ProgressBar();
        this.SongProgressBar.Name = "SongProgressBar";
        this.vbox8.Add(this.SongProgressBar);
        Gtk.Box.BoxChild w48 = ((Gtk.Box.BoxChild)(this.vbox8[this.SongProgressBar]));
        w48.Position = 1;
        w48.Expand = false;
        this.GtkAlignment5.Add(this.vbox8);
        this.frame6.Add(this.GtkAlignment5);
        this.GtkLabel5 = new Gtk.Label();
        this.GtkLabel5.Name = "GtkLabel5";
        this.GtkLabel5.LabelProp = Mono.Unix.Catalog.GetString("<b>Currently recording</b>");
        this.GtkLabel5.UseMarkup = true;
        this.frame6.LabelWidget = this.GtkLabel5;
        this.vbox4.Add(this.frame6);
        Gtk.Box.BoxChild w51 = ((Gtk.Box.BoxChild)(this.vbox4[this.frame6]));
        w51.Position = 2;
        this.Add(this.vbox4);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 272;
        this.DefaultHeight = 185;
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.Preferences.Activated += new System.EventHandler(this.OnPreferencesActivated);
        this.Exit.Activated += new System.EventHandler(this.OnExitActivated);
        this.GeneratePlaylist.Activated += new System.EventHandler(this.OnGeneratePlaylistClick);
        this.OnlineHelp.Activated += new System.EventHandler(this.OnOnlineHelpActivated);
        this.About.Activated += new System.EventHandler(this.OnAboutActivated);
        this.LegalNotice.Activated += new System.EventHandler(this.OnLegalNoticeActivated);
        this.ConnectButton.Clicked += new System.EventHandler(this.OnConnectButtonClicked);
        this.SkipButton.Clicked += new System.EventHandler(this.OnSkipButtonClicked);
        this.LoveButton.Clicked += new System.EventHandler(this.OnLoveButtonClicked);
        this.BanButton.Clicked += new System.EventHandler(this.OnBanButtonClicked);
    }
}
