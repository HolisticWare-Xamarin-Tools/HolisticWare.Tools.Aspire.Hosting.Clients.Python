using System;
using System.Text;
using System.Threading.Tasks;
using WatsonWebserver.Core;

namespace HolisticWare.Tools.Aspire.Hosting.Clients.Python;

public partial class 
                                        ProcessRemoteControl
{
    
    static
                                        ProcessRemoteControl
                                        (
                                        )
    {
        
    }
    
    static async
        Task
                                        DefaultRoute
                                        (
                                            HttpContextBase ctx
                                        )
    {
        StringBuilder sb = new();
        sb.AppendLine($"Request on DefaultRoute");
        sb.AppendLine($"        DateTime =  {DateTime.Now.ToString("HH:mm:ss.ss")}");
        
        await ctx.Response.Send(sb.ToString());

        return;
    }
    
}