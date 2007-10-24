/*
 * Created by SharpDevelop.
 * User: jopsen
 * Date: 13-08-2007
 * Time: 16:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace LibLastRip
{
	/// <summary>
	/// Event arguments for LastManager.Progress
	/// </summary>
	public class ProgressEventArgs : System.EventArgs {
		protected System.Int32 _Streamprogress;

		public ProgressEventArgs(System.Int32 param) {
			this._Streamprogress = param;
		}
		
		public System.Int32 Streamprogress
		{
			get
			{
				return this._Streamprogress;
			}
		}
	}
}
