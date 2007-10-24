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
using System.IO;

namespace LibLastRip
{
	
	
	public class MetaMusic : IMetaMusic
	{
		
		public MetaMusic(System.String FileURL)
		{
			FileInfo MusicInfo = new FileInfo(FileURL);
			
			this._TrackDuration = (MusicInfo.Length / 128).ToString();
			this._Artist = MusicInfo.Directory.Parent.Name;
			this._Album = MusicInfo.Directory.Name;
			this._Track = MusicInfo.Name.Replace(".mp3","");
		}
		
		protected System.String _Track;
		protected System.String _Artist;
		protected System.String _TrackDuration;
		protected System.String _Album;
		
		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}
		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}
		public System.String Trackduration
		{
			get
			{
				return this._TrackDuration;
			}
		}
		public System.String Album
		{
			get
			{
				return this._Album;
			}
		}
		public override System.String ToString()
		{
			return this._Artist + " - " + this._Track;
		}
	}
	
	public class MetaTrack : IMetaTrack
	{
		public MetaTrack(System.String Track, System.String Artist)
		{
			this._Track = Track;
			this._Artist = Artist;
		}
		protected System.String _Track;
		protected System.String _Artist;
		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}
		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}
	}
}
