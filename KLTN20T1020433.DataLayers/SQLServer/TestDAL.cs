using System;
using System.Data;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Enum;
using KLTN20T1020433.DomainModels.Interfaces;
using Dapper;
using System.Buffers;
using Azure;


namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class TestDAL : _BaseDAL, ITestDAL
    {
        public TestDAL(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> AddStudentParticipantTest(string studentId, int testId)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"insert into Tests (studentId, testId) 
                            values (@StudentId,@TestId);
                            ";
                var parameters = new
                {
                    StudentId = studentId,
                    TestId = testId,

                };
                result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
            }
            return result;
        }

        public async Task<int> Add(Test data)
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

        public async Task<Guid> AddTestFile(TestFile file)
        {

            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"insert into TestFiles (FileId, FileName, FilePath, MimeType, Size, 
                                TestId) 
                            values (@FileId,@FileName,@FilePath,@MimeType,@Size,@TestId,
                                );
                            ";
                var parameters = new
                {
                    FileId = file.FileId,
                    FileName = file.FileName ?? "",
                    FilePath = file.FilePath ?? "",
                    MimeType = file.MimeType ?? "",
                    Size = file.Size,
                    TestId = file.TestId

                };
                await connection.ExecuteScalarAsync<Guid>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

            }
            return file.FileId;
        }
        public async Task<bool> DeleteStudentParticipantTest(string studentId, int testId)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"delete from TestStudents where StudentId = @StudentId and TestID = @TestID";
                var parameters = new
                {
                    StudentId = studentId,
                    TestId = testId,

                };
                result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;

            }
            return result;
        }

        public async Task<bool> Delete(int testID)
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
                result = await connection.ExecuteAsync (sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                
            }
            return result;
        }

        public async Task<bool> DeleteTestFile(Guid fileId)
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

        public async Task<Test?> GetById(int testId)
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

        public async Task<TestFile?> GetTestFile(Guid fileId)
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

        public async Task<IList<Student>> GetStudentIdsParticipantTest(int testId)
        {
            List<Student> list = new List<Student>();

            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"select studentId
                            from Submissions as s
                                join Tests as t on s.testId = t.testId
                            where s.testId = @TestId";

                var parameters = new
                {
                    TestId = testId,
                };

                list = (await connection.QueryAsync<Student>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();

                
            }
            return list;
        }

        public async Task<IList<TestFile>> GetFilesOfTest(int testId)
        {
            List<TestFile> list = new List<TestFile>();

            using (var connection = await OpenConnectionAsync())
            {
                var sql = @"select FileId, FileName, FilePath, MimeType, Size
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

        public async Task<IList<Test>> GetTestsOfStudent(int page = 1, int pageSize = 0, string studentId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0, string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Test>> GetTestsForStudentHome(int page = 1, int pageSize = 0, string studentId = "")
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
                    StartTime = data.StartTime,
                    EndTime = data.EndTime,
                    Status = data.Status.ToString(),
                    IsCheckIP = data.IsCheckIP,
                    IsConductedAtSchool = data.IsConductedAtSchool,
                    CreatedTime = data.CreatedTime,
                    LastUpdateTime = data.LastUpdateTime,
                    TestType = data.TestType.ToString(),
                    TeacherId = data.TeacherId,
                    TestId = data.TestId
                };

                result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                
            }

            return result;
        }

        public async Task<bool> CheckFileOwner(string teacherId, Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountTestsOfStudent(string studentId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
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


    }
}
