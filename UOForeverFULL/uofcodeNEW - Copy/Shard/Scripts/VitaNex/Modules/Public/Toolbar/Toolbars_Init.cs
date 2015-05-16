﻿#region Header
//   Vorspire    _,-'/-'/  Toolbars_Init.cs
//   .      __,-; ,'( '/
//    \.    `-.__`-._`:_,-._       _ , . ``
//     `:-._,------' ` _,`--` -: `_ , ` ,' :
//        `---..__,,--'  (C) 2014  ` -'. -'
//        #  Vita-Nex [http://core.vita-nex.com]  #
//  {o)xxx|===============-   #   -===============|xxx(o}
//        #        The MIT License (MIT)          #
#endregion

#region References
using System;
using System.Collections.Generic;

using Server;
using Server.Mobiles;

using VitaNex.IO;
#endregion

namespace VitaNex.Modules.Toolbar
{
	[CoreModule("Toolbars", "1.1.0", false)]
	public static partial class Toolbars
	{
		static Toolbars()
		{
			EntryTypes = typeof(ToolbarEntry).GetConstructableChildren();

			CMOptions = new ToolbarsOptions();

			Profiles = new BinaryDataStore<PlayerMobile, ToolbarState>(VitaNexCore.SavesDirectory + "/Toolbars", "States")
			{
				Async = true,
				OnSerialize = Serialize,
				OnDeserialize = Deserialize
			};

			DefaultEntries = new ToolbarState(null, CMOptions.DefaultWidth, CMOptions.DefaultHeight);
		}

		private static void CMConfig()
		{
			EventSink.Login += OnLogin;

			LoadDefaultEntries = LoadDefaults;
			ClearDefaultEntries = ClearDefaults;
		}

		private static void CMEnabled()
		{
			EventSink.Login += OnLogin;

			CommandUtility.Register(CMOptions.PopupCommand, AccessLevel.Player, CMOptions.HandlePopupCommand);
			CommandUtility.Register(CMOptions.PositionCommand, AccessLevel.Player, CMOptions.HandlePositionCommand);

			if (CMOptions.LoginPopup)
			{
				OpenAll();
			}
		}

		private static void CMDisabled()
		{
			EventSink.Login -= OnLogin;

			CommandUtility.Unregister(CMOptions.PopupCommand);
			CommandUtility.Unregister(CMOptions.PositionCommand);

			CloseAll();
		}

		private static void CMSave()
		{
			VitaNexCore.TryCatch(
				() =>
				{
					DataStoreResult result = Profiles.Export();
					CMOptions.ToConsole("{0} profiles saved, {1}", Profiles.Count > 0 ? Profiles.Count.ToString("#,#") : "0", result);
				},
				CMOptions.ToConsole);
		}

		private static void CMLoad()
		{
			VitaNexCore.TryCatch(
				() =>
				{
					DataStoreResult result = Profiles.Import();
					CMOptions.ToConsole(
						"{0} profiles loaded, {1}.", Profiles.Count > 0 ? Profiles.Count.ToString("#,#") : "0", result);
				},
				CMOptions.ToConsole);
		}

		public static bool Serialize(GenericWriter writer)
		{
			int version = writer.SetVersion(0);

			switch (version)
			{
				case 0:
					{
						if (DefaultEntries == null)
						{
							writer.Write(false);
						}
						else
						{
							writer.Write(true);
							DefaultEntries.Serialize(writer);
						}

						writer.WriteBlockDictionary(
							Profiles,
							(k, v) =>
							{
								writer.Write(k);
								v.Serialize(writer);
							});
					}
					break;
			}

			return true;
		}

		public static bool Deserialize(GenericReader reader)
		{
			int version = reader.GetVersion();

			switch (version)
			{
				case 0:
					{
						if (reader.ReadBool())
						{
							if (DefaultEntries != null)
							{
								DefaultEntries.Deserialize(reader);
							}
							else
							{
								DefaultEntries = new ToolbarState(reader);
							}
						}

						reader.ReadBlockDictionary(
							() =>
							{
								PlayerMobile k = reader.ReadMobile<PlayerMobile>();
								ToolbarState v = new ToolbarState(reader);
								return new KeyValuePair<PlayerMobile, ToolbarState>(k, v);
							},
							Profiles);
					}
					break;
			}

			return true;
		}
	}
}