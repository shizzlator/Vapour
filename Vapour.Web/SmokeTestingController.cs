using System.Web.Http;

namespace Vapour.Web
{
    public class SmokeTestingController : ApiController
    {
        [Route("smoketest/{appName}/{environment}")]
        public TestResult Get(string appName, string environment)
        {
            return new TestResult();
        }
    }

    public class TestResult
    {
    }
}