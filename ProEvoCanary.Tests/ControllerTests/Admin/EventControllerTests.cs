﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Areas.Admin.Controllers;
using ProEvoCanary.Domain.Repositories.Interfaces;
using ProEvoCanary.ModelBuilders;
using ProEvoCanary.Models;
using EventModel = ProEvoCanary.Areas.Admin.Models.EventModel;

namespace ProEvoCanary.Tests.ControllerTests.Admin
{
    [TestFixture]
    public class EventControllerTests
    {
        readonly EventModel _eventModel =new EventModel(It.IsAny<EventTypes>(),It.IsAny<string>(),It.IsAny<DateTime>(),It.IsAny<List<PlayerModel>>());
        Mock<IAdminEventRepository> repo;
        Mock<IPlayerRepository> userrepo;
        Mock<IPlayerModelBuilder> modelBuilder;
        private EventController authenticationController;

        [SetUp]
        public void Setup()
        {
            repo = new Mock<IAdminEventRepository>();
            userrepo = new Mock<IPlayerRepository>();
            modelBuilder = new Mock<IPlayerModelBuilder>();
            authenticationController = new EventController(repo.Object, userrepo.Object, modelBuilder.Object);
        }

        [Test]
        public void ShouldSetDefaultViewName()
        {
            //given + when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(viewResult.ViewName, Is.EqualTo("Create"));
        }

        [Test]
        public void ShouldSetTournamentDateToToday()
        {
            //given + when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(((EventModel)viewResult.Model).Date, Is.EqualTo(DateTime.Today));
        }


        [Test]
        public void ShouldSetDefaultModel()
        {
            //given + when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
        }

        [Test]
        public void ShouldSetDefaultModelProperties()
        {
            //given
            var players = new List<PlayerModel>();
            //userrepo.Setup(x => x.GetAllPlayers()).Returns(players);

            //when
            var viewResult = authenticationController.Create() as ViewResult;

            //then
            var model = viewResult.Model as EventModel;

            Assert.That(viewResult.Model, Is.TypeOf<EventModel>());
            Assert.That(model.Players, Is.EqualTo(players));
        }

        [Test]
        public void ShouldNotCallUserRepositoryWhenModelIsInValid()
        {
            //given
            //when
            authenticationController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            authenticationController.Create(_eventModel);

            //then
            repo.Verify(x => x.CreateEvent(_eventModel.TournamentName, _eventModel.Date, (int)_eventModel.EventType, It.IsAny<int>()), Times.Never);
        }
        [Test]
        public void ListModelShouldBeRepopulatedIfModelPostIsInvalid()
        {
            //given
            userrepo.Setup(x => x.GetAllPlayers()).Returns(new List<Domain.Models.PlayerModel>());

            //when
            authenticationController.ModelState.AddModelError("TournamentName", "Missing Tournament Name");
            var viewResult = authenticationController.Create(_eventModel) as ViewResult;

            //then
            Assert.IsNotNull(((EventModel)viewResult.Model).Players);
        }
    }
}
