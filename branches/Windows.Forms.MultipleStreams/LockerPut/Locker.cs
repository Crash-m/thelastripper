/*
 * Created by SharpDevelop.
 * User: q
 * Date: 15-02-2009
 * Time: 18:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Security.Cryptography;

namespace LockerPut
{
	/// <summary>
	/// A simple representation of an music locker from MP3tunes
	/// </summary>
	/// <remarks>
	/// Each instance of locker is associated with a remote account
	/// </remarks>
	public class Locker
	{
		/// <summary>
		/// Creates a new instance of locker
		/// </summary>
		/// <remarks>You must login before you can upload tracks</remarks>
		/// <param name="PartnerToken">PartnerToken for mp3tunes, can be freely (and instantly) created.</param>
		public Locker(String PartnerToken){
			this.worker = new WebClient();
			this.worker.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.LoginCallback);
			this.worker.UploadDataCompleted += new UploadDataCompletedEventHandler(this.PutTrackCallback);
			this.PartnerToken = PartnerToken;
		}
		
		private String _Username = null;
		protected String SessionID = null;
		protected String PartnerToken = null;
		
		/// <summary>
		/// Gets true if this instance is logged in
		/// </summary>
		public Boolean IsLoggedin{
			get{
				return this.SessionID != null;
			}
		}
		
		/// <summary>
		/// Gets the username we're logged into
		/// </summary>
		public String Username{
			get{
				if(!this.IsLoggedin)
					throw new Exception("This locker instance is not logged in, and thus not associated with a account!");
				return this._Username;
			}
		}
		
		private WebClient worker;
		
		/// <summary>
		/// Create a new mp3tunes account, asynchronously
		/// </summary>
		/// <remarks>This also performs a login and all errors are return through OnLogin</remarks>
		/// <param name="Username">Desired username</param>
		/// <param name="Password">Password</param>
		/// <param name="FirstName">Firstname</param>
		/// <param name="LastName">Lastname</param>
		/// <param name="Email">Valid email address</param>
		public void CreateAccount(String Username, String Password, String FirstName, String LastName, String Email){
			Uri address = new Uri("https://shop.mp3tunes.com/api/v1/createAccount?output=xml&firstname=" + Uri.EscapeDataString(FirstName)
			                      + "&lastname=" + Uri.EscapeDataString(LastName)
			                      + "&password=" + Uri.EscapeDataString(Password)
			                      + "&email=" + Uri.EscapeDataString(Email)
			                      + "&partner_token=" + this.PartnerToken);
			this.worker.DownloadStringAsync(address);
		}
		
		/// <summary>
		/// Login at mp3tunes, asynchronously
		/// </summary>
		/// <param name="username">mp3tunes username</param>
		/// <param name="Password">mp3tunes password</param>
		public void Login(String Username, String Password){
			Uri address = new Uri("https://shop.mp3tunes.com/api/v1/login?output=xml&username=" + Uri.EscapeDataString(Username) + "&password=" + Uri.EscapeDataString(Password) + "&partner_token=" + this.PartnerToken);
			this.worker.DownloadStringAsync(address);
		}
		
		private void LoginCallback(Object Sender, DownloadStringCompletedEventArgs Args){
			if(Args.Error != null){
				if(this.OnLogin != null)
					this.OnLogin(this, new LockerLoginEventArgs(false, "Exception occured during communication with remote host: " + Args.Error.StackTrace));
			}else if(Args.Cancelled){
				if(this.OnLogin != null)
					this.OnLogin(this, new LockerLoginEventArgs(false, "Network failure, communication with remote host fail."));
			}else{
				XmlDocument data = new XmlDocument();
				data.LoadXml(Args.Result);
				if(data.SelectSingleNode("mp3tunes/status").InnerText != "1"){
					if(this.OnLogin != null)
						this.OnLogin(this, new LockerLoginEventArgs(false, data.SelectSingleNode("mp3tunes/errorMessage").InnerText));
				}else{
					this.SessionID = data.SelectSingleNode("mp3tunes/session_id").InnerText;
					this._Username = data.SelectSingleNode("mp3tunes/username").InnerText;
					if(this.OnLogin != null)
						this.OnLogin(this, new LockerLoginEventArgs(true));
				}
			}
		}
		
		/// <summary>
		/// Occurs when a login completes
		/// </summary>
		/// <remarks>Handlers of this should do thread synchronization</remarks>
		public event System.EventHandler<LockerLoginEventArgs> OnLogin;
		
		private String ComputeMD5(Stream stream){
			//Compute hash
			MD5 hasher = MD5.Create();
			byte[] res = hasher.ComputeHash(stream);
			System.Text.StringBuilder hash = new System.Text.StringBuilder();
			for (int i = 0; i < res.Length; i++)
				hash.Append(res[i].ToString("x2"));

			return hash.ToString();
		}
		
		/// <summary>
		/// Put a file from disk to locker
		/// </summary>
		/// <param name="File">Path to the file</param>
		public void PutTrack(String File){
			FileStream FS = new FileStream(File, FileMode.Open);
			FileInfo FI = new FileInfo(File);
			
			this.PutTrack(FS);
		}
		
		/// <summary>
		/// Put a file from stream
		/// </summary>
		/// <remarks>Please don't use networkstream the stream is buffered to memory synchronously</remarks>
		/// <param name="TrackStream">Stream containing track</param>
		public void PutTrack(Stream TrackStream){
			if(!TrackStream.CanSeek)
				throw new Exception("TrackStream must support seeking, or md5sum must be given.");
			
			String Hash = this.ComputeMD5(TrackStream);
			
			//Seek to begining of stream
			TrackStream.Seek(0, SeekOrigin.Begin);
			
			//Put the track
			this.PutTrack(TrackStream, Hash);
		}
		
		/// <summary>
		/// Put a file from stream
		/// </summary>
		/// <remarks>Please don't use networkstream the stream is buffered to memory synchronously</remarks>
		/// <param name="TrackStream">Stream containing track</param>
		/// <param name="MD5sum">MD5sum of the file</param>
		public void PutTrack(Stream TrackStream, String MD5sum){
			MemoryStream buffer = new MemoryStream(Convert.ToInt32(TrackStream.Length));
			TrackStream.Read(buffer.GetBuffer(), 0, Convert.ToInt32(TrackStream.Length));
			this.PutTrack(buffer, MD5sum);
		}
		
		/// <summary>
		/// Put a file from memory
		/// </summary>
		/// <param name="TrackStream">The track</param>
		public void PutTrack(MemoryStream TrackStream){
			//Compute hash of this stream
			String Hash = this.ComputeMD5(TrackStream);
			
			//Seek to begining of stream
			TrackStream.Seek(0, SeekOrigin.Begin);
			
			//Put the track
			this.PutTrack(TrackStream, Hash);
		}
		
		/// <summary>
		/// Put file from memory
		/// </summary>
		/// <param name="TrackStream">The track</param>
		/// <param name="MD5sum">MD5sum of the track</param>
		public void PutTrack(MemoryStream TrackStream, String MD5sum) {
			if (this.worker.IsBusy) {
				// TODO: implement queue
				if(this.OnPutTrackComplete != null)
					this.OnPutTrackComplete(this, new PutTrackCompleteEventArgs(false, "Failed to upload track, previous upload blocks!"));
			} else {
				Uri Address = new Uri("http://content.mp3tunes.com/storage/lockerPut/" + MD5sum + "?partner_token=" + this.PartnerToken + "&sid=" + this.SessionID);
				this.worker.UploadDataAsync(Address, "PUT", TrackStream.GetBuffer());
			}
		}
		
		/// <summary>
		/// Occurs after uploading track
		/// </summary>
		public event System.EventHandler<PutTrackCompleteEventArgs> OnPutTrackComplete;
		
		private void PutTrackCallback(Object Sender, UploadDataCompletedEventArgs Args){
			if(Args.Error != null){
				if(this.OnPutTrackComplete != null)
					this.OnPutTrackComplete(this, new PutTrackCompleteEventArgs(false, "Exception occured during track upload: " + Args.Error.StackTrace));
			}else if(Args.Cancelled){
				if(this.OnPutTrackComplete != null)
					this.OnPutTrackComplete(this, new PutTrackCompleteEventArgs(false, "Failed to upload track, upload was chancelled!"));
			}else{
				if(this.OnPutTrackComplete != null)
					this.OnPutTrackComplete(this, new PutTrackCompleteEventArgs(true));
			}
		}
	}
	
	/// <summary>
	/// Event arguments for the OnLogin event occuring afther Login
	/// </summary>
	public class LockerLoginEventArgs : System.EventArgs{
		
		/// <summary>
		/// Create a new instance of LockerLoginEventArgs
		/// </summary>
		/// <param name="Success">True if login went good</param>
		/// <param name="Message">Message if it went bad</param>
		public LockerLoginEventArgs(Boolean Success, String Message){
			this._Success = Success;
			this._Message = Message;
		}
		
		/// <summary>
		/// Create a new instance of LockerLoginEventArgs
		/// </summary>
		/// <param name="Success">True if login went good</param>
		public LockerLoginEventArgs(Boolean Success){
			this._Success = Success;
		}
		
		private String _Message = "";
		private Boolean _Success = false;
		
		/// <summary>
		/// Get error message if not successfull
		/// </summary>
		public String Message {
			get{
				return this._Message;
			}
		}
		
		/// <summary>
		/// Gets if login succeeded
		/// </summary>
		public Boolean Success {
			get{
				return this._Success;
			}
		}
	}
	
	/// <summary>
	/// Event arguments for the OnPutTrackComplete event occuring after putting a track
	/// </summary>
	public class PutTrackCompleteEventArgs : System.EventArgs{
		
		/// <summary>
		/// Create a new instance of PutTrackCompleteEventArgs
		/// </summary>
		/// <param name="Success">True if upload went good</param>
		/// <param name="Message">Message if it went bad</param>
		public PutTrackCompleteEventArgs(Boolean Success, String Message){
			this._Success = Success;
			this._Message = Message;
		}
		
		/// <summary>
		/// Create a new instance of PutTrackCompleteEventArgs
		/// </summary>
		/// <param name="Success">True if upload went good</param>
		public PutTrackCompleteEventArgs(Boolean Succes){
			this._Success = Success;
		}
		
		private String _Message = "";
		private Boolean _Success = false;
		
		/// <summary>
		/// Get error message if unsuccessfull
		/// </summary>
		public String Message {
			get{
				return _Message;
			}
		}
		
		/// <summary>
		/// Gets if upload succeeded
		/// </summary>
		public Boolean Success {
			get{
				return _Success;
			}
		}
	}
}
