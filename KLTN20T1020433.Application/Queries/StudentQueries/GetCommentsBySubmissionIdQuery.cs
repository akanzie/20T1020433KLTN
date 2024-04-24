using AutoMapper;
using KLTN20T1020433.Domain.Comment;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using MediatR;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetCommentsBySubmissionIdQuery : IRequest<IEnumerable<GetCommentResponse>>
    {
        public int SubmissionId { get; set; }
    }
    public class GetCommentsBySubmissionIdQueryHandler : IRequestHandler<GetCommentsBySubmissionIdQuery, IEnumerable<GetCommentResponse>>
    {
        private readonly ICommentRepository _commentDB;
        private readonly IMapper _mapper;

        public GetCommentsBySubmissionIdQueryHandler(ICommentRepository commentDB, IMapper mapper)
        {
            _commentDB = commentDB;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCommentResponse>> Handle(GetCommentsBySubmissionIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentDB.GetCommentsBySubmissionId(request.SubmissionId);
            if (comments != null && comments.Any())
            {
                List<GetCommentResponse> commentResponses = new List<GetCommentResponse>();
                foreach (var item in comments)
                {
                    GetCommentResponse getTestFileResponse = _mapper.Map<GetCommentResponse>(item);
                    commentResponses.Add(getTestFileResponse);
                }
                return commentResponses;
            }
            return new List<GetCommentResponse>();
        }
    }
}
