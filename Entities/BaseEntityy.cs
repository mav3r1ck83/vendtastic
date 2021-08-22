using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendOrama.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
}
