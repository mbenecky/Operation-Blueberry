using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Enemy
    {
        Random rnd = new Random();
        public string Name;
        public int Damage;
        public int Health;
        public static int WaitTime;
        public Rarity RarityOfEnemy;
        public Types Type;
        public Size Size;
        public Image Img;
        public Image DeathImage;
        public Enemy()
        {

        }
        public Enemy(string Name, int Damage, Rarity RarityOfEnemy, Types Type, Size Size, Image Img, int Health, Image deathImage)
        {
            WaitTime = rnd.Next(0, 5);
            this.Name = Name;
            this.Damage = Damage;
            this.RarityOfEnemy = RarityOfEnemy;
            this.Type = Type;
            this.Size = Size;
            this.Img = Img;
            this.Health = Health;
            DeathImage = deathImage;
        }
        async Task Attack()
        {
            await Task.Delay(WaitTime * 1000);

        }
        async Task Death()
        {
            Img = DeathImage;
            await Task.Delay(2000);
        }
       
        public enum Types
        {
            Flying,
            Ground
        };
    }
}
