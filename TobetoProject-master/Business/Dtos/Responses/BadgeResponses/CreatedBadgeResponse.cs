﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.BadgeResponses
{
    public class CreatedBadgeResponse
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
    }
}
