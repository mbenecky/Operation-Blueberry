using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxesAndShoesTWO
{
    public partial class StatsPanel : Control
    {
        public static PictureBox healthBar = new PictureBox();
        public static PictureBox thirstBar = new PictureBox();
        public static PictureBox hungerBar = new PictureBox();
        public StatsPanel(int Width, int Height)
        {
            InitializeComponent();
            
            healthBar.Location = new Point(0, 0);
            thirstBar.Location = new Point(0,  Height / 16);
            hungerBar.Location = new Point(0, (Height / 16) * 2);

            healthBar.Size = new Size(Width/4, Height / 16);
            thirstBar.Size = new Size(Width/4, Height / 16);
            hungerBar.Size = new Size(Width/4, Height / 16);

            healthBar.Image = Properties.Resources.blackPanel;
            thirstBar.Image = Properties.Resources.whitePanel;
            hungerBar.Image = Properties.Resources.blackPanel;

            healthBar.SizeMode = PictureBoxSizeMode.StretchImage;
            thirstBar.SizeMode = PictureBoxSizeMode.StretchImage;
            hungerBar.SizeMode = PictureBoxSizeMode.StretchImage;
            
            
            this.Controls.Add(healthBar);
            this.Controls.Add(thirstBar);
            this.Controls.Add(hungerBar);

            this.Size = new Size(480,270);
            MessageBox.Show(healthBar.Size.ToString());
            MessageBox.Show(healthBar.Location.ToString());
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
