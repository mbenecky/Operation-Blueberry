using System;
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
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED      
                return handleParam;
            }
        }
        public int WidthSet = Screen.PrimaryScreen.Bounds.Width, HeightSet = Screen.PrimaryScreen.Bounds.Height;

        StatsPanel statsPanel = new StatsPanel(1920, 1080);

        public static List<Characters> Chars = new List<Characters>();
        public static List<string> CharacterInteractions = new List<string>();

        public static Label loadLabel = new Label();
        public static Panel loadPanel = new Panel();
        public static Panel mainGamePanel = new Panel();

        public static Panel mapPanel = new Panel();
        public static PictureBox catacombsPB = new PictureBox();
        public static PictureBox electricityRoomPB = new PictureBox();
        public static PictureBox engineRoomPB = new PictureBox();
        public static PictureBox entrancePB = new PictureBox();
        public static PictureBox rogersShrinePB = new PictureBox();
        public static PictureBox boryHQPB = new PictureBox();

        public static Panel characterInteractPanel = new Panel();
        public static Label characterInteractLabel = new Label();
        public static Label characterInteractLabelName = new Label();
        public static Button characterInteractButton = new Button();

        public int loadColor = 0;
        public int loadOpacity = 255;
        public int currentInteraction = 0;

        public int currentMoney = 10;

        public bool isWriting = false;
        public bool isPaused = false;

        public MainGame()
        {
            InitializeComponent();

            if (File.Exists("logOperation.txt"))
            {
                File.Delete("logOperation.txt");
            }
            MessageBox.Show(WidthSet.ToString() + " " + HeightSet.ToString());
            Chars = CharactersLoad();
            CharacterInteractions = InteractionsLoad();

            characterInteractButton.Text = "Next";
            characterInteractButton.Location = new Point(WidthSet - WidthSet / 10, HeightSet -HeightSet / 10);
            characterInteractButton.Size = new Size(WidthSet / 10, HeightSet / 10);
            characterInteractButton.BackgroundImage = Properties.Resources.buttonTemp;


            characterInteractPanel.Size = new Size(WidthSet, HeightSet);
            characterInteractPanel.BackgroundImage = Chars[0].img;
            characterInteractPanel.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.Click += new EventHandler(interactButton_Click);

            characterInteractLabel.Location = new Point(0,HeightSet/2);
            characterInteractLabel.BackColor = Color.Transparent;
            characterInteractLabel.Font = new Font(characterInteractLabel.Font.FontFamily, 36);
            characterInteractLabel.Size = new Size(WidthSet - WidthSet / 10, HeightSet);
            characterInteractLabel.MaximumSize = new Size(WidthSet-WidthSet/10, HeightSet);
            characterInteractLabel.BackColor = Color.Transparent;

            characterInteractLabelName.Text = Chars[0].Name;
            characterInteractLabelName.AutoSize = true;
            characterInteractLabelName.Location = new Point(0, 0);
            characterInteractLabelName.BackColor = Color.Transparent;
            characterInteractLabelName.Font = new Font(characterInteractLabelName.Font.FontFamily, 50);

            characterInteractPanel.Visible = false;

            characterInteractPanel.Controls.Add(characterInteractButton);
            characterInteractPanel.Controls.Add(characterInteractLabel);
            characterInteractPanel.Controls.Add(characterInteractLabelName);

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

            logoPicBox.Location = new Point(WidthSet / 4, HeightSet / 6);
            logoPicBox.Size = new Size(WidthSet/2, HeightSet/4);
            logoPicBox.Image = Properties.Resources.OPbLUEBERRYTEMPLOGO2;
            logoPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPicBox.BackColor = Color.Transparent;

            newGameButton.Size = new Size(WidthSet/6, HeightSet/8);
            newGameButton.Location = new Point(WidthSet/2 - newGameButton.Size.Width/2,  HeightSet / 2); 
            newGameButton.BackgroundImage = Properties.Resources.buttonTemp;
            newGameButton.Click += new EventHandler(newGameButton_Click);

            optionsButton.Size = newGameButton.Size;
            optionsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2, HeightSet / 2 +newGameButton.Size.Height + HeightSet/20);
            optionsButton.BackgroundImage = Properties.Resources.buttonTemp;

            creditsButton.Size = newGameButton.Size;
            creditsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2,HeightSet / 2 + newGameButton.Size.Height + optionsButton.Size.Height +HeightSet / 10);
            creditsButton.BackgroundImage = Properties.Resources.buttonTemp;

            mainGamePanel.BackColor = Color.FromArgb(64, 64, 64);

            mainGamePanel.Controls.Add(logoPicBox);
            mainGamePanel.Controls.Add(newGameButton);
            mainGamePanel.Controls.Add(optionsButton);
            mainGamePanel.Controls.Add(creditsButton);

            mapPanel.BackgroundImage = Properties.Resources.mapBackGround;
            mapPanel.Size = new Size(WidthSet, HeightSet);
            mapPanel.Location = new Point(0, 0);

            this.Controls.Add(loadPanel);
            this.Controls.Add(characterInteractPanel);
            this.Controls.Add(statsPanel);
            this.Controls.Add(mainGamePanel);
            

            Task.Run(() => mainGameTimer_Tick());
        }


        //START OF TASKS
        async Task writeOutLines(string Message)
        {
            if(isWriting) { currentInteraction--; return; }
            isWriting = true;
            characterInteractLabel.Text = String.Empty;
            for(int i = 0; i!= Message.Length;i++)
            {
                characterInteractLabel.Text+=Message[i];
                await Task.Delay(50);
            }
            isWriting = false;
        }
        async Task mainGameTimer_Tick()
        {
            while (!isPaused)
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
                await Task.Delay(80);

            }
            await Task.Delay(4000);
            characterInteractPanel.Visible = true;
            while (loadOpacity > 0)
            {
                loadPanel.BackColor = Color.FromArgb(loadOpacity, Color.Black);
                loadOpacity -= 5;
                await Task.Delay(50);
            }
            loadLabel.Visible = false;
            await Task.Delay(2000);
            loadPanel.Visible = false;
            
        }
        //END OF TASKS
        //START OF METHODS
        public void CreateMessage(string Message)
        {
            Panel panelMessage = new Panel();
            Button OKButton = new Button();
            Label labelMessage = new Label();
            panelMessage.Size = new Size(WidthSet/4, HeightSet/4);
            panelMessage.Location = new Point(WidthSet-WidthSet/2-WidthSet/8, HeightSet-HeightSet/2-HeightSet/8);
            panelMessage.BackColor = Color.White;
            panelMessage.Visible = true;

            panelMessage.Controls.Add(labelMessage);
            panelMessage.Controls.Add(OKButton);

            labelMessage.Text = Message;
            labelMessage.Location = new Point(0, 0);
            labelMessage.MaximumSize = new Size(WidthSet / 4, HeightSet / 4);
            
            OKButton.Text = "OK";
            OKButton.Location = new Point(panelMessage.Size.Width - panelMessage.Size.Width / 8, panelMessage.Size.Height - panelMessage.Size.Height / 8);
            OKButton.Click += new EventHandler(OKButton_Click);            
            this.Controls.Add(panelMessage);
            panelMessage.BringToFront();
        }


        void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter("logOperation.txt", true))
            {
                sw.WriteLine(DateTime.Now + ": " + message);
            }
        }
        public List<Characters> CharactersLoad()
        {
            List<Characters> list = new List<Characters>();
            Characters SgtBory = new Characters("Sgt. Blueberry",  Properties.Resources.voiceLineTestBedImage);
            Characters EndBossBory = new Characters("Blueberry",  Properties.Resources.voiceLineTestBedImage);
            Characters Korky = new Characters("Mr. Korky",  Properties.Resources.voiceLineTestBedImage);
            Characters Medved = new Characters("Medved",  Properties.Resources.voiceLineTestBedImage);
            Characters Horkymi = new Characters("Horkymi", Properties.Resources.voiceLineTestBedImage);
            Characters Mako = new Characters("Mako", Properties.Resources.voiceLineTestBedImage);
            Characters ChiefPear = new Characters("Chief Pear", Properties.Resources.voiceLineTestBedImage);


            list.Add(SgtBory);
            list.Add(EndBossBory);
            list.Add(Korky);
            list.Add(Medved);
            list.Add(Horkymi);
            list.Add(Mako);
            list.Add(ChiefPear);
            
            return list;
        }
        public List<Items> ItemsLoad()
        {
            List<Items> list = new List<Items>(); 
            list.Add(new Items("Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("First Aid Kit", "Tool for immediate help with injuries", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Water Bottle", "Standard source of drinking water for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("Energy Bar", "Quick source of energy for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Flashlight", "Tool for lighting in dark areas", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("Tent", "Sleeping space for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Sleeping Bag", "Sleeping space for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Matches", "Tool for lighting fire", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("Cooking Pot", "Tool for cooking food in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Rope", "Tool for climbing and pulling things", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("Multi-Tool", "Tool for various purposes in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Portable Generator", "Tool for generating electricity in combat situations", Rarity.Rare, Properties.Resources.itemTest));
            list.Add(new Items("Duct Tape", "Tool for repairing things in combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items("Portable Water Filter", "Tool for filtering water in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items("Firestarter", "Tool for easy lighting of fire in combat situations", Rarity.Common, Properties.Resources.itemTest));
            return list;
        }
        public List<string> InteractionsLoad()
        {
            List<string> list = new List<string>();
            //LIST MA 3 MOZNY STAVY:
            //A) NORMALNI TEXT, NEOBSAHUJICI NEXT
            //B) NEXT s cislem za pro urceni postavy
            // C) END, vypne panel.



            list.Add("Hey, I thought you were dead, we were about to transport you outside, lucky you woke up");
            list.Add("You're in safe hands that's for sure, welcome to the bunker, after the nuclear bombs this is the only place where humans live far and wide...");
            list.Add("Yeah I know... pretty sick. Forgot to introduce myself, I am Sargeant Blueberry, in command of this whole bunker.");
            list.Add("Why don't you get off this ground and help us a bit, will ya?");
            list.Add("END");
            return list;
        }

        public Inventory CreateRandomDrops(Rarity rarity)
        {
            Inventory inventory = new Inventory();
            return inventory;
        }

        //END OF METHODS
        //START OF EVENTS

        private async void newGameButton_Click(object sender, EventArgs e)
        {
            loadPanel.Visible = true;
            mainGamePanel.Visible = false;
            await loadTimer_Tick();
        }

        private async void interactButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (!CharacterInteractions[currentInteraction].Contains("NEXT") && !CharacterInteractions[currentInteraction].Contains("END"))
                {
                    await writeOutLines(CharacterInteractions[currentInteraction]);
                }
                else
                if (CharacterInteractions[currentInteraction].Contains("NEXT"))
                {
                    string[] args = CharacterInteractions[currentInteraction].Split(' ');
                    characterInteractLabel.Text = String.Empty;
                    characterInteractLabelName.Text = Chars[Convert.ToInt32(args[1])].Name;
                    characterInteractPanel.BackgroundImage = Chars[Convert.ToInt32(args[1])].img;
                } else
                {
                    characterInteractPanel.Visible = false;
                }
                currentInteraction++;
            } 
            catch(Exception ex)
            {
                Log("Wrong characterInteraction, Maybe currentInteraction value is wrong?" + ex.Message);
            }
        }

        private void MainGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape: break; //pauses
                case Keys.M: break; //opens map
                case Keys.R: break; //reload
            }
        }

        private void MainGame_Load(object sender, EventArgs e)
        {

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Parent.Visible = false;
        }
        //END OF EVENTS
    }









    public enum Rarity
    {
        Common, //prodej za 3
        Uncommon, //prodej za 6
        Rare, //prodej za 15
        Epic, //prodej za 40
        Legendary //prodej za 125
    };

    public enum KeysRoom
    {   
        Catacombs,          //base mistnost na nauceni controls
        ElectricityRoom,    //second level mistnost s mensima enemies
        EngineRoom,         //third level mistnost s vetsima enemies a lepsimy dropy
        VaultDoor,          //fourth a vetsi level mistnost s moznym vstupem jen s gasmaskou
        RogersShrineDoor,   //fifth level mistnost kde najdes jak korky zemre :(
        BorysHQDoor         //last boss kde budes muset porazit sgt. boryho a end :)
    };
}
