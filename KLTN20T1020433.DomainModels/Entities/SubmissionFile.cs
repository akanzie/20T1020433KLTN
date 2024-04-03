﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DomainModels.Entities
{
    public class SubmissionFile : File
    {

        public int SubmissionId { get; set; }
        public bool IsSubmitted {  get; set; } = false;
    }
}
