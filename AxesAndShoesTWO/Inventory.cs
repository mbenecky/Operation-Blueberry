using System.Collections.Generic;
namespace AxesAndShoesTWO
{
    public class Inventory
    {
        public string Name;
        public List<Items> Storage;
        public Inventory()
        {
            Storage = new List<Items>();
            Name = "Base Name";
        }
        public Inventory(List<Items> Storage, string Name)
        {
            this.Storage = Storage;
            this.Name = Name;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
