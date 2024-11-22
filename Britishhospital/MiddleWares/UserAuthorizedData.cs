using System.Security.Claims;

namespace Booking.MiddleWares;

public static class UserAuthorizedData
{
    public static long getEmpId()
    {

        var claimes = Thread.CurrentPrincipal as ClaimsPrincipal;
        if (claimes != null)
        {
            long EmpId = long.Parse(claimes.FindFirst("EmpId")?.Value ?? "");
            return EmpId;
        }
        return 0;

    }


    public static int getReqId()
    {

        var claimes = Thread.CurrentPrincipal as ClaimsPrincipal;
        if (claimes != null)
        {
            int reqId = int.Parse(claimes.FindFirst("ReqId")?.Value ?? "");
            return reqId;
        }
        return 0;

    }
}
