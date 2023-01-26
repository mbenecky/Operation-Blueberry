using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Guns : Items
    {
       
        public int NumberOfRounds;
        public int Damage;
        public int WaitTime;
        public Guns() { }
        public Guns(string Name, string Description, Rarity Rarity,Image img,int NumberOfRounds, int Damage, int WaitTime) : base("NULL", "NULL",Rarity.Common, Properties.Resources.whitePanel)
        {
            base.Name = Name;
            base.Description = Description; 
            base.Rarity = Rarity;
            base.img = img;
            this.NumberOfRounds = NumberOfRounds;
            this.Damage = Damage;
            this.WaitTime = WaitTime;
        }
        public override string ToString()
        {
            return base.Name;
        }
    }
}
