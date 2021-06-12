using Xunit;

namespace Netension.Covider.Test.Integration
{
    public class Pipeline_test
    {
        [Fact(DisplayName = "[INT-PLT0001][Success]: Pipeline test")]
        [Trait("Feature", "Pipeline test")]
        public void PipelineTest_Success()
        {
            Assert.True(true);
        }

        [Fact(DisplayName = "[INT-PLT0002][Failure]: Pipeline test")]
        [Trait("Feature", "Pipeline test")]
        public void PipelineTest_Failure()
        {
            Assert.True(false);
        }
    }
}
