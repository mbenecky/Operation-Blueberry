using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System;

namespace AxesAndShoesTWO
{
    public class Player
    {
        public int Health;
        public int HealthWA;
        public int Thirst;
        public int Hunger;
        public int Radiation;
        public Guns HotBar;
        public List<KeysRoom> CurrentKeys;
        public Player()
        { }
        public Player(int Health, int Thirst, int Hunger, int Radiation, Guns HotBar, List<KeysRoom> CurrentKeys)
        {
            this.Health = Health;
            this.HealthWA = Health;
            this.Thirst = Thirst;
            this.Hunger = Hunger;
            this.Radiation = Radiation;
            this.HotBar = HotBar;
            this.CurrentKeys= CurrentKeys;
        }
        public bool IsAlive()
        {
            if (this.Health <= 0)
            {
                return true;
            }
            return false;
        }
        public bool IsTorH()
        {
            if (this.Thirst <= 0 || this.Hunger <= 0)
            {
                return true;
            }
            return false;

        }
        
        async Task DegradeHealth()
        {
            while(IsTorH())
            {
                this.Health -= 1;
                await Task.Delay(1000);
            }
        }
        
        //200 - 480px
        //100 - 240px
        //1 - 4,8px


        public void ChangeStats(StatsPanel sp)
        {
            sp.healthBar.Size = new Size(Convert.ToInt32(this.Health * 2.4), sp.healthBar.Height);
            sp.thirstBar.Size = new Size(Convert.ToInt32(this.Thirst * 2.4), sp.healthBar.Height);
            sp.hungerBar.Size = new Size(Convert.ToInt32(this.Hunger * 2.4), sp.healthBar.Height);
            sp.radiationBar.Size = new Size(Convert.ToInt32(this.Radiation * 2.4), sp.healthBar.Height);
        }

        public override string ToString()
        {
            return "BasePlayerName";
        }
    }
}
