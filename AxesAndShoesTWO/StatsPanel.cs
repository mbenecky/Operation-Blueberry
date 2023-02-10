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
        public  PictureBox healthBar = new PictureBox();
        public  PictureBox thirstBar = new PictureBox();
        public  PictureBox hungerBar = new PictureBox();
        public  PictureBox radiationBar = new PictureBox();
        public StatsPanel(int Width, int Height)
        {
            InitializeComponent();

            healthBar.Location = new Point(0, 0);
            thirstBar.Location = new Point(0,  Height / 32);
            hungerBar.Location = new Point(0, (Height / 32) * 2);
            radiationBar.Location = new Point(0, (Height / 32) * 3);

            healthBar.Size = new Size(Width/16, Height / 32);
            thirstBar.Size = new Size(Width / 16, Height / 32);
            hungerBar.Size = new Size(Width / 16, Height / 32);
            radiationBar.Size = new Size(Width / 16, Height / 32);

            healthBar.Image = Properties.Resources.healthBar;
            thirstBar.Image = Properties.Resources.thirstBar;
            hungerBar.Image = Properties.Resources.hungerBar;
            radiationBar.Image = Properties.Resources.radiationBar;

            this.BackColor = Color.Black;

            healthBar.SizeMode = PictureBoxSizeMode.StretchImage;
            thirstBar.SizeMode = PictureBoxSizeMode.StretchImage;
            hungerBar.SizeMode = PictureBoxSizeMode.StretchImage;
            radiationBar.SizeMode = PictureBoxSizeMode.StretchImage;
            
            this.Controls.Add(healthBar);
            this.Controls.Add(thirstBar);
            this.Controls.Add(hungerBar);
            this.Controls.Add(radiationBar);

            this.Size = new Size(Width/8,Height/8);
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
