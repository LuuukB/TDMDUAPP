using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMDUAPP.Domain.Model
{
    public class Lamp
    {
        public int? LampId { get; set; }

        public bool IsOn { get; set; }
        public int Brightness { get; set; }
        public int Saturation { get; set; }
        public int Hue { get; set; }

        public Lamp(int lampid, bool ison, int brightness, int sat, int hue)
        {
            LampId = lampid;
            IsOn = ison;
            Brightness = brightness;
            Saturation = sat;
            Hue = hue;

        }
    }
}
