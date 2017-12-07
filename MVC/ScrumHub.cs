using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumMvc
{
    public class ScrumHub : Hub
    {
        //public Task Send(string message)
        //{
        //    return Clients.All.InvokeAsync("Send", message);
        //}

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.InvokeAsync("broadcastMessage", name, message);
        }

        public void GetTime(string countryZone)
        {
            TimeZone currentZone = TimeZone.CurrentTimeZone;
            DateTime currentDate = DateTime.Now;
            DateTime currentUTC = currentZone.ToUniversalTime(currentDate);
            TimeZoneInfo selectedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(countryZone);
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(currentUTC, selectedTimeZone);
            //Clients.All.setTime(currentDateTime.ToString("h:mm:ss tt"));
        }
    }
}
