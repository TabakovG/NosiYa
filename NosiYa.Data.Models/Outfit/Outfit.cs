using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NosiYa.Data.Models.Outfit
{
    public class Outfit
    {
        public Outfit()
        {
            this.Accessories = new HashSet<Accessory>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Region Region { get; set; } = null!;

        public ICollection<Accessory> Accessories { get; set; }

        public int ShirtId { get; set; } 
        public Shirt Shirt { get; set; } = null!;

        public int LegsId { get; set; }
        public Legs Legs { get; set; } = null!;

        public int? VestId { get; set; }
        public Vest? Vest { get; set; }

        public int? BeltId { get; set; }
        public Belt? Belt { get; set; }

        public UserType UserType { get; set; }

        public string Description { get; set; } = null!;


    }
}
