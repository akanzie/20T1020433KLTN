using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Domain.Teacher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetCommentsBySubmissionIdQuery : IRequest<IEnumerable<GetCommentResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetCommentsBySubmissionIdQueryHandler : IRequestHandler<GetCommentsBySubmissionIdQuery, IEnumerable<GetCommentResponse>>
    {
        private readonly ICommentRepository _commentDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetCommentsBySubmissionIdQueryHandler(ICommentRepository commentDB, ITeacherRepository teacherDB, IMapper mapper)
        {
            _commentDB = commentDB;
            _teacherDB = teacherDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCommentResponse>> Handle(GetCommentsBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comments = await _commentDB.GetCommentsBySubmissionId(request.SubmissionId);
                
                if (comments != null && comments.Any())
                {
                    List<GetCommentResponse> commentResponses = new List<GetCommentResponse>();
                    foreach (var item in comments)
                    {
                        var teacher = await _teacherDB.GetTeacherById(item.TeacherId);
                        GetCommentResponse comment = _mapper.Map<GetCommentResponse>(item);
                        comment.TeacherName = teacher.TeacherName;
                        commentResponses.Add(comment);
                    }
                    return commentResponses;
                }
                return new List<GetCommentResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy danh sách bình luận: " + ex.Message);
                throw;
            }
        }
    }
}
