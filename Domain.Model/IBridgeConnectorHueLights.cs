using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMDUAPP.Domain.Model
{
    public interface IBridgeConnectorHueLights
    {
        Task SendApiLinkAsync();

        Task<List<string>> GetAllLightIDsAsync();

        Task TurnLightOnOffAsync(string lightID, bool isOn);
        Task SetLighColorAsync(string lightId, int hue, int saturation, int brightness);

        Task GetLightInfoAsync(string lightId);



    }
}
