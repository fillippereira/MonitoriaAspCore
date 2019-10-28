using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoriaAspCore.Models
{
    public class AppMessage
    {
       

        public string Message { get; set; }

        public  AppMessage(string Content,string Class)
        {
            Message = "<div class=\"alert alert-"+Class+" alert-dismissible text-center shadow-sm\"><button type = \"button\" class=\"close\" data-dismiss=\"alert\">&times;</button><strong></strong>"+Content+"</div>";
            
        }

        public AppMessage()
        {
        }
    }
}
