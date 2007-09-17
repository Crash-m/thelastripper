using System;
using System.Xml;
using System.Collections.Generic;

namespace nSpiff
{
	/// <summary>
	/// A class to representate tracks in an XSPF file, used by Playlist.
	/// </summary>
	public class Track
	{
		//TODO: Add support for creating tracks too, must be done with nSpiff.Playlist too
		//TODO: Extent the amount of properties to support everything defined by the specification at http://xspf.org
				//Currently it only implements what needed to use Last.fm's services
		
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
		/// <param name="xml">The XmlNode with xpath playlist/tracklist/track of an XSPF.</param>
		protected void ParseXml(XmlNode xml)
		{
			//Set the different fields
			this._Title = xml.SelectSingleNode("title").InnerText;
			this._Location = xml.SelectSingleNode("location").InnerText;
			this._Image = xml.SelectSingleNode("image").InnerText;
			this._Artist = xml.SelectSingleNode("creatir").InnerText;
			this._Album = xml.SelectSingleNode("album").InnerText;
			
			//Get the duration and try to convert it
			System.String duration = xml.SelectSingleNode("duration").InnerText;
			if(duration != null)
			{
				//Catch different exceptions, since these aren't worth dying for!
				try
				{
					this._Duration = System.Convert.ToInt32(duration);
				}
				//Bad format don't kill the app
				catch(System.FormatException)
				{
					//Do nothing, just a bad value
				}
				//Bad big duration (probably bad format or server error) don't kill the app
				catch(System.OverflowException)
				{
					//Do nothing, just a big value :)
					//Seriously nothing cry about
				}
			}
		}
		
		/// <summary>
		/// Gets the title of the track, returns null of no title is available.
		/// </summary>
		public virtual System.String Title
		{
			get
			{
				return this._Title;
			}
		}

		/// <summary>
		/// Gets the album of the track, returns null of no album is available.
		/// </summary>
		public virtual System.String Album
		{
			get
			{
				return this._Album;
			}
		}

		/// <summary>
		/// Gets the location of the track, returns null of no location is available.
		/// </summary>
		public virtual System.String Location
		{
			get
			{
				return this._Location;
			}
		}

		/// <summary>
		/// Gets the artist of the track, returns null of no artist is available.
		/// </summary>
		public virtual System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}

		/// <summary>
		/// Gets the image location of the track, returns null of no image location is available.
		/// </summary>
		public virtual System.String Image
		{
			get
			{
				return this._Image;
			}
		}

		/// <summary>
		/// Gets the duration of the track in milliseconds, returns null of no duration is available.
		/// </summary>
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

		/// <summary>
		/// Compares this object to another object.
		/// </summary>
		/// <remarks>This method uses GetHashCode(), and therefore only compares Title, Album and Artist.</remarks>
		/// <param name="obj">Objec to compare with this</param>
		/// <returns>Result of the comparation, true if they are the same.</returns>
		public override bool Equals(object obj)
		{
			return obj != null && this.GetHashCode() == obj.GetHashCode();
		}

		/// <summary>
		/// Generates a unique hashcode for this track, based on Title, Album and Artist. 
		/// </summary>
		/// <remarks>This hashcode doesn't care about duration, image, location etc. only Title, Album and Artist matters.</remarks>
		/// <returns>A unique hashcode</returns>
		public override int GetHashCode()
		{
			//When comparing tracks use only Title, Album and Artist.
			return this.ToString().GetHashCode();
		}
	}
}
