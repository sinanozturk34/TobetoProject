﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concretes
{
    public class UserOperationClaim:Entity<int>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
