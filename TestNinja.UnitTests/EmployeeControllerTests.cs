using NUnit.Framework;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test] 
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDB()
        {
            var storage = new Mock<IEmployeeStorage>();
            var controller = new EmployeeController(storage.Object);
            controller.DeleteEmployee(1);
            storage.Verify(s => s.DeleteEmployee(1));
        }
    }
}
