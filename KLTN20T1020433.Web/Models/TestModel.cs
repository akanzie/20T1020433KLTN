using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModelsModels.Entities;

namespace KLTN20T1020433.Web.Models
{
    public class TestModel
    {
        public Test Test { get; set; }
        public List<TestFile> Files { get; set; }
     
    }
}
