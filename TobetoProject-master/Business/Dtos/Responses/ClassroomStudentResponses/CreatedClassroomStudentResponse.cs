﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.ClassroomStudentResponses
{
    public class CreatedClassroomStudentResponse
    {
        public int Id { get; set; }
        public int ClassroomGroupId { get; set; }
        public int StudentId { get; set; }
    }
}
