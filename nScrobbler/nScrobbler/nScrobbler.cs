using System;
using System.Xml;
using System.Collections.Generic;
using System.Net;

namespace nScrobbler
{
	//TODO: Document this class and all members
	public partial class nScrobbler : nSpiff.Playlist
	{
		/// <summary>
		/// Is this instance of nScrobbler logged in?
		/// </summary>
		protected System.Boolean _Connected;

		/// <summary>
		/// Is this instance of nScrobbler logged in?
		/// </summary>
		public virtual System.Boolean Connected
		{
			get
			{
				return this._Connected;
			}
		}

		/// <summary>
		/// Generate a hashsum that can be used with Last.fm (MD5)
		/// </summary>
		/// <param name="Password">The string you want to compute the hash of</param>
		/// <returns>A hexadecimal md5sum string representation of the password.</returns>
		public static System.String GenerateHash(System.String Password)
		{
			//Compute the hash
			System.Byte[] ComputedHash = System.Security.Cryptography.MD5.Create().ComputeHash (System.Text.Encoding.Default.GetBytes(Password));
			
			
			//Convert to hexadecimal string
			System.Text.StringBuilder HashString = new System.Text.StringBuilder();
			for (System.Int32 i = 0; i < ComputedHash.Length; ++i)
			{
				HashString.Append (ComputedHash[i].ToString ("x2"));
			}
			
			//TODO: try using System.BitConverter, I've seen it done somewhere
				
			//Return a string
			return HashString.ToString();
		}
		
		/// <summary>
		/// Private helper method for creating an asynchronous HttpWebRequests, read the response and post it to a callback.
		/// </summary>
		/// <param name="Uri">Uniform Resource Identifier, HTTP-address of the wanted resource.</param>
		/// <param name="Callback">The callback to be called when the request completes.</param>
		/// <remarks>This method merly warps around HttpWebRequest and HttpWebResponse to avoid dublicate code and
		///  to make future implementation of proxy and different request methods easier. Currently it warps around
		///  some weird hack, needed to get internet access some places in the world.</remarks>
		private void HttpWebRequest(System.String Uri, HttpWebCallback Callback)
		{
			//Create a HttpWebRequest
			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
			
			//Weird hack to make sure this works in other parts of the world too			
			Request.Accept = "*/*";
			/*
			 * This is somehow needed because some routhers and/or internet connections requires a useragent.
			 * Don't know why exactly, but this solves the problem have a look at 72 issue for futher information:
			 * http://code.google.com/p/thelastripper/issues/detail?id=72
			 *  */
			
			//Start the asynchronous request
			Request.BeginGetResponse(new AsyncCallback(this.HttpWebResponse), new System.Object[]{Request, Callback});
		}
		
		/// <summary>
		/// Private helper method for handling HttpWebRequest callbacks from nScrobbler.HttpWebRequest
		/// </summary>
		/// <param name="Ar">An AsyncResult created by nScrobbler.HttpWebRequest</param>
		/// <remarks>The AsyncResult.AsyncState is a System.Object array, where the first entry is the request
		/// and secound entry is the nScrobbler.HttpWebCallback the data should be returned to.</remarks>
		private void HttpWebResponse(System.IAsyncResult Ar)
		{
			//Get the system object array from the AsyncState
			System.Object[] AsyncStates = (System.Object[])Ar.AsyncState;
			
			//Get Response
			HttpWebRequest Request = (HttpWebRequest)(AsyncStates[0]);
			HttpWebResponse Response = (HttpWebResponse)Request.EndGetResponse(Ar);
			
			//Get stream and create StreamReader
			Stream Stream = Response.GetResponseStream();
			System.IO.StreamReader StreamReader = new System.IO.StreamReader(Stream, Encoding.UTF8);
			
			//Read the stream to end
			System.String Data = StreamReader.ReadToEnd();
			
			//Get the HttpWebCallback delegate
			HttpWebCallback Callback = (HttpWebCallback)(AsyncStates[1]);
			
			//Invoke the callback
			Callback.Invoke(Data);
		}
		
		/// <summary>
		/// A private helper delegate for providing callbacks to nScrobbler.HttpWebRequest later used by nScrobbler.HttpWebResponse
		/// </summary>
		private delegate void HttpWebCallback(System.String Data);
	}
}
