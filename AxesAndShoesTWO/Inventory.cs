using System.Collections.Generic;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Inventory
    {
        public string Name;
        public List<Items> Storage;
        public Image img;
        public Inventory()
        {
            Storage = new List<Items>();
            Name = "Base Name";
            img = Properties.Resources.crateTest;
        }
        public Inventory(List<Items> Storage, string Name)
        {
            this.Storage = Storage;
            this.Name = Name;
            img = Properties.Resources.crateTest;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
