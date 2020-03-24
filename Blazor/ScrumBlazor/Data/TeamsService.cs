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
        public void Initialize()
        {
            using (var db = new DatabaseContext())
            {
                var team = new Team() {Name = "Team1", CreatedTime = DateTime.Now};

                List<Member> members = new List<Member>();
                members.Add(new Member() { CreatedTime = DateTime.Now, Name = "Kamil", TeamId = team.Id});
                members.Add(new Member() { CreatedTime = DateTime.Now, Name = "Sylwia", TeamId = team.Id});

                db.Teams.Add(new Team() { Name = "Team1", CreatedTime = DateTime.Now, Members = members});
                db.SaveChanges();
            }
        }

        public bool LogIn(string name, string password)
        {
            return (!CheckTeamAvailability(name));
        }


        public Team GetTeam(string teamName)
        {
            using (var db = new DatabaseContext())
            {
                return (db.Teams.Include(m => m.Members).Where(t => t.Name.Equals(teamName))).First();
            }
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