﻿using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.DataAccess.Repositories
{
	public class PlayerRepository : IPlayerRepository
	{
		private readonly IDbHelper _helper;

		public PlayerRepository(IDbHelper helper)
		{
			_helper = helper;
		}

		public List<PlayerModel> GetTopPlayersRange(int pageNumber = 1, int playersPerPage = 10)
		{
			if (pageNumber < 1 || playersPerPage < 1)
			{
				throw new Exception();
			}

			var players = new List<PlayerModel>();
			var reader = _helper.ExecuteReader("up_GetTopPlayers", new { RowsPerPage = playersPerPage, PageNumber = pageNumber });
			while (reader.Read())
			{
				players.Add(new PlayerModel
				{
					PlayerId = (int)reader["UserId"],
					PlayerName = reader["Name"].ToString(),
					GoalsPerGame = float.Parse(reader["GoalsPerGame"] == DBNull.Value ? "0" : reader["GoalsPerGame"].ToString()),
					PointsPerGame = float.Parse(reader["PointsPerGame"] == DBNull.Value ? "0" : reader["PointsPerGame"].ToString()),
					MatchesPlayed = (int)reader["MatchesPlayed"]
				});
			}


			return players;
		}

		public List<PlayerModel> GetTopPlayers()
		{
			var players = new List<PlayerModel>();
			using var reader = _helper.ExecuteReader("up_GetTopPlayers");
			while (reader.Read())
			{
				players.Add(new PlayerModel
				{
					PlayerId = (int)reader["UserId"],
					PlayerName = reader["Name"].ToString(),
					GoalsPerGame = float.Parse(reader["GoalsPerGame"] == DBNull.Value ? "0" : reader["GoalsPerGame"].ToString()),
					PointsPerGame = float.Parse(reader["PointsPerGame"] == DBNull.Value ? "0" : reader["PointsPerGame"].ToString()),
					MatchesPlayed = (int)reader["MatchesPlayed"]
				});
			}

			return players;
		}

		public List<PlayerModel> GetAllPlayers()
		{
			var players = new List<PlayerModel>();
			var reader = _helper.ExecuteReader("up_GetUsers");

			while (reader.Read())
			{
				players.Add(new PlayerModel
				{
					PlayerName = reader["Name"].ToString(),
					PlayerId = (int)reader["UserId"]
				});
			}

			return players;
		}
	}
}