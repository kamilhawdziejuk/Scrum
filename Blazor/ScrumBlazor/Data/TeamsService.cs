using System;
using System.Collections.Generic;
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