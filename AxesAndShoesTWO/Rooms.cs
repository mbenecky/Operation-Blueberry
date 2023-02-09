using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Messaging;
using System.Reflection.Emit;
using System.Windows.Forms;

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
        public KeysRoom RequiredKey;
        public KeysRoom ReceivedKey;
        public List<Enemy> Enemies;
        public List<Items> Drops;
        public Image Img;
        public Rooms() { }
        public Rooms(int ID, string Name, string Description,KeysRoom RequiredKey, KeysRoom ReceivedKey,  Image Img)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.RequiredKey = RequiredKey;
            this.ReceivedKey = ReceivedKey;
            this.Img = Img;
            
        }

        public bool AreThereEnemies()
        {
            if (Enemies.Count == 0)
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
        
        public List<Items> CreateDrops(KeysRoom RequiredKey, List<Items> DropTable)
        {
            List<Items> finalDrops = new List<Items>();
            List<Items> helpList = new List<Items>();
            int amount = rnd.Next(5, 9);
            switch (RequiredKey)
            {
                case KeysRoom.Catacombs:        //100% Common
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalDrops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.ElectricityRoom:  //75% Common 25% Uncommon
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalDrops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.EngineRoom:       //50% Common 50% Uncommon
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Common) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalDrops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.VaultDoor:        //10% Common 50% Uncommon 40% Rare
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Rare) || x.Rarity.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalDrops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.RogersShrineDoor: //10% Uncommon 60% Rare 30% Legendary
                    helpList = DropTable.Where(x => x.Rarity.Equals(Rarity.Epic) || x.Rarity.Equals(Rarity.Legendary)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalDrops.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.BorysHQDoor:      //END. doesn't have to be anything lol
                    break;
            }
            return finalDrops;
        }
        public List<Enemy> CreateEnemies(KeysRoom RequiredKey, List<Enemy> Enemies)
        {
            //int amount = rnd.Next(5, 10);
            int amount = 1;
            List<Enemy> finalEnemies = new List<Enemy>();
            List<Enemy> helpList = new List<Enemy>();
            switch (RequiredKey)
            {
                case KeysRoom.Catacombs: //Common
                    helpList = Enemies.Where(x => x.RarityOfEnemy.Equals(Rarity.Common)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalEnemies.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.ElectricityRoom: //Uncommon
                    helpList = Enemies.Where(x => x.RarityOfEnemy.Equals(Rarity.Uncommon)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalEnemies.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.EngineRoom: //Rare
                    helpList = Enemies.Where(x => x.RarityOfEnemy.Equals(Rarity.Rare)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalEnemies.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.VaultDoor: //Epic
                    helpList = Enemies.Where(x => x.RarityOfEnemy.Equals(Rarity.Epic)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalEnemies.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.RogersShrineDoor: //Legendary
                    helpList = Enemies.Where(x => x.RarityOfEnemy.Equals(Rarity.Legendary)).ToList();
                    for (int i = 0; i < amount; i++)
                    {
                        finalEnemies.Add(helpList[rnd.Next(0, helpList.Count)]);
                    }
                    break;
                case KeysRoom.BorysHQDoor: //Final Boss Fight
                    break;
            }
            return finalEnemies;
        }

    }
}
