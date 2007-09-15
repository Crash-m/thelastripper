using System;
using System.Xml;
using System.Collections.Generic;

namespace libLastRip
{
	//TODO: Document this class and all members
	public partial class LastManager //TODO: implement ISerialization
	{
		protected System.String _Passwordmd5;
		protected System.String _Username;
		protected System.Boolean _Recording;
		protected System.String _MusicPath;
		
		/// <summary>
		/// Holds the currently recording track
		/// </summary>
		protected nSpiff.Track _Track;
		
		/// <summary>
		/// Memory stream to save the song to, while it's being recorded
		/// </summary>
		protected System.IO.MemoryStream _SongData;
		
		protected nScrobbler.nScrobbler Scrobbler = new nScrobbler.nScrobbler();

		public virtual nSpiff.Track Track
		{
			get
			{
				return this._Track;
			}
		}
		
		public virtual System.String Passwordmd5
		{
			get
			{
				return this._Passwordmd5;
			}
			set
			{
				this._Passwordmd5 = value;
			}
		}

		public virtual System.String Username
		{
			get
			{
				return this._Username;
			}
			set
			{
				this._Username = value;
			}
		}

		/// <summary>
		/// Perform a login
		/// </summary>
		/// <remarks>If successfull this will change this.Connected to true</remarks>
		public virtual void Login()
		{
			//Parse this.Username and this.Passwordmd5 on to this.Scrobbler.Login(...)
			this.Scrobbler.Login(this._Username, this._Passwordmd5);
		}

		/// <summary>
		/// Are we recording?
		/// </summary>
		public virtual System.Boolean Recording
		{
			get
			{
				return this._Recording;
			}
		}

		public virtual System.String MusicPath
		{
			get
			{
				return this._MusicPath;
			}
			set
			{
				//TODO: Test if it exists and throw an error event if not!
				this._MusicPath = value;
			}
		}
		
		public delegate void SongEventHandler(System.Object Sender, SongEventArgs Args);
		public virtual event SongEventHandler NewSong;
		
		public delegate void ErrorEventHandler(System.Object Sender, ErrorEventArgs Args);
		public virtual event ErrorEventHandler Error;
		
		protected virtual void OnCommandReturn(System.Object Sender, nScrobbler.CommandEventArgs Args)
		{
			if(Args.Type == nScrobbler.CommandType.StationAdjustment && Args.Success)		
			{
				//TODO: initiate recording
			}
			
			if(this.CommandReturn != null)
				this.CommandReturn(this, Args);
		}

		/// <summary>
		/// URL of the currently playing station
		/// </summary>
		public virtual System.String StationURL
		{
			get
			{
				//TODO: implement this
				return "None";
			}
		}

		/// <summary>
		/// Name of the currently playing station
		/// </summary>
		public virtual System.String Station
		{
			get
			{
				//TODO: implement this
				return "None";
			}
		}

		public event nScrobbler.nScrobbler.CommandReturnEventHandler CommandReturn;

		/// <summary>
		/// Changes station and starts recording
		/// </summary>
		/// <param name="Station">URL of the station to record</param>
		public void AdjustStation(System.String Station)
		{
			//Warp around this.Scrobbler.AdjustStation(Station) and start recording once it returns, needs threading !
		}

		public void Love()
		{
			this.Scrobbler.Love();
		}

		public void Hate()
		{
			this.Scrobbler.Hate();
			this.Skip();
		}
		
		public void Skip()
		{
			//TODO: Skip current song
		}

		/// <summary>
		/// Is this instance connected?
		/// </summary>
		public System.Boolean Connected
		{
			get
			{
				return this.Scrobbler.Connected;
			}
		}

		public System.String GenerateHash(System.String Password)
		{
			return nScrobbler.nScrobbler.GenerateHash(Password);
		}
	}
}
