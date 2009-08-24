
using System;

namespace MonoClient
{
	
	public partial class About : Gtk.Dialog
	{
		public About()
		{
			//Let Stetic build the GUI
			this.Build();
		}
		
		protected virtual void OnCloseButtonClicked(object sender, System.EventArgs e)
		{
			this.Destroy();
		}
	}
}
