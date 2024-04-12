﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Course
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourseByTeacherId(string teacherId);
    }
}