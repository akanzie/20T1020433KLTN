using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DataLayers.SQLServer;
using KLTN20T1020433.DataLayers.Interfaces;

namespace KLTN20T1020433.DataLayers.API
{
    public class TeacherDAL : _BaseApi, ITeacherDAL
    {
        public TeacherDAL(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<Teacher?> GetTeacher(string id)
        {
            Teacher? teacher = new Teacher
            {
                TeacherId = "1",
                FirstName = "A",
                LastName = "Nguyễn Văn",
            };
            return teacher;
        }
    }
}
