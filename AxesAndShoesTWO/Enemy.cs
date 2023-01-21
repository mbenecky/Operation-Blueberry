using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
namespace AxesAndShoesTWO
{
    public class Enemy
    {
        Random rnd = new Random();
        public string Name;
        public int Damage;
        public static int WaitTime;
        public PictureBox pbImage;
        public List<Items> Drops;
        public Enemy()
        {
            pbImage = new PictureBox();
            Drops = new List<Items>();
            WaitTime = rnd.Next(0, 4);
        }
        public Enemy(string Name, int Damage,PictureBox pbImage, List<Items> Drops)
        {
            WaitTime = rnd.Next(0, 5);
            this.Name = Name;
            this.Damage = Damage;
            this.pbImage = pbImage;
            this.Drops = Drops; 
        }
        async Task Attack()
        {

            await Task.Delay(WaitTime*1000);
            //attack on player
        }
        

    }
}
