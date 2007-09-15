using System;
using System.Xml;
using System.Collections.Generic;

namespace nScrobbler
{
	//TODO: Document this class and all members
	public class nScrobbler : nSpiff.Playlist
	{
		protected System.Boolean _Connected;

		public virtual System.Boolean Connected
		{
			get
			{
				return this._Connected;
			}
		}

		public virtual void AdjustStation(System.String Station)
		{
			//TODO: Initiate Adjustment command
		}

		public static System.String GenerateHash(System.String Password)
		{
			/*
			Inspired by Last-Exit-4 another GNU GPL licensed Last.FM client
			*/
			System.Security.Cryptography.MD5 Hasher = System.Security.Cryptography.MD5.Create ();
			byte[] Hash = Hasher.ComputeHash (System.Text.Encoding.Default.GetBytes (Password));
			System.Text.StringBuilder StrHash = new System.Text.StringBuilder();
				
			for (int i = 0; i < Hash.Length; ++i)
			{
				StrHash.Append (Hash[i].ToString ("x2"));
			}
				
			return StrHash.ToString ();
		}

		public virtual void Login(System.String Username, System.String passwordmd5)
		{
			//TODO: Initiate login command
		}

		public virtual void RequestPlaylist()
		{
			//TODO: Initiate playlist request
		}

		public delegate void CommandReturnEventHandler(System.Object Sender, CommandEventArgs Args);
		public virtual event CommandReturnEventHandler CommandReturn;

		public delegate void NewTracksEventHandler(System.Object Sender, nSpiff.Playlist Tracks);
		public virtual event NewTracksEventHandler NewTracks;

		public virtual void Love()
		{
			//TODO: Initiate love command
		}

		public virtual void Hate()
		{
			//TODO: Initiate hate command
		}
	}
}
