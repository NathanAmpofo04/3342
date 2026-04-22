using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TheLinkUp.Hubs
{
    public class EventHub : Hub
    {
        public async Task SendRSVPUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveRSVPUpdate", message);
        }
    }
}