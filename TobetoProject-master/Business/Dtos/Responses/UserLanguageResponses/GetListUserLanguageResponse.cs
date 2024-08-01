﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.UserLanguageResponses
{
    public class GetListUserLanguageResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int LanguageLevelId { get; set; }
        public string LanguageLevelName { get; set; }
    }
}
