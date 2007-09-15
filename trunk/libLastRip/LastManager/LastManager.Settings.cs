/*
 * Created by SharpDevelop.
 * User: q
 * Date: 09-09-2007
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace libLastRip
{
	//Implement all settings related features of LastManager
	public partial class LastManager
	{
		/// <summary>
		/// Stores settings for the frontend
		/// </summary>
		/// <remarks>This object may NOT be used to store anything from within the backend, it's retained for the frontend completely</remarks>
		public System.Collections.Hashtable Settings;

		/// <summary>
		/// Stores a list of all stations that are known to work.
		/// </summary>
		/// <remarks>This list maybe extented as user successfully tunes in to new stations</remarks>
		public System.Collections.ArrayList Stations;

		//NOTE: The only method of creating an instance of LastManager should be through Restore()
		protected LastManager(/*Serialization parameters needed*/)
		{
			//Interact with Restore()
		}

		public static LastManager Restore()
		{
			//Restore settings from file using deserialization, and interact with the constructor somehow
			//NOTE: load default settings if no file is found, handle older files too!
			return new LastManager();
		}

		/// <summary>
		/// Saves settings to a file
		/// </summary>
		public virtual void Save()
		{
			//this should serialize all data needed to restore this class again
		}
	}
}
