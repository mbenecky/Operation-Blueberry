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

        StatsPanel statsPanel = new StatsPanel(1920, 1080);
        public static Label loadLabel = new Label();
        public static Panel loadPanel = new Panel();
        public static Panel mainGamePanel = new Panel();
        public MainGame()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            loadLabel.Text = "I know not with what weapons World War III will be fought, but World War IV will be fought with sticks and stones. \n -Albert Einstein";
            loadLabel.Size = new Size(WidthSet, HeightSet);
            loadLabel.TextAlign = ContentAlignment.MiddleCenter;
            loadPanel.Controls.Add(loadLabel);
            loadPanel.Location = new Point(0, 0);
            loadPanel.Size = new Size(WidthSet, HeightSet);
            loadPanel.BackColor = Color.Black;

            statsPanel.Location = new Point(WidthSet / 2 + WidthSet / 4, HeightSet / 2 + HeightSet / 4);
            statsPanel.Visible = false;
            this.Controls.Add(statsPanel);
            this.Controls.Add(loadPanel);
            loadColor = 0;

            Task.Run(() => loadTimer_Tick());
            Task.Run(() => mainGameTimer_Tick());
        }

        public static int loadColor;

        async Task mainGameTimer_Tick()
        {
            while (true)
            {
                await Task.Delay(16); //Main game is set to ~60 fps
            }
        }
        async Task loadTimer_Tick()
        {
            while (loadColor < 255)
            {
                loadLabel.ForeColor = Color.FromArgb(loadColor, loadColor, loadColor);
                loadLabel.Refresh();
                loadColor += 10;
                await Task.Delay(50);
            }
            await Task.Delay(5000);
            loadPanel.Dispose();
        }

    }
    public enum Keys
    {
        Catacombs,          //base mistnost na nauceni controls
        ElectricityRoom,    //second level mistnost s mensima enemies
        EngineRoom,         //third level mistnost s vetsima enemies a lepsimy dropy
        VaultDoor,          //fourth a vetsi level mistnost s moznym vstupem jen s gasmaskou
        RogersShrineDoor,   //fifth level mistnost kde najdes jak korky zemre :(
        BorysHQDoor         //last boss kde budes muset porazit sgt. boryho a end :)
    };
}
