﻿using KLTN20T1020433.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.Interfaces
{
    public interface ITeacherDAL
    {
        Task <Teacher?> GetTeacher(string id);

    }
}
