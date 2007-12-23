/*
 * Created by SharpDevelop.
 * User: q
 * Date: 13-10-2007
 * Time: 12:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace nScrobbler
{
	/// <summary>
	/// Command event arguments used whenever a login command is returned.
	/// </summary>
	public class LoginCommandEventArgs : CommandEventArgs
	{
		/// <summary>
		/// Is the user a subscriber?
		/// </summary>
		private System.Boolean _Subscriber = false;
		
		/// <summary>
		/// Gets whether or not the user is a subscriber
		/// </summary>
		public System.Boolean Subscriber
		{
			get{
				return this._Subscriber;
			}
		}
		
		/// <summary>
		/// Create a new instance of LoginCommandEventArgs
		/// </summary>
		/// <param name="Success">Indication of whether or not the request was succesfull</param>
		/// <param name="Subscriber">Indication of whether or not the user was a subscriber</param>
		public LoginCommandEventArgs(System.Boolean Success, System.Boolean Subscriber) : base(Success, CommandType.Login)
		{
			this._Subscriber = Subscriber;
		}
		
		/// <summary>
		/// Create a new instance of LoginCommandEventArgs
		/// </summary>
		/// <param name="Success">Indication of whether or not the request was succesfull</param>
		/// <param name="Subscriber">Indication of whether or not the user was a subscriber</param>
		/// <param name="Exception">Exception that occured during command request/execution.</param>
		public LoginCommandEventArgs(System.Boolean Success, System.Boolean Subscriber, System.Exception e) : base(Success, CommandType.Login, e)
		{
			this._Subscriber = Subscriber;
		}
	}
}
