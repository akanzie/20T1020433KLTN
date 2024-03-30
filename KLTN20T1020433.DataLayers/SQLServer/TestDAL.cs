using System;
using System.Data;
using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Enum;
using _20T1020433KLTN.Domain.Interfaces;
using Dapper;


namespace _20T1020433KLTN.DataLayers.SQLServer
{
    public class TestDAL : _BaseDAL, ITestDAL
    {
        public TestDAL(string connectionString) : base(connectionString)
        {
        }

        public bool AddStudentParticipantTest(string studentId, int testId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into Tests (studentId, testId) 
                            values (@StudentId,@TestId);
                            ";
                var parameters = new
                {
                    StudentId = studentId,
                    TestId = testId,

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public int AddTest(Test data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into Tests (Title, Instructions, StartTime, EndTime, Status, 
                                IsCheckIP, IsConductedAtSchool, CreatedTime, TestType, TeacherId) 
                            values (@Title,@Instructions,@StartTime,@EndTime,@Status,@IsCheckIP,
                                @IsConductedAtSchool,@CreatedTime,@TestType,@TeacherId);
                            SELECT SCOPE_IDENTITY()";
                var parameters = new
                {
                    Title = data.Title ?? "",
                    Instructions = data.Instructions ?? "",
                    StartTime = data.StartTime,
                    EndTime = data.EndTime,
                    Status = data.Status.ToString(),
                    IsCheckIP = data.IsCheckIP,
                    IsConductedAtSchool = data.IsConductedAtSchool,
                    CreatedTime = data.CreatedTime,
                    TestType = data.TestType.ToString(),
                    TeacherId = data.TeacherId
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public Guid AddTestFile(TestFile file)
        {
            Guid id = Guid.NewGuid();
            using (var connection = OpenConnection())
            {
                var sql = @"insert into TestFiles (FileId, FileName, FilePath, MimeType, Size, 
                                TestId) 
                            values (@FileId,@FileName,@FilePath,@MimeType,@Size,@TestId,
                                );
                            ";
                var parameters = new
                {
                    FileId = id,
                    FileName = file.FileName ?? "",
                    FilePath = file.FilePath ?? "",
                    MimeType = file.MimeType ?? "",
                    Size = file.Size,
                    TestId = file.TestId

                };
                connection.ExecuteScalar<Guid>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Tests where (@SearchValue = N'') or (TestType like @SearchValue)";
                var parameters = new
                {
                    SearchValue = searchValue ?? ""
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool DeleteStudentParticipantTest(string studentId, int testId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from TestStudents where StudentId = @StudentId and TestID = @TestID";
                var parameters = new
                {
                    StudentId = studentId,
                    TestId = testId,

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeleteTest(int testID)
        {

            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Tests where TestID = @TestID";
                var parameters = new
                {
                    TestID = testID
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeleteTestFile(Guid fileId)
        {

            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from TestFiles where FileID = @FileID";
                var parameters = new
                {
                    FileID = fileId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Test GetById(int testId)
        {
            Test? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Tests where TestID = @TestID";
                var parameters = new
                {
                    TestID = testId
                };
                data = connection.QueryFirstOrDefault<Test>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public TestFile? GetTestFile(Guid fileId)
        {
            TestFile? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from TestFiles where FileID = @FileID";
                var parameters = new
                {
                    FileId = fileId
                };
                data = connection.QueryFirstOrDefault<TestFile>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public Student? GetStudent(string studentId)
        {
            throw new NotImplementedException();
        }

        public IList<Student> GetStudentIdsParticipantTest(int testId)
        {
            List<Student> list = new List<Student>();

            using (var connection = OpenConnection())
            {
                var sql = @"select studentId
                            from TestStudents as ts
                                join Tests as t on ts.testId = t.testId
                            where ts.testId = @TestId";

                var parameters = new
                {
                    TestId = testId,
                };

                list = connection.Query<Student>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();

                connection.Close();
            }
            return list;
        }

        public IList<TestFile> GetFilesOfTest(int testId)
        {
            List<TestFile> list = new List<TestFile>();

            using (var connection = OpenConnection())
            {
                var sql = @"select FileId, FileName, FilePath, MimeType, Size
                            from TestFiles as tf
                                join Tests as t on tf.testId = t.testId
                            where ts.testId = @TestId";

                var parameters = new
                {
                    TestId = testId,
                };

                list = connection.Query<TestFile>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();

                connection.Close();
            }
            return list;
        }

        public IList<Test> GetTestsOfStudent(int page = 1, int pageSize = 0, string studentId = "", string searchValue = "", TestType testType = TestType.All, TestStatus testStatus = TestStatus.All, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }

        public IList<Test> GetTestsOfTeacher(int page = 1, int pageSize = 0, string teacherId = "", string searchValue = "", TestType testType = TestType.All, TestStatus testStatus = TestStatus.All, DateTime? fromTime = null, DateTime? toTime = null)
        {
            throw new NotImplementedException();
        }

        public IList<Test> GetTestsForStudentHome(int page = 1, int pageSize = 0, string studentId = "")
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from TestFiles where TestId = @TestId)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    ShipperID = id
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public bool UpdateTest(Test data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update Tests
                            set Title = @Title,
                                Instructions = @Instructions,
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
                    Instructions = data.Instructions ?? "",
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

                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }

            return result;
        }
    }
}
