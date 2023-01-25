
namespace AxesAndShoesTWO
{
    public class Player
    {
        public int Health;
        public int Thirst;
        public int Hunger;
        public int Radiation;
        public Inventory PlayerClothes;
        public Inventory PlayerInventory;
        public Inventory HotBar;
        public Player()
        {}
        public Player(int Health, int Thirst, int Hunger, int Radiation, Inventory PlayerClothes, Inventory PlayerInventory, Inventory Hotbar)
        {
            this.Health = Health;
            this.Thirst = Thirst;
            this.Hunger = Hunger;
            this.Radiation = Radiation;
            this.PlayerClothes = PlayerClothes;
            this.PlayerInventory = PlayerInventory;
            this.HotBar = Hotbar;
        }
        public bool IsAlive()
        {
            if(this.Health <= 0)
            {
                return true;
            }
            return false;
        }
        public bool
        public override string ToString()
        {
            return "BasePlayerName";
        }
    }
}
