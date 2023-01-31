
using System.Drawing;
using System.Windows.Forms;

namespace AxesAndShoesTWO
{
    public class Characters
    {
        public string Name;
        private int CurrentIteration;
        public Image img;
        public Characters(string Name, Image img)
        {
            this.Name = Name;
            this.img = img;
            CurrentIteration = 0;
        }
        public Characters()
        {
            CurrentIteration = 0;
        }
    }
}
