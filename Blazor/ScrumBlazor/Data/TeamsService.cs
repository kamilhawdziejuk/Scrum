using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScrumBlazor.Data
{
    public class TeamsService
    {
        public TeamsService()
        {
        }
        public Team CreateTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) return null;
            using var db = new DatabaseContext();
            var team = new Team() {Name = teamName, CreatedTime = DateTime.Now, Members  = new List<Member>()};
            db.Teams.Add(team);
            db.SaveChanges();
            return team;
        }

        public bool LogIn(string name, string password)
        {
            return (!CheckTeamAvailability(name));
        }


        public Team GetTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) return null;
            using var db = new DatabaseContext();
            return (db.Teams.Include(m => m.Members).Where(t => t.Name.Equals(teamName))).FirstOrDefault();
        }

        public Team RemoveMember(Team team, int id)
        {
            if (team == null) return team;

            using var db = new DatabaseContext();
            Member memberToRemove = db.Teams.Include(m => m.Members).First(t => t.Id.Equals(team.Id)).Members
                .First(m => m.TeamId == team.Id && m.Id == id);
            db.Teams.Include(m => m.Members).First(t => t.Id.Equals(team.Id)).Members.Remove(memberToRemove);
            db.SaveChanges();

            return GetTeam(team.Name);
        }

        public Team AddMember(Team team, string newMember)
        {
            if (team == null) return team;

            using var db = new DatabaseContext();
            Member m = new Member() {CreatedTime = DateTime.Now, Name = newMember, TeamId = team.Id};
            db.Teams.Include(m => m.Members).First(t => t.Id.Equals(team.Id)).Members.Add(m);
            db.SaveChanges();
            return GetTeam(team.Name);
        }

        public bool CheckTeamAvailability(string teamName)
        {
            using (var db = new DatabaseContext())
            {
                return (db.Teams.Count(t => t.Name.Equals(teamName)) == 0);
            }
        }

        public async Task<Participant[]> Load()
        {
            var list = new List<Participant>();
            try
            {
                using (var db = new DatabaseContext())
                {
                    foreach (var team in db.Teams)
                    {
                        Participant p = new Participant() {Name = team.Name};
                        list.Add(p);
                    }
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                return list.ToArray();
            }

        }
    }
}