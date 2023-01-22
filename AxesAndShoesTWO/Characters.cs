
using System.Drawing;
using System.Windows.Forms;

namespace AxesAndShoesTWO
{
    public class Characters
    {
        public string Name;
        public string Text;
        public Image img;
        public Characters(string Name, string Text, Image img)
        {
            this.Name = Name;
            this.Text = Text;
            this.img = img;
        }
        public Characters()
        {

        }
    }
}
