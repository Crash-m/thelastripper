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
		/// <summary>
		/// Performs a login with the provided username and password, result is returned as event.
		/// </summary>
		/// <param name="Username">Username</param>
		/// <param name="PasswordMd5">MD5 sum of the password</param>
		public virtual void Login(System.String Username, System.String PasswordMd5)
		{
			try
			{
				//TODO: Autodetect platform!
				System.String platform = "linux";
				
				//Get a Uri
				System.String Uri = "http://ws.audioscrobbler.com/radio/handshake.php?version=1.3.1.1&platform=" + platform + "&username=" + Username + "&passwordmd5=" + PasswordMd5;
				
				this.HttpWebRequest(Uri, new nScrobbler.HttpWebCallback(this.ParseLogin), new nScrobbler.HttpExceptionCallback(this.LoginException));
			}
			catch(System.Exception e)
			{
				//Raise event if anybody is subscribted to it
				if(this.CommandReturn != null)
				{
					//Warp the exception nicely
					System.Exception Exception = new System.Exception("Exception while launching login request", e);
					//Return the command with the exception
					this.CommandReturn(this, new LoginCommandEventArgs(false, false, Exception));
				}	
			}
		}
		
		/// <summary>
		/// Parse login data from a request
		/// </summary>
		/// <param name="Data">Data from the request</param>
		protected virtual void ParseLogin(System.String Data)
		{
			try
			{
				//Did this request go well?
				System.Boolean Result = false;
				
				//Subscriber
				System.Boolean Subscriber = false;
				
				foreach(System.String Line in Data.Split(new System.Char[]{'\n'}))
				{
					System.String[] Opts = Line.Split(new System.Char[]{'='});
					
					//Skip this line if it's bad
					if(Opts.Length != 2)
						continue;
					
					switch(Opts[0].ToLower())
					{
						case "session":
							if(Opts[1].ToLower() == "failed")
							{
								Result = false;
							}else{
								Result = true;
								this.SessionID = Opts[1];
								this._Connected = true;
							}
							break;
						case "stream_url":
							//Ignore this attribute, it obsolete
							break;
						case "subscriber":
							if(Opts[1] == "1")
							{
								Subscriber = true;
							}else{
								Subscriber = false;
							}
							break;
						case "framehack":
							//Don't know what this is for, ignoring it
							break;
						case "base_url":
							this.BaseURL = Opts[1];
							break;
						case "base_path":
							this.BasePath = Opts[1];
							break;
						default:
							Console.WriteLine("nScrobbler.ParseLogin() Unknown key: " + Opts[0] + " Value: " + Opts[1]);
							break;
					}
				}
				
				//Raise event if anybody is subscribted to it
				if(this.CommandReturn != null)
				{
					CommandEventArgs Args = new LoginCommandEventArgs(Result, Subscriber);
					this.CommandReturn(this, Args);
				}
			}
			catch(System.Exception e)
			{				
				//Raise event if anybody is subscribted to it
				if(this.CommandReturn != null)
				{
					//Warp the exception nicely
					System.Exception Exception = new System.Exception("Exception while parsing login data", e);
					//Return the command with the exception
					this.CommandReturn(this, new LoginCommandEventArgs(false, false, Exception));
				}
			}
		}
		
		/// <summary>
		/// Notify subsribers of CommandReturn event that an exception occured while fetching login request.
		/// </summary>
		/// <param name="e">Exception that occured during download of login data.</param>
		protected virtual void LoginException(System.Exception e)
		{
			//Raise event if anybody is subscribted to it
			if(this.CommandReturn != null)
			{
				//Warp the exception nicely
				System.Exception Exception = new System.Exception("Exception while communication with server during login", e);
				//Return the command with the exception
				this.CommandReturn(this, new LoginCommandEventArgs(false, false, Exception));
			}
		}
	}
}
