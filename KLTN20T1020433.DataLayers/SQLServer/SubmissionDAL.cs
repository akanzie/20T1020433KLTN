using System;
using System.Net;
using System.Numerics;
using _20T1020433KLTN.Domain.Entities;
using _20T1020433KLTN.Domain.Interfaces;
using Dapper;
using DataLayers.SQLServer;

namespace _20T1020433KLTN.DataLayers.SQLServer
{
    public class SubmissionDAL : _BaseDAL, ISubmissionDAL
    {
        public SubmissionDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Submission data)
        {
            throw new NotImplementedException();
        }

        public int AddSubmissionFile(SubmissionFile file)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int submissionId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSubmissionFile(SubmissionFile file)
        {
            throw new NotImplementedException();
        }

        public Submission Get(int SubmissionId)
        {
            throw new NotImplementedException();
        }

        public IList<SubmissionFile> GetFilesOfSubmission(int submissionId = 0)
        {
            throw new NotImplementedException();
        }

        public SubmissionFile? GetSubmissionFile(int submission, int fileId)
        {
            throw new NotImplementedException();
        }

        public IList<Submission> GetSubmissionsOfTest(string testId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Submission data)
        {
            throw new NotImplementedException();
        }
    }
}
