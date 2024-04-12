using System;
using System.Data;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.DomainModels.Interfaces;
using Dapper;


namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class TestDAL : _BaseDAL, ITestDAL
    {
        public TestDAL(string connectionString) : base(connectionString)
        {
        }

        

        public async Task<int> Add(Test data)
        {
            try
            {
                int id = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"insert into Tests (Title, Instruction, StartTime, EndTime, Status, 
                            IsCheckIP, IsConductedAtSchool, CreatedTime, TestType, TeacherId) 
                        values (@Title,@Instruction,@StartTime,@EndTime,@Status,@IsCheckIP,
                            @IsConductedAtSchool,@CreatedTime,@TestType,@TeacherId);
                        SELECT SCOPE_IDENTITY()";
                    var parameters = new
                    {
                        Title = data.Title ?? "",
                        Instruction = data.Instruction ?? "",
                        StartTime = data.StartTime,
                        EndTime = data.EndTime,
                        Status = data.Status.ToString(),
                        IsCheckIP = data.IsCheckIP,
                        IsConductedAtSchool = data.IsConductedAtSchool,
                        CreatedTime = data.CreatedTime,
                        TestType = data.TestType.ToString(),
                        TeacherId = data.TeacherId
                    };
                    id = await connection.ExecuteScalarAsync<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm bài kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<Guid> AddTestFile(TestFile file)
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
                return file.FileId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm tệp kiểm tra: " + ex.Message);
                throw;
            }
        }
       

        public async Task<bool> Delete(int testID)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"begin 
                            delete from Tests where TestID = @TestID
                            delete from TestFiles where TestID = @TestID
                        end;";
                    var parameters = new
                    {
                        TestID = testID
                    };
                    result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa bài kiểm tra và các tệp liên quan: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteTestFile(Guid fileId)
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

        public async Task<Test?> GetById(int testId)
        {
            try
            {
                Test? data = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"select * from Tests where TestID = @TestID";
                    var parameters = new
                    {
                        TestID = testId
                    };
                    data = await connection.QueryFirstOrDefaultAsync<Test>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn bài kiểm tra theo ID: " + ex.Message);
                throw;
            }
        }

        public async Task<TestFile?> GetTestFile(Guid fileId)
        {
            try
            {
                TestFile? data = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"select * from TestFiles where FileID = @FileID";
                    var parameters = new
                    {
                        FileId = fileId
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

       

        public async Task<IList<TestFile>> GetFilesOfTest(int testId)
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

        public async Task<IList<Test>> GetTestsForStudentHome(int page = 1, int pageSize = 0, string studentId = "")
        {
            try
            {
                List<Test> listTests = new List<Test>();
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"
                    with cte as
                    (
	                    select t.*, ROW_NUMBER() over (order by StartTime) as RowNumber
	                    from Tests t join Submissions s on t.TestId = s.TestId
	                    where StudentId like @studentId
                    )

                    select * from cte
                    where (@pageSize= 0)
	                    or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                    order by RowNumber;";

                    var parameters = new
                    {
                        page = page,
                        pageSize = pageSize,
                        studentId = studentId
                    };

                    listTests = (await connection.QueryAsync<Test>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();

                }

                return listTests;
            }
            catch (Exception e)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn danh sách bài kiểm tra của sinh viên: " + e.Message);
                throw;
            }
            
        }

        public async Task<IList<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0, string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn danh sách bài kiểm tra của giáo viên: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckFileOwner(string teacherId, Guid fileId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi kiểm tra quyền sở hữu tệp: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng bài kiểm tra của giáo viên: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountTestsOfStudent(string studentId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng bài kiểm tra của sinh viên: " + ex.Message);
                throw;
            }
        }        

        public async Task<int> CountTestsForStudentHome(string studentId = "")
        {
            int count = 0;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"select count(*) from Tests t join Submissions s on t.TestId = s.TestId
	                    where StudentId like @studentId";
                var parameters = new
                {
                    StudentId = studentId ?? ""
                };
                count = await connection.ExecuteScalarAsync<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

            }
            return count;
        }

        public async Task<bool> Update(Test data)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"update Tests
                            set Title = @Title,
                                Instruction = @Instruction,
                                StartTime = @StartTime,
                                EndTime = @EndTime,
                                Status = @Status,
                                IsCheckIP = @IsCheckIP,
                                IsConductedAtSchool = @IsConductedAtSchool,
                                CreatedTime = @CreatedTime,
                                LastUpdateTime = @LastUpdateTime,
                                TestType = @TestType,
                                TeacherId = @TeacherId
                            where TestId = @TestId";

                var parameters = new
                {
                    Title = data.Title ?? "",
                    Instruction = data.Instruction ?? "",
                    StartTime = data.StartTime ?? null,
                    EndTime = data.EndTime ?? null,
                    Status = data.Status.ToString(),
                    IsCheckIP = data.IsCheckIP,
                    IsConductedAtSchool = data.IsConductedAtSchool,
                    CreatedTime = data.CreatedTime,
                    LastUpdateTime = data.LastUpdateTime ?? null,
                    TestType = data.TestType.ToString(),
                    TeacherId = data.TeacherId,
                    TestId = data.TestId
                };

                result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text) > 0;

            }

            return result;
        }

        public async Task<bool> IsUsed(int id)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"if exists(select * from Submissions where TestId = @TestId)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    TestId = id
                };
                result = await connection.ExecuteScalarAsync<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

            }
            return result;
        }

        public Task<IList<Test>> GetTestsOfStudent(int page = 1, int pageSize = 0, string studentId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }
    }
}
