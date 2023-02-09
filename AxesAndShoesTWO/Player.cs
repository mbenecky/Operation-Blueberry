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
        public Clothes[] PlayerClothes;
        public List<Items> PlayerInventory;
        public Guns HotBar;
        public List<KeysRoom> CurrentKeys;
        public Player()
        { }
        public Player(int Health, int Thirst, int Hunger, int Radiation, Clothes[] PlayerClothes, List<Items> PlayerInventory, Guns HotBar, List<KeysRoom> CurrentKeys)
        {
            this.Health = Health;
            this.Thirst = Thirst;
            this.Hunger = Hunger;
            this.Radiation = Radiation;
            this.PlayerClothes = PlayerClothes;
            this.PlayerInventory = PlayerInventory;
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
            sp.healthBar.Size = new Size(Convert.ToInt32(sp.MaxWidth - ((double)sp.MaxWidth / 100 * Health)), sp.healthBar.Height);
            sp.hungerBar.Size = new Size(Convert.ToInt32(sp.MaxWidth - ((double)sp.MaxWidth / 100 * Hunger)), sp.hungerBar.Height);
            sp.thirstBar.Size = new Size(Convert.ToInt32(sp.MaxWidth - ((double)sp.MaxWidth / 100 * Thirst)), sp.thirstBar.Height);
            sp.radiationBar.Size = new Size(Convert.ToInt32(sp.MaxWidth - ((double)sp.MaxWidth / 100 * Radiation)), sp.radiationBar.Height);
        }

        public override string ToString()
        {
            return "BasePlayerName";
        }
    }
}
