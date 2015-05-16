#region References
using System;
using System.Collections.Generic;
using System.Net;

using Server.Accounting;
using Server.Commands;
using Server.Engines.Help;
using Server.Network;
using Server.Regions;
#endregion

namespace Server.Misc
{
	public enum PasswordProtection
	{
		None,
		Crypt,
		NewCrypt,
		SaltedCrypt
	}

	public class AccountHandler
	{
		private static int MaxAccountsPerIP = 4;
		private static bool AutoAccountCreation = true;
		private static readonly bool RestrictDeletion = !TestCenter.Enabled;
		private static readonly TimeSpan DeleteDelay = TimeSpan.FromDays(7.0);
		private static int m_AccountMaxLength = 16;

		public static PasswordProtection ProtectPasswords = PasswordProtection.SaltedCrypt;

		private static AccessLevel m_LockdownLevel;

		public static AccessLevel LockdownLevel { get { return m_LockdownLevel; } set { m_LockdownLevel = value; } }

		private static readonly CityInfo[] StartingCities = new[]
		{
			//new CityInfo( "New Haven",	"New Haven Bank",		1150168, 3667,	2625,	0  ),
			new CityInfo("Britain", "The Wayfarer's Inn", 1075074, 1602, 1591, 20),
			new CityInfo("Yew", "The Empath Abbey", 1075072, 633, 858, 0),
			new CityInfo("Minoc", "The Barnacle", 1075073, 2476, 413, 15),
			new CityInfo("Britain", "The Wayfarer's Inn", 1075074, 1602, 1591, 20),
			new CityInfo("Moonglow", "The Scholars Inn", 1075075, 4408, 1168, 0),
			new CityInfo("Trinsic", "The Traveler's Inn", 1075076, 1845, 2745, 0),
			new CityInfo("Jhelom", "The Mercenary Inn", 1075078, 1374, 3826, 0),
			new CityInfo("Skara Brae", "The Falconer's Inn", 1075079, 618, 2234, 0),
			new CityInfo("Vesper", "The Ironwood Inn", 1075080, 2771, 976, 0)
		};

		/* Old Haven/Magincia Locations
			new CityInfo( "Britain", "Sweet Dreams Inn", 1496, 1628, 10 );
			// ..
			// Trinsic
			new CityInfo( "Magincia", "The Great Horns Tavern", 3734, 2222, 20 ),
			// Jhelom
			// ..
			new CityInfo( "Haven", "Buckler's Hideaway", 3667, 2625, 0 )

			if ( Core.AOS )
			{
				//CityInfo haven = new CityInfo( "Haven", "Uzeraan's Mansion", 3618, 2591, 0 );
				CityInfo haven = new CityInfo( "Haven", "Uzeraan's Mansion", 3503, 2574, 14 );
				StartingCities[StartingCities.Length - 1] = haven;
			}
		*/

		private static bool PasswordCommandEnabled = false;

		public static void Initialize()
		{
			EventSink.DeleteRequest += EventSink_DeleteRequest;
			EventSink.AccountLogin += EventSink_AccountLogin;
			EventSink.GameLogin += EventSink_GameLogin;

			if (PasswordCommandEnabled)
			{
				CommandSystem.Register("Password", AccessLevel.Player, Password_OnCommand);
			}
		}

		[Usage("Password <newPassword> <repeatPassword>")]
		[Description(
			"Changes the password of the commanding players account. Requires the same C-class IP address as the account's creator."
			)]
		public static void Password_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			Account acct = from.Account as Account;

			if (acct == null)
			{
				return;
			}

			if (acct.LoginIPs.Count == 0)
			{
				return;
			}

			NetState ns = from.NetState;

			if (ns == null)
			{
				return;
			}

			if (e.Length == 0)
			{
				from.SendMessage("You must specify the new password.");
				return;
			}
			else if (e.Length == 1)
			{
				from.SendMessage("To prevent potential typing mistakes, you must type the password twice. Use the format:");
				from.SendMessage("Password \"(newPassword)\" \"(repeated)\"");
				return;
			}

			string pass = e.GetString(0);
			string pass2 = e.GetString(1);

			if (pass != pass2)
			{
				from.SendMessage("The passwords do not match.");
				return;
			}

			bool isSafe = true;

			for (int i = 0; isSafe && i < pass.Length; ++i)
			{
				isSafe = (pass[i] >= 0x20 && pass[i] < 0x80);
			}

			if (!isSafe)
			{
				from.SendMessage("That is not a valid password.");
				return;
			}

			try
			{
				IPAddress ipAddress = ns.Address;

				if (Utility.IPMatchClassC(acct.LoginIPs[0], ipAddress))
				{
					acct.SetPassword(pass);
					from.SendMessage("The password to your account has changed.");
				}
				else
				{
					PageEntry entry = PageQueue.GetEntry(from);

					if (entry != null)
					{
						if (entry.Message.StartsWith("[Automated: Change Password]"))
						{
							from.SendMessage("You already have a password change request in the help system queue.");
						}
						else
						{
							from.SendMessage("Your IP address does not match that which created this account.");
						}
					}
					else if (PageQueue.CheckAllowedToPage(from))
					{
						from.SendMessage(
							"Your IP address does not match that which created this account.  A page has been entered into the help system on your behalf.");

						from.SendLocalizedMessage(501234, "", 0x35);
							/* The next available Counselor/Game Master will respond as soon as possible.
																	    * Please check your Journal for messages every few minutes.
																	    */

						PageQueue.Enqueue(
							new PageEntry(
								from,
								String.Format(
									"[Automated: Change Password]<br>Desired password: {0}<br>Current IP address: {1}<br>Account IP address: {2}",
									pass,
									ipAddress,
									acct.LoginIPs[0]),
								PageType.Account));
					}
				}
			}
			catch
			{ }
		}

		private static void EventSink_DeleteRequest(DeleteRequestEventArgs e)
		{
			NetState state = e.State;
			int index = e.Index;

			Account acct = state.Account as Account;

			if (acct == null)
			{
				state.Dispose();
			}
			else if (index < 0 || index >= acct.Length)
			{
				state.Send(new DeleteResult(DeleteResultType.BadRequest));
				state.Send(new CharacterListUpdate(acct));
			}
			else
			{
				Mobile m = acct[index];

				if (m == null)
				{
					state.Send(new DeleteResult(DeleteResultType.CharNotExist));
					state.Send(new CharacterListUpdate(acct));
				}
				else if (m.NetState != null)
				{
					state.Send(new DeleteResult(DeleteResultType.CharBeingPlayed));
					state.Send(new CharacterListUpdate(acct));
				}
				else if (RestrictDeletion && DateTime.UtcNow < (m.CreationTime + DeleteDelay))
				{
					state.Send(new DeleteResult(DeleteResultType.CharTooYoung));
					state.Send(new CharacterListUpdate(acct));
				}
				else if (m.AccessLevel == AccessLevel.Player &&
						 Region.Find(m.LogoutLocation, m.LogoutMap).GetRegion(typeof(Jail)) != null)
					//Don't need to check current location, if netstate is null, they're logged out
				{
					state.Send(new DeleteResult(DeleteResultType.BadRequest));
					state.Send(new CharacterListUpdate(acct));
				}
				else
				{
					Console.WriteLine(
						"Client: {0}: Deleting character {1} (0x{2:X}) [{3}]", state, index, m.Serial.Value, acct.Username);

					acct.Comments.Add(
						new AccountComment("System", String.Format("Character #{0} {1} deleted by {2}", index + 1, m, state)));

					m.Delete();
					state.Send(new CharacterListUpdate(acct));
				}
			}
		}

		public static bool CanCreate(IPAddress ip)
		{
			if (!IPTable.ContainsKey(ip))
			{
				return true;
			}

			return (IPTable[ip] < MaxAccountsPerIP);
		}

		private static Dictionary<IPAddress, Int32> m_IPTable;

		public static Dictionary<IPAddress, Int32> IPTable
		{
			get
			{
				if (m_IPTable == null)
				{
					m_IPTable = new Dictionary<IPAddress, Int32>();

					foreach (Account a in Accounts.GetAccounts())
					{
						if (a.LoginIPs.Count > 0)
						{
							IPAddress ip = a.LoginIPs[0];

							if (ip != null && m_IPTable.ContainsKey(ip))
							{
								m_IPTable[ip]++;
							}
							else
							{
								m_IPTable[ip] = 1;
							}
						}
					}
				}

				return m_IPTable;
			}
		}

		private static readonly char[] m_ForbiddenChars = new[] {'<', '>', ':', '"', '/', '\\', '|', '?', '*'};

		private static bool IsForbiddenChar(char c)
		{
			for (int i = 0; i < m_ForbiddenChars.Length; ++i)
			{
				if (c == m_ForbiddenChars[i])
				{
					return true;
				}
			}

			return false;
		}

		private static Account CreateAccount(NetState state, string un, string pw)
		{
			if (un.Length > 0 && un.Length <= m_AccountMaxLength && pw.Length > 0)
			{
				bool isSafe = !(un.StartsWith(" ") || un.EndsWith(" ") || un.EndsWith("."));

				for (int i = 0; isSafe && i < un.Length; ++i)
				{
					isSafe = (un[i] >= 0x20 && un[i] < 0x80 && !IsForbiddenChar(un[i]));
				}

				for (int i = 0; isSafe && i < pw.Length; ++i)
				{
					isSafe = (pw[i] >= 0x20 && pw[i] < 0x80);
				}

				if (isSafe)
				{
					if (CanCreate(state.Address))
					{
						Console.WriteLine("Login: {0}: Creating new account '{1}'", state, un);
						Account a = new Account(un, pw);
						return a;
					}
					else
					{
						Console.WriteLine(
							"Login: {0}: Account '{1}' not created, ip already has {2} account{3}.",
							state,
							un,
							MaxAccountsPerIP,
							MaxAccountsPerIP == 1 ? "" : "s");
					}
				}
			}

			return null;
		}

		public static void EventSink_AccountLogin(AccountLoginEventArgs e)
		{
			string un = e.Username;
			string pw = e.Password;

			e.Accepted = false;
			Account acct = Accounts.GetAccount(un) as Account;

			if ((acct == null || acct.AccessLevel == AccessLevel.Player) && !IPLimiter.SocketBlock &&
				!IPLimiter.Verify(e.State.Address))
			{
				e.Accepted = false;
				e.RejectReason = ALRReason.InUse;
				return;
			}

			if (acct == null)
			{
				if (AutoAccountCreation && un.Trim().Length > 0)
					//To prevent someone from making an account of just '' or a bunch of meaningless spaces
				{
					e.State.Account = acct = CreateAccount(e.State, un, pw);
					e.Accepted = acct != null && acct.CheckAccess(e.State);

					if (!e.Accepted)
					{
						e.RejectReason = ALRReason.BadComm;
					}
				}
				else
				{
					Console.WriteLine("Login: {0}: Invalid username '{1}'", e.State, un);
					e.RejectReason = ALRReason.Invalid;
				}
			}
			else if (!acct.HasAccess(e.State))
			{
				Console.WriteLine("Login: {0}: Access denied for '{1}'", e.State, un);
				e.RejectReason = (m_LockdownLevel > AccessLevel.Player ? ALRReason.BadComm : ALRReason.BadPass);
			}
			else if (!acct.CheckPassword(pw))
			{
				Console.WriteLine("Login: {0}: Invalid password for '{1}'", e.State, un);
				e.RejectReason = ALRReason.BadPass;
			}
			else if (acct.Banned)
			{
				Console.WriteLine("Login: {0}: Banned account '{1}'", e.State, un);
				e.RejectReason = ALRReason.Blocked;
			}
			else
			{
				Console.WriteLine("Login: {0}: Valid credentials for '{1}'", e.State, un);
				e.State.Account = acct;
				e.Accepted = true;

				//acct.LogAccess( e.State );
			}

			if (!e.Accepted)
			{
				AccountAttackLimiter.RegisterInvalidAccess(e.State);
			}
		}

		public static void EventSink_GameLogin(GameLoginEventArgs e)
		{
			string un = e.Username;
			string pw = e.Password;

			Account acct = Accounts.GetAccount(un) as Account;

			if (acct == null)
			{
				e.Accepted = false;
			}
			else if (!acct.HasAccess(e.State))
			{
				Console.WriteLine("Login: {0}: Access denied for '{1}'", e.State, un);
				e.Accepted = false;
			}
			else if (!acct.CheckPassword(pw))
			{
				Console.WriteLine("Login: {0}: Invalid password for '{1}'", e.State, un);
				e.Accepted = false;
			}
			else if (acct.Banned)
			{
				Console.WriteLine("Login: {0}: Banned account '{1}'", e.State, un);
				e.Accepted = false;
			}
			else
			{
				acct.LogAccess(e.State);

				Console.WriteLine("Login: {0}: Account '{1}' at character list", e.State, un);
				e.State.Account = acct;
				e.Accepted = true;
				e.CityInfo = StartingCities;
			}

			if (!e.Accepted)
			{
				AccountAttackLimiter.RegisterInvalidAccess(e.State);
			}
		}

		public static bool CheckAccount(Mobile mobCheck, Mobile accCheck)
		{
			if (accCheck != null)
			{
				Account a = accCheck.Account as Account;

				if (a != null)
				{
					for (int i = 0; i < a.Length; ++i)
					{
						if (a[i] == mobCheck)
						{
							return true;
						}
					}
				}
			}

			return false;
		}
	}
}