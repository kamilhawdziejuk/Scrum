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

            var team = new Team()
            { 
                Name = teamName, CreatedTime = DateTime.Now, Password = hash, Members  = new List<Member>(),
                Id = Guid.NewGuid()
            };
            db.Teams.Add(team);
            db.SaveChanges();
            return team;
        }

        public Team GetTeam(string teamName, string password)
        {
            if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(password)) return null;

            Team team = this.GetTeam(teamName);
            if (team?.Password == null) return null;

            string hash = this.Encode(password);
            return team.Password.Equals(hash) ? team : null;
        }

        private string Encode(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        public async Task Save(Participant[] participants, Team team)
        {
            if (team == null) return;

            await using var db = new DatabaseContext();
            var members = db.Members.ToList();
            foreach (var p in participants)
            {
                var member = members.FirstOrDefault(m => m.Id.Equals(p.Id));
                if (member != null)
                {
                    member.SummaryTime += p.Timer;
                    if (p.Timer > 0)
                    {
                        member.DailyAmount += 1;
                    }
                }
            }

            db.Teams.First(t => t.Id.Equals(team.Id)).DailyAmount += 1;
            db.SaveChanges();
        }

        public Team GetTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName)) return null;
            using var db = new DatabaseContext();
            return (db.Teams.Include(m => m.Members).Where(t => t.Name.Equals(teamName))).FirstOrDefault();
        }

        public Team RemoveMember(Team team, Guid id)
        {
            if (team == null) return team;

            using var db = new DatabaseContext();
            Member memberToRemove = db.Teams.Include(m => m.Members).First(t => t.Id.Equals(team.Id)).Members
                .First(m => m.TeamId == team.Id && m.Id == id);
            db.Teams.Include(m => m.Members).First(t => t.Id.Equals(team.Id)).Members.Remove(memberToRemove);
            db.SaveChanges();

            return GetTeam(team.Name);
        }

        public Team AddMember(Guid teamId, string newMember)
        {
            if (teamId == Guid.Empty) return null;

            using var db = new DatabaseContext();
            Member m = new Member() {CreatedTime = DateTime.Now, Name = newMember, TeamId = teamId, Id = Guid.NewGuid()};
            db.Members.Add(m);
            db.SaveChanges();
            var team = db.Teams.Include(m => m.Members).First(t => t.Id.Equals(teamId));

            return GetTeam(team.Name);
        }

        public bool CheckMemberAvailability(Guid teamId, string newMember)
        {
            if (teamId == Guid.Empty || string.IsNullOrWhiteSpace(newMember)) return false;
            using var db = new DatabaseContext();
            if (!db.Members.Any(m => m.TeamId.Equals(teamId) && m.Name.Equals(newMember))) return true;
            return false;
        }

        public bool CheckTeamAvailability(string teamName)
        {
            using var db = new DatabaseContext();
            return (db.Teams.Count(t => t.Name.Equals(teamName)) == 0);
        }

        public async Task<Participant[]> Load()
        {
            var list = new List<Participant>();
            try
            {
                await using (var db = new DatabaseContext())
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