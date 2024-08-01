﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class UserSocialMedia : Entity<int>
    {

        public int UserId { get; set; }
        public int SocialMediaId { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}
