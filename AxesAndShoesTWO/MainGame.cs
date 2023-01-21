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
            loadLabel.Text = "I know not with what weapons World War III will be fought, but World War IV will be fought with sticks and stones. \n -Albert Einstein";
            loadLabel.Size = new Size(WidthSet, HeightSet);
            loadLabel.TextAlign = ContentAlignment.MiddleCenter;
            loadPanel.Controls.Add(loadLabel);
            loadPanel.Location = new Point(0, 0);
            loadPanel.Size = new Size(WidthSet, HeightSet);
            loadPanel.BackColor = Color.Black;

            statsPanel.Location = new Point(WidthSet / 2 + WidthSet / 4, HeightSet / 2 + HeightSet / 4);
            statsPanel.Visible = false;
            loadPanel.Visible = false;

            mainGamePanel.Size = new Size(WidthSet, HeightSet);
            PictureBox logoPicBox = new PictureBox();
            Button newGameButton = new Button();
            Button testButton1 = new Button();
            Button testButton2 = new Button(); //change these asap :)

            logoPicBox.Location = new Point(WidthSet / 4, HeightSet / 4);
            logoPicBox.Size = new Size(960, 540);

            newGameButton.Location = new Point(WidthSet - WidthSet / 2  -WidthSet/6,  HeightSet / 2);
            newGameButton.Size = new Size(320,135);
            mainGamePanel.Controls.Add(logoPicBox);
            mainGamePanel.Controls.Add(newGameButton);

            this.Controls.Add(statsPanel);
            this.Controls.Add(mainGamePanel);
            this.Controls.Add(loadPanel);
            loadColor = 0;

           
           




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
            loadPanel.Visible = false;
            CreateMessage("This is a test");
        }
        
        public void CreateMessage(string Message)
        {
            Panel panelMessage = new Panel();
            Button OKButton = new Button();
            Label labelMessage = new Label();
            panelMessage.Size = new Size(500, 500);
            panelMessage.Location = new Point(30, 30);
            panelMessage.BackColor = Color.White;
            panelMessage.Visible = true;

            panelMessage.Controls.Add(labelMessage);
            panelMessage.Controls.Add(OKButton);

            labelMessage.Text = Message;
            labelMessage.Location = new Point(0, 0);
            labelMessage.AutoSize = true;
            
            OKButton.Text = "OK";
            OKButton.Location = new Point(panelMessage.Size.Width - panelMessage.Size.Width / 8, panelMessage.Size.Height - panelMessage.Size.Height / 8);
            OKButton.Click += new EventHandler(OKButton_Click);            
            this.Controls.Add(panelMessage);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Parent.Visible = false;
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
