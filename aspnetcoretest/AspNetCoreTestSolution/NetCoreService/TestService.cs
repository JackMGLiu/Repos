using System;
using NetCoreRepository;

namespace NetCoreService
{
    public class TestService: ITestService
    {
        protected ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            this._testRepository = testRepository;
        }

        public long GetUsersCount()
        {
            try
            {
                string sql = "select count(*) from test";
                return _testRepository.GetCount(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
