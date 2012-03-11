﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BoxOffice.Models
{
    public class CastMember
    {
        public int CastMemberID { get; set; }

        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        public string Character { get; set; }
        
        public string Job { get; set; }

        // public ??? thumb { get; set; }
        
        public string Department { get; set; }
        
        [DataType(DataType.Url)]
        public string Url { get; set; }
        
        public int Order { get; set; }
        
        public int Cast_id { get; set; }
    }
}