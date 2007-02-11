
using System;

namespace MonoClient
{
	
	public class About : Gtk.Dialog
	{
		public About()
		{
			Stetic.Gui.Build(this, typeof(MonoClient.About));
		}


		protected virtual void OnCloseButtonClicked(object sender, System.EventArgs e)
		{
			this.Destroy();
		}
	}
}
