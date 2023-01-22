
using System.Windows.Forms;

namespace AxesAndShoesTWO
{
    public class Characters
    {
        public string Name;
        public string Text;
        public PictureBox pBox;
        public Button OkButton;
        public Characters(string Name, string Text, PictureBox pBox, Button OkButton)
        {
            this.Name = Name;
            this.Text = Text;
            this.pBox = pBox;
            this.OkButton = OkButton;
        }
        public Characters()
        {

        }
    }
}
