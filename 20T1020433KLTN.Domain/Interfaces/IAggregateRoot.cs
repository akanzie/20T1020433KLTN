using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Interfaces
{
    public interface IAggregateRoot
    {
        public int AddEntity(BaseEntity entity);
        public int UpdateEntity(BaseEntity entity);
        public int DeleteEntity(BaseEntity entity);
        public BaseEntity GetBaseEntityById(Guid id);

    }
}
