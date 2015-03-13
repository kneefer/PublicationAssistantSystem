using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;
using PublicationAssistantSystem.Web.Controllers;

namespace PublicationAssistantSystem.Web.UnitTests.FacultyControllerTests
{
    [TestFixture]
    public class TestGetItems
    {
        [Test]
        public void TestReturnAllItems()
        {
            // Assign
            var testRepository = new List<Faculty>
            {
                new Faculty {Id = 0, Name = "Test Faculty 1"},
                new Faculty {Id = 1, Name = "Test Faculty 2"},
                new Faculty {Id = 2, Name = "Test Faculty 3"},
                new Faculty {Id = 3, Name = "Test Faculty 4"},
            };

            var contextMock = new Mock<IPublicationAssistantContext>();
            var repositoryMock = new Mock<IFacultyRepository>();
            repositoryMock.Setup(m => m.Get(null, null, "")).Returns(testRepository);

            var controller = new FacultyController(contextMock.Object, repositoryMock.Object);

            // Act
            var result = controller.FacultyIndex();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);

            var viewResult = result as ViewResult;
            if (viewResult == null) 
                return;

            Assert.IsInstanceOf<IEnumerable<Faculty>>(viewResult.Model);
            var model = viewResult.Model as IEnumerable<Faculty>;
            if (model != null)
            {
                CollectionAssert.AreEquivalent(testRepository, model);
            }
        }
    }
}
