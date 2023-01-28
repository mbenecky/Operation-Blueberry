using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Messaging;

namespace AxesAndShoesTWO
{
    public class Rooms
    {
        //1- Start of game
        //2- Main Room
        //3 - Catacombs         -- Requires 1st Key
        //4 - Storage room      
        //5 - Electrical Room   -- Requires 2nd Key
        //6 - Engine Room       -- Requires 3nd Key
        //7 - Vault Door        -- Requires 4th Key && Gasmask
        //8 - RogersShrineDoor  -- Requires 5th Key && Gasmask
        //9 - BorysHQDoor       -- Requires 6th Key


        Random rnd = new Random();
        public int ID;
        public string Name;
        public string Description;
        public bool GasmaskRequire;
        public KeysRoom RequiredKey;
        public KeysRoom ReceivedKey;
        public List<Enemy> Enemies;
        public List<Items> Drops;
        public Image Img;
        public Rooms() { }
        public Rooms(int ID, string Name, string Description, bool GR, KeysRoom RequiredKey, KeysRoom ReceivedKey, List<Enemy> Enemies, List<Items> Drops, Image Img)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.GasmaskRequire = GR;
            this.RequiredKey = RequiredKey;
            this.ReceivedKey = ReceivedKey;
            this.Enemies = Enemies;
            this.Drops = Drops;
            this.Img = Img;
        }

        public bool AreThereEnemies()
        {
            if (this.Enemies.Count == 0)
            {
                return false;
            }
            return true;
        }
        public bool CanPlayerEnter(Player player)
        {
            switch (RequiredKey)
            {
                case KeysRoom.Catacombs: return true;
                case KeysRoom.ElectricityRoom:
                    if (player.CurrentKeys.Contains(KeysRoom.ElectricityRoom)) return true;
                    else return false;
                case KeysRoom.EngineRoom:
                    if (player.CurrentKeys.Contains(KeysRoom.EngineRoom)) return true;
                    else return false;
                case KeysRoom.VaultDoor:
                    if (player.CurrentKeys.Contains(KeysRoom.VaultDoor)) return true;
                    else return false;
                case KeysRoom.RogersShrineDoor:
                    if (player.CurrentKeys.Contains(KeysRoom.RogersShrineDoor)) return true;
                    else return false;
                case KeysRoom.BorysHQDoor:
                    if (player.CurrentKeys.Contains(KeysRoom.BorysHQDoor)) return true;
                    else return false;
            }
            return true;
        }
        public void CreateDrops(KeysRoom RequiredKey, List<Items> DropTable)
        {
            List<Items> helpList = new List<Items>();
            int amount = rnd.Next(5, 9);
            switch (RequiredKey)
            {
                case KeysRoom.Catacombs:        //100% Common
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        this.Drops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.ElectricityRoom:  //75% Common 25% Uncommon
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        this.Drops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.EngineRoom:       //50% Common 50% Uncommon
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        this.Drops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.VaultDoor:        //10% Common 50% Uncommon 40% Rare
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Rare) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        this.Drops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.RogersShrineDoor: //10% Uncommon 60% Rare 30% Legendary
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Rare) || x.Rarity.Equals(Rarity.Legendary)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        this.Drops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.BorysHQDoor:      //END. doesn't have to be anything lol
                    break;
            }
        }
        public void CreateEnemies(KeysRoom RequiredKey)
        {
            switch (RequiredKey)
            {
                case KeysRoom.Catacombs:
                    
                    break;
                case KeysRoom.ElectricityRoom:

                    break;
                case KeysRoom.EngineRoom:

                    break;
                case KeysRoom.VaultDoor:

                    break;
                case KeysRoom.RogersShrineDoor:

                    break;
                case KeysRoom.BorysHQDoor:
                    break;
            }

        }
    }
}
