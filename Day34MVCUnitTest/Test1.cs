using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCDemoLabpart1.Controllers;
namespace Day34MVCUnitTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange : create an instance of the controller
            TestController controller = new TestController();

            // Act : it is used to test the methods of the controller
            double result = controller.div(10, 2);

            // Assert : verify the result
            Assert.AreEqual(5, result); // it take two parameters expected and actual
        }
        [TestMethod] // testm 
        public void TestView()
        {
            // Arrange : create an instance of the controller
            TestController controller = new TestController();

            // Act : it is used to test the methods of the controller
            var result = controller.Index() as ViewResult; // here we are casting it to ViewResult to access ViewData

            // Assert : verify the result
            Assert.AreEqual("This is TestController Index Action Method", result.ViewData["Title"]);
        }
    }
}
