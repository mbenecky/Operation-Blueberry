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
        public int Thirst;
        public int Hunger;
        public int Radiation;
        public double HealthP = 1;
        public double ThirstP = 1;
        public double HungerP = 1;
        public double RadiationP = 1;
        public Guns HotBar;
        public List<KeysRoom> CurrentKeys;
        public Player()
        { }
        public Player(int Health, int Thirst, int Hunger, int Radiation, Guns HotBar, List<KeysRoom> CurrentKeys)
        {
            this.Health = Health;
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
        
        private void ChangeStats(StatsPanel sp)
        {
            sp.healthBar = new Size(sp.healthBar.Width*HealthP, sp.healthBar.Height);
        }

        public override string ToString()
        {
            return "BasePlayerName";
        }
    }
}
