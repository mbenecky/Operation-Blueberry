using System.Collections.Generic;
using System.Threading.Tasks;
namespace AxesAndShoesTWO
{
    public class Player
    {
        public int Health;
        public int Thirst;
        public int Hunger;
        public int Radiation;
        public List<Clothes> PlayerClothes;
        public List<Items> PlayerInventory;
        public List<Items> HotBar;
        public List<KeysRoom> CurrentKeys;
        public Player()
        { }
        public Player(int Health, int Thirst, int Hunger, int Radiation, List<Clothes> PlayerClothes, List<Items> PlayerInventory, List<Items> HotBar, List<KeysRoom> CurrentKeys)
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
        public bool IsAbleToAccess(KeysRoom GivenKey)
        {
            if(this.CurrentKeys.Contains(GivenKey))
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
        
        public override string ToString()
        {
            return "BasePlayerName";
        }
    }
}
