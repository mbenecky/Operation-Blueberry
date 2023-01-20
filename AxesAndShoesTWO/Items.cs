using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxesAndShoesTWO
{
    public class Items
    {
        
        public int ID;
        public string Name;
        public string Description;
        public int Count;
        public int MaxCount;
        public Items() { }
        public Items(int ID, string Name, string Description, int Count, int MaxCount)
        {
            this.ID = ID;
            this.Name= Name;
            this.Description= Description;
            this.Count = Count;
            this.MaxCount = MaxCount;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    
}
