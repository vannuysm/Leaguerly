﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Leaguerly.Api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [IgnoreDataMember]
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeTeamScore { get { return GetScore(HomeTeamId); } }

        [IgnoreDataMember]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int AwayTeamScore { get { return GetScore(AwayTeamId); } }

        [IgnoreDataMember]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public bool WasForfeited { get; set; }
        public int? ForfeitingTeamId { get; set; }
        public bool IncludeInStandings { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Booking> Bookings { get; set; }

        public int DivisionId { get; set; }

        public Game() {
            if (Goals == null) {
                Goals = new Collection<Goal>();
            }

            if (Bookings == null) {
                Bookings = new Collection<Booking>();
            }
        }

        private int GetScore(int teamId) {
            return Goals
                .Where(goal =>
                    goal.Player.Teams.Select(team => team.Id).Contains(teamId)
                )
                .Sum(goal => goal.Count);
        }
    }
}
