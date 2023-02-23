using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Consumables : Items
    {
        public TypeOfCons TypeOf;
        public int HealthAdd;
        public int DrinkAdd;
        public int FoodAdd;
        public int RadAdd;

        public Consumables()
        {

        }
        public Consumables(int ID, string Name, string Description, Rarity Rarity, Image img, TypeOfCons TypeOf, int HAdd, int DAdd, int FAdd, int RAdd) : base(-1, "NULL", "NULL", Rarity.Common, Properties.Resources.whitePanel)
        {
            base.ID = ID;
            base.Name = Name;
            base.Description = Description;
            base.Rarity = Rarity;
            base.img = img;

            this.TypeOf = TypeOf;
            this.HealthAdd= HAdd;
            this.DrinkAdd= DAdd;    
            this.FoodAdd= FAdd;
            this.RadAdd= RAdd;
        }

    }
    
}
