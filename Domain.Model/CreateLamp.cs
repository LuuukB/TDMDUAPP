using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TDMDUAPP.Domain.Model
{
    public class CreateLampFabric
    {
        private ILampControl _lampControl;
        public CreateLampFabric(ILampControl lampControl) { _lampControl = lampControl; }
        public void CreateLamps(string json) {
            JsonDocument jsonDoc = JsonDocument.Parse(json);
            var rootArray = jsonDoc.RootElement;

            //foreach (JsonElement element in rootArray.EnumerateArray()) 
            //{
            //    var baseProperty = element.GetProperty("state");
            //    //var lamp = new Lamp
            //    //{
            //    //    LampId = element.GetInt32(),
            //    //    Saturation = baseProperty.GetProperty("sat").GetInt32(),
            //    //    IsOn = baseProperty.GetProperty("on").GetBoolean(),
            //    //    Brightness = baseProperty.GetProperty("bri").GetInt32(),
            //    //    Hue = baseProperty.GetProperty("hue").GetInt32()
            //    //};
            //    //Debug.WriteLine("Lampieess");
            //    //await _lampControl.AddLamp(lamp);
            //}


        }
    }
}
