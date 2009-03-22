// CreateLockerAccount.cs created with MonoDevelop
// User: jopsen at 16:34 21-03-2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace MonoClient
{
	public partial class CreateLockerAccount : Gtk.Dialog
	{
		
		public LockerPut.Locker locker;
		public CreateLockerAccount(LockerPut.Locker locker)
		{
			this.locker = locker;
			this.Build();
		}

		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			//Generate validation error message
			String msg = "";
			if(this.LockerPwd != this.Pwd2Entry.Text){
				msg = "The entered passwords are not the same, please correct them";
			}else if(!this.LockerMail.Contains("@")){
				msg = "Please enter a valid email address";
			}else if(String.IsNullOrEmpty(this.FirstName) || String.IsNullOrEmpty(this.LastName)){
				msg = "You must enter both first name and last name.";
			}
			//Display if there is a message
			if(msg != ""){
				Gtk.MessageDialog dl = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, false, msg);
				dl.Run();
				dl.Destroy();
			}else{//If no msg, validation ok. Attempt to create account
				//Disable buttons
				this.buttonCancel.Sensitive = false;
				this.buttonOk.Sensitive = false;
				//Do login
				this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback);
				this.locker.CreateAccount(this.LockerMail, this.LockerPwd, this.FirstName, this.LastName, this.LockerMail);
			}
		}
		
		/// <summary>
		/// Handle locker callback
		/// </summary>
		private void LockerLoginCallback(Object Sender, LockerPut.LockerLoginEventArgs Args){
			Gtk.Application.Invoke(delegate {
				//Remove event handler
				this.locker.OnLogin -= new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback);
				
				if(Args.Success){
					this.Respond(Gtk.ResponseType.Ok);
				}else{
					Gtk.MessageDialog dl = new Gtk.MessageDialog(this, Gtk.DialogFlags.Modal, Gtk.MessageType.Error, Gtk.ButtonsType.Ok, false, "Failed to create new account\nError message:\n" + Args.Message);
					dl.Run();
					dl.Destroy();
					this.buttonCancel.Sensitive = true;
					this.buttonOk.Sensitive = true;
				}
			});
		}		
		
		public string LockerMail{
			get{
				return this.MailEntry.Text;
			}
		}
		
		public string LockerPwd{
			get{
				return this.PwdEntry.Text;
			}
		}
		
		public string FirstName{
			get{
				return this.FirstNameEntry.Text;
			}
		}
		
		public string LastName{
			get{
				return this.LastnameEntry.Text;
			}
		}
	}
}
