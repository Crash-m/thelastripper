// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoClient {
    
    
    public partial class About {
        
        private Gtk.Image image15;
        
        private Gtk.Button CloseButton;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget MonoClient.About
            this.Events = ((Gdk.EventMask)(256));
            this.Name = "MonoClient.About";
            this.Title = Mono.Unix.Catalog.GetString("About TheLastRipper");
            this.Icon = Stetic.IconLoader.LoadIcon("gtk-about", 16);
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.Modal = true;
            this.Resizable = false;
            this.AllowGrow = false;
            this.HasSeparator = false;
            // Internal child MonoClient.About.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Events = ((Gdk.EventMask)(256));
            w1.Name = "dialog_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog_VBox.Gtk.Box+BoxChild
            this.image15 = new Gtk.Image();
            this.image15.Name = "image15";
            this.image15.Pixbuf = Gdk.Pixbuf.LoadFromResource("AboutBox.svg");
            w1.Add(this.image15);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(w1[this.image15]));
            w2.Position = 0;
            // Internal child MonoClient.About.ActionArea
            Gtk.HButtonBox w3 = this.ActionArea;
            w3.Events = ((Gdk.EventMask)(256));
            w3.Name = "MonoClient.About_ActionArea";
            w3.Spacing = 6;
            w3.BorderWidth = ((uint)(5));
            w3.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child MonoClient.About_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.CloseButton = new Gtk.Button();
            this.CloseButton.CanDefault = true;
            this.CloseButton.CanFocus = true;
            this.CloseButton.Name = "CloseButton";
            // Container child CloseButton.Gtk.Container+ContainerChild
            Gtk.Alignment w4 = new Gtk.Alignment(0.5F, 0.5F, 0F, 0F);
            w4.Name = "GtkAlignment";
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            Gtk.HBox w5 = new Gtk.HBox();
            w5.Name = "GtkHBox";
            w5.Spacing = 2;
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Image w6 = new Gtk.Image();
            w6.Name = "image1";
            w6.Pixbuf = Stetic.IconLoader.LoadIcon("gtk-close", 16);
            w5.Add(w6);
            // Container child GtkHBox.Gtk.Container+ContainerChild
            Gtk.Label w8 = new Gtk.Label();
            w8.Name = "GtkLabel";
            w8.LabelProp = Mono.Unix.Catalog.GetString("Close");
            w5.Add(w8);
            w4.Add(w5);
            this.CloseButton.Add(w4);
            this.AddActionWidget(this.CloseButton, 0);
            Gtk.ButtonBox.ButtonBoxChild w12 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.CloseButton]));
            w12.Expand = false;
            w12.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 264;
            this.DefaultHeight = 322;
            this.Show();
            this.CloseButton.Clicked += new System.EventHandler(this.OnCloseButtonClicked);
        }
    }
}
