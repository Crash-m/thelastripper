using System;
using System.Collections;

namespace LastRipperLib
{
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

