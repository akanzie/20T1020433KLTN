using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Domain.Test
{
    public interface ITestFileRepository
    {
        Task<bool> Add(TestFile file);

        Task<bool> Delete(Guid fileId);

        Task<IEnumerable<TestFile>> GetFilesByTestId(int testId);

        Task<TestFile?> GetById(Guid id);
        Task<bool> CheckFileOwner(string teacherId, Guid fileId);
    }
}
