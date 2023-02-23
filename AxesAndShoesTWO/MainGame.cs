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

        public Panel statsPanel = new Panel();
        public PictureBox healthBar = new PictureBox();
        public PictureBox thirstBar = new PictureBox();
        public PictureBox hungerBar = new PictureBox();
        public PictureBox radiationBar = new PictureBox();

        public static Clothes[] Clothes = new Clothes[4];
        public static List<Items> pItems = new List<Items>();
        public static Guns pGuns = new Guns();
        public static List<KeysRoom> pKeys = new List<KeysRoom>();

        public Player CurrentPlayer = new Player(85, 30, 30, 20, pGuns, pKeys);

        public  List<Characters> Chars = new List<Characters>();
        public  List<string> CharacterInteractions = new List<string>();
        public  List<Items> AllItems = new List<Items>();
        public  List<Enemy> AllEnemies = new List<Enemy>();
        public  List<Rooms> AllRooms = new List<Rooms>();

        public  Panel InventoryToStorage = new Panel();
        public  Panel InventorySpace = new Panel();
        public  Panel StorageSpace = new Panel();
        public  PictureBox lastPb = new PictureBox();
        public  Panel DropsPanel = new Panel();
        public  Panel PlayerClothes = new Panel();
        public  PictureBox PHotBar = new PictureBox();
        public  Label AmmoLabel = new Label();

        public  Panel CurrentRoom = new Panel();
        public  Rooms CurrentRoomR = null;
        public  Enemy CurrentEnemy = new Enemy();

        public  Label loadLabel = new Label();
        public  Panel loadPanel = new Panel();
        public  Panel mainGamePanel = new Panel();

        public  Panel MainRoom = new Panel();
        //dodelat pictureboxy jako NPCs



        public  Panel mapPanel = new Panel();

        public Panel dsPanel = new Panel();
        public Label dsMessage = new Label();



        public Panel characterInteractPanel = new Panel();
        public  Label characterInteractLabel = new Label();
        public  Label characterInteractLabelName = new Label();
        public  Button characterInteractButton = new Button();

        public Items CurrentItem; //will cause bugs I can guarantee it 

        public string DeathMessage = "You have died from ";
        
        public int loadColor = 0;
        public int loadOpacity = 255;
        public int currentInteraction = 0;

        public int currentMoney = 10;

        public bool isSelected = false;
        public bool isWriting = false;
        public bool isPaused = false;
        public bool enemyIsDead = false;
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
            CurrentRoom.Visible = false;

            MainRoom.Size = new Size(WidthSet, HeightSet);
            MainRoom.Location = new Point(0, 0);
            MainRoom.Visible = false;

            characterInteractButton.Text = "Next";
            characterInteractButton.Location = new Point(WidthSet - WidthSet / 10, HeightSet - HeightSet / 10);
            characterInteractButton.Size = new Size(WidthSet / 10, HeightSet / 10);
            characterInteractButton.BackgroundImage = Properties.Resources.buttonTemp;


            characterInteractPanel.Size = new Size(WidthSet, HeightSet);
            characterInteractPanel.BackgroundImage = Chars[0].img;
            characterInteractPanel.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.BackgroundImageLayout = ImageLayout.Stretch;
            characterInteractButton.Click += new EventHandler(interactButton_Click);

            characterInteractLabel.Location = new Point(0, HeightSet / 2+HeightSet/8);
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
            AmmoLabel.Text = CurrentPlayer.HotBar.CurrentAmountOfRounds.ToString();
            AmmoLabel.ForeColor = Color.White;
            AmmoLabel.BackColor = Color.Black;
            AmmoLabel.Font = new Font(AmmoLabel.Font.FontFamily, HeightSet/30,FontStyle.Bold);
            AmmoLabel.Size = new Size(WidthSet/20, HeightSet/16);
            AmmoLabel.Location = new Point(PHotBar.Location.X - WidthSet / 20, PHotBar.Location.Y);


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

            for(int i =0;i !=11;i++)
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

            healthBar.Location = new Point(0, 0);
            thirstBar.Location = new Point(0, HeightSet / 32);
            hungerBar.Location = new Point(0, (HeightSet / 32) * 2);
            radiationBar.Location = new Point(0, (HeightSet / 32) * 3);

            healthBar.Size = new Size(WidthSet / 16, HeightSet / 32);
            thirstBar.Size = new Size(WidthSet / 16, HeightSet / 32);
            hungerBar.Size = new Size(WidthSet / 16, HeightSet / 32);
            radiationBar.Size = new Size(WidthSet / 16, HeightSet/ 32);

            healthBar.Image = Properties.Resources.healthbar;
            thirstBar.Image = Properties.Resources.thirstbar;
            hungerBar.Image = Properties.Resources.hungerbar;
            radiationBar.Image = Properties.Resources.radiatonbar;

            statsPanel.BackgroundImage = Properties.Resources.barstats;
            statsPanel.BackgroundImageLayout = ImageLayout.Stretch;
            statsPanel.BackColor = Color.Transparent;

            healthBar.SizeMode = PictureBoxSizeMode.StretchImage;
            thirstBar.SizeMode = PictureBoxSizeMode.StretchImage;
            hungerBar.SizeMode = PictureBoxSizeMode.StretchImage;
            radiationBar.SizeMode = PictureBoxSizeMode.StretchImage;

            statsPanel.Controls.Add(healthBar);
            statsPanel.Controls.Add(thirstBar);
            statsPanel.Controls.Add(hungerBar);
            statsPanel.Controls.Add(radiationBar);

            statsPanel.Size = new Size(WidthSet / 8, HeightSet / 8);


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

            CurrentEnemy = null;

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

            dsPanel.Size = new Size(WidthSet, HeightSet);
            dsPanel.Location = new Point(0, 0);
            dsPanel.BackColor = Color.Black;
            dsPanel.Visible = false;


            dsMessage.ForeColor = Color.White;
            dsMessage.BackColor = Color.Black;
            dsMessage.AutoSize = true;
            dsMessage.Location = new Point(WidthSet / 2 - dsMessage.Size.Width / 2, HeightSet / 2);
            dsPanel.Controls.Add(dsMessage);


            ChangeStats();
            this.Controls.Add(dsPanel);
            this.Controls.Add(loadPanel);
            this.Controls.Add(characterInteractPanel);
            this.Controls.Add(mainGamePanel);
            
            this.Controls.Add(PHotBar);
            this.Controls.Add(statsPanel);
            this.Controls.Add(AmmoLabel);
            this.Controls.Add(CurrentRoom);
            this.Controls.Add(mapPanel);
            this.Controls.Add(InventoryToStorage);

        }


        //START OF TASKS
        async Task DeathScreen()
        {
            CurrentRoom.Visible = false;
            InventoryToStorage.Visible = false;
            mapPanel.Visible = false;
            dsMessage.Text = DeathMessage;
            dsPanel.Visible = true;
            dsPanel.BringToFront();

            for (int i = 0; i <= 270; i += 5)
            {
                dsPanel.BackColor = Color.FromArgb(i, Color.Black);
                Log(i.ToString());
                dsPanel.Refresh();
                await Task.Delay(100);
            }
            await Task.Delay(5000);
            Application.Exit();
        }
        async Task Attack()
        {
            while(!enemyIsDead && CurrentEnemy != null)
            {
                CurrentPlayer.Health -= CurrentEnemy.Damage;
                CurrentPlayer.HealthWA -= CurrentEnemy.Damage;
                ChangeStats();

                if (!CurrentPlayer.IsAlive())
                {
                    DeathMessage += "a " + CurrentEnemy.Name + " attack.";
                    await Task.Run(() => DeathScreen());
                    return;
                }
                await Task.Delay(3000);
            }
        }
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
            ChangeStats();
            if(!CurrentPlayer.IsAlive())
            {
                DeathMessage += " taking off your clothes, which were holding you together.";
                await Task.Run(() => DeathScreen());
                return;
            }
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

            RefreshLabel();

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
            enemyIsDead = true;
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
                MessageBox.Show("Please contact beneckym.05@spst.eu with this ex Message:");
                MessageBox.Show(ex.Message);
            }
            if (CurrentRoomR.Enemies.Count == 0)
            {
                ShowDrops(CurrentRoomR);
                ClearRooms();
            }
            else
            {
                CurrentEnemy = CurrentRoomR.Enemies[0];
                (CurrentRoom.Controls[0] as PictureBox).Image = CurrentRoomR.Enemies[0].Img;
                (CurrentRoom.Controls[1] as ProgressBar).Maximum = CurrentRoomR.Enemies[0].Health;
                (CurrentRoom.Controls[1] as ProgressBar).Value = CurrentRoomR.Enemies[0].Health;
                (sender as PictureBox).Click += new EventHandler(pbClick);

                enemyIsDead = false;
                await Task.Run(() =>Attack());
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
            panelMessage.BackColor = Color.DarkGray;
            panelMessage.Visible = true;

            panelMessage.Controls.Add(OKButton);
            panelMessage.Controls.Add(labelMessage);
            

            labelMessage.Text = Message;
            labelMessage.Location = new Point(0, 0);
            labelMessage.Size = new Size(WidthSet / 4, HeightSet / 4);

            labelMessage.MaximumSize = new Size(WidthSet / 4, HeightSet / 4);

            OKButton.Text = "OK";
            OKButton.Location = new Point(panelMessage.Size.Width - panelMessage.Size.Width / 8, panelMessage.Size.Height - panelMessage.Size.Height / 8);
            OKButton.Click += new EventHandler(OKButton_Click);
            this.Controls.Add(panelMessage);
            panelMessage.BringToFront();
        }
        public void SmartTooltip(Items SelectedItem, string PosOfPic)
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
            use.Tag = SelectedItem.ID.ToString();
            ToolTip.Controls.Add(use);
            if ((SelectedItem is Consumables))
            {
                switch ((SelectedItem as Consumables).TypeOf)
                {
                    case TypeOfCons.Health:
                        use.Text = "Use";
                        break;
                    case TypeOfCons.Drink:
                        use.Text = "Drink";
                        break;
                    case TypeOfCons.Food:
                        use.Text = "Eat";
                        break;
                    case TypeOfCons.Rad:
                        use.Text = "Take";
                        break;
                }
            }
            this.Controls.Add(ToolTip);
            ToolTip.BringToFront();

            CurrentItem = SelectedItem;
        }


        public static void Log(string message)
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


        // 
        //
        //                              Common, //prodej za 3
        //                              Uncommon, //prodej za 6
        //                              Rare, //prodej za 15
        //                              Epic, //prodej za 40
        //                               Legendary //prodej za 125
        //
        List<Items> list = new List<Items>();
            
            list.Add(new Guns(1, "1911", "Standard handgun for all situations", Rarity.Uncommon, Properties.Resources._1911, 12, 2, 4000));
            list.Add(new Guns(2, "Judge", "Strong revolver with high damage and blowback", Rarity.Legendary, Properties.Resources.Judge, 6, 5, 5600));
            list.Add(new Clothes(3, "Bonnie hat", "Perfect headwarmer", Rarity.Common, Properties.Resources.bonnieHat, Place.Head, 10));
            list.Add(new Clothes(4, "T-Shirt", "Good old classic shirt", Rarity.Common, Properties.Resources.tshirt, Place.Chest, 10));
            list.Add(new Clothes(5, "Sweatpants", "Great at looking average", Rarity.Common, Properties.Resources.sweatPants, Place.Pants, 10));
            list.Add(new Clothes(6, "Socks", "Great for everyone!", Rarity.Common, Properties.Resources.socks, Place.Boots, 10));
            list.Add(new Guns(7, "AK-47", "Fast firing Soviet-made assault rifle", Rarity.Rare, Properties.Resources.gunTest, 30, 1, 6500));
            list.Add(new Items(8, "Hunting Knife", "Made for a true killer!", Rarity.Epic, Properties.Resources.HuntingKnife));
            list.Add(new Clothes(9, "Knitted hat", "Canadian Classic!", Rarity.Uncommon, Properties.Resources.KnittedHat, Place.Head, 13));
            list.Add(new Items(10, "Flint and Steel", "Great for creating a housefire!", Rarity.Common, Properties.Resources.FlintAndSteel));
            list.Add(new Clothes(11, "Army Issued backpack", "A lot of space for items, sadly there's a hole...", Rarity.Rare, Properties.Resources.BigBackpack, Place.Chest, 20));
            list.Add(new Clothes(12, "Medium-sized backpack", "Kevlar fitted backpack, could stop a bullet.", Rarity.Rare, Properties.Resources.MediumSizedBackpack, Place.Chest, 25));
            list.Add(new Clothes(13, "Small backpack", "Kindergartener's favourite.", Rarity.Uncommon, Properties.Resources.SmallBackpack, Place.Chest, 10));
            list.Add(new Clothes(14, "Gloves", "Best way of hiding fingerprints.", Rarity.Common, Properties.Resources.Gloves, Place.Chest, 5));
            list.Add(new Items(15, "Axe", "Great way to chop down a tree.", Rarity.Uncommon, Properties.Resources.Axe));
            list.Add(new Items(16, "Compass", "For some reason always points to the north...", Rarity.Epic, Properties.Resources.Compass));
            list.Add(new Consumables(17, "First Aid Kit", "Tool for immediate assistance in injuries", Rarity.Rare, Properties.Resources.FirstAidKit, TypeOfCons.Health, 50, 0, 0, 10));
            list.Add(new Consumables(18, "Water Bottle", "Best thing to mix with alcohol!", Rarity.Common, Properties.Resources.WaterBottle, TypeOfCons.Drink, 0, 55, 1,5));
            list.Add(new Consumables(19, "Energy Bar", "Great way to collapse from caffeine induced heart attack!", Rarity.Uncommon, Properties.Resources.EnergyBar, TypeOfCons.Food, 5, 0, 30, 0));
            list.Add(new Items(20, "Flashlight", "Best way to blind yourself permanently.", Rarity.Epic, Properties.Resources.Flashlight));
            list.Add(new Items(21, "Tent", "Still better than the schools's dormatory!", Rarity.Legendary, Properties.Resources.Tent));
            list.Add(new Items(22, "Sleeping Bag", "Better than the school's dormatory!", Rarity.Epic, Properties.Resources.SleepingBag));
            list.Add(new Items(23, "Matches", "Second best thing at starting a housefire!", Rarity.Uncommon, Properties.Resources.Matches));
            list.Add(new Items(24, "Cooking Pot", "Made of asbestos.", Rarity.Uncommon, Properties.Resources.CookingPot));
            list.Add(new Items(25, "Rope", "Tool for climbing, pulling things or...", Rarity.Common, Properties.Resources.Rope));
            list.Add(new Items(26, "Multi-Tool", "It literally does everything...", Rarity.Uncommon, Properties.Resources.MultiTool));
            list.Add(new Items(27, "Portable Generator", "Industrial grade generator.", Rarity.Legendary, Properties.Resources.PortableGenerator));
            list.Add(new Items(28, "Duct Tape", "You could probably repair a car with this!", Rarity.Common, Properties.Resources.DuctTape));
            list.Add(new Consumables(29, "Portable Water Filter", "You probably shouldn't eat these...", Rarity.Uncommon, Properties.Resources.PortableWaterFilter, TypeOfCons.Rad, -5, 0, 0, 10));
            list.Add(new Items(30, "Firestarter", "Classic PEPO set.", Rarity.Rare, Properties.Resources.FireStarter));
            list.Add(new Clothes(31, "Headlamp", "Can't see?", Rarity.Epic, Properties.Resources.Headlamp, Place.Head, 10));
            list.Add(new Clothes(32, "Military Boots", "Skinhead's favourite.", Rarity.Epic, Properties.Resources.MilitaryBoots, Place.Boots, 20));
            list.Add(new Clothes(33, "Briefs", "There seems to be some kind of stain...", Rarity.Uncommon, Properties.Resources.Briefs, Place.Pants, 5));
            list.Add(new Guns(34, "M4A1", "US issued fully automatic rifle.", Rarity.Epic, Properties.Resources.M4, 25, 1, 5000));
            list.Add(new Consumables(35, "Chocolate Bar", "Diabetic's least favourite.", Rarity.Common, Properties.Resources.ChocolateBar, TypeOfCons.Food, 1, 0, 30, 0));
            list.Add(new Clothes(36, "Watches", "Sadly the battery ran out, so you still can't tell time...", Rarity.Rare, Properties.Resources.Watches, Place.Chest, 0));
            list.Add(new Items(37, "Steel Mug", "I guess you could bash someone with this...", Rarity.Common, Properties.Resources.SteelMug));
            list.Add(new Clothes(38, "Leather Jacket", "Skinhead's second favourite!", Rarity.Rare, Properties.Resources.LeatherJacket, Place.Chest, 30));
            list.Add(new Items(39, "Baseball Bat", "Perfect for baseball!", Rarity.Rare, Properties.Resources.BaseballBat));
            list.Add(new Clothes(40, "Gas Mask", "Mmpmhmpphmmm!", Rarity.Rare, Properties.Resources.GasMask, Place.Head, 5));

            return list;

        }
        public List<Enemy> EnemiesLoad()
        {
            List<Enemy> list = new List<Enemy>();

            list.Add(new Enemy(
                "Bear", 10, Rarity.Common,  new Size(WidthSet / 4, HeightSet / 4), Properties.Resources.enemyTestnew, 50
                ));
            list.Add(new Enemy(
                "Mouse", 10, Rarity.Uncommon, new Size(WidthSet / 4, HeightSet / 4), Properties.Resources.mouse, 30
                ));
            list.Add(new Enemy(
                "Houmles", 20, Rarity.Rare, new Size(WidthSet / 4, HeightSet / 2), Properties.Resources.houmles, 55
                ));
            list.Add(new Enemy(
                "Crip", 20, Rarity.Epic, new Size(WidthSet / 4, HeightSet / 4), Properties.Resources.crip, 55
                ));
            list.Add(new Enemy(
                "Gunman", 20, Rarity.Legendary, new Size(WidthSet / 4, HeightSet / 4), Properties.Resources.gunman, 100
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
            list.Add(new Rooms(2, "Electricity Room", "NULL", KeysRoom.ElectricityRoom, KeysRoom.EngineRoom, Properties.Resources.electricityRoom));
            list.Add(new Rooms(3, "Engine Room", "NULL", KeysRoom.EngineRoom, KeysRoom.VaultDoor, Properties.Resources.engineRoom));
            list.Add(new Rooms(4, "Vault Air-Lock", "NULL", KeysRoom.VaultDoor, KeysRoom.RogersShrineDoor, Properties.Resources.vaultDoor));
            list.Add(new Rooms(5, "Roger's Shrine", "NULL", KeysRoom.RogersShrineDoor, KeysRoom.BorysHQDoor, Properties.Resources.rogersShrine));
            list.Add(new Rooms(6, "Blueberry's HeadQuarters Room", "NULL", KeysRoom.BorysHQDoor, KeysRoom.BorysHQDoor, Properties.Resources.borysHQ));

            list.Add(new Rooms(7, "Storage room", "NULL", KeysRoom.Catacombs, KeysRoom.Catacombs, Properties.Resources.blackPanel));
            list.Add(new Rooms(8, "Medic's lab", "NULL", KeysRoom.Catacombs, KeysRoom.Catacombs, Properties.Resources.medicslab));
            list.Add(new Rooms(9, "Armory", "NULL", KeysRoom.Catacombs, KeysRoom.Catacombs, Properties.Resources.armory));
            list.Add(new Rooms(10, "Main room", "NULL", KeysRoom.Catacombs, KeysRoom.Catacombs, Properties.Resources.mainRoom));

            return list;

        }
        public void RefreshLabel()
        {
            if (PHotBar.Tag.ToString() != "0")
            {
                AmmoLabel.Text = CurrentPlayer.HotBar.CurrentAmountOfRounds.ToString();
            }
            else
            {
                AmmoLabel.Text = "0";
            }
        }
        public void ShowDrops(Rooms GivenRoom)
        {
            CloseMap();
            OpenInventory();
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
        public void GoTo(Rooms GivenRoom)
        {
            CurrentRoomR = GivenRoom;
            switch (GivenRoom.ID)
            {
                case 7:

                    CloseInventory();
                    CloseMap();
                    OpenInventory();
                    StorageSpace.Visible = true;
                    break;
                case 8:
                    CurrentRoom.BackgroundImage = AllRooms[7].Img;
                    CurrentRoom.Visible = true;

                    break;
            }
            
        }
        public void SpawnARoom(Rooms GivenRoom)
        {
            if(GivenRoom.ID >= 7)
            {
                GoTo(GivenRoom); //timhle jen serazuju spawning a goto room, jsou to dve jine funkce logicky lmfao :)
                return;
            }
            PHotBar.Visible = true;
            AmmoLabel.Visible = true;
            statsPanel.Visible = true;
            InventoryToStorage.Visible = false;
            CurrentRoom.BackgroundImage =GivenRoom.Img;

            Random rnd = new Random();
            GivenRoom.Drops = GivenRoom.CreateDrops(GivenRoom.RequiredKey, AllItems);
            GivenRoom.Enemies = GivenRoom.CreateEnemies(GivenRoom.RequiredKey, AllEnemies);
            CurrentEnemy = GivenRoom.Enemies[0];
            enemyIsDead = false;

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
            Task.Run(() => Attack());
            CurrentRoomR = GivenRoom;
        }
        public void ChangeStats()
        {
            healthBar.Size = new Size(Convert.ToInt32(CurrentPlayer.Health * 1.2), healthBar.Height);
            thirstBar.Size = new Size(Convert.ToInt32(CurrentPlayer.Thirst *1.2),thirstBar.Height);
            hungerBar.Size = new Size(Convert.ToInt32(CurrentPlayer.Hunger *1.2), hungerBar.Height);
            radiationBar.Size = new Size(Convert.ToInt32(CurrentPlayer.Radiation* 1.2), radiationBar.Height);
        }
        public void OpenMap()
        {
            mapPanel.Visible = true;
            CloseInventory();
            
        }
        public void CloseMap()
        {
            mapPanel.Visible = false;
        }
        public void OpenInventory()
        {
            InventoryToStorage.Visible =true;  //opens inventory
            CloseMap();
                DropsPanel.Visible = false;
            StorageSpace.Visible = false;
                PHotBar.Visible = true;
                statsPanel.Visible = true;
                AmmoLabel.Visible = true;
        }
        public void CloseInventory()
        {
            InventoryToStorage.Visible = false;
            StorageSpace.Visible = false;
            DropsPanel.Visible = false;
                AmmoLabel.Visible = false;
                statsPanel.Visible = false;
            PHotBar.Visible = false;
        }
        public void ClearRooms()
        {
            CurrentRoom.Controls.Clear();
            CurrentRoomR = null;
        }

        //END OF METHODS
        //START OF EVENTS
        private void useEvent(object sender, EventArgs e)
        {
            if (AllItems[Convert.ToInt32((sender as Button).Tag)-1] is Consumables)
            {
                
                CurrentPlayer.Health += (AllItems[Convert.ToInt32((sender as Button).Tag) - 1] as Consumables).HealthAdd;
                CurrentPlayer.HealthWA += (AllItems[Convert.ToInt32((sender as Button).Tag) - 1] as Consumables).HealthAdd;
                CurrentPlayer.Thirst += (AllItems[Convert.ToInt32((sender as Button).Tag) - 1] as Consumables).DrinkAdd;
                CurrentPlayer.Hunger += (AllItems[Convert.ToInt32((sender as Button).Tag) - 1] as Consumables).FoodAdd;
                CurrentPlayer.Radiation += (AllItems[Convert.ToInt32((sender as Button).Tag) - 1] as Consumables).RadAdd;

            }
            
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
            try { 
            SpawnARoom(AllRooms[currentRoomID]);
            } catch
            {
                MessageBox.Show("mistnost jeste neni dodelana..");
            }
            CurrentRoom.Visible = true;
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
                    MessageBox.Show("An error has occured, please contact beneckym.05@spst.eu with your error, error code: ");
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
            RefreshLabel();

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
                    CreateMessage("Tohle je jen Beta verze!!!!\nPro otevreni mapy - M\nPro otevreni inventare - E\nStrileni - LMB\n Reload - R\n jo a mensi oznameni, hra je nehorazne hlasita protoze neexistuje nic jako volumecontrol na soundplayeru, takze doporocuji ztisit!!!!");
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
                    if(CurrentRoomR == null) { 
                    OpenMap();
                    }
                    break; //Opens map
                case Keys.E:
                    if (CurrentRoomR == null)
                    {
                        OpenInventory();
                    }
                    break;
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
        InspectStorage,
        RunElectriity,
        ThreathenHorkymi,
        BringBackARareItem,
        RunEngine,
        MurderKorky,
        ObtainAGasMask,
        RunVault
        //to be done
    };
    public enum TypeOfCons
    {
        Health,
        Drink,
        Food,
        Rad
    };
}