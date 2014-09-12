﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ProEvoCanary.Controllers;
using ProEvoCanary.Helpers;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary
{
    [TestFixture]
    public class DefaultControllerTests
    {

        Mock<ICachePlayerRepository> _playerRepository;
        Mock<IRssFeedRepository> _rssFeedRepository;
        Mock<IEventRepository> _eventsRepository;
        Mock<IResultRepository> _resultsRepository;
        private DefaultController _defaultController;
        private ViewResult _result;
        private void Setup()
        {

            _playerRepository = new Mock<ICachePlayerRepository>();
            _playerRepository.Setup(x => x.GetTopPlayers()).Returns(new List<PlayerModel>{new PlayerModel
            {
                PlayerId = 1,
                PlayerName = "Hemang",
                GoalsPerGame = 2,
                MatchesPlayed = 3,
                PointsPerGame = 3.2f
                
            }});


            _rssFeedRepository = new Mock<IRssFeedRepository>();
            _rssFeedRepository.Setup(x => x.GetFeed(It.IsAny<string>())).Returns(new List<RssFeedModel>{new RssFeedModel
                {
                    LinkTitle = "hemang",
                    LinkDescription = "ha"
                }});

            _eventsRepository = new Mock<IEventRepository>();
            _eventsRepository.Setup(x => x.GetEvents()).Returns(new List<EventModel>{new EventModel
            {
                EventId = 1,
                EventName = "Hemang",
                Venue = "Home",
                Date = "10/10/2014",
                Name = "Hemang",
                Completed = true

            }});

            _resultsRepository = new Mock<IResultRepository>();
            _resultsRepository.Setup(x => x.GetResults()).Returns(new List<ResultsModel>{new ResultsModel
            {
                ResultId = 1,
                HomeTeamId = 1,
                HomeTeam = "Arsenal",
                HomeScore = 5,
                AwayTeamId =2,
                AwayTeam = "Aston Villa",
                AwayScore = 2,

            }});

            _defaultController = new DefaultController(_playerRepository.Object, _rssFeedRepository.Object, _eventsRepository.Object, _resultsRepository.Object);
            _result = _defaultController.Index() as ViewResult;
        }

        [Test]
        public void ShouldSetViewToDefault()
        {
            //given
            Setup();

            //then
            Assert.That(_result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ShouldSetPlayerModel()
        {
            //given
            Setup();

            //when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Players.Count, Is.EqualTo(1));
            Assert.That(model.Players[0].PlayerName, Is.EqualTo("Hemang"));
            Assert.That(model.Players[0].PlayerId, Is.EqualTo(1));
            Assert.That(model.Players[0].GoalsPerGame, Is.EqualTo(2));
            Assert.That(model.Players[0].MatchesPlayed, Is.EqualTo(3));
            Assert.That(model.Players[0].PointsPerGame, Is.EqualTo(3.2f));
        }

        [Test]
        public void ShouldSetRssFeedModel()
        {
            //given
            Setup();

            //when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.News.Count(), Is.EqualTo(1));
            Assert.That(model.News.First().LinkTitle, Is.EqualTo("hemang"));
            Assert.That(model.News.First().LinkDescription, Is.EqualTo("ha"));
        }

        [Test]
        public void ShouldSetEventsModel()
        {
            //given
            Setup();

            //when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Events.Count(), Is.EqualTo(1));
            Assert.That(model.Events.First().EventId, Is.EqualTo(1));
            Assert.That(model.Events.First().EventName, Is.EqualTo("Hemang"));
            Assert.That(model.Events.First().Venue, Is.EqualTo("Home"));
            Assert.That(model.Events.First().Date, Is.EqualTo("10/10/2014"));
            Assert.That(model.Events.First().Name, Is.EqualTo("Hemang"));
            Assert.That(model.Events.First().Completed, Is.EqualTo(true));
        }
        
        [Test]
        public void ShouldSetResultsModel()
        {
            //given
            Setup();

            //when
            var model = _result.Model as HomeModel;

            //then
            Assert.That(model.Results.Count(), Is.EqualTo(1));
            Assert.That(model.Results.First().ResultId, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeamId, Is.EqualTo(1));
            Assert.That(model.Results.First().HomeTeam, Is.EqualTo("Arsenal"));
            Assert.That(model.Results.First().HomeScore, Is.EqualTo(5));
            Assert.That(model.Results.First().AwayTeamId, Is.EqualTo(2));
            Assert.That(model.Results.First().AwayTeam, Is.EqualTo("Aston Villa"));
            Assert.That(model.Results.First().AwayScore, Is.EqualTo(2));
        }


       

    }
}
