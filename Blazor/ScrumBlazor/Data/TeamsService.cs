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
        public Team CreateTeam(string teamName, string password)
        {
            if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(password)) return null;
            using var db = new DatabaseContext();

            string hash = this.Encode(password);

            var team = new Team() {Name = teamName, CreatedTime = DateTime.Now, Password = hash, Members  = new List<Member>()};
            db.Teams.Add(team);
            db.SaveChanges();
            return team;
        }

        public Team GetTeam(string teamName, string password)
        {
            if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(password)) return null;

            Team team = this.GetTeam(teamName);
            if (team == null) return null;

            string hash = this.Encode(password);
            return team.Password.Equals(hash) ? team : null;
        }


        //public bool LogIn(string name, string password)
        //{
        //    return (!CheckTeamAvailability(name));
        //}

        private string Encode(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
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