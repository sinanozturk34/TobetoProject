﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.ClassroomGroupRequests
{
    public class UpdateClassroomGroupRequest
    {
        public int Id { get; set; }
        public int ClassroomId { get; set; }
        public int GroupId { get; set; }
    }
}
