using System;
using System.Collections.Generic;
using System.Text;

using StardewValley;
using StardewModdingAPI;
using StardewModdingAPI.Events;

using Microsoft.Xna.Framework;
using xTile;
using xTile.ObjectModel;
using xTile.Tiles;

namespace SivsSpouseRooms
{
    public class ModEntry : Mod
    {
        private static bool spouseRoomLoaded;
        public Map map;

        public override void Entry(IModHelper Helper)
        {
            GameEvents.UpdateTick += Events_UpdateTick;
        }

        private void Events_UpdateTick(object sender, EventArgs e)
        {
            if (!Game1.hasLoadedGame)
                return;
            if (!spouseRoomLoaded)
                LoadSpouseRoom();
        }

        public static void LoadSpouseRoom()
        {
            NPC npc = Game1.player.getSpouse();
            if (npc == null)
                return;

            int num = -1;
            string spouse = npc.name;
            if (spouse == "Gus")
            {
                num = 11;
            }

            int houseUpgradeLevel = Game1.player.HouseUpgradeLevel;
            Rectangle rectangle = houseUpgradeLevel == 1 ? new Rectangle(29, 1, 6, 9) : new Rectangle(35, 10, 6, 9);
            Map map = Game1.content.Load<Map>("Maps\\spouseRooms");
            Point point = new Point(num % 5 * 6, num / 5 * 9);
            map.Properties.Remove("DayTiles");
            map.Properties.Remove("NightTiles");

            for (int index1 = 0; index1 < rectangle.Width; index1++)
            {
                for (int index2 = 0; index2 < rectangle.Height; index2++)
                {
                    if (map.GetLayer("Back").Tiles[point.X + index1, point.Y + index2] != null)
                        map.GetLayer("Back").Tiles[rectangle.X + index1, rectangle.Y + index2] = new StaticTile(map.GetLayer("Back"), map.TileSheets[0], BlendMode.Alpha, map.GetLayer("Back").Tiles[point.X + index1, point.Y + index2].TileIndex);
                    if (map.GetLayer("Buildings").Tiles[point.X + index1, point.Y + index2] != null)
                    {
                        map.GetLayer("Buildings").Tiles[rectangle.X + index1, rectangle.Y + index2] = new StaticTile(map.GetLayer("Buildings"), map.TileSheets[0], BlendMode.Alpha, map.GetLayer("Buildings").Tiles[point.X + index1, point.Y + index2].TileIndex);
                        adjustMapLightPropertiesForLamp(map.GetLayer("Buildings").Tiles[point.X + index1, point.Y + index2].TileIndex, rectangle.X + index1, rectangle.Y + index2, "Buildings");
                    }
                    else
                        map.GetLayer("Buildings").Tiles[rectangle.X + index1, rectangle.Y + index2] = null;
                    if (index2 < rectangle.Height - 1 && map.GetLayer("Front").Tiles[point.X + index1, point.Y + index2] != null)
                    {
                        map.GetLayer("Front").Tiles[rectangle.X + index1, rectangle.Y + index2] = new StaticTile(map.GetLayer("Front"), map.TileSheets[0], BlendMode.Alpha, map.GetLayer("Front").Tiles[point.X + index1, point.Y + index2].TileIndex);
                        adjustMapLightPropertiesForLamp(map.GetLayer("Front").Tiles[point.X + index1, point.Y + index2].TileIndex, rectangle.X + index1, rectangle.Y + index2, "Front");
                    }
                    else if (index2 < rectangle.Height - 1)
                        map.GetLayer("Front").Tiles[rectangle.X + index1, rectangle.Y + index2] = null;
                    if (index1 == 4 && index2 == 4)
                        map.GetLayer("Back").Tiles[rectangle.X + index1, rectangle.Y + index2].Properties.Add(new KeyValuePair<string, PropertyValue>("NoFurniture", new PropertyValue("T")));
                }
            }
            spouseRoomLoaded = true;
        }

        public static void adjustMapLightPropertiesForLamp(int tile, int x, int y, string layer)
        {
            switch (tile)
            {
                case 480:
                    changeMapProperties("DayTiles", layer + " " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", layer + " " + x + " " + y + " " + 809);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    break;
                case 826:
                    changeMapProperties("DayTiles", layer + " " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", layer + " " + x + " " + y + " " + 827);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    break;
                case 1344:
                    changeMapProperties("DayTiles", layer + " " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", layer + " " + x + " " + y + " " + 1345);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    break;
                case 1346:
                    changeMapProperties("DayTiles", "Front " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", "Front " + x + " " + y + " " + 1347);
                    changeMapProperties("DayTiles", "Buildings " + x + " " + (y + 1) + " " + 452);
                    changeMapProperties("NightTiles", "Buildings " + x + " " + (y + 1) + " " + 453);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    break;
                case 8:
                    changeMapProperties("Light", x.ToString() + " " + y + " 9");
                    break;
                case 225:
                    if ((Game1.getLocationFromName("FarmHouse").name).Contains("BathHouse")) // This is impossible, the name is FarmHouse which clearly does not contain BathHouse
                        break;
                    changeMapProperties("DayTiles", layer + " " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", layer + " " + x + " " + y + " " + 1222);
                    changeMapProperties("DayTiles", layer + " " + x + " " + (y + 1) + " " + 257);
                    changeMapProperties("NightTiles", layer + " " + x + " " + (y + 1) + " " + 1254);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    changeMapProperties("Light", x.ToString() + " " + (y + 1) + " 4");
                    break;
                case 256:
                    changeMapProperties("DayTiles", layer + " " + x + " " + y + " " + tile);
                    changeMapProperties("NightTiles", layer + " " + x + " " + y + " " + 1253);
                    changeMapProperties("DayTiles", layer + " " + x + " " + (y + 1) + " " + 288);
                    changeMapProperties("NightTiles", layer + " " + x + " " + (y + 1) + " " + 1285);
                    changeMapProperties("Light", x.ToString() + " " + y + " 4");
                    changeMapProperties("Light", x.ToString() + " " + (y + 1) + " 4");
                    break;
            }
        }

        private static void changeMapProperties(string propertyName, string toAdd)
        {
            try
            {
                bool flag = true;
                if (!Game1.getLocationFromName("FarmHouse").map.Properties.ContainsKey(propertyName))
                {
                    Game1.getLocationFromName("FarmHouse").map.Properties.Add(propertyName, new PropertyValue(string.Empty));
                    flag = false;
                }
                if (Game1.getLocationFromName("FarmHouse").map.Properties[propertyName].ToString().Contains(toAdd))
                    return;
                StringBuilder stringBuilder = new StringBuilder(Game1.getLocationFromName("FarmHouse").map.Properties[propertyName].ToString());
                if (flag)
                    stringBuilder.Append(" ");
                stringBuilder.Append(toAdd);
                Game1.getLocationFromName("FarmHouse").map.Properties[propertyName] = new PropertyValue(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}