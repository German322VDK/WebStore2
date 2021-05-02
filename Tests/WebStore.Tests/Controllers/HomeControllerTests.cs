using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Controllers;
using Assert =  Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            //Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_View()
        {
            const string base_text = "Action with value id: ";

            var controller = new HomeController();

            const string id = "Test_id";

            var result = controller.SecondAction(id);

            var content = Assert.IsType<ContentResult>(result);

            const string exepted_content = base_text + id;

            Assert.Equal(exepted_content, content.Content);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_Test_Error()
        {
            var controller = new HomeController();

            controller.Throw();
        }

        [TestMethod]
        public void Throw_Test_Error2()
        {
            const string exepted_exception_message = "Test Error!";

            var controller = new HomeController();

            Exception exception = null;

            try
            {
                controller.Throw();
            }
            catch(ApplicationException e)
            {
                exception = e;
            }

            var app_exception = Assert.IsType<ApplicationException>(exception);

            Assert.Equal(exepted_exception_message, app_exception.Message);
        }

        [TestMethod]
        public void Throw_Test_Error3()
        {
            const string exepted_exception_message = "Test Error!";

            var controller = new HomeController();

            var exception = Assert.Throws<ApplicationException>(() => controller.Throw());

            Assert.Equal(exepted_exception_message, exception.Message);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Error();

            Assert.IsType<ViewResult>(result);
        }


        [TestMethod]
        public void ErrorStatus_404_Redirect_To_Error404()
        {
            var controller = new HomeController();

            const string expected_action_name = nameof(HomeController.Error);
            const string error_status_404 = "404";

            var result = controller.ErrorStatus(error_status_404);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(expected_action_name, redirect_to_action.ActionName);
            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
