using System;
using System.Xml;
using System.Collections.Generic;

namespace nSpiff
{
	//TODO: Document this class and members
	public class Track
	{
		/// <summary>
		/// Creates an instance of Track
		/// </summary>
		/// <param name="Data">The XmlNode that holds this track</param>
		/// <remarks>This method is only called from nSpiff.Playlist</remarks>
		internal Track(System.Xml.XmlNode Data)
		{
			this._Data = Data;
			this.ParseXml(this._Data);
		}
		
		/// <summary>
		/// A reference to the XmlNode that holds this track
		/// </summary>
		protected System.Xml.XmlNode _Data;
		protected System.String _Title;
		protected System.String _Album;
		protected System.String _Location;
		protected System.String _Artist;
		protected System.String _Image;
		protected System.Int32 _Duration;

		/// <summary>
		/// Parses the Xml to the different fields on this instance of track
		/// </summary>
		/// <param name="Data">The XmlNode that holds this track</param>
		protected virtual void ParseXml(System.Xml.XmlNode Data)
		{
			//TODO: Parse XML
		}
		
		public virtual System.String Title
		{
			get
			{
				return this._Title;
			}
		}

		public virtual System.String Album
		{
			get
			{
				return this._Album;
			}
		}

		public virtual System.String Location
		{
			get
			{
				return this._Location;
			}
		}

		public virtual System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}

		public virtual System.String Image
		{
			get
			{
				return this._Image;
			}
		}

		public virtual System.Int32 Duration
		{
			get
			{
				return this._Duration;
			}
		}

		/// <summary>
		/// Gets a short string representation of title, album and artist
		/// </summary>
		/// <returns>String representation with title, album and artist</returns>
		public override string ToString()
		{
			return this.Title + " on " + this.Album + " by " + this.Artist;
		}

		public override bool Equals(object obj)
		{
			return obj != null && this.GetHashCode() == obj.GetHashCode();
		}

		public override int GetHashCode()
		{
			//TODO: improve this!
			return this.ToString().GetHashCode();
		}
	}
}
