using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doorbell
{
    public class DoorbellData
    {
        public DateTime TimeStamp { get; set; }

        public string Doorbell { get; set;}


        public DoorbellData()
        {
            this.TimeStamp = DateTime.Now;
        }

        public DoorbellData(string doorbell):this()
        {
            this.Doorbell = doorbell;

        }
    }
}
