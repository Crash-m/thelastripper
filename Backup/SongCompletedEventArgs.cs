/*
 * Created by SharpDevelop.
 * User: q
 * Date: 21-02-2009
 * Time: 19:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace LibLastRip
{
	/// <summary>
	/// Representation of event arguments for a song that's been recorded
	/// </summary>
	public class SongCompletedEventArgs : System.EventArgs
	{
		/// <summary>
		/// Create instance of SongCompletedEventArgs.
		/// </summary>
		/// <param name="SongInfo">Info about the song that got recorded</param>
		/// <param name="Filename">Filename of the songs that got recorded</param>
		public SongCompletedEventArgs(MetaInfo SongInfo, String Filename)
		{
			this._Filename = Filename;
			this._SongInfo = SongInfo;
		}
		
		private MetaInfo _SongInfo;
		private String _Filename;
		
		/// <summary>
		/// Filename of the song that got recorded
		/// </summary>
		public string Filename {
			get{
				return _Filename;
			}
		}
		
		/// <summary>
		/// metadata about recorded song
		/// </summary>
		public MetaInfo SongInfo {
			get{
				return _SongInfo;
			}
		}
		
	}
}
