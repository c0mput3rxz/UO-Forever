﻿#region References
#endregion

using System;

namespace VitaNex.MySQL
{
	/// <summary>
	///     ODBC Driver Versions
	/// </summary>
	public enum ODBCVersion
	{
		/// <summary>
		///     Version 3.51
		/// </summary>
		V_3_51,

		/// <summary>
		///     Version 5.1
		/// </summary>
		V_5_1,

		/// <summary>
		///     Version 5.2a (ANSI)
		/// </summary>
		V_5_3_ANSI,

		/// <summary>
		///     Version 5.2w (UNICODE)
		/// </summary>
		V_5_3_UNICODE
	}

	public static class ODBCVersions
	{
		public static string AsString(this ODBCVersion ver, bool descriptive = true)
		{
			switch (ver)
			{
				case ODBCVersion.V_3_51:
					return "3.51";
				case ODBCVersion.V_5_1:
					return "5.1";
				case ODBCVersion.V_5_3_ANSI:
					return "5.3" + (descriptive ? " ANSI" : "a");
				case ODBCVersion.V_5_3_UNICODE:
					return "5.3" + (descriptive ? " Unicode" : "w");
				default:
					return string.Empty;
			}
		}

		public static string AsConnectionString(this ODBCVersion ver, bool descriptive = true)
		{
			return '{' + String.Format("MySQL ODBC {0} Driver", AsString(ver, descriptive)) + '}';
		}
	}
}