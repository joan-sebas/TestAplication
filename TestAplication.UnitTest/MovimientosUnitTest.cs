using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using TestAplication.Controllers;
using TestAplication.Models.ApiModels;
using TestAplication.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using TestAplication.EntityFramework.Data;

namespace TestAplication.Tests
{
    [TestFixture]
    public class MovimientoControllerTests
    {
        [Test]
        public void GetMovimientos_ReturnsOkResult_WithListOfMovimientos()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var movimientos = new List<Movimiento>();
           

            mockUnitOfWork.Setup(uow => uow.Movimientos.Get()).Returns(movimientos);

            var controller = new MovimientoController(mockUnitOfWork.Object, mockMapper.Object);

            // Act
            var result = controller.GetMovimientos() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);

            var model = result.Value as IEnumerable<MovimientoApiModel>;
            Assert.IsNotNull(model);
            Assert.AreEqual(movimientos.Count, model.Count());
        }
    }
}
