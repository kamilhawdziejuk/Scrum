using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ScrumBlazor.Data
{
    public class EstimateService
    {
        public event Action Notify;
        public string User { get; set; }
        public string Message { get; set; }

        HubConnection hubConnection;

        public EstimateService(NavigationManager navigationManager)
        {
            hubConnection = new HubConnectionBuilder()
           .WithUrl(navigationManager.ToAbsoluteUri("/chatHub"))
           .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user,
                                                              message) =>
            {
                User = user;
                Message = message;

                if (Notify != null)
                {
                    Notify?.Invoke();
                }
            });

            hubConnection.StartAsync();
            hubConnection.SendAsync("SendMessage", null, null);
        }

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

        public void Send(string userInput, string messageInput) =>
            hubConnection.SendAsync("SendMessage", userInput, messageInput);

        public bool IsConnected => hubConnection.State ==
                                               HubConnectionState.Connected;
    }
}