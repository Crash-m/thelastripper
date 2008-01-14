/*
 * Created by SharpDevelop.
 * User: q
 * Date: 15-08-2007
 * Time: 21:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace LibLastRip
{
	/// <summary>
	/// EventArgs for an LastManager.OnError
	/// </summary>
	public class ErrorEventArgs : System.EventArgs
	{
		protected System.Exception e = null;
		protected System.String _Message = "";
		
		/// <summary>
		/// Creates an instance of ErrorEventArgs with an error message.
		/// </summary>
		/// <param name="Message">A human readable message for the user</param>
		internal ErrorEventArgs(System.String Message)
		{
			this._Message = Message;
		}
		
		/// <summary>
		/// Creates an instance of ErrorEventArgs with an error message and a techical exception.
		/// </summary>
		/// <param name="Message">A human readable message for the user</param
		/// <param name="e">Exception that have occured.</param>
		internal ErrorEventArgs(System.String Message, System.Exception e)
		{
			this._Message = Message;
			this.e = e;
		}
		
		/// <summary>
		/// A System.Exception that have occured.
		/// </summary>
		/// <remarks>This property may return null if no exception occured.</remarks>
		public System.Exception Exception
		{
			get{
				return this.e;
			}
		}
		
		/// <summary>
		/// A human readable message that can be shown to the user.
		/// </summary>
		public System.String Message
		{
			get{
				return this._Message;
			}
		}
	}
}
