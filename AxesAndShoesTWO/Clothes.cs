using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Clothes : Items
    {
        public Place Place;
        public int HealthBoost;
        public Clothes(int ID, string Name, string Description, Rarity Rarity, Image img, Place Place, int HealthBoost) : base(-1, "NULL", "NULL", Rarity.Common, Properties.Resources.whitePanel)
        {
            base.ID = ID;
            base.Name = Name;
            base.Description = Description;
            base.Count = Count;
            base.MaxCount = 1;
            base.Rarity = Rarity;
            base.img = img;
            this.Place = Place;
             this.HealthBoost= HealthBoost;
        }
    }
    public enum Place
    { 
        Head,
        Chest,
        Pants,
        Boots
    };

}
