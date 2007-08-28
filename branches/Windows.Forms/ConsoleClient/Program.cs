/*
 * Erstellt mit SharpDevelop.
 * Benutzer: alangman
 * Datum: 28.08.2007
 * Zeit: 14:06
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace ConsoleClient
{
	class Program	
	{		
		public static string PAR_MUSICPATH = "-musicpath=";
		public static string PAR_USERNAME = "-username=";
		public static string PAR_PASSWORD = "-password=";
		public static string PAR_PROXYADRESS = "-proxyadress=";
		public static string PAR_PROXYUSERNAME = "-proxyusername=";
		public static string PAR_PROXYPASSWORD = "-proxypassword=";
		public static string PAR_RADIOSTATION = "-radiostation=";
		
		protected LibLastRip.LastManager Manager;
		protected System.String musicPath;
		protected System.String userName;
		protected System.String password;
		
		protected System.String proxyAdress;
		protected System.String proxyUsername;
		protected System.String proxyPassword;
		
		protected System.String radioStation;
		
		protected bool working = true;

		public static void Main(string[] args)
		{
			Program program = new Program();
			Console.WriteLine("Welcome to LibLastRip Console Client");
			Console.WriteLine("usage info...");
			Console.WriteLine("ConsoleClient -musicpath=C:\\last.fm");
			
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
				if (args[i].ToLower().StartsWith(PAR_PROXYADRESS)) {
					program.proxyAdress = args[i].Substring(PAR_PROXYADRESS.Length);
					Console.WriteLine("set proxyAdress to " + program.proxyAdress);
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
			
			program.Manager = new LibLastRip.LastManager(program.musicPath);
			program.Manager.OnNewSong += new EventHandler(program.OnNewSong);
			program.Manager.OnProgress += new EventHandler(program.OnProgress);
			
			//Subscribe to stations changed event
			program.Manager.StationChanged += new EventHandler(program.TuneInCallback);
			
			//Subscribe to command callback
			program.Manager.CommandReturn += new EventHandler(program.CommandCallback);
			
			//Subscribe to OnError
			program.Manager.OnError += new EventHandler(program.OnError);
			
			//Subscribe to HandshakeReturn
			program.Manager.HandshakeReturn += new EventHandler(program.LoginCallback);
            
			program.Manager.Handshake(program.userName, LibLastRip.LastManager.CalculateHash(program.password));
						
			while (program.working) {
			} 
		}
		
				protected virtual void OnError(System.Object Sender, System.EventArgs e)
		{
			Console.WriteLine("}");
			Console.WriteLine("# ERROR #");
			this.working = false;
		}
		
		protected virtual void OnProgress(System.Object Sender, System.EventArgs Args)
		{
			Console.Write(".");
		}
		
		protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
		{
			Console.WriteLine("}");

			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;

			//Get length of the track
			System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(Info.Trackduration));
			Console.WriteLine("TrackLength: " + TrackLength.ToString());
			
			Console.Write("{");
		}

		void TuneInCallback(System.Object Sender, System.EventArgs e)
		{
			Console.WriteLine("# TUNEINCALLBACK #");
		}

		void CommandCallback(object Sender, EventArgs e)
		{
			Console.WriteLine("# COMMANDCALLBACK #");
		}
		
		void LoginCallback(System.Object Sender, System.EventArgs Args)
		{
			Console.WriteLine("# LOGINCALLBACK #");
			this.Manager.ChangeStation(this.radioStation);
		}

	}
}
