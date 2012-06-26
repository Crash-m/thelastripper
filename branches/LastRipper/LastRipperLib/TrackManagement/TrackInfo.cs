using System;

namespace LastRipperLib
{
	public class TrackInfo
	{
		protected System.String _trackauth;
		protected System.String _albumId;
		protected System.String _artistId;

		/// <summary>
        /// Get an empty instance of TrackInfo, used to represent no song.
        /// </summary>
        public static TrackInfo GetEmptyTrackInfo()
        {
            return new TrackInfo();
        }
       
        /// <summary>
        /// Creates an instance of LastFm.
        /// </summary>
        protected TrackInfo()
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
}

