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
using System.Collections;
	
namespace LibLastRip
{
	public class LastFm
	{
		protected System.String _trackauth;
		protected System.String _albumId;
		protected System.String _artistId;

		/// <summary>
        /// Get an empty instance of LastFm, used to represent no song.
        /// </summary>
        public static LastFm GetEmptyLastFm()
        {
            return new LastFm();
        }
       
        /// <summary>
        /// Creates an instance of LastFm.
        /// </summary>
        protected LastFm()
        {
        }

		public System.String Trackauth
		{
			get
			{
				return this._trackauth;
			}
			set
			{
				this._trackauth = value;
			}
		}

		public System.String AlbumId
		{
			get
			{
				return this._albumId;
			}
			set
			{
				this._albumId = value;
			}
		}

		public System.String ArtistId
		{
			get
			{
				return this._artistId;
			}
			set
			{
				this._artistId = value;
			}
		}
		
	}
	
	public class XSPFTrack
	{
		protected System.String _location;
		protected System.String _title;
		protected System.String _id;
		protected System.String _album;
		protected System.String _creator;
		protected System.String _duration;
		protected System.String _image;
		protected LastFm _lastfm;
		
		/// <summary>
        /// Get an empty instance of XSPF, used to represent no song.
        /// </summary>
        public static XSPFTrack GetEmptyXSPFTrack()
        {
            return new XSPFTrack();
        }
       
        /// <summary>
        /// Creates an instance of XSPF representing no song.
        /// </summary>
        protected XSPFTrack()
        {
        	this.LastFm = LastFm.GetEmptyLastFm();
        }

		public System.String Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._location = value;
			}
		}
		
		public System.String Title
		{
			get
			{
				if (String.IsNullOrEmpty(this._title)) {
					this._title = "unknown";
				}
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}
		
		public System.String Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public System.String Album
		{
			get
			{
				if (String.IsNullOrEmpty(this._album)) {
					this._album = "unknown";
				}
				return this._album;
			}
			set
			{
				this._album = value;
			}
		}
		
		public System.String Creator
		{
			get
			{
				if (String.IsNullOrEmpty(this._creator)) {
					this._creator = "unknown";
				}
				return this._creator;
			}
			set
			{
				this._creator = value;
			}
		}
			
		public System.String Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}
		
		public System.String Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
			}
		}
		
		public LastFm LastFm
		{
			get
			{
				return this._lastfm;
			}
			set
			{
				this._lastfm = value;
			}
		}
				
		public override System.String ToString()
		{
			System.String OutStr = "";
			
//	TODO		if(this._Streaming)
//			{
//				OutStr += "Track: " + this._Artist + " - " + this._Album + " - " + this._Track + "\n";
//				OutStr += "From: " + this._Station + "\n";
//				OutStr += "Duration: " + this._Trackduration;
//			}else{
//				OutStr = "Streaming: " + this._Streaming.ToString();
//			}
			return OutStr;
		}
		
		public override bool Equals(object obj)
        {
            return XSPF.Equals(obj,this);
        }
        public static bool operator == (XSPFTrack Obj1, XSPFTrack Obj2)
        {
            return XSPF.Equals(Obj1,Obj2);
        }
        public static bool operator != (XSPFTrack Obj1, XSPFTrack Obj2)
        {
            return !(Obj1 == Obj2);
        }
		
		public override System.Int32 GetHashCode()
		{
			return (this.Id).GetHashCode();
		}
		new public static System.Boolean Equals(System.Object Obj1, System.Object Obj2)
		{
			if(Obj1 == null ||Obj2 == null)
				return false;
			if(Obj1.GetType()==typeof(XSPF)&&Obj2.GetType()==typeof(XSPF))
			{
				if(((XSPF)Obj1).GetHashCode()==((XSPF)Obj2).GetHashCode())
				{
					return true;
				}
			}
			return false;
		}

	}

	public class XSPF
	{
		protected ArrayList _xspfList;
		protected System.String _title;
		protected System.String _creator;
		protected System.String _station;

		/// <summary>
        /// Get an empty instance of LastFm, used to represent no song.
        /// </summary>
        public static XSPF GetEmptyXSPF()
        {
            return new XSPF();
        }
       
        /// <summary>
        /// Creates an instance of LastFm.
        /// </summary>
        protected XSPF()
        {
        	this._xspfList = new ArrayList();
        }

		public System.String Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		public System.String Creator
		{
			get
			{
				return this._creator;
			}
			set
			{
				this._creator = value;
			}
		}		
		
		public System.String Station
		{
			get
			{
				return this._station;
			}
			set
			{
				this._station = value;
			}
		}		
		
		public void AddTrack(XSPFTrack track) {
			this._xspfList.Add(track);
		}

		public System.Int32 CountTracks() {
			return this._xspfList.Count;
		}
		
		public XSPFTrack getTrack() {
			XSPFTrack track = (XSPFTrack)this._xspfList.ToArray()[0];
			this._xspfList.RemoveAt(0);
			return track;
		}
	}
}

