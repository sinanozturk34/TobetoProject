﻿using System;
using Core.Entities;

namespace Entities.Concretes
{
	public class SchoolName:Entity<int>
	{
		public string Name { get; set; }
        public List<Education> Educations { get; set; }
    }
}

