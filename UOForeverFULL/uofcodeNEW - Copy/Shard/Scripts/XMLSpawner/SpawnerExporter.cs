#region References
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

using Server.Commands;
using Server.Commands.Generic;
#endregion

/*
** Sno's distro spawner importer/exporter
**
** [exportspawner filename - Saves distro spawners to XML to 'Saves/Spawners/filename'
**
** [importspawner filename - Restores distro spawners from 'Saves/Spawners/filename'.  Note, this command does not check for
** duplication, so if you run it more than once you will end up with multiple spawners.
**
** These spawns can also be imported back in as XmlSpawners by using the '[xmlimportspawners Saves/Spawners/filename' command.
*/

namespace Server.Mobiles
{
	public class SpawnerExporter
	{
		public static void Initialize()
		{
			TargetCommands.Register(new ExportSpawnerCommand());
			CommandSystem.Register("ImportSpawners", AccessLevel.Administrator, ImportSpawners_OnCommand);
		}

		public class ExportSpawnerCommand : BaseCommand
		{
			public ExportSpawnerCommand()
			{
				AccessLevel = AccessLevel.Administrator;
				Supports = CommandSupport.Area | CommandSupport.Region | CommandSupport.Global | CommandSupport.Multi |
						   CommandSupport.Single;
				Commands = new[] {"ExportSpawner"};
				ObjectTypes = ObjectTypes.Items;
				Usage = "ExportSpawner <filename>";
				Description = "Exports all Spawner objects to the specified filename.";
				ListOptimized = true;
			}

			public override void ExecuteList(CommandEventArgs e, ArrayList list)
			{
				string filename = e.GetString(0);

				var spawners = new List<Spawner>();

				spawners.AddRange(
					list.AsParallel()
						.OfType<Spawner>()
						.Where(spawner => spawner != null && !spawner.Deleted && spawner.Map != Map.Internal && spawner.Parent == null));

				AddResponse(String.Format("{0} spawners exported to Saves/Spawners/{1}.", spawners.Count.ToString("#,0"), filename));

				ExportSpawners(spawners, filename);
			}

			public override bool ValidateArgs(BaseCommandImplementor impl, CommandEventArgs e)
			{
				if (e.Arguments.Length >= 1)
				{
					return true;
				}

				e.Mobile.SendMessage("Usage: " + Usage);
				return false;
			}

			private void ExportSpawners(List<Spawner> spawners, string filename)
			{
				if (spawners.Count == 0)
				{
					return;
				}

				if (!Directory.Exists("Saves/Spawners"))
				{
					Directory.CreateDirectory("Saves/Spawners");
				}

				string filePath = Path.Combine("Saves/Spawners", filename);

				using (StreamWriter op = new StreamWriter(filePath))
				{
					XmlTextWriter xml = new XmlTextWriter(op)
					{
						Formatting = Formatting.Indented,
						IndentChar = '\t',
						Indentation = 1
					};

					xml.WriteStartDocument(true);
					xml.WriteStartElement("spawners");
					xml.WriteAttributeString("count", spawners.Count.ToString(CultureInfo.InvariantCulture));

					foreach (Spawner spawner in spawners)
					{
						ExportSpawner(spawner, xml);
					}

					xml.WriteEndElement();

					xml.Close();
				}
			}

			private static void ExportSpawner(Spawner spawner, XmlTextWriter xml)
			{
				xml.WriteStartElement("spawner");

				xml.WriteStartElement("count");
				xml.WriteString(spawner.Count.ToString(CultureInfo.InvariantCulture));
				xml.WriteEndElement();

				xml.WriteStartElement("group");
				xml.WriteString(spawner.Group.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("homerange");
				xml.WriteString(spawner.HomeRange.ToString(CultureInfo.InvariantCulture));
				xml.WriteEndElement();

				xml.WriteStartElement("walkingrange");
				xml.WriteString(spawner.WalkingRange.ToString(CultureInfo.InvariantCulture));
				xml.WriteEndElement();

				xml.WriteStartElement("maxdelay");
				xml.WriteString(spawner.MaxDelay.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("mindelay");
				xml.WriteString(spawner.MinDelay.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("team");
				xml.WriteString(spawner.Team.ToString(CultureInfo.InvariantCulture));
				xml.WriteEndElement();

				xml.WriteStartElement("creaturesname");

				foreach (string creatureName in spawner.SpawnNames)
				{
					xml.WriteStartElement("creaturename");
					xml.WriteString(creatureName);
					xml.WriteEndElement();
				}

				xml.WriteEndElement();

				// Item properties

				xml.WriteStartElement("name");
				xml.WriteString(spawner.Name);
				xml.WriteEndElement();

				xml.WriteStartElement("location");
				xml.WriteString(spawner.Location.ToString());
				xml.WriteEndElement();

				xml.WriteStartElement("map");
				xml.WriteString(spawner.Map.ToString());
				xml.WriteEndElement();

				xml.WriteEndElement();
			}
		}

		[Usage("ImportSpawners")]
		[Description("Recreates Spawner items from the specified file.")]
		public static void ImportSpawners_OnCommand(CommandEventArgs e)
		{
			if (e.Arguments.Length >= 1)
			{
				string filename = e.GetString(0);
				string filePath = Path.Combine("Saves/Spawners", filename);

				if (File.Exists(filePath))
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(filePath);

					XmlElement root = doc["spawners"];

					if (root != null)
					{
						int successes = 0, failures = 0;

						foreach (XmlElement spawner in root.GetElementsByTagName("spawner"))
						{
							try
							{
								ImportSpawner(spawner);
								successes++;
							}
							catch
							{
								failures++;
							}
						}

						e.Mobile.SendMessage("{0} spawners loaded successfully from {1}, {2} failures.", successes, filePath, failures);
					}
					else
					{
						e.Mobile.SendMessage("File {0} does not contain document root node 'spawner'", filePath);
					}
				}
				else
				{
					e.Mobile.SendMessage("File {0} does not exist.", filePath);
				}
			}
			else
			{
				e.Mobile.SendMessage("Usage: [ImportSpawners <filename>");
			}
		}

		private static string GetText(XmlElement node, string defaultValue)
		{
			if (node == null)
			{
				return defaultValue;
			}

			return node.InnerText;
		}

		private static void ImportSpawner(XmlElement node)
		{
			int count = int.Parse(GetText(node["count"], "1"));
			int homeRange = int.Parse(GetText(node["homerange"], "4"));

			int walkingRange = int.Parse(GetText(node["walkingrange"], "-1"));

			int team = int.Parse(GetText(node["team"], "0"));

			//bool group = bool.Parse(GetText(node["group"], "False"));
			TimeSpan maxDelay = TimeSpan.Parse(GetText(node["maxdelay"], "10:00"));
			TimeSpan minDelay = TimeSpan.Parse(GetText(node["mindelay"], "05:00"));
			var creaturesName = LoadCreaturesName(node["creaturesname"]);

			string name = GetText(node["name"], "Spawner");
			Point3D location = Point3D.Parse(GetText(node["location"], "Error"));
			Map map = Map.Parse(GetText(node["map"], "Error"));

			Spawner spawner = new Spawner(count, minDelay, maxDelay, team, homeRange, creaturesName);

			if (walkingRange >= 0)
			{
				spawner.WalkingRange = walkingRange;
			}

			spawner.Name = name;
			spawner.MoveToWorld(location, map);

			if (spawner.Map == Map.Internal)
			{
				spawner.Delete();
				throw new Exception("Spawner created on Internal map.");
			}

			spawner.Respawn();
		}

		private static List<string> LoadCreaturesName(XmlElement node)
		{
			var names = new List<string>();

			if (node != null)
			{
				names.AddRange(
					node.GetElementsByTagName("creaturename")
						.OfType<XmlElement>()
						.Where(ele => ele != null)
						.Select(ele => ele.InnerText));
			}

			return names;
		}
	}
}