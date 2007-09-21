/*
 * Created by SharpDevelop.
 * User: q
 * Date: 21-09-2007
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace nScrobbler
{
	public partial class nScrobbler
	{
		public virtual void RequestPlaylist()
		{
			//TODO: Initiate playlist request
		}
		
		public delegate void NewTracksEventHandler(System.Object Sender, nSpiff.Playlist Tracks);
		public virtual event NewTracksEventHandler NewTracks;
	}
}
