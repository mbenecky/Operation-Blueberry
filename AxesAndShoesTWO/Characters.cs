
using System.Drawing;
using System.Windows.Forms;

namespace AxesAndShoesTWO
{
    public class Characters
    {
        public string Name;
        public Image img;
        public Characters(string Name, Image img)
        {
            this.Name = Name;
            this.img = img;
        }
        public Characters()
        {

        }
    }
}
