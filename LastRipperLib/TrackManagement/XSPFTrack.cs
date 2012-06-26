using System;
using System.Collections;

namespace LastRipperLib
{
	public class XSPFTrack
	{
		protected System.String _location;
		protected System.String _title;
		protected System.String _id;
		protected System.String _album;
		protected System.String _creator;
		protected System.String _duration;
		protected System.String _image;
		protected TrackInfo _trackInfo;
		
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
        	this._trackInfo = TrackInfo.GetEmptyTrackInfo();
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
		
		public TrackInfo TrackInformation
		{
			get
			{
				return this._trackInfo;
			}
			set
			{
				this._trackInfo = value;
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

}

