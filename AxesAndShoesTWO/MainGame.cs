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

        public static Clothes[] Clothes = new Clothes[4];
        public static List<Items> pItems = new List<Items>();
        public static Guns pGuns = new Guns();
        public static List<KeysRoom> pKeys = new List<KeysRoom>();

        public Player CurrentPlayer = new Player(85, 30, 30, 0, pGuns, pKeys);

        public static List<Characters> Chars = new List<Characters>();
        public static List<string> CharacterInteractions = new List<string>();
        public static List<Items> AllItems = new List<Items>();
        public static List<Enemy> AllEnemies = new List<Enemy>();
        public static List<Rooms> AllRooms = new List<Rooms>();

        public static Panel InventoryToStorage = new Panel();
        public static Panel InventorySpace = new Panel();
        public static Panel StorageSpace = new Panel();
        public static PictureBox lastPb = new PictureBox();
        public static Panel DropsPanel = new Panel();
        public static Panel PlayerClothes = new Panel();
        public static PictureBox PHotBar = new PictureBox();
        public static Label AmmoLabel = new Label();

        public static Panel CurrentRoom = new Panel();
        public static Rooms CurrentRoomR = new Rooms();
        public static Enemy CurrentEnemy = new Enemy();

        public static Label loadLabel = new Label();
        public static Panel loadPanel = new Panel();
        public static Panel mainGamePanel = new Panel();



        public static Panel mapPanel = new Panel();

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
            AllEnemies = EnemiesLoad();
            AllRooms = RoomsLoad();


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



            statsPanel.Location = new Point(WidthSet / 2 + WidthSet / 4+WidthSet/8, HeightSet / 2 + HeightSet / 4+HeightSet/8);
            loadPanel.Visible = false;

            mainGamePanel.Size = new Size(WidthSet, HeightSet);

            PictureBox newGameButton = new PictureBox();
            PictureBox quitButton = new PictureBox();//options nehodlam delat nevim co bych tam optionoval a je lepsi kdyz hrac nevi
            PictureBox creditsButton = new PictureBox();



            CurrentPlayer.HotBar = (AllItems[6] as Guns);
            PHotBar.BackgroundImage = Properties.Resources.backgroundItem;
            PHotBar.Size = new Size(WidthSet / 20, HeightSet / 10);
            PHotBar.Location = new Point(WidthSet / 2 - WidthSet / 40, HeightSet - HeightSet / 10);
            PHotBar.Click += new EventHandler(inventoryCheck);
            PHotBar.Tag = "7";
            PHotBar.Name = "Hotbar";
            PHotBar.Image = Properties.Resources.gunTest;
            AmmoLabel.Text = CurrentPlayer.HotBar.CurrentAmountOfRounds + "/" + CurrentPlayer.HotBar.NumberOfRounds;
            AmmoLabel.BackColor = Color.White;
            AmmoLabel.ForeColor = Color.Black;
            AmmoLabel.Location = new Point(PHotBar.Location.X - WidthSet / 10, PHotBar.Location.Y);


            //1920/6, 1080/8
            newGameButton.Size = new Size(WidthSet / 6, HeightSet / 8);
            newGameButton.Location = new Point(WidthSet - WidthSet / 4, HeightSet / 2);
            newGameButton.Image = Properties.Resources.play_button;
            newGameButton.SizeMode = PictureBoxSizeMode.StretchImage;
            newGameButton.BackColor = Color.Transparent;
            newGameButton.Name = "newGameButton";
            newGameButton.Click += new EventHandler(newGameButton_Click);
            newGameButton.MouseEnter += new EventHandler(globalMouseEnterEvent);
            newGameButton.MouseLeave += new EventHandler(globalMouseLeaveEvent);

            quitButton.Size = newGameButton.Size;
            quitButton.Location = new Point(newGameButton.Location.X - 3, newGameButton.Location.Y + HeightSet / 6);
            quitButton.Image = Properties.Resources.quit_button;
            quitButton.BackColor = Color.Transparent;
            quitButton.Name = "quitButton";
            quitButton.SizeMode = PictureBoxSizeMode.StretchImage;
            quitButton.MouseEnter += new EventHandler(globalMouseEnterEvent);
            quitButton.MouseLeave += new EventHandler(globalMouseLeaveEvent);

            creditsButton.Size = newGameButton.Size;
            creditsButton.Location = new Point(quitButton.Location.X, quitButton.Location.Y + HeightSet / 6 - 5);
            creditsButton.Image = Properties.Resources.credits_button;
            creditsButton.BackColor = Color.Transparent;
            creditsButton.Name = "creditsButton";
            creditsButton.SizeMode = PictureBoxSizeMode.StretchImage;
            creditsButton.MouseEnter += new EventHandler(globalMouseEnterEvent);
            creditsButton.MouseLeave += new EventHandler(globalMouseLeaveEvent);


            mainGamePanel.BackgroundImage = Properties.Resources.menu;

            mainGamePanel.Controls.Add(newGameButton);
            mainGamePanel.Controls.Add(quitButton);
            mainGamePanel.Controls.Add(creditsButton);

            mapPanel.BackgroundImage = Properties.Resources.mapBackGround;
            mapPanel.Size = new Size(WidthSet, HeightSet);
            mapPanel.Location = new Point(0, 0);
            mapPanel.Visible = false;

            for(int i =0;i !=6;i++)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Transparent;
                pb.Tag = i.ToString();
                pb.Size = new Size(WidthSet / 8, HeightSet/4);
                pb.Location = new Point(WidthSet / 8 * i, HeightSet / 4);
                pb.Image = Properties.Resources.mapButton;
                pb.Click += new EventHandler(mapButtonClick);
                mapPanel.Controls.Add(pb);
            }

            InventoryToStorage.Size = new Size(1920, 1080);
            InventoryToStorage.Location = new Point(0, 0);
            InventoryToStorage.BackgroundImage = Properties.Resources.inventoryBI;
            InventoryToStorage.BackgroundImageLayout = ImageLayout.Stretch;


            InventorySpace.Size = new Size(WidthSet / 2, HeightSet);
            InventorySpace.Location = new Point(0, 0);
            InventorySpace.BackColor = Color.Transparent;

            StorageSpace.Size = new Size(WidthSet / 2, HeightSet);
            StorageSpace.Location = new Point(WidthSet / 2, 0);
            StorageSpace.BackColor = Color.Transparent;

            DropsPanel.Size = new Size(WidthSet / 2, HeightSet);
            DropsPanel.Location = new Point(WidthSet / 2, 0);
            DropsPanel.BackColor = Color.Transparent;
            DropsPanel.Visible = false;

            PlayerClothes.Size = new Size(WidthSet / 20, HeightSet / 10 * 4);
            PlayerClothes.Location = new Point(WidthSet / 8, HeightSet / 4);
            PlayerClothes.BackColor = Color.Transparent;
            PlayerClothes.Visible = true;
            PlayerClothes.Name = "PlayerClothes";

            InventoryToStorage.Controls.Add(PlayerClothes);
            InventoryToStorage.Controls.Add(InventorySpace);
            InventoryToStorage.Controls.Add(StorageSpace);
            InventoryToStorage.Controls.Add(DropsPanel);

            for (int i = 0; i != 5; i++)
            {
                for (int j = 0; j != 2; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.BackgroundImage = Properties.Resources.backgroundItem;
                    pb.Size = new Size(WidthSet / 20, HeightSet / 10);
                    pb.Location = new Point((WidthSet / 20) * j + WidthSet / 4 + WidthSet / 16, (HeightSet / 10) * i + HeightSet / 4);
                    pb.Tag = "0";
                    pb.Click += new EventHandler(inventoryCheck);
                    pb.MouseDown += new MouseEventHandler(MouseClickEvent);
                    InventorySpace.Controls.Add(pb);
                }
            }

            for (int i = 0; i != 4; i++)
            {
                PictureBox pb = new PictureBox();
                pb.BackgroundImage = Properties.Resources.headPlace;
                pb.Size = new Size(WidthSet / 20, HeightSet / 10);
                pb.Location = new Point(0, (HeightSet / 10) * i);
                pb.Tag = "0";
                pb.Click += new EventHandler(inventoryCheck);
                pb.MouseDown += new MouseEventHandler(MouseClickEvent);
                pb.Name = "Head";
                PlayerClothes.Controls.Add(pb);
            }
            //Zmena playerClothes imagů
            (PlayerClothes.Controls[1] as PictureBox).BackgroundImage = Properties.Resources.chestPlace;
            (PlayerClothes.Controls[1] as PictureBox).Name = "Chest";

            (PlayerClothes.Controls[2] as PictureBox).BackgroundImage = Properties.Resources.legsPlace;
            (PlayerClothes.Controls[2] as PictureBox).Name = "Legs";

            (PlayerClothes.Controls[3] as PictureBox).BackgroundImage = Properties.Resources.feetPlace;
            (PlayerClothes.Controls[3] as PictureBox).Name = "Feet";

            //zmena pl...

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
                    pb.MouseDown += new MouseEventHandler(MouseClickEvent);
                    StorageSpace.Controls.Add(pb);
                    DropsPanel.Controls.Add(pb);
                }
            }
            InventoryToStorage.Visible = false;

            //Test Batch
            InventorySpace.Controls[0].Tag = "1";
            (InventorySpace.Controls[0] as PictureBox).Image = Properties.Resources.gunTest;
            InventorySpace.Controls[1].Tag = "16";
            (InventorySpace.Controls[1] as PictureBox).Image = Properties.Resources.itemTest;
            InventorySpace.Controls[2].Tag = "3";
            (InventorySpace.Controls[2] as PictureBox).Image = Properties.Resources.bonnieHat;
            InventorySpace.Controls[3].Tag = "4";
            (InventorySpace.Controls[3] as PictureBox).Image = Properties.Resources.tshirt;
            InventorySpace.Controls[4].Tag = "5";
            (InventorySpace.Controls[4] as PictureBox).Image = Properties.Resources.sweatPants;
            InventorySpace.Controls[5].Tag = "6";
            (InventorySpace.Controls[5] as PictureBox).Image = Properties.Resources.socks;
            //Test Batch
            CurrentPlayer.ChangeStats(statsPanel);
            this.Controls.Add(loadPanel);
            this.Controls.Add(characterInteractPanel);
            this.Controls.Add(mainGamePanel);
            this.Controls.Add(mapPanel);
            this.Controls.Add(PHotBar);
            this.Controls.Add(statsPanel);
            this.Controls.Add(AmmoLabel);
            this.Controls.Add(CurrentRoom);
            this.Controls.Add(InventoryToStorage);

            Task.Run(() => mainGameTimer_Tick());
        }


        //START OF TASKS
        async Task logicalInventory()
        {
            int CurrentHealth = CurrentPlayer.HealthWA;
            foreach (PictureBox pb in PlayerClothes.Controls)
            {
                
                if(pb.Tag.ToString() != "0")
                {
                    Clothes CurrentClothing = AllItems[Convert.ToInt32(pb.Tag) - 1] as Clothes;
                    CurrentHealth += CurrentClothing.HealthBoost;
                }
            }
            if(PHotBar.Tag.ToString() == "0" || PHotBar.Image == null)
            {
                PHotBar.Tag = "0";
                PHotBar.Image = null;
                CurrentPlayer.HotBar = null;
            }else
            {
                CurrentPlayer.HotBar = AllItems[Convert.ToInt32(PHotBar.Tag) - 1] as Guns;
            }
            CurrentPlayer.Health = CurrentHealth;
            CurrentPlayer.ChangeStats(statsPanel);
            RefreshLabel();
        }
        async Task writeOutLines(string Message)
        {
            //neprepise se kdyz posle dalsi writeOutLines, seru na uzivatele
            if (isWriting) { currentInteraction--; return; }
            isWriting = true;
            characterInteractLabel.Text = String.Empty;
            for (int i = 0; i != Message.Length; i++)
            {
                characterInteractLabel.Text += Message[i];
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
            Reload.Play();
            CurrentPlayer.HotBar.isAbleToShoot = false;
            await Task.Delay(CurrentPlayer.HotBar.WaitTime);
            CurrentPlayer.HotBar.isAbleToShoot = true;
            CurrentPlayer.HotBar.CurrentAmountOfRounds = CurrentPlayer.HotBar.NumberOfRounds;
            await logicalInventory();
        }
        async Task WaitBetweenShots()
        {
            SoundPlayer Gunshot = new SoundPlayer(Properties.Resources.gunshot);
            Gunshot.Play();
            CurrentPlayer.HotBar.isAbleToShoot = false;
            await Task.Delay(CurrentPlayer.HotBar.WaitTime / 10);
            CurrentPlayer.HotBar.isAbleToShoot = true;
            RefreshLabel();

        }
        async Task Death(object sender)
        {
            (sender as PictureBox).Click -= new EventHandler(pbClick);
            await Task.Delay(2000);
            try
            {
                CurrentRoomR.Enemies.RemoveAt(0);
            }
            catch (Exception ex)
            {
                ShowDrops(CurrentRoomR);
                ClearRooms();
                MessageBox.Show(ex.Message);
            }
            if (CurrentRoomR.Enemies.Count == 0)
            {
                ShowDrops(CurrentRoomR);
                ClearRooms();
            }
            else
            {
                (CurrentRoom.Controls[0] as PictureBox).Image = CurrentRoomR.Enemies[0].Img;
                (CurrentRoom.Controls[1] as ProgressBar).Maximum = CurrentRoomR.Enemies[0].Health;
                (CurrentRoom.Controls[1] as ProgressBar).Value = CurrentRoomR.Enemies[0].Health;
                (sender as PictureBox).Click += new EventHandler(pbClick);
            }
        }
        //END OF TASKS
        //START OF METHODS
        public void CreateMessage(string Message)
        {
            Panel panelMessage = new Panel();
            Button OKButton = new Button();
            Label labelMessage = new Label();
            panelMessage.Size = new Size(WidthSet / 4, HeightSet / 4);
            panelMessage.Location = new Point(WidthSet - WidthSet / 2 - WidthSet / 8, HeightSet - HeightSet / 2 - HeightSet / 8);
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
        public void SmartTooltip(Items SelectedItem)
        {
            Panel ToolTip = new Panel();
            ToolTip.Location = MousePosition;
            ToolTip.Size = new Size(WidthSet / 8, HeightSet / 10);
            ToolTip.BackColor = Color.DarkOliveGreen;
            ToolTip.MouseLeave += new EventHandler(SmartTTLeave);
            Button use = new Button();
            use.Location = new Point(0, 0);
            use.Size = new Size(ToolTip.Size.Width, HeightSet / 10);
            use.Text = "Use";
            use.BackColor = Color.DarkOliveGreen;
            use.Click += new EventHandler(useEvent);
            use.MouseLeave += new EventHandler(SmartTTLeave);
            ToolTip.Controls.Add(use);


            if (SelectedItem is Items)
            {
                this.Controls.Add(ToolTip);
                ToolTip.BringToFront();
            }

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
            Characters SgtBory = new Characters("Sgt. Blueberry", Properties.Resources.voiceLineTestBedImage);
            Characters EndBossBory = new Characters("Blueberry", Properties.Resources.voiceLineTestBedImage);
            Characters Korky = new Characters("Mr. Korky", Properties.Resources.voiceLineTestBedImage);
            Characters Medved = new Characters("Medved", Properties.Resources.voiceLineTestBedImage);
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
            
            list.Add(new Guns(1, "1911", "Standard handgun for all situations", Rarity.Common, Properties.Resources.gunTest, 12, 2, 3000));
            list.Add(new Guns(2, "Judge", "Strong revolver with high damage and blowback", Rarity.Legendary, Properties.Resources.gunTest, 6, 5, 3000));
            list.Add(new Clothes(3, "Bonnie hat", "Perfect headwarmer", Rarity.Common, Properties.Resources.bonnieHat, Place.Head, 10));
            list.Add(new Clothes(4, "T-Shirt", "Good old classic shirt", Rarity.Common, Properties.Resources.tshirt, Place.Chest, 10));
            list.Add(new Clothes(5, "Sweatpants", "Great at looking average", Rarity.Common, Properties.Resources.sweatPants, Place.Pants, 10));
            list.Add(new Clothes(6, "Socks", "Great for everyone!", Rarity.Common, Properties.Resources.socks, Place.Boots, 10));
            list.Add(new Guns(7, "AK-47", "Fast firing Soviet-made assault rifle", Rarity.Uncommon, Properties.Resources.gunTest, 30, 1, 100));
            list.Add(new Items(8, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(9, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(10, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(11, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(12, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(13, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(14, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
            list.Add(new Items(15, "Can of Beans", "Standard source of protein for combat situations", Rarity.Common, Properties.Resources.itemTest));
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
                "Medved", 10, Rarity.Common, Enemy.Types.Ground, new Size(WidthSet / 4, HeightSet / 4), Properties.Resources.enemyTestnew, 50, Properties.Resources.enemyDeathTest
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
        public void RefreshLabel()
        {
            if (PHotBar.Tag.ToString() != "0")
            {
                AmmoLabel.Text = CurrentPlayer.HotBar.CurrentAmountOfRounds + "/" + CurrentPlayer.HotBar.NumberOfRounds;
            }
            else
            {
                AmmoLabel.Text = "0/0";
            }
            AmmoLabel.Refresh();
        }
        public void ShowDrops(Rooms GivenRoom)
        {
            CurrentRoom.Visible = false;
            InventoryToStorage.Visible = true;
            DropsPanel.Visible = true;
            StorageSpace.Visible = false;
            int currentPicBox = 0;
            foreach (Items it in GivenRoom.Drops)
            {
                DropsPanel.Controls[currentPicBox].Tag = it.ID.ToString();
                (DropsPanel.Controls[currentPicBox] as PictureBox).Image = it.img;
                currentPicBox++;
            }
        }

        public void SpawnARoom(Rooms GivenRoom)
        {

            Random rnd = new Random();
            GivenRoom.Drops = GivenRoom.CreateDrops(GivenRoom.RequiredKey, AllItems);
            GivenRoom.Enemies = GivenRoom.CreateEnemies(GivenRoom.RequiredKey, AllEnemies);
            CurrentEnemy = GivenRoom.Enemies[0];

            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = CurrentEnemy.Img;
            pb.BackColor = Color.FromArgb(0, Color.Transparent);
            pb.BackgroundImage = null;


            pb.Size = CurrentEnemy.Size;
            pb.Location = new Point(WidthSet / 2 - pb.Size.Width / 2, HeightSet / 4);
            pb.Tag = CurrentEnemy.Health.ToString();
            pb.Click += new EventHandler(pbClick);

            ProgressBar progBar = new ProgressBar();
            progBar.Maximum = CurrentEnemy.Health;
            progBar.Step = 1;
            progBar.Value = CurrentEnemy.Health;
            progBar.Size = new Size(WidthSet / 4, HeightSet / 8);
            progBar.Location = new Point(WidthSet / 2 - WidthSet / 8, HeightSet - HeightSet / 4);

            CurrentRoom.Controls.Add(pb);
            CurrentRoom.Controls.Add(progBar);
            CurrentRoomR = GivenRoom;
        }

        public void ClearRooms()
        {
            CurrentRoom.Controls.Clear();
            CurrentRoomR = new Rooms();

        }

        //END OF METHODS
        //START OF EVENTS
        private void useEvent(object sender, EventArgs e)
        {
            //do some things
            ((sender as Button).Parent as Panel).Dispose();
        }
        private void MouseClickEvent(object sender, MouseEventArgs e)
        {
            //do some things
            if ((sender as PictureBox).Tag.ToString() == "0")
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                SmartTooltip(AllItems[Convert.ToInt32((sender as PictureBox).Tag) - 1]);
            }

        }
        private void SmartTTLeave(object sender, EventArgs e)
        {
            if (sender is Panel)
            {
                (sender as Panel).Dispose();
            }
            if (sender is Button)
            {
                (sender as Button).Parent.Dispose();
            }
        }
        private void mapButtonClick(object sender, EventArgs e)
        {
            int currentRoomID = Convert.ToInt32((sender as PictureBox).Tag);
            string currentRoomString = "Clicked on : ";
            switch(currentRoomID)
            {
                case 0: currentRoomString += "catacombs"; break;
                case 1: currentRoomString += "electricity"; break;
                case 2: currentRoomString += "engine"; break;
                case 3: currentRoomString += "vaultdoor"; break;
                case 4: currentRoomString += "rogersshrine"; break;
                case 5: currentRoomString += "boryshq"; break;
            }
            MessageBox.Show(currentRoomString);
        }
        private void inventoryCheck(object sender, EventArgs e)
        {
            PictureBox pictureBox = (sender as PictureBox);
            if (!isSelected)
            {
                if (pictureBox.Tag.ToString() != "0")
                {
                    lastPb = pictureBox;
                }
                else { return; }
                isSelected = true;
                foreach (Control c in InventorySpace.Controls)
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
                if (AllItems[Convert.ToInt32(pictureBox.Tag) - 1] is Clothes)
                {
                    switch ((AllItems[Convert.ToInt32(pictureBox.Tag) - 1] as Clothes).Place)
                    {
                        case Place.Head:
                            (PlayerClothes.Controls[0] as PictureBox).BackgroundImage = Properties.Resources.backgroundItemFree;
                            break;
                        case Place.Chest:
                            (PlayerClothes.Controls[1] as PictureBox).BackgroundImage = Properties.Resources.backgroundItemFree;
                            break;
                        case Place.Pants:
                            (PlayerClothes.Controls[2] as PictureBox).BackgroundImage = Properties.Resources.backgroundItemFree;
                            break;
                        case Place.Boots:
                            (PlayerClothes.Controls[3] as PictureBox).BackgroundImage = Properties.Resources.backgroundItemFree;
                            break;
                    }

                }
                if (AllItems[Convert.ToInt32(pictureBox.Tag) - 1] is Guns)
                {
                    PHotBar.BackgroundImage = Properties.Resources.backgroundItemFree;
                }
                return;
            }
            if (pictureBox.Tag.ToString() == "0" && (pictureBox.Parent.Name != "PlayerClothes" && pictureBox.Name != "Hotbar"))
            {
                pictureBox.Image = lastPb.Image;
                pictureBox.Tag = lastPb.Tag;
                lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                lastPb.Tag = "0";
                lastPb.Image = null;
            }
            else if (pictureBox.Tag.ToString() == "0" && pictureBox.Parent.Name == "PlayerClothes") //Hodne nested ifu, ale tak co uz lol
            { //Zjisteni jestli uz. klikl na PlayerClothes Panel
                try
                {
                    if (AllItems[Convert.ToInt32(lastPb.Tag) - 1] is Clothes) //Zjisteni jestli obsah kokotiny je vubec clouts
                    { //musim zjistit jestli nedava cepici do gati
                        switch (pictureBox.Name)
                        {
                            case "Head":
                                if ((AllItems[Convert.ToInt32(lastPb.Tag) - 1] as Clothes).Place == Place.Head)
                                {
                                    pictureBox.Image = lastPb.Image;
                                    pictureBox.Tag = lastPb.Tag;
                                    lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                                    lastPb.Tag = "0";
                                    lastPb.Image = null;
                                }
                                break;
                            case "Chest":
                                if ((AllItems[Convert.ToInt32(lastPb.Tag) - 1] as Clothes).Place == Place.Chest)
                                {
                                    pictureBox.Image = lastPb.Image;
                                    pictureBox.Tag = lastPb.Tag;
                                    lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                                    lastPb.Tag = "0";
                                    lastPb.Image = null;
                                }
                                break;
                            case "Legs":
                                if ((AllItems[Convert.ToInt32(lastPb.Tag) - 1] as Clothes).Place == Place.Pants)
                                {
                                    pictureBox.Image = lastPb.Image;
                                    pictureBox.Tag = lastPb.Tag;
                                    lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                                    lastPb.Tag = "0";
                                    lastPb.Image = null;
                                }
                                break;
                            case "Feet":
                                if ((AllItems[Convert.ToInt32(lastPb.Tag) - 1] as Clothes).Place == Place.Boots)
                                {
                                    pictureBox.Image = lastPb.Image;
                                    pictureBox.Tag = lastPb.Tag;
                                    lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                                    lastPb.Tag = "0";
                                    lastPb.Image = null;
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show((Convert.ToInt32(pictureBox.Tag)).ToString());
                    MessageBox.Show((Convert.ToInt32(lastPb.Tag)).ToString());
                }
            }
            else if (pictureBox.Tag.ToString() == "0" && pictureBox.Name == "Hotbar")
            {
                if (AllItems[Convert.ToInt32(lastPb.Tag) - 1] is Guns)
                {
                    
                    pictureBox.Image = lastPb.Image;
                    pictureBox.Tag = lastPb.Tag;
                    lastPb.BackgroundImage = Properties.Resources.backgroundItem;
                    lastPb.Tag = "0";
                    lastPb.Image = null;
                }
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
            (PlayerClothes.Controls[0] as PictureBox).BackgroundImage = Properties.Resources.headPlace;
            (PlayerClothes.Controls[1] as PictureBox).BackgroundImage = Properties.Resources.chestPlace;
            (PlayerClothes.Controls[2] as PictureBox).BackgroundImage = Properties.Resources.legsPlace;
            (PlayerClothes.Controls[3] as PictureBox).BackgroundImage = Properties.Resources.feetPlace;
            PHotBar.BackgroundImage = Properties.Resources.backgroundItem;
            logicalInventory();
            
        }
        private async void newGameButton_Click(object sender, EventArgs e)
        {
            loadPanel.Visible = true;
            mainGamePanel.Visible = false;
            await loadTimer_Tick();
        }

        private void globalMouseEnterEvent(object sender, EventArgs e)
        {
            string nameOfSender = (sender as PictureBox).Name;
            switch (nameOfSender)
            {
                case "newGameButton":
                    (sender as PictureBox).Image = Properties.Resources.play_buttonHover;
                    break;
                case "creditsButton":
                    (sender as PictureBox).Image = Properties.Resources.credits_buttonHover;
                    break;
                case "quitButton":
                    (sender as PictureBox).Image = Properties.Resources.quit_buttonHover;
                    break;
                default:
                    Log("globalMouseEnterEvent failed, possibly something to do with name? " + nameOfSender + sender.GetType());
                    break;
            }

        }
        private void globalMouseLeaveEvent(object sender, EventArgs e)
        {
            string nameOfSender = (sender as PictureBox).Name;
            switch (nameOfSender)
            {
                case "newGameButton":
                    (sender as PictureBox).Image = Properties.Resources.play_button;
                    break;
                case "creditsButton":
                    (sender as PictureBox).Image = Properties.Resources.credits_button;
                    break;
                case "quitButton":
                    (sender as PictureBox).Image = Properties.Resources.quit_button;
                    break;
                default:
                    Log("globalMouseLeaveEvent failed, possibly something to do with name? " + nameOfSender + sender.GetType());
                    break;
            }
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
                }
                else
                {
                    characterInteractPanel.Visible = false;
                    SpawnARoom(AllRooms[0]);
                }
                currentInteraction++;
            }
            catch (Exception ex)
            {
                Log("Wrong characterInteraction, Maybe currentInteraction value is wrong?" + ex.Message);
            }
        }

        private void MainGame_KeyDown(object sender, KeyEventArgs e)
        {
            Log("Something was pressed!");
            switch (e.KeyCode)
            {
                case Keys.Escape: break; //pauses
                case Keys.M:
                    mapPanel.Visible = !mapPanel.Visible;
                    break; //Opens map
                case Keys.E: 
                    InventoryToStorage.Visible = !InventoryToStorage.Visible; break; //opens inventory
                case Keys.R:
                    Task.Run(() => Reloading());
                    break; //reload
                case Keys.F: statsPanel.Visible = !statsPanel.Visible; break;
            }
        }
        private void pbClick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (CurrentPlayer.HotBar.isAbleToShoot)
            {
                Cursor.Position = new Point(Cursor.Position.X - rnd.Next(-50, 50), Cursor.Position.Y - rnd.Next(30, 100));
                int help = Convert.ToInt32((CurrentRoom.Controls[1] as ProgressBar).Value) - CurrentPlayer.HotBar.Damage * 5;
                if (help <= 0)
                {
                    (CurrentRoom.Controls[1] as ProgressBar).Value = 0;
                    try
                    {
                        Death(sender);
                    }
                    catch (Exception ex)
                    {
                        Log("Something went wrong when trying the Death() method, ex code: " + ex.Message);
                    }
                }
                else
                {
                    (CurrentRoom.Controls[1] as ProgressBar).Value = Convert.ToInt32((CurrentRoom.Controls[1] as ProgressBar).Value) - CurrentPlayer.HotBar.Damage * 5;
                }
                CurrentPlayer.HotBar.CurrentAmountOfRounds--;
                RefreshLabel();
                if (CurrentPlayer.HotBar.CurrentAmountOfRounds != 0)
                {
                    Task.Run(() => WaitBetweenShots());
                }
                else
                {
                    CurrentPlayer.HotBar.isAbleToShoot = false;
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

    public enum Quests
    {
        RunCatacombs,
        //to be done
    };
}