using System.Threading.Tasks;
using Moq;
using Quartz;
using ViajaNet.JobApplication.Host.Web.Controllers;
using ViajaNet.JobApplication.Host.Worker;
using Xunit;

namespace ViajaNet.JobApplication.Tests
{
    public class QueueComsumptionJobTests
    {
        [Fact]
        public void Validate_Job_Execution_Succeeds()
        {
            //Arrange
            var context = Mock.Of<IJobExecutionContext>();
            var mock = new Mock<QueueConsumptionJob>();

            /* mock.Setup(x => x.Execute(It.IsAny<IJobExecutionContext>()))
                .Returns(Task.CompletedTask); */

            //Act
            // var task = mock.Object.Execute(context);

            //Assert
            // Assert.NotNull(new { });
        }
    }
}
