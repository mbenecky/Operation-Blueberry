using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using static AxesAndShoesTWO.StatsPanel;
namespace AxesAndShoesTWO
{
    public partial class MainGame : Form
    {
        public int WidthSet = 1920, HeightSet = 1080;
        public MainGame()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            mainGameTimer.Start();
            loadTimer.Start();
        }
        StatsPanel statsPanel = new StatsPanel(1920, 1080);
        public static Thread mainGameTimer = new Thread(new ThreadStart(mainGameTimer_Tick));
        public static Thread loadTimer = new Thread(new ThreadStart(loadTimer_Tick));
        public static Label loadLabel = new Label();
        public static Panel loadPanel = new Panel();
        public static Panel mainGamePanel = new Panel();
        public static int loadColor = 0;
        static void mainGameTimer_Tick()
        {
            Thread.Sleep(16); //Main game is set to ~60 fps
        }
        static void loadTimer_Tick()
        {
            if (loadColor >= 255)
            {
                Thread.Sleep(2000);
                loadPanel.Dispose();
                loadTimer.Abort();
            } else
            {
                loadLabel.BackColor = Color.FromArgb(loadColor);
                Thread.Sleep(50);
                loadColor += 10;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadLabel.Location = new Point(WidthSet/2, HeightSet/2);
            loadLabel.Text = "I know not with what weapons World War III will be fought, but World War IV will be fought with sticks and stone \n -Albert Einstein";
            loadPanel.Controls.Add(loadLabel);
            loadPanel.Location = new Point(0, 0);
            loadPanel.Size = new Size(WidthSet, HeightSet);
            loadPanel.BackColor = Color.Black;

            statsPanel.Location = new Point(WidthSet/2 + WidthSet/4, HeightSet/2 + HeightSet/4);
            statsPanel.Visible = false;
            this.Controls.Add(statsPanel);
            this.Controls.Add(loadPanel);
        }
        
    }
}
