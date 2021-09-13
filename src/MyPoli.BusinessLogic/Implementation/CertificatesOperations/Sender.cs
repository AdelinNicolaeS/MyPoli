using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoli.BusinessLogic.Implementation.CertificatesOperations
{
    public class Sender
    {
        public string Specialization { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Reason { get; set; }
        public string Date { get; set; }


        public Sender(string _specialization, string _name, string _year, string _reason, string _date)
        {
            Specialization = _specialization;
            Name = _name;
            Year = _year;
            Reason = _reason;
            Date = _date;
        }

        public Sender()
        {
        }
    }
}
