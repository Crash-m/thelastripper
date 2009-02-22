/*
 * Created by SharpDevelop.
 * User: q
 * Date: 16-02-2009
 * Time: 19:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsClient
{
	/// <summary>
	/// Create a new Locker account
	/// </summary>
	public partial class CreateLockerDialog : Form
	{
		private LockerPut.Locker locker;
		
		public CreateLockerDialog(LockerPut.Locker locker)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.locker = locker;
			this.locker.OnLogin += new EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback);
		}
		
		void OkbuttonClick(object sender, EventArgs e)
		{
			String Password = this.PasswordTextBox.Text;
			if(this.Password2TextBox.Text != Password){
				MessageBox.Show("You must enter the same password twice!", "Password validation error");
				return;
			}
			this.Okbutton.Enabled = false;
			this.LoginInfogroupBox.Enabled = false;
			this.NameGroupBox.Enabled = false;
			this.AbortButton.Enabled = false;
			this.locker.CreateAccount(this.EmailTextBox.Text, Password, this.FirstNameTextBox.Text, this.LastNameTextBox.Text, this.EmailTextBox.Text);
		}
		
		void AbortButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
		
		/// <summary>
		/// Handle login callback
		/// </summary>
		void LockerLoginCallback(Object Sender, LockerPut.LockerLoginEventArgs Args){
			//HACK: This should be handled before the events were fired, but .Net doesn't have any methods to do that independant of GUI set.
			//Check for if we're on the UI-thread, if not invoke this method to run on UI-thread.
			//This is done since the event launching the method may occur on a different thread.
			if(this.InvokeRequired)
			{
				//Invoke this method and it's arguments to the correct thread.
				this.Invoke(new System.EventHandler<LockerPut.LockerLoginEventArgs>(this.LockerLoginCallback), new System.Object[]{Sender, Args});
				//Return this method to avoid executing the logic on the wrong thread.
				return;
			}
			//Check if we're on the right thread now, we should be!
			System.Diagnostics.Debug.Assert(!this.InvokeRequired, "Failed to invoke correctly");
			
			if(Args.Success){
				this.DialogResult = DialogResult.OK;
			}else{
				MessageBox.Show(Args.Message, "Error during account creation");
				this.Okbutton.Enabled = true;
				this.LoginInfogroupBox.Enabled = true;
				this.NameGroupBox.Enabled = true;
				this.AbortButton.Enabled = true;
			}
		}
		
		/// <summary>
		/// Gets the username entered
		/// </summary>
		public String Username{
			get{
				return this.EmailTextBox.Text;
			}
		}
		
		/// <summary>
		/// Gets the password entered
		/// </summary>
		public String Password{
			get{
				return this.PasswordTextBox.Text;
			}
		}
	}
}
