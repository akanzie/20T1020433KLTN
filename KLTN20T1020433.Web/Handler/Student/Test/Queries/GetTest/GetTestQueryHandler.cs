using AutoMapper;
using KLTN20T1020433.Domain.Test;
using KLTN20T1020433.Web.Areas.Student.Models.TestModel;
using MediatR;

namespace KLTN20T1020433.Web.Handler.Student.Test.Queries.GetTest
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, GetTestResponse>
    {
        private readonly ITestRepository _dbContext;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IDDDExampleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<DDDExample.Infrastructure.Entities.Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == request.Id)
                                                .Include(o => o.User).ThenInclude(u => u.Role)
                                                .AsNoTracking()
                                             .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                                             .FirstOrDefaultAsync();
            if (order != null) return order;
            return new DDDExample.Infrastructure.Entities.Order();
        }
    }
}
