/*
 * Created by SharpDevelop.
 * User: q
 * Date: 21-09-2007
 * Time: 18:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace nScrobbler
{
	public partial class nScrobbler
	{
		public virtual void Login(System.String Username, System.String PasswordMd5)
		{
			//TODO: Autodetect platform!
			System.String platform = "linux";
			
			//Get a Uri
			System.String Uri = "http://ws.audioscrobbler.com/radio/handshake.php?version=1.3.1.1&platform=" + platform + "&username=" + Username + "&passwordmd5=" + PasswordMd5;
			
			this.HttpWebRequest(Uri, new nScrobbler.HttpWebCallback(this.ParseLogin));
		}
		
		protected virtual void ParseLogin(System.String Data)
		{
			foreach(System.String Line in Data.Split(new System.Char[]{'\n'}))
			{
				System.String[] Opts = Line.Split(new System.Char[]{'='});
				
				//Skip this line if it's bad
				if(Opts.Length != 2)
					continue;
				
				switch(Opts[0].ToLower())
				{
					case "session":
					//TODO: Finish this
				}
			}
		}
	}
}
