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
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"insert into TestFiles (FileId, FileName, FilePath, MimeType, Size, OriginalName,
                            TestId) 
                        values (@FileId,@FileName,@FilePath,@MimeType,@Size,@OriginalName,@TestId)";
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
                    await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return true;
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
                    var sql = @"delete from TestFiles where FileID = @FileID";
                    var parameters = new
                    {
                        FileID = fileId
                    };
                    result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<TestFile> GetById(Guid id)
        {
            try
            {
                TestFile? data = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"select * from TestFiles where FileID = @FileID";
                    var parameters = new
                    {
                        FileId = id
                    };
                    data = await connection.QueryFirstOrDefaultAsync<TestFile>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn tệp kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TestFile>> GetFileByTestId(int testId)
        {
            try
            {
                List<TestFile> list = new List<TestFile>();

                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"select tf.*
                        from TestFiles as tf
                            join Tests as t on tf.testId = t.testId
                        where tf.testId = @TestId";

                    var parameters = new
                    {
                        TestId = testId,
                    };

                    list = (await connection.QueryAsync<TestFile>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn danh sách các tệp của bài kiểm tra: " + ex.Message);
                throw;
            }
        }

        public Task<IEnumerable<TestFile>> GetFilesByTestId(int testId)
        {
            throw new NotImplementedException();
        }
    }
}
