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
using System.Media;

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

        public static List<Clothes> Clothes = new List<Clothes>();
        public static List<Items> pItems = new List<Items>();
        public static Guns pGuns = new Guns();
        public static List<KeysRoom> pKeys = new List<KeysRoom>();

        public Player CurrentPlayer = new Player(85, 30, 30, 0, Clothes, pItems, pGuns, pKeys);

        public static List<Characters> Chars = new List<Characters>();
        public static List<string> CharacterInteractions = new List<string>();
        public static List<Items> AllItems = new List<Items>();
        public static List<Enemy> AllEnemies = new List<Enemy>();
        public static List<Rooms> AllRooms = new List<Rooms>();

        public static Panel InventoryToStorage = new Panel();
        public static Panel InventorySpace = new Panel();
        public static Panel StorageSpace = new Panel();
        public static PictureBox lastPb = new PictureBox();

        public static Panel CurrentRoom = new Panel();


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

        public bool isSelected = false;
        public bool isWriting = false;
        public bool isPaused = false;

        public MainGame()
        {
            InitializeComponent();
            this.KeyPreview = true;
            if (File.Exists("logOperation.txt"))
            {
                File.Delete("logOperation.txt");
            }
            Chars = CharactersLoad();
            CharacterInteractions = InteractionsLoad();
            AllItems = ItemsLoad();
            AllEnemies= EnemiesLoad();
            AllRooms = RoomsLoad();

            CurrentPlayer.HotBar = (AllItems[1] as Guns);

            CurrentRoom.Size = new Size(WidthSet, HeightSet);
            CurrentRoom.Location = new Point(0, 0);
            CurrentRoom.BackgroundImage = Properties.Resources.roomTest;


            characterInteractButton.Text = "Next";
            characterInteractButton.Location = new Point(WidthSet - WidthSet / 10, HeightSet - HeightSet / 10);
            characterInteractButton.Size = new Size(WidthSet / 10, HeightSet / 10);
            characterInteractButton.BackgroundImage = Properties.Resources.buttonTemp;


            characterInteractPanel.Size = new Size(WidthSet, HeightSet);
            characterInteractPanel.BackgroundImage = Chars[0].img;
            characterInteractPanel.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.Click += new EventHandler(interactButton_Click);

            characterInteractLabel.Location = new Point(0, HeightSet / 2);
            characterInteractLabel.BackColor = Color.Transparent;
            characterInteractLabel.Font = new Font(characterInteractLabel.Font.FontFamily, 36);
            characterInteractLabel.Size = new Size(WidthSet - WidthSet / 10, HeightSet);
            characterInteractLabel.MaximumSize = new Size(WidthSet - WidthSet / 10, HeightSet);
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
            logoPicBox.Size = new Size(WidthSet / 2, HeightSet / 4);
            logoPicBox.Image = Properties.Resources.OPbLUEBERRYTEMPLOGO2;
            logoPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPicBox.BackColor = Color.Transparent;

            newGameButton.Size = new Size(WidthSet / 6, HeightSet / 8);
            newGameButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2, HeightSet / 2);
            newGameButton.BackgroundImage = Properties.Resources.buttonTemp;
            newGameButton.Click += new EventHandler(newGameButton_Click);

            optionsButton.Size = newGameButton.Size;
            optionsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2, HeightSet / 2 + newGameButton.Size.Height + HeightSet / 20);
            optionsButton.BackgroundImage = Properties.Resources.buttonTemp;

            creditsButton.Size = newGameButton.Size;
            creditsButton.Location = new Point(WidthSet / 2 - newGameButton.Size.Width / 2, HeightSet / 2 + newGameButton.Size.Height + optionsButton.Size.Height + HeightSet / 10);
            creditsButton.BackgroundImage = Properties.Resources.buttonTemp;

            mainGamePanel.BackColor = Color.FromArgb(64, 64, 64);

            mainGamePanel.Controls.Add(logoPicBox);
            mainGamePanel.Controls.Add(newGameButton);
            mainGamePanel.Controls.Add(optionsButton);
            mainGamePanel.Controls.Add(creditsButton);

            mapPanel.BackgroundImage = Properties.Resources.mapBackGround;
            mapPanel.Size = new Size(WidthSet, HeightSet);
            mapPanel.Location = new Point(0, 0);

            InventoryToStorage.Size = new Size(1920, 1080);
            InventoryToStorage.Location = new Point(0, 0);
            InventoryToStorage.BackgroundImage = Properties.Resources.mapBackGround;

            InventorySpace.Size = new Size(WidthSet / 2, HeightSet);
            InventorySpace.Location = new Point(0, 0);
            InventorySpace.BackColor = Color.Transparent;

            StorageSpace.Size = new Size(WidthSet / 2, HeightSet);
            StorageSpace.Location = new Point(WidthSet / 2, 0);
            StorageSpace.BackColor = Color.Transparent;

            InventoryToStorage.Controls.Add(InventorySpace);
            InventoryToStorage.Controls.Add(StorageSpace);

            for(int i = 0; i != 5;i++)
            {
                for(int j = 0;j!=2;j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImage = Properties.Resources.backgroundItem;
                    pb.Size = new Size(WidthSet / 20, HeightSet / 10);
                    pb.Location = new Point((WidthSet / 20) * j + WidthSet / 4 + WidthSet / 16, (HeightSet / 10) * i + HeightSet / 4);
                    pb.Tag = "0";
                    pb.Click += new EventHandler(inventoryCheck);
                    InventorySpace.Controls.Add(pb);
               }
            }

            for (int i = 0; i != 5; i++)
            {
                for (int j = 0; j != 5; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImage = Properties.Resources.backgroundItem;
                    pb.Size = new Size(WidthSet / 20, HeightSet / 10);
                    pb.Location = new Point((WidthSet / 20) * j + WidthSet / 16, (HeightSet / 10) * i + HeightSet / 4);
                    pb.Tag = "0";
                    pb.Click += new EventHandler(inventoryCheck);
                    StorageSpace.Controls.Add(pb);
                }
            }
            InventoryToStorage.Visible = false;

            //Test Batch
            InventorySpace.Controls[0].Tag = "1";
            (InventorySpace.Controls[0] as PictureBox).Image = Properties.Resources.gunTest;
            InventorySpace.Controls[1].Tag = "16";
            (InventorySpace.Controls[1] as PictureBox).Image = Properties.Resources.itemTest;
            //Test Batch

            this.Controls.Add(loadPanel);
            this.Controls.Add(characterInteractPanel);
            this.Controls.Add(mainGamePanel);
            this.Controls.Add(statsPanel);
            this.Controls.Add(CurrentRoom);
            this.Controls.Add(InventoryToStorage);

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
                await Task.Delay(1);
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
                await Task.Delay(1);

            }
            await Task.Delay(1);
            characterInteractPanel.Visible = true;
            while (loadOpacity > 0)
            {
                loadPanel.BackColor = Color.FromArgb(loadOpacity, Color.Black);
                loadOpacity -= 5;
                await Task.Delay(1);
            }
            loadLabel.Visible = false;
            await Task.Delay(1);
            loadPanel.Visible = false;
            
        }
        async Task Reloading()
        {
            SoundPlayer Reload = new SoundPlayer(Properties.Resources.reload);
            Reload.PlaySync();
            CurrentPlayer.HotBar.isAbleToShoot = false;
            await Task.Delay(CurrentPlayer.HotBar.WaitTime);
            CurrentPlayer.HotBar.isAbleToShoot = true;
            CurrentPlayer.HotBar.CurrentAmountOfRounds = CurrentPlayer.HotBar.NumberOfRounds;
        }
        async Task WaitBetweenShots()
        {
            SoundPlayer Gunshot = new SoundPlayer(Properties.Resources.gunshot);
            Gunshot.PlaySync();
            CurrentPlayer.HotBar.isAbleToShoot = false;
            await Task.Delay(CurrentPlayer.HotBar.WaitTime / 10);
            CurrentPlayer.HotBar.isAbleToShoot = true;
        }
        async Task Death(object sender)
        {

            PictureBox pb = new PictureBox();
         
            pb = (sender as PictureBox);
            pb.Click -= (pbClick);
            pb.Size = Properties.Resources.deathImage.Size;
            pb.Image = Properties.Resources.deathImage;
            pb.Location = new Point(pb.Location.X,pb.Location.Y+pb.Size.Height);
            await Task.Delay(2000);
            pb.Dispose();
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
            list.Add(new Guns(1, "The Enforcer", "Standard handgun for all situations", Rarity.Common, Properties.Resources.gunTest, 12, 2, 3000));
            //ID: 12, Název: "The Hellraiser", Deskripce: "Silný revolver s vysokým poškozením na krátkou vzdálenost", Rarita: Legendary, Velikost zásobníku: 6, Damage: 5
            list.Add(new Guns(2, "The Hellraiser", "Strong revolver with high damage and blowback", Rarity.Legendary, Properties.Resources.gunTest, 6, 5, 3000));
            list.Add(new Items(16, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(17, "First Aid Kit", "Tool for immediate assistance in injuries", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(18, "Water Bottle", "Standard source of drinking water for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(19, "Energy Bar", "Quick source of energy for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(20, "Flashlight", "Tool for lighting in dark areas", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(21, "Tent", "Sleeping space for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(22, "Sleeping Bag", "Sleeping space for combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(23, "Matches", "Tool for lighting fire", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(24, "Cooking Pot", "Tool for cooking food in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(25, "Rope", "Tool for climbing and pulling things", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(26, "Multi-Tool", "Tool for various purposes in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(27, "Portable Generator", "Tool for generating electric energy in combat situations", Rarity.Rare, Properties.Resources.itemTest));
            list.Add(new Items(28, "Duct Tape", "Tool for repairing things in combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(29, "Portable Water Filter", "Tool for filtering water in combat situations", Rarity.Uncommon, Properties.Resources.itemTest));
            list.Add(new Items(30, "Firestarter", "Tool for easy lighting of fire in combat situations", Rarity.Common, Properties.Resources.itemTest)); return list;
        }
        public List<Enemy> EnemiesLoad()
        {
            List<Enemy> list = new List<Enemy>();

            list.Add(new Enemy(
                "Medved", 10, Rarity.Common, Enemy.Types.Ground,new Size(WidthSet/4,HeightSet/4),Properties.Resources.enemyTestnew, 50, Properties.Resources.enemyDeathTest
                ));

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

        public List<Rooms> RoomsLoad()
        {
            List<Rooms> list = new List<Rooms>();

            list.Add(new Rooms(1, "Catacombs", "NULL", KeysRoom.Catacombs, KeysRoom.ElectricityRoom, Properties.Resources.roomTest));
            list.Add(new Rooms(2, "Electricity Room", "NULL", KeysRoom.ElectricityRoom, KeysRoom.EngineRoom, Properties.Resources.roomTest));
            list.Add(new Rooms(3, "Engine Room", "NULL", KeysRoom.EngineRoom, KeysRoom.VaultDoor, Properties.Resources.roomTest));
            list.Add(new Rooms(4, "Vault Air-Lock", "NULL", KeysRoom.VaultDoor, KeysRoom.RogersShrineDoor, Properties.Resources.roomTest));
            list.Add(new Rooms(5, "Roger's Shrine", "NULL", KeysRoom.RogersShrineDoor, KeysRoom.BorysHQDoor, Properties.Resources.roomTest));
            list.Add(new Rooms(6, "Blueberry's HeadQuarters Room", "NULL", KeysRoom.BorysHQDoor, KeysRoom.BorysHQDoor, Properties.Resources.roomTest));

            return list;

        }

        public void SpawnARoom(Rooms GivenRoom)
        {
            Random rnd = new Random();
            GivenRoom.Drops = GivenRoom.CreateDrops(GivenRoom.RequiredKey, AllItems);
            GivenRoom.Enemies = GivenRoom.CreateEnemies(GivenRoom.RequiredKey, AllEnemies);
            int i = 0;
            foreach (Enemy enemy in GivenRoom.Enemies)
            {
                PictureBox pb = new PictureBox();
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Image = enemy.Img;
                pb.BackColor = Color.FromArgb(0, Color.Transparent) ;
                pb.BackgroundImage = null;


                pb.Size = enemy.Size;
                pb.Tag = enemy.Health.ToString();
                if(enemy.Type == Enemy.Types.Ground)
                {
                    pb.Location = new Point(rnd.Next(0,WidthSet-enemy.Size.Width), HeightSet - enemy.Size.Height);
                } else
                {
                    pb.Location = new Point(rnd.Next(0, WidthSet - enemy.Size.Width), HeightSet / 8);
                }
                pb.Click += new EventHandler(pbClick);
                CurrentRoom.Controls.Add(pb);
                i++;
            }
        }

        //END OF METHODS
        //START OF EVENTS
        private void inventoryCheck(object sender, EventArgs e)
        {
            PictureBox pictureBox =(sender as PictureBox);
            if (!isSelected)
            {
                if(pictureBox.Tag.ToString() != "0")
                {
                    lastPb = pictureBox;
                }
                else { return; }
                isSelected = true;
                foreach(Control c in InventorySpace.Controls)
                {
                    if ((c.Tag).ToString() == "0")
                    {
                        c.BackgroundImage = Properties.Resources.backgroundItemFree;
                    }
                }
                foreach (Control c in StorageSpace.Controls)
                {
                    if ((c.Tag).ToString() == "0")
                    {
                        c.BackgroundImage = Properties.Resources.backgroundItemFree;
                    }
                }
                return;
            }
            if(pictureBox.Tag.ToString() == "0")
            {
                (sender as PictureBox).Image = lastPb.Image;
                (sender as PictureBox).Tag = lastPb.Tag;
                lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                lastPb.Tag = "0";
                lastPb.Image = null;
            } 
            isSelected = false;
            foreach (Control c in InventorySpace.Controls)
            {
                c.BackgroundImage = Properties.Resources.backgroundItem;
            }
            foreach (Control c in StorageSpace.Controls)
            {
                c.BackgroundImage = Properties.Resources.backgroundItem;
            }
            
        }
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
                    SpawnARoom(AllRooms[0]);
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
            Log("Something was pressed!");
            switch(e.KeyCode)
            {
                case Keys.Escape: break; //pauses
                case Keys.M: break; //Opens map
                case Keys.E: InventoryToStorage.Visible = !InventoryToStorage.Visible; break; //opens inventory
                case Keys.R: break; //reload
                case Keys.F: statsPanel.Visible = !statsPanel.Visible; break;
            }
        }
        private void pbClick(object sender, EventArgs e)
        {
            if(CurrentPlayer.HotBar.isAbleToShoot) {
               
                (sender as PictureBox).Tag = Convert.ToInt32((sender as PictureBox).Tag) - CurrentPlayer.HotBar.Damage*5;
                if (Convert.ToInt32((sender as PictureBox).Tag) <= 0)
                {
                    try { 
                     Death(sender);
                    }
                    catch(Exception ex)
                    {
                        Log("Something went wrong when trying the Death() method, ex code: " + ex.Message);
                    }
                }
                CurrentPlayer.HotBar.CurrentAmountOfRounds--;
                if (CurrentPlayer.HotBar.CurrentAmountOfRounds == 0)
                {
                    Task.Run(() => Reloading());
                } else
                {
                    Task.Run(() => WaitBetweenShots());
                }
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
        Catacombs = 1,          //base mistnost na nauceni controls
        ElectricityRoom = 2,    //second level mistnost s mensima enemies
        EngineRoom = 3,         //third level mistnost s vetsima enemies a lepsimy dropy
        VaultDoor = 4,          //fourth a vetsi level mistnost s moznym vstupem jen s gasmaskou
        RogersShrineDoor = 5,   //fifth level mistnost kde najdes jak korky zemre :(
        BorysHQDoor = 6         //last boss kde budes muset porazit sgt. boryho a end :)
    };
}
