using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doorbell.Web.Models
{
    public class Rings
    {


        public string doorbell { get; set; }
//        public int EditedFrom { get; set; }
        public string IoTHub { get; set; }

        public DateTime timestamp { get; set; }
    }
}
