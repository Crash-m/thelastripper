using System;
using System.Xml;
using System.Collections.Generic;

namespace nSpiff
{
	/// <summary>
	/// A class for reading Xml Shareable Playlist Format files.
	/// </summary>
	public class Playlist : System.Collections.Generic.Queue<Track>
	{
		//TODO: Implement support for writing XSPF files
		
		/// <summary>
		/// Create an instance of Playlist without initialization, only used by derivatives
		/// </summary>
		protected Playlist()
		{
			//Used by derivative
		}
		
		/// <summary>
		/// Creates an instance of playlist populated by the XmlData
		/// </summary>
		/// <param name="XmlData">Xml Shareable Playlist Format</param>
		public Playlist(System.String XmlData)
		{
			XmlDocument XmlDoc = new XmlDocument();
			XmlDoc.LoadXml(XmlData);
			this._Data = XmlDoc;
			this.ParseXml(this._Data);
		}
		
		/// <summary>
		/// Create an instance of playlist populated by the given XmlNode
		/// </summary>
		/// <param name="Data">Root XmlNode of an XSPF</param>
		public Playlist(XmlNode Data)
		{
			this._Data = Data;
			this.ParseXml(this._Data);
		}
		
		//Complying with FxCop so these can't be protected
		private System.String __Title;
		private System.String __Creator;
		private System.Int32 __Version;
		private XmlNode __Data;
		
		/// <summary>
		/// Holds XmlData, null if none
		/// </summary>
		protected XmlNode _Data
		{
			get
			{
				return this.__Data;
			}
			set
			{
				this.__Data = value;
			}
		}
		
		/// <summary>
		/// Holds version number, null if none
		/// </summary>
		protected System.Int32 _Version
		{
			get
			{
				return this.__Version;
			}
			set
			{
				this.__Version = value;
			}
		}
		
		/// <summary>
		/// Holds creator, null if none
		/// </summary>
		protected System.String _Creator
		{
			get
			{
				return this.__Creator;
			}
			set
			{
				this.__Creator = value;
			}
		}
		
		/// <summary>
		/// Holds title, null if none
		/// </summary>
		protected System.String _Title
		{
			get
			{
				return this.__Title;
			}
			set
			{
				this.__Title = value;
			}
		}

		/// <summary>
		/// Gets the title of the playlist, null if no title is provided.
		/// </summary>
		public virtual System.String Title
		{
			get
			{
				return this._Title;
			}
		}

		/// <summary>
		/// Gets the creator of the playlist, null if no creator is provided.
		/// </summary>
		public virtual System.String Creator
		{
			get
			{
				return this._Creator;
			}
		}

		/// <summary>
		/// Gets the version of the playlist, null if no version is provided.
		/// </summary>
		public virtual System.Int32 Version
		{
			get
			{
				return this._Version;
			}
		}

		/// <summary>
		/// Populates this playlist with data from an XmlNode
		/// </summary>
		/// <param name="xml">Root XmlNode of an XSPF</param>
		protected void ParseXml(XmlNode xml)
		{
			//Get Creator, Title and Version
			this._Creator = xml.SelectSingleNode("playlist/creator").InnerText;
			this._Title = xml.SelectSingleNode("playlist/title").InnerText;
			
			//Get the version and try to convert it
			System.String Version = xml.SelectSingleNode("playlist").Attributes.GetNamedItem("version").InnerText;
			if(Version != null)
			{
				//Catch different exceptions, since these aren't worth dying for!
				try
				{
					this._Version = System.Convert.ToInt32(Version);
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
			
			//Loop through the tracks an enqueue them
			foreach(XmlNode xmlTrack in xml.SelectNodes("playlist/trackList/track"))
			{
				this.Enqueue(new Track(xmlTrack));
			}
		}
	}
}
