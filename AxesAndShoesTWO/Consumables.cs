using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Consumables(TypeOfCons TypeOf, int HAdd, int DAdd, int FAdd, int RAdd) : base(-1, "NULL", "NULL", Rarity.Common, Properties.Resources.whitePanel)
        {

            this.TypeOf = TypeOf;
            this.HealthAdd= HAdd;
            this.DrinkAdd= DAdd;    
            this.FoodAdd= FAdd;
            this.RadAdd= RAdd;
        }

    }
    public enum TypeOfCons
    {
        Health,
        Drink,
        Food,
        Rad
    };
}
