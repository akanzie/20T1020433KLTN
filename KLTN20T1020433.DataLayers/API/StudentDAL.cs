using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DataLayers.Interfaces;

namespace KLTN20T1020433.DataLayers.API
{
    public class StudentDAL : _BaseApi, IStudentDAL
    {
        public StudentDAL(string baseUrl) : base(baseUrl)
        {
        }
        public async Task<Student?> GetStudent(string studentId)
        {
            Student? student = new Student
            {
                StudentId = "20T1020433",
                FirstName = "Kiệt",
                LastName = "Châu Anh",
            };
            return student;
        }
        public async Task<List<Student>> GetStudentsOfCourse(string courseId)
        {
            try
            {
                // Tạo yêu cầu GET đến điểm cuối API để lấy danh sách sinh viên của khóa học cụ thể
                string endpoint = $"/api/students?courseId={courseId}";
                List<Student> students = await GetAsync<List<Student>>(endpoint);
                return students;
            }
            catch (Exception ex)
            {
                // Xử lý bất kỳ ngoại lệ nào xảy ra, ghi nhật ký hoặc ném lại nếu cần
                Console.WriteLine($"Đã xảy ra lỗi khi lấy danh sách sinh viên: {ex.Message}");
                throw;
            }
        }
    }

}
