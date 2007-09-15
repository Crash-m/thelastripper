using System;
using System.Xml;
using System.Collections.Generic;

namespace nSpiff
{
	//TODO: Document this class and all members
	public class Playlist : System.Collections.Generic.Queue<Track>
	{
		protected Playlist()
		{
			//Used by derivative
			//TODO: add some default values
		}
		
		public Playlist(System.String Data)
		{
			System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
			XmlDoc.LoadXml(Data);
			this._Data = XmlDoc;
			this.ParseXml(this._Data);
		}
		
		public Playlist(System.Xml.XmlNode Data)
		{
			this._Data = Data;
			this.ParseXml(this._Data);
		}
		
		protected System.String _Title;
		protected System.String _Creator;
		protected System.Int32 _Version;
		protected System.Xml.XmlNode _Data;

		public virtual System.String Title
		{
			get
			{
				return this._Title;
			}
		}

		public virtual System.String Creator
		{
			get
			{
				return this._Creator;
			}
		}

		public virtual System.Int32 Version
		{
			get
			{
				return this._Version;
			}
		}

		protected virtual void ParseXml(System.Xml.XmlNode Xml)
		{
			//TODO: Loop through the xml and populate this instance with tracks
		}
	}
}
