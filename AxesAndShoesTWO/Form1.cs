using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AxesAndShoesTWO.StatsPanel;
namespace AxesAndShoesTWO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        StatsPanel statsPanel = new StatsPanel(1920,1080);
        private void Form1_Load(object sender, EventArgs e)
        {
            statsPanel.Location = new Point(1440, 810);
            this.Controls.Add(statsPanel);
        }
    }
}
