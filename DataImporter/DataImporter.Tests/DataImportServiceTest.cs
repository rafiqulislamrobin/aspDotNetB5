using Autofac.Extras.Moq;
using DataImporter.Areas.User.Models;
using DataImporter.Info.Business_Object;
using DataImporter.Info.Services;
using DataImporter.Info.UnitOfWorks;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DataImporter.Tests
{
    [ExcludeFromCodeCoverage]
    public class DataImportServiceTest
    {
        private AutoMock _mock;
        private Mock<IDataUnitOfWork> _dataUnitOfWork;
        
        private DataImporterService _service;
          

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanUp()
        {
            _mock?.Dispose();
        }


        [SetUp]
        public void Setup()
        {
            _dataUnitOfWork = _mock.Mock<IDataUnitOfWork>();
            _service = _mock.Create<DataImporterService>();
        }

        [Test]
        public void CreateGroup_GroupExist_throwNewInvalidParameterException()
        {
            var group = new Group
            {
                Id = 5,
                Name = "c++",
                ApplicationUserId = Guid.NewGuid()
            };
            Guid id = Guid.NewGuid();

            //_dataUnitOfWork.Setup(x => x.Group(group,id)).Verifiable();

            //arrange

            //act
            //_service.CreateGroup(group , id);

            //assert
            _dataUnitOfWork.Verify();
        }
    }
}