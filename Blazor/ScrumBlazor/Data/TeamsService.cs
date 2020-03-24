using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumBlazor.Data
{
    public class TeamsService
    {
        public TeamsService()
        {
        }
        public void Create()
        {
            using (var db = new DatabaseContext())
            {
                db.Teams.Add(new Team() { Name = "Team0", CreatedTime = DateTime.Now, Members = new List<Member>() });
                db.SaveChanges();
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