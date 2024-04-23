using Dapper;
using KLTN20T1020433.Domain.Test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class TestFileRepository : _BaseRepository, ITestFileRepository
    {
        public TestFileRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> Add(TestFile file)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = file.FileId,
                        FileName = file.FileName ?? "",
                        FilePath = file.FilePath ?? "",
                        MimeType = file.MimeType ?? "",
                        Size = file.Size,
                        OriginalName = file.OriginalName ?? "",
                        TestId = file.TestId
                    };
                    result = await connection.ExecuteAsync("AddTestFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm tệp kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckFileOwner(string teacherId, Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid fileId)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = fileId
                    };

                    // Call the stored procedure
                    result = await connection.ExecuteAsync("DeleteTestFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<TestFile?> GetById(Guid id)
        {
            try
            {
                TestFile? data = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = id
                    };

                    // Call the stored procedure
                    data =  await connection.QueryFirstOrDefaultAsync<TestFile>("GetTestFileById", parameters, commandType: CommandType.StoredProcedure);
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn tệp kiểm tra: " + ex.Message);
                throw;
            }
        }        

        public async Task<IEnumerable<TestFile>> GetFilesByTestId(int testId)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TestId = testId
                    };

                    // Call the stored procedure
                    return await connection.QueryAsync<TestFile>("GetTestFilesByTestId", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn danh sách các tệp của bài kiểm tra: " + ex.Message);
                throw;
            }
        }
    }
}
