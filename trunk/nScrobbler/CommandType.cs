using System;
using System.Xml;
using System.Collections.Generic;

namespace nScrobbler
{
	/// <summary>
	/// Differnt types of commands
	/// </summary>
	public enum CommandType
	{
		/// <summary>
		/// A login/handshake command
		/// </summary>
		Login,
		
		/// <summary>
		/// A station adjustment command
		/// </summary>
		StationAdjustment,
		
		/// <summary>
		/// A love command
		/// </summary>
		Love,
		
		/// <summary>
		/// A hate command
		/// </summary>
		Hate
	}
}
