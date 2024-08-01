﻿using Core.Entities;

namespace Entities.Concretes
{
    public class Country:Entity<int>
    {
        public string Name { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
