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
        public int Damage;
        public int WaitTime;
        public Guns() { }
        public Guns(int ID, string Name, string Description, int Count, int MaxCount, int NumberOfRounds, int Damage, int WaitTime) : base(-1,"NULL", "NULL", 0, 0)
        {
            base.ID = ID;
            base.Name = Name;
            base.Description = Description; 
            base.Count = Count;
            base.MaxCount = MaxCount;
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
