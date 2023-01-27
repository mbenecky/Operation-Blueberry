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
            this.ID= ID;
            this.Name= Name;
            this.Description= Description;
            this.GasmaskRequire= GR;
            this.RequiredKey= RequiredKey;
            this.ReceivedKey= ReceivedKey;
            this.Enemies= Enemies;
            this.Drops= Drops;
            this.Img= Img;
        }
        public void CreateDrops(KeysRoom RequiredKey, Rarity MaxRarity, List<Items> DropTable)
        {
            switch (RequiredKey) 
            {
                case KeysRoom.Catacombs:        //100% Common
                    
                    break;
                case KeysRoom.ElectricityRoom:  //75% Common 25% Uncommon

                    break;
                case KeysRoom.EngineRoom:       //50% Common 50% Uncommon

                    break;
                case KeysRoom.VaultDoor:        //10% Common 50% Uncommon 40% Rare

                    break;
                case KeysRoom.RogersShrineDoor: //10% Uncommon 60% Rare 30% Legendary

                    break;
                case KeysRoom.BorysHQDoor:      //END. doesn't have to be anything lol
                    return;
            }


        }
    }
}
