using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Alexa.Controllers;
using System.Web.Mvc;
using Alexa.Tests.Repositories;
using Alexa.Service.Dtos;
using Alexa.Service;
using Alexa.Tests.Repository;
using Alexa.Service.Interfaces;

namespace Alexa.Tests.Controllers
{
    [TestClass]
    public class SuppliersControllerTest
    {
        AlexaDbContextTest db;
        ISupplierService supplierService;
        IGroupService groupService;
        SuppliersController controller;

        [TestMethod]
        public void Suppliers_Index()
        {
            // Arrange
            db = new AlexaDbContextTest();
            supplierService = new SupplierService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            groupService = new GroupService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            controller = new SuppliersController(supplierService, groupService);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Suppliers_Details()
        {
            // Arrange
            db = new AlexaDbContextTest();
            supplierService = new SupplierService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            groupService = new GroupService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            controller = new SuppliersController(supplierService, groupService);

            // Act
            ViewResult result = controller.Details(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(SupplierDto));
            var supplier = (SupplierDto)result.ViewData.Model;
            Assert.AreEqual(supplier.Name, "Uklízečky.cz");
        }

        [TestMethod]
        public void Suppliers_Edit()
        {
            // Arrange
            db = new AlexaDbContextTest();
            supplierService = new SupplierService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            groupService = new GroupService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            controller = new SuppliersController(supplierService, groupService);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(SupplierDto));
            var supplier = (SupplierDto)result.ViewData.Model;
            Assert.AreEqual(supplier.Name, "Papírna.cz");
        }

        [TestMethod]
        public void Suppliers_EditSupplier()
        {
            // Arrange
            db = new AlexaDbContextTest();
            supplierService = new SupplierService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            groupService = new GroupService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            controller = new SuppliersController(supplierService, groupService);

            var supplier = supplierService.GetById(1);
            supplier.Name = "Papírna.cz - Edit";

            // Act
            ViewResult result_1 = controller.Edit(supplier) as ViewResult;
            ViewResult result_2 = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result_2);
            Assert.IsInstanceOfType(result_2.ViewData.Model, typeof(SupplierDto));
            var supplier_edit = (SupplierDto)result_2.ViewData.Model;
            Assert.AreEqual(supplier_edit.Name, "Papírna.cz - Edit");
        }

        [TestMethod]
        public void Suppliers_Create()
        {
            // Arrange
            db = new AlexaDbContextTest();
            supplierService = new SupplierService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            groupService = new GroupService(new SupplierRepositoryTest(db), new GroupRepositoryTest(db));
            controller = new SuppliersController(supplierService, groupService);

            var supplier = new SupplierDto
            {
                ID = 5,
                Name = "test",
                Address = "test",
                Email = "test",
                Phone = "123 456 789",
                Groups = new List<GroupDto>() { groupService.GetById(0) }
            };

            // Act
            ViewResult result_1 = controller.Create(supplier) as ViewResult;
            ViewResult result_2 = controller.Details(5) as ViewResult;

            // Assert
            Assert.IsNotNull(result_2);
            Assert.IsInstanceOfType(result_2.ViewData.Model, typeof(SupplierDto));
            var supplier_edit = (SupplierDto)result_2.ViewData.Model;
            Assert.AreEqual(supplier_edit.Name, "test");
        }
    }
}
