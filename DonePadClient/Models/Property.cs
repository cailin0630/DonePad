using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonePadClient.Models
{
    public class Property : Attribute
    {
        public string Value { get; set; }

        public Property(string Value)
        {
            this.Value = Value;
        }
    }
}
