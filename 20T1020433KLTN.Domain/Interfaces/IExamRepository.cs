﻿using _20T1020433KLTN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface IExamRepository
    {
        Guid Add(Exam exam);
        Exam GetById(Guid id);
        IEnumerable<Exam> GetAll();
        void Update(Exam exam);
        void Delete(Guid id);
    }
}
