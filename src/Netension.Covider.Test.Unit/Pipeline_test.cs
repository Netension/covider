using Netension.Covider.Application;
using Xunit;

namespace Netension.Covider.Test.Unit
{
    public class Pipeline_test
    {
        [Fact(DisplayName = "[UNT-PLT0001][Success]: Pipeline test")]
        [Trait("Feature", "Pipeline test")]
        public void PipelineTest_Success()
        {
            Assert.True(true);
        }

        [Fact(DisplayName = "[UNT-PLT0002][Failure]: Pipeline test")]
        [Trait("Feature", "Pipeline test")]
        public void PipelineTest_Failure()
        {
            Assert.True(false);
        }

        [Fact(DisplayName = "[UNT-PLT0003][Success]: Coverage")]
        [Trait("Feature", "Pipeline test")]
        public void PipelineTest_Coverage()
        {
            new Service().Test();

            Assert.True(true);
        }
    }
}
