// project created on 21-01-2007 at 00:15
using System;
using Gtk;

namespace MonoClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}