﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AxesAndShoesTWO
{
    public class Items
    {
        public int ID;
        public string Name;
        public string Description;
        public int Count;
        public int MaxCount;
        public Rarity Rarity;
        public Image img;
        public Items() { }
        public Items(int ID, string Name, string Description, Rarity Rarity, Image img)
        {
            this.ID = ID;
            this.Name= Name;
            this.Description= Description;
            this.Rarity = Rarity;
            this.img = img;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    
    
}
