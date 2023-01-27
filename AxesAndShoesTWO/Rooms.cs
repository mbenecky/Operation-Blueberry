using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        public Image img;
        public Rooms() { }
        
    }
}
