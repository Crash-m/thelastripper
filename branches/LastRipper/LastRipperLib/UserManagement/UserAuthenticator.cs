using System;

namespace LastRipperLib
{
	public class UserAuthenticator
	{
		private UserSession _session;

		public UserAuthenticator ()
		{
			_session = new UserSession();
		}

		public UserSession Session
		{
			get { return _session;}
		}
	}
}

