/*
 * Created by SharpDevelop.
 * User: q
 * Date: 10-09-2007
 * Time: 16:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace libLastRip
{
	//TODO: Document this class and all members
	public class SongEventArgs : System.EventArgs
	{
		protected nSpiff.Track _Track;
		
		public nSpiff.Track Track {
			get { return _Track; }
		}
		public SongEventArgs(nSpiff.Track Track)
		{
			this._Track = Track;
		}
		
	}
}
