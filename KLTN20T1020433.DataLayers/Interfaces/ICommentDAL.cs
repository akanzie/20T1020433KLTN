using KLTN20T1020433.DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.Interfaces
{
    public interface ICommentDAL
    {
        Task<Comment> GetById(int id);
        Task<int> Add(Comment data);
        Task<bool> Update(Comment data);
        Task<bool> Delete(int id);
        Task<IEnumerable<Comment>> GetCommentBySubmissionId(int submissionId);
    }
}
