/*
 * Erstellt mit SharpDevelop.
 * Benutzer: alangman
 * Datum: 28.08.2007
 * Zeit: 14:06
 * 
 * Sie k�nnen diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader �ndern.
 */
using System;
using System.Threading;

namespace ConsoleClient
{
	class Program
	{
		public static string PAR_MUSICPATH = "-musicpath=";
		public static string PAR_USERNAME = "-username=";
		public static string PAR_PASSWORD = "-password=";
		public static string PAR_PROXYADDRESS = "-proxyaddress=";
		public static string PAR_PROXYUSERNAME = "-proxyusername=";
		public static string PAR_PROXYPASSWORD = "-proxypassword=";
		public static string PAR_RADIOSTATION = "-radiostation=";
		
		protected libLastRip.LastManager Manager;
		protected System.String musicPath;
		protected System.String userName;
		protected System.String password;
		
		protected System.String proxyAddress;
		protected System.String proxyUsername;
		protected System.String proxyPassword;
		
		protected System.String radioStation;
		
		protected bool working = true;
		protected long timeout;

		public static void Main(string[] args)
		{
			Program program = new Program();
			Console.WriteLine("Welcome to LibLastRip Console Client");
			
			for (int i = 0; i < args.Length; i++) {
				if (args[i].ToLower().StartsWith(PAR_MUSICPATH)) {
					program.musicPath = args[i].Substring(PAR_MUSICPATH.Length);
					Console.WriteLine("set musicPath to " + program.musicPath);
				}
				if (args[i].ToLower().StartsWith(PAR_USERNAME)) {
					program.userName = args[i].Substring(PAR_USERNAME.Length);
					Console.WriteLine("set userName to " + program.userName);
				}
				if (args[i].ToLower().StartsWith(PAR_PASSWORD)) {
					program.password = args[i].Substring(PAR_PASSWORD.Length);
					Console.WriteLine("set password to " + program.password);
				}
				if (args[i].ToLower().StartsWith(PAR_PROXYADDRESS)) {
					program.proxyAddress = args[i].Substring(PAR_PROXYADDRESS.Length);
					Console.WriteLine("set proxyAddress to " + program.proxyAddress);
				}
				if (args[i].ToLower().StartsWith(PAR_PROXYUSERNAME)) {
					program.proxyUsername = args[i].Substring(PAR_PROXYUSERNAME.Length);
					Console.WriteLine("set proxyUsername to " + program.proxyUsername);
				}
				if (args[i].ToLower().StartsWith(PAR_PROXYPASSWORD)) {
					program.proxyPassword = args[i].Substring(PAR_PROXYPASSWORD.Length);
					Console.WriteLine("set proxyPassword to " + program.proxyPassword);
				}
				if (args[i].ToLower().StartsWith(PAR_RADIOSTATION)) {
					program.radioStation = args[i].Substring(PAR_RADIOSTATION.Length);
					Console.WriteLine("set radioStation to " + program.radioStation);
				}
			}

			if (String.IsNullOrEmpty(program.password) || String.IsNullOrEmpty(program.userName)) {
				Console.WriteLine("usage info - not enought parameters to run client");
				Console.WriteLine("");
				Console.WriteLine("ConsoleClient");
				Console.WriteLine("(required parameters)");
				Console.WriteLine("-" + PAR_USERNAME +      "=lastfmUserName           (your lastfm login)");
				Console.WriteLine("-" + PAR_PASSWORD +      "=lastfmPassword           (your lastfm password)");
				Console.WriteLine("-" + PAR_MUSICPATH +     "=C:\\collection            (your target path)");
				Console.WriteLine("-" + PAR_RADIOSTATION +  "=lastfm://globaltags/rock (radiostation url)");
				Console.WriteLine("(optional parameters)");
				Console.WriteLine("-" + PAR_PROXYADDRESS +  "=squid:80                 [proxy address and port]");
				Console.WriteLine("-" + PAR_PROXYUSERNAME + "=prousr                   [proxy username]");
				Console.WriteLine("-" + PAR_PROXYPASSWORD + "=propass                  [proxy password]");
			} else {
				
				program.Manager = libLastRip.LastManager.Restore();
				
				//program.Manager = new libLastRip.LastManager(program.musicPath);
				program.Manager.NewSong += new libLastRip.LastManager.SongEventHandler(program.NewSong);
				//program.Manager.OnProgress += new EventHandler(program.OnProgress);
				
				//Subscribe to stations changed event
				//program.Manager.StationChanged += new EventHandler(program.TuneInCallback);
				
				//Subscribe to command callback
				//program.Manager.CommandReturn += new EventHandler(program.CommandCallback);
				
				//Subscribe to OnError
				program.Manager.Error += new libLastRip.LastManager.ErrorEventHandler(program.Error);
				
				//Subscribe to HandshakeReturn
				//program.Manager.HandshakeReturn += new EventHandler(program.LoginCallback);
				
				program.Manager.Passwordmd5 = program.Manager.GenerateHash(program.password);
				program.Manager.Username = program.userName;
				program.Manager.Login();
				
				while (program.working && program.Manager.Connected) {
					program.timeout++;
					Thread.Sleep(1000);
					if (program.timeout > 60) {
						Console.WriteLine("timeout for " + program.timeout.ToString() + "seconds");
						program.working = false;
					}
					
				}
			}}
		
		protected virtual void Error(System.Object sender, libLastRip.ErrorEventArgs args)
		{
			Console.WriteLine("}");
			Console.WriteLine("# ERROR #");
			this.working = false;
		}
		
		protected virtual void OnProgress(System.Object sender, System.EventArgs args)
		{
			Console.Write(".");
			timeout = 0;
		}
		
		protected virtual void NewSong(System.Object sender, libLastRip.SongEventArgs args)
		{
			Console.WriteLine("}");

			//libLastRip.MetaInfo Info = (libLastRip.MetaInfo)Args;

			//Get length of the track
			//System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(Info.Trackduration));
			//Console.WriteLine("TrackLength: " + TrackLength.ToString());
			
			Console.Write("{");
			timeout = 0;
		}

		void TuneInCallback(System.Object sender, System.EventArgs args)
		{
			Console.WriteLine("# TUNEINCALLBACK #");
			timeout = 0;
		}

		void CommandCallback(System.Object sender, System.EventArgs args)
		{
			Console.WriteLine("# COMMANDCALLBACK #");
			timeout = 0;
		}
		
		void LoginCallback(System.Object sender, System.EventArgs args)
		{
			Console.WriteLine("# LOGINCALLBACK #");
			this.Manager.AdjustStation(this.radioStation);
			timeout = 0;
		}

	}
}
