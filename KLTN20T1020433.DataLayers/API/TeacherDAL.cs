using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using KLTN20T1020433.DomainModels.Entities;
using DataLayers.Interfaces;
using KLTN20T1020433.DataLayers.SQLServer;

namespace KLTN20T1020433.DataLayers.API
{
    public class TeacherDAL : _BaseApi
    {
        public TeacherDAL(string baseUrl) : base(baseUrl)
        {
        }
    }
}
