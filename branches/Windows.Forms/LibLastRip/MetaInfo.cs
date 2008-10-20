// LibLastRip - A Last.FM ripping library for TheLastRipper
// Copyright (C) 2007  Jop... (Jonas F. Jensen).
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Xml;
using System.Net;
using System.IO;

namespace LibLastRip
{
	public class MetaInfo : System.EventArgs, IMetaMusic
	{
		protected System.String _Station;
		protected System.String _Artist;
		protected System.String _Track;
		protected uint _TrackNum = 0;
		protected System.String _Album;
		protected System.String _Albumcover;
		protected System.String _Trackduration;
		protected System.String _Genre;
		protected System.String _Comment;
		protected System.String _Trackprogress;
		
		/// <summary>
        /// Get an empty instance of MetaInfo, used to represent no song.
        /// </summary>
        public static MetaInfo GetEmptyMetaInfo()
        {
            return new MetaInfo();
        }
       
        /// <summary>
        /// Creates an instance of metaInfo representing no song.
        /// </summary>
        protected MetaInfo()
        {
            this._Track = "Refreshing...";
            this._Artist = "";
            this._Album = "";
            this._TrackNum = 0;
            this._Trackduration = "1";
            this._Genre = "";
            this._Trackprogress = "0";
        }
		
		internal MetaInfo(XSPF xspf, XSPFTrack xspfTrack)
		{
			this._Album = xspfTrack.Album;
			this._Artist = xspfTrack.Creator;
			this._Trackduration = (Int32.Parse(xspfTrack.Duration) / 1000).ToString();
			this._Track = xspfTrack.Title;
			this._Albumcover = xspfTrack.Image;
			System.String station = xspf.Title.Replace("+"," ");
			this._Station = System.Uri.UnescapeDataString(station);
			
			//We've got to have something to write as ID3tag's, filename and directories
			if(String.IsNullOrEmpty(this._Track))
				this._Track = "unknown";
			if(String.IsNullOrEmpty(this._Album))
				this._Album = "unknown";
			if(String.IsNullOrEmpty(this._Artist))
				this._Artist = "unknown";
				_TrackNum = SearchTrackNum();
				_Genre = SearchTrackGenre();
		}
		
		private uint SearchTrackNum(){
			try{
				System.String XmlPath = "http://ws.audioscrobbler.com/1.0/album/";
				XmlPath += _Artist + "/" + _Album + "/info.xml";
				XmlDocument xmlDocument = LastManager.getXmlDocument(XmlPath);
				XmlNodeList nl = xmlDocument.GetElementsByTagName("track");
				uint tnum = 0;
				foreach(XmlNode node in nl){
					tnum++;
					System.String title = node.Attributes.GetNamedItem("title").InnerText;
					if(title.ToLower().Equals(_Track.ToLower()))
						return tnum;
				}
				return 0;
			}catch(Exception e){
				return 0;
			}
		}
		
		private System.String SearchTrackGenre(){
			try{
				System.String XmlPath = "http://ws.audioscrobbler.com/1.0/track/";
				XmlPath += this._Artist + "/" + this._Track + "/toptags.xml";
				XmlDocument xmlDocument = LastManager.getXmlDocument(XmlPath);
				XmlNodeList nl = xmlDocument.GetElementsByTagName("tag");
				if(nl.Count > 0){
					nl = nl.Item(0).ChildNodes;
					foreach(XmlNode node in nl)
						if(node.Name.Equals("name"))
							return node.InnerText;
				}
				return "";
			}catch(Exception e){
				return "";
			}
		}
		
		public System.String Station
		{
			get
			{
				return this._Station;
			}
		}
		
		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}
		
		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}
		
		public uint TrackNum
		{
			set{
				this._TrackNum = value;
			}
			get
			{
				return this._TrackNum;
			}
		}
		
		public System.String Album
		{
			get
			{
				return this._Album;
			}
		}
		
		public System.String Albumcover
		{
			get
			{
				return this._Albumcover;
			}
		}
				
		public System.String Trackduration
		{
			get
			{
				return this._Trackduration;
			}
		}
		
		public System.String Genre
		{
			set{
				this._Genre = value;
			}
			get
			{
				return this._Genre;
			}
		}

		public System.String Comment
		{
			set{
				this._Comment = value;
			}
			get
			{
				return this._Comment;
			}
		}

		public System.String Trackprogress
		{
			get
			{
				return this._Trackprogress;
			}
		}
		
		public override System.String ToString()
		{
			System.String OutStr = "";
			
			OutStr += "Track: " + this._Artist + " - " + this._Album + " - " + this._Track + "\n";
			OutStr += "From: " + this._Station + "\n";
			OutStr += "Duration: " + this._Trackduration;
			return OutStr;
		}
		
		public override bool Equals(object obj)
        {
            return MetaInfo.Equals(obj,this);
        }
        public static bool operator == (MetaInfo Obj1, MetaInfo Obj2)
        {
            return MetaInfo.Equals(Obj1,Obj2);
        }
        public static bool operator != (MetaInfo Obj1, MetaInfo Obj2)
        {
            return !(Obj1 == Obj2);
        }
		
		public override System.Int32 GetHashCode()
		{
			return (this._Album + this._Artist + this._Track).GetHashCode();
		}
		new public static System.Boolean Equals(System.Object Obj1, System.Object Obj2)
		{
			if(Obj1 == null ||Obj2 == null)
				return false;
			if(Obj1.GetType()==typeof(MetaInfo)&&Obj2.GetType()==typeof(MetaInfo))
			{
				if(((MetaInfo)Obj1).GetHashCode()==((MetaInfo)Obj2).GetHashCode())
				{
					return true;
				}
			}
			return false;
		}

		///<summary>
		///Appends ID3tag to a file
		///</summary>
		///<param name="Stream">A Stream representation of the file, which ID3tags should be appended</param>
		public void AppendID3(System.IO.FileStream Stream)
		{
			StreamAbstraction SA = new StreamAbstraction(Stream);
			this.AppendID3(SA);
		}
		
		///<summary>
		///Appends ID3tag to a file
		///</summary>
		///<param name="Path">Path of the file, which ID3tags should be appended</param>
		public void AppendID3(System.String Path)
		{
			TagLib.File.LocalFileAbstraction FA = new TagLib.File.LocalFileAbstraction(Path);
			this.AppendID3(FA);
		}
		
		///<summary>
		///Appends ID3tag to an IFileAbstraction
		///</summary>
		protected void AppendID3(TagLib.File.IFileAbstraction IFile)
		{
			//Create TagLib file from file abstraction 
			TagLib.File File = TagLib.File.Create(IFile);
			
			//Create id3v2 tags by trying to read them, with permission to create if none existing
			File.GetTag(TagLib.TagTypes.Id3v2, true);

			//Set track title
			File.Tag.Title = this.Track;
			if(this.TrackNum > 0)
				File.Tag.Track = this.TrackNum;
			
			//Set Artists
			File.Tag.Performers =  new System.String[]{this.Artist};
			File.Tag.AlbumArtists = new System.String[]{this.Artist};
			
			//Set album
			if(!this.Album.Equals("unknown"))
				File.Tag.Album = this.Album;
			
			if(!String.IsNullOrEmpty(this.Genre))
				File.Tag.Genres = new System.String[]{this.Genre};
			//Add comment, defining from where the music was recorded.
			//Modify: personal message
			if(!String.IsNullOrEmpty(this.Comment))
				File.Tag.Comment = LastManager.ReplacePattern(this.Comment,this);

			//TODO: Add picture using TagLib-Sharp
			//TagLib.Picture.CreateFromFile()
			
			//Saves the Current file
			File.Save();
		}
		
		public bool isEmpty() {
			return MetaInfo.GetEmptyMetaInfo().Equals(this);
		}
	}
}
