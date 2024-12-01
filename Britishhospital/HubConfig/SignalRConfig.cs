using Microsoft.AspNetCore.SignalR;
using Models.Models;

namespace BritshHospital.HubConfig;

public class SignalRConfig:Hub
{

    public async Task JoinOrganizationGroup(string orgId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orgId);
    }
    public async Task LeaveOrganizationGroup(string orgId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, orgId);
    }
    public async Task invoke(SignalRTest test) {
   await Clients.Client(this.Context.ConnectionId).SendAsync("StatesUpdated", test);
    }
}
