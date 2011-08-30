/*
 * Erstellt mit SharpDevelop.
 * Benutzer: alangman
 * Datum: 28.08.2007
 * Zeit: 14:06
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Threading;

namespace ConsoleClient
{
	class Program
	{
		// Login
		public static string PAR_USERNAME = "-username=";
		protected System.String userName;

		public static string PAR_PASSWORD = "-password=";
		protected System.String password;

		// Network
		public static string PAR_PROXYADDRESS = "-proxyaddress=";
		protected System.String proxyAddress = null;

		public static string PAR_PROXYUSERNAME = "-proxyusername=";
		protected System.String proxyUsername = null;
		
		public static string PAR_PROXYPASSWORD = "-proxypassword=";
		protected System.String proxyPassword = null;

		// Storage
		public static string PAR_MUSICPATH = "-musicpath=";
		protected System.String musicPath;

		// Advanced
		public static string PAR_EXCLUDEFILE = "-excludefile=";
		protected System.String excludefile = null;
		
		public static string PAR_QUARANTINE = "-quarantine=";
		protected System.String quarantine = null;

		public static string PAR_SKIPEXISTING = "-skipexisting=";
		protected System.Boolean skipexisting = false;

		public static string PAR_SKIPNEW = "-skipnew=";
		protected System.Boolean skipnew = false;

		// Rip Control
		public static string PAR_RADIOSTATION = "-radiostation=";
		protected System.String radioStation;
		
		protected LibLastRip.LastManager Manager;
		
		protected bool working = true;
		protected long timeout = 0;

		public static void Main(string[] args)
		{
			Program program = new Program();
			Console.WriteLine("Welcome to LibLastRip Console Client");
			
			for (int i = 0; i < args.Length; i++) {
				String argLower = args[i].ToLower();
				if (argLower.StartsWith(PAR_EXCLUDEFILE)) {
					program.excludefile = args[i].Substring(PAR_EXCLUDEFILE.Length);
					Console.WriteLine("set excludefile to " + program.excludefile);
				}
				if (argLower.StartsWith(PAR_QUARANTINE)) {
					program.quarantine = args[i].Substring(PAR_QUARANTINE.Length);
					Console.WriteLine("set quarantine to " + program.quarantine);
				}
				if (argLower.StartsWith(PAR_SKIPEXISTING)) {
					program.skipexisting = Boolean.Parse(args[i].Substring(PAR_SKIPEXISTING.Length));
					Console.WriteLine("set skipexisting to " + program.skipexisting);
				}
				if (argLower.StartsWith(PAR_SKIPNEW)) {
					program.skipnew = Boolean.Parse(args[i].Substring(PAR_SKIPNEW.Length));
					Console.WriteLine("set skipnew to " + program.skipnew);
				}
				if (argLower.StartsWith(PAR_MUSICPATH)) {
					program.musicPath = args[i].Substring(PAR_MUSICPATH.Length);
					Console.WriteLine("set musicPath to " + program.musicPath);
				}
				if (argLower.StartsWith(PAR_USERNAME)) {
					program.userName = args[i].Substring(PAR_USERNAME.Length);
					Console.WriteLine("set userName to " + program.userName);
				}
				if (argLower.StartsWith(PAR_PASSWORD)) {
					program.password = args[i].Substring(PAR_PASSWORD.Length);
					Console.WriteLine("set password to " + program.password);
				}
				if (argLower.StartsWith(PAR_PROXYADDRESS)) {
					program.proxyAddress = args[i].Substring(PAR_PROXYADDRESS.Length);
					Console.WriteLine("set proxyAddress to " + program.proxyAddress);
				}
				if (argLower.StartsWith(PAR_PROXYUSERNAME)) {
					program.proxyUsername = args[i].Substring(PAR_PROXYUSERNAME.Length);
					Console.WriteLine("set proxyUsername to " + program.proxyUsername);
				}
				if (argLower.StartsWith(PAR_PROXYPASSWORD)) {
					program.proxyPassword = args[i].Substring(PAR_PROXYPASSWORD.Length);
					Console.WriteLine("set proxyPassword to " + program.proxyPassword);
				}
				if (argLower.StartsWith(PAR_RADIOSTATION)) {
					program.radioStation = args[i].Substring(PAR_RADIOSTATION.Length);
					Console.WriteLine("set radioStation to " + program.radioStation);
				}
			}
			
			if (program.musicPath == null || program.userName == null || program.password == null || program.radioStation == null) {
				Console.WriteLine("Missing required parameters!");
				Console.WriteLine("");
				Console.WriteLine("ConsoleClient -username=hans -password=geheim -musicpath=c:/lfm/music -radiostation=lastfm://user/hans/playlist [OPTIONS]");
				Console.WriteLine("");
				Console.WriteLine("(Required)   Option Values - Description");
				Console.WriteLine("* username      [USERNAME]      - your last.fm username");
				Console.WriteLine("* password      [PASSWORD]      - your last.fm password");
				Console.WriteLine("* musicpath     c:/lfm/music    - your music collection directory");
				Console.WriteLine("* radiostation  lastfm://user/hans/playlist - your network proxy password");
				Console.WriteLine("  proxyaddress  adress:3128     - your network proxy");
				Console.WriteLine("  proxyusername top             - your network proxy user");
				Console.WriteLine("  proxypassword secret          - your network proxy password");
				Console.WriteLine("  excludefile   c:/lfm/skip.txt - file with artists to skip");
				Console.WriteLine("  quarantine    c:/lfm/new      - your quarantine directory for new files");
				Console.WriteLine("  skipexisting  [true,false]    - skip existing files");
				Console.WriteLine("  skipnew       [true,false]    - skip not existing artists (in music collection)");
			} else {
				
				program.Manager = new LibLastRip.LastManager(program.musicPath);
				program.Manager.ExcludeFile = program.excludefile;
				program.Manager.QuarantinePath = program.quarantine;
				program.Manager.ExcludeExistingMusic = program.skipexisting;
				program.Manager.ExcludeNewMusic = program.skipnew;
				
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
					program.timeout++;
					Thread.Sleep(1000);
					if (program.timeout > 60) {
						Console.WriteLine("timeout for " + program.timeout.ToString() + "seconds");
						program.working = false;
					}
				}
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
			timeout = 0;
		}
		
		protected virtual void OnNewSong(System.Object Sender, System.EventArgs Args)
		{
			Console.WriteLine("}");

			LibLastRip.MetaInfo Info = (LibLastRip.MetaInfo)Args;

			//Get length of the track
			System.Int32 TrackLength = System.Convert.ToInt32(System.Convert.ToInt32(Info.Trackduration));
			Console.WriteLine("TrackLength: " + TrackLength.ToString());
			
			Console.Write("{");
			timeout = 0;
		}

		void TuneInCallback(System.Object Sender, System.EventArgs e)
		{
			Console.WriteLine("# TUNEINCALLBACK #");
			timeout = 0;
		}

		void CommandCallback(object Sender, EventArgs e)
		{
			Console.WriteLine("# COMMANDCALLBACK #");
			timeout = 0;
		}
		
		void LoginCallback(System.Object Sender, System.EventArgs Args)
		{
			Console.WriteLine("# LOGINCALLBACK #");
			this.Manager.ChangeStation(this.radioStation);
			timeout = 0;
		}

	}
}
