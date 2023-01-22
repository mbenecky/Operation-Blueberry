﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
        public int loadColor = 0;
        public int loadOpacity = 255;

        public MainGame()
        {
            InitializeComponent();

            if (File.Exists("logOperation.txt"))
            {
                File.Delete("logOperation.txt");
            }


            loadPanel.Location = new Point(0, 0);
            loadPanel.Size = new Size(WidthSet, HeightSet);
            loadPanel.BackColor = Color.Black;

            loadLabel.Text = "I know not with what weapons World War III will be fought, but World War IV will be fought with sticks and stones. \n -Albert Einstein";
            loadLabel.Size = new Size(WidthSet, HeightSet);
            loadLabel.TextAlign = ContentAlignment.MiddleCenter;
            loadPanel.Controls.Add(loadLabel);
            

            statsPanel.Location = new Point(WidthSet / 2 + WidthSet / 4, HeightSet / 2 + HeightSet / 4);
            statsPanel.Visible = false;
            loadPanel.Visible = false;

            mainGamePanel.Size = new Size(WidthSet, HeightSet);

            PictureBox logoPicBox = new PictureBox();
            Button newGameButton = new Button();
            Button optionsButton = new Button();
            Button creditsButton = new Button(); //change these asap :)

            logoPicBox.Location = new Point(WidthSet / 4, HeightSet / 4-200);
            logoPicBox.Size = new Size(960, 270);
            logoPicBox.Image = Properties.Resources.OPbLUEBERRYTEMPLOGO2;
            logoPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPicBox.BackColor = Color.Transparent;

            newGameButton.Size = new Size(320, 135);
            newGameButton.Location = new Point(WidthSet/2 - newGameButton.Size.Width/2,  HeightSet / 2); 
            newGameButton.BackgroundImage = Properties.Resources.buttonTemp;
            newGameButton.Click += new EventHandler(newGameButton_Click);

            optionsButton.Size = newGameButton.Size;
            optionsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2, HeightSet / 2 +newGameButton.Size.Height + HeightSet/20);
            optionsButton.BackgroundImage = Properties.Resources.buttonTemp;

            creditsButton.Size = newGameButton.Size;
            creditsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2,HeightSet / 2 + newGameButton.Size.Height + optionsButton.Size.Height +HeightSet / 10);
            creditsButton.BackgroundImage = Properties.Resources.buttonTemp;

            mainGamePanel.Controls.Add(logoPicBox);
            mainGamePanel.Controls.Add(newGameButton);
            mainGamePanel.Controls.Add(optionsButton);
            mainGamePanel.Controls.Add(creditsButton);


            this.Controls.Add(statsPanel);
            this.Controls.Add(mainGamePanel);
            this.Controls.Add(loadPanel);

            Task.Run(() => mainGameTimer_Tick());
        }

        
        //START OF TASKS
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
                loadColor += 10;
                await Task.Delay(100);
                Log("Task was delayed successfuly and the code of loadColor is: " + loadColor.ToString());
            }
            await Task.Delay(5000);
            while(loadOpacity >0)
            {
                loadPanel.BackColor = Color.FromArgb(loadOpacity, Color.Black);
                loadOpacity -= 5;
                await Task.Delay(50);
                Log("Task was delayed successfuly and the value of loadOpacity is: " + loadOpacity.ToString());
            }
            loadPanel.Visible = false;
        }
        //END OF TASKS
        //START OF METHODS
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


        void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter("logOperation.txt", true))
            {
                if (!File.Exists("logOperation.txt"))
                {
                    File.Create("logOperation.txt");
                }
                sw.WriteLine(DateTime.Now + ": " + message);
            }
        }

        //END OF METHODS
        //START OF EVENTS

        private async void newGameButton_Click(object sender, EventArgs e)
        {
            loadPanel.Visible = true;
            mainGamePanel.Visible = false;
            await loadTimer_Tick();
        }






        private void OKButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Parent.Visible = false;
        }
        //END OF EVENTS
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
