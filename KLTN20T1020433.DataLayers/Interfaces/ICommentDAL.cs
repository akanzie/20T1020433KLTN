using KLTN20T1020433.DomainModelsModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.Interfaces
{
    public interface ICommentDAL
    {

        Comment? GetBySubmissionId(int submissionId);
        Comment? Get(int id);
        int Add(Comment data);

        bool Update(Comment data);

        bool Delete(int id);

    }
}
