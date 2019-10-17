using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoriaAspCore.Models
{
    public class Message
    {
        

        public string GenerateMessage(string Content, string Class) {

            var Message = "<div class=\"col - md - 12 p - 0\"><div class=\"alert alert-"+Class+"warning alert-dismissible text-center shadow-sm\"><button type = \"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>"+Content+"</div></div>";

            return Message;
        }

    }
}
