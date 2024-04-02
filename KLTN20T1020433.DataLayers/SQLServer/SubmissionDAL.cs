using System;
using System.Net;
using System.Numerics;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Interfaces;
using Dapper;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class SubmissionDAL : _BaseDAL, ISubmissionDAL
    {
        public SubmissionDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Submission data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"INSERT INTO Submissions (StudentId, TestId, SubmittedTime, IPAddress, Status)
                    VALUES (@StudentId, @TestId, @SubmittedTime, @IPAddress, @Status);
                    SELECT SCOPE_IDENTITY()";
                var parameters = new
                {
                    StudentId = data.StudentId,
                    TestId = data.TestId,
                    SubmittedTime = data.SubmittedTime,
                    IPAddress = data.IPAddress,
                    Status = data.Status.ToString()
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }
        public Guid AddSubmissionFile(SubmissionFile file)
        {
            Guid id = Guid.NewGuid();
            using (var connection = OpenConnection())
            {
                var sql = @"INSERT INTO SubmissionFiles (FileId, SubmissionId, FileName, FilePath, MimeType, Size)
                    VALUES (@FileId, @SubmissionId, @FileName, @FilePath, @MimeType, @Size);
                            ";
                var parameters = new
                {
                    FileId = id,
                    FileName = file.FileName ?? "",
                    FilePath = file.FilePath ?? "",
                    MimeType = file.MimeType ?? "",
                    Size = file.Size,
                    SubmissionId = file.SubmissionId

                };
                connection.ExecuteScalar<Guid>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return id;
        }                

        public bool DeleteSubmissionFile(Guid fileId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"DELETE FROM SubmissionFiles WHERE FileId = @FileId";
                var parameters = new
                {
                    FileId = fileId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Submission Get(int testId, string studentId)
        {
            Submission? submission = null;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Submissions WHERE TestId = @TestId AND StudentId = @StudentId";
                var parameters = new
                {
                    TestId = testId,
                    StudentId = studentId
                };
                submission = connection.QueryFirstOrDefault<Submission>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return submission;
        }

        public Submission? GetById(int submissionId)
        {
            Submission? submission = null;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Submissions WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                submission = connection.QueryFirstOrDefault<Submission>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return submission;
        }

        public IList<SubmissionFile> GetFilesOfSubmission(int submissionId)
        {
            List<SubmissionFile> files = new List<SubmissionFile>();
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM SubmissionFiles WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmissionId = submissionId
                };
                files = connection.Query<SubmissionFile>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();
            }
            return files;
        }

        public SubmissionFile? GetSubmissionFile(Guid fileId)
        {
            SubmissionFile? file = null;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM SubmissionFiles WHERE FileId = @FileId";
                var parameters = new
                {                    
                    FileId = fileId
                };
                file = connection.QueryFirstOrDefault<SubmissionFile>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return file;
        }

        public IList<Submission> GetSubmissions(int testId)
        {
            List<Submission> submissions = new List<Submission>();
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Submissions WHERE TestId = @TestId";
                var parameters = new
                {
                    TestId = testId
                };
                submissions = connection.Query<Submission>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();
            }
            return submissions;
        }

        public bool Update(Submission data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE Submissions
                    SET SubmittedTime = @SubmittedTime, IPAddress = @IPAddress, Status = @Status
                    WHERE SubmissionId = @SubmissionId";
                var parameters = new
                {
                    SubmittedTime = data.SubmittedTime,
                    IPAddress = data.IPAddress,
                    Status = data.Status,
                    SubmissionId = data.SubmissionId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

    }
}
