﻿using Core.Entities;

namespace Entities.Concretes;

public class SocialMedia : Entity<int>
{
    public string Name { get; set; }
    public List<UserSocialMedia> userSocialMedias { get; set; }
    
}
