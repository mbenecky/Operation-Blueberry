using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AxesAndShoesTWO
{
    public class Guns : Items
    {
        public int NumberOfRounds;
        public Guns() { }
        public Guns(int ID, string Name, string Description, int Count, int MaxCount, int NumberOfRounds) : base(-1,"NULL", "NULL", 0, 0)
        {
            base.ID = ID;
            base.Name = Name;
            base.Description = Description; 
            base.Count = Count;
            base.MaxCount = MaxCount;
            this.NumberOfRounds = NumberOfRounds;
        }
        public override string ToString()
        {
            return base.Name;
        }
    }
}
