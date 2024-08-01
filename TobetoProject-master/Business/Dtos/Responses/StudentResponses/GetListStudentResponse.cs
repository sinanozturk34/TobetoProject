﻿using Business.Dtos.Responses.UserResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.StudentResponses
{
    public class GetListStudentResponse
    {
        public int Id { get; set; }
        public int StudentNumber { get; set; }
        public int UserId { get; set; }
        public GetListlimitedUserResponse UserDetail { get; set; }            
    }
}
