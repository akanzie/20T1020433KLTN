using Dapper;
using KLTN20T1020433.Domain.Test;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class TestRepository : _BaseRepository, ITestRepository
    {
        public TestRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> Add(Test data)
        {
            try
            {
                int id = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Title", data.Title ?? "");
                    parameters.Add("@Instruction", data.Instruction ?? "");
                    parameters.Add("@StartTime", data.StartTime);
                    parameters.Add("@EndTime", data.EndTime);
                    parameters.Add("@Status", data.Status.ToString());
                    parameters.Add("@IsCheckIP", data.IsCheckIP);
                    parameters.Add("@IsConductedAtSchool", data.IsConductedAtSchool);
                    parameters.Add("@CanSubmitLate", data.CanSubmitLate);
                    parameters.Add("@CreatedTime", data.CreatedTime);
                    parameters.Add("@LastUpdateTime", data.LastUpdateTime);
                    parameters.Add("@TestType", data.TestType.ToString());
                    parameters.Add("@TeacherId", data.TeacherId);
                    parameters.Add("@TestId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    await connection.ExecuteAsync(
                       "AddTest", parameters, commandType: CommandType.StoredProcedure);
                    id = parameters.Get<int>("@TestId");

                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm bài kiểm tra: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountTestsOfStudent(string studentId = "", string searchValue = "", TestType? testType = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                int count = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        StudentId = studentId,
                        SearchValue = searchValue ?? "",
                        TestType = testType.ToString() ?? "",
                        FromTime = fromTime,
                        ToTime = toTime
                    };
                    count = await connection.ExecuteScalarAsync<int>(
                        "CountTestsOfStudent", parameters, commandType: CommandType.StoredProcedure);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng bài kiểm tra của sinh viên: " + ex.Message);
                throw;
            }
        }

        public async Task<int> CountTestsOfTeacher(string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                int count = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TeacherId = teacherId ?? "",
                        SearchValue = searchValue ?? "",
                        TestType = testType.ToString() ?? "",
                        TestStatus = testStatus.ToString() ?? "",
                        FromTime = fromTime,
                        ToTime = toTime
                    };
                    count = await connection.ExecuteScalarAsync<int>(
                        "CountTestsOfTeacher", parameters, commandType: CommandType.StoredProcedure);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng bài kiểm tra của giáo viên: " + ex.Message);
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

                    var parameters = new
                    {
                        TestID = testID
                    };
                    result = await connection.ExecuteAsync(
                        "DeleteTest", parameters, commandType: CommandType.StoredProcedure) > 0;

                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa bài kiểm tra và các tệp liên quan: " + ex.Message);
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
                    data = await connection.QueryFirstOrDefaultAsync<Test>(
                        "GetTestById",
                        new { TestID = testId },
                        commandType: CommandType.StoredProcedure
                    );
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn bài kiểm tra theo ID: " + ex.Message);
                throw;
            }
        }


        public async Task<IEnumerable<Test>> GetTestsOfStudent(int page = 1, int pageSize = 0, string studentId = "", string searchValue = "", TestType? testType = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                List<Test> listTests = new List<Test>();
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        Page = page,
                        PageSize = pageSize,
                        StudentId = studentId,
                        SearchValue = searchValue ?? "",
                        TestType = testType.ToString() ?? "",
                        FromTime = fromTime,
                        ToTime = toTime
                    };
                    var result = await connection.QueryAsync<Test>(
                        "GetTestsOfStudent",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    listTests = result.ToList();
                }
                return listTests;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy danh sách bài kiểm tra của sinh viên: " + ex.Message);
                throw;
            }
        }


        public async Task<IEnumerable<Test>> GetTestsOfTeacher(int page = 1, int pageSize = 0, string teacherId = "", string searchValue = "", TestType? testType = null, TestStatus? testStatus = null, DateTime? fromTime = null, DateTime? toTime = null)
        {
            try
            {
                List<Test> listTests = new List<Test>();
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        Page = page,
                        PageSize = pageSize,
                        TeacherId = teacherId,
                        SearchValue = searchValue ?? "",
                        TestType = testType.ToString() ?? "",
                        TestStatus = testStatus.ToString() ?? "",
                        FromTime = fromTime,
                        ToTime = toTime
                    };
                    var result = await connection.QueryAsync<Test>(
                        "GetTestsOfTeacher",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    listTests = result.ToList();
                }
                return listTests;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy danh sách bài kiểm tra của giáo viên: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> IsUsed(int id)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    TestId = id
                };
                result = await connection.ExecuteScalarAsync<bool>(
                    "IsTestUsed",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

            }
            return result;
        }

        public async Task<bool> Update(Test data)
        {
            bool result = false;
            using (var connection = await OpenConnectionAsync())
            {
                var parameters = new
                {
                    Title = data.Title ?? "",
                    Instruction = data.Instruction ?? "",
                    StartTime = data.StartTime ?? null,
                    EndTime = data.EndTime ?? null,
                    Status = data.Status.ToString(),
                    IsCheckIP = data.IsCheckIP,
                    IsConductedAtSchool = data.IsConductedAtSchool,
                    CanSubmitLate = data.CanSubmitLate,
                    CreatedTime = data.CreatedTime,
                    LastUpdateTime = data.LastUpdateTime,
                    TestType = data.TestType.ToString(),
                    TeacherId = data.TeacherId,
                    TestId = data.TestId
                };

                result = await connection.ExecuteAsync(
               "UpdateTest", parameters, commandType: CommandType.StoredProcedure) > 0;

            }

            return result;
        }
    }
}
