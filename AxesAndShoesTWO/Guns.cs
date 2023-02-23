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
        public int CurrentAmountOfRounds;
        public int WaitTime; //in milisecodns
        public int WBSTime;
        public bool isAbleToShoot;
        public Guns() { }
        public Guns(int ID, string Name, string Description, Rarity Rarity, Image img, int NumberOfRounds, int Damage, int WaitTime, int WBSTime) : base(0, "NULL", "NULL", Rarity.Common, Properties.Resources.whitePanel)
        {
            base.ID = ID;
            base.Name = Name;
            base.Description = Description;
            base.Rarity = Rarity;
            base.img = img;
            this.NumberOfRounds = NumberOfRounds;
            this.Damage = Damage;
            this.WaitTime = WaitTime;
            this.WBSTime = WBSTime;
            isAbleToShoot = true;
            CurrentAmountOfRounds = NumberOfRounds;
        }
        
        public override string ToString()
        {
            return base.Name;
        }
    }
}
