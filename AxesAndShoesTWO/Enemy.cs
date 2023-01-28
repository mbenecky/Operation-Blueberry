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
        public static int WaitTime;
        public Rarity RarityOfEnemy;
        public Types Type;
        public Size Size;
        public Image Img;
        public Enemy()
        {

        }
        public Enemy(string Name, int Damage,Rarity RarityOfEnemy,Types Type,Size Size,Image Img)
        {
            WaitTime = rnd.Next(0, 5);
            this.Name = Name;
            this.Damage = Damage;
            this.RarityOfEnemy= RarityOfEnemy;
            this.Type = Type;
            this.Size = Size;
            this.Img = Img;

        }
        async Task Attack()
        {

            await Task.Delay(WaitTime*1000);
            //attack player
        }
        
        public enum Types
        {
            Flying,
            Ground
        };
    }
}
