using System.Linq;
using System.Threading.Tasks;

namespace ScrumBlazor.Data
{
    public class EstimateService
    {
        public async Task Save(Participant participant, Team team)
        {
            if (team == null) return;

            await using var db = new DatabaseContext();
            var members = db.Members;

            var member = members.FirstOrDefault(m => m.Id.Equals(participant.Id));
            if (member != null)
            {
                member.StoryPoint = participant.Joined ? 1 : 0;
                member.Estimate = participant.Estimate;
            }

            db.SaveChanges();

        }
    }
}