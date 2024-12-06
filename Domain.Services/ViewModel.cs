using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TDMDUAPP.Domain.Model;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP.Domain.Services
{
    public partial class ViewModel : ObservableObject, ILampControl
    {
        private IBridgeConnectorHueLights BridgeConnector;
       
        public ViewModel(IPreferences preferences, IBridgeConnectorHueLights bridgeConnectorHueLights) 
        {
            BridgeConnector = bridgeConnectorHueLights;
            //bridgeConnectorHueLights = new BridgeConnector(preferences);
        }
        [ObservableProperty]
        private string _lightId;
        [ObservableProperty]
        private bool _isLightOn;
        [ObservableProperty]
        private int _hue;
        [ObservableProperty]
        private int _saturation;
        [ObservableProperty]
        private int _brightness;
        [ObservableProperty]
        private string _infoLamp;
        public ObservableCollection<Lamp> Lamps { get; } = new();
        [RelayCommand]
        public async Task SendApiLink() {
            await BridgeConnector.SendApiLinkAsync();
        }
        [RelayCommand]
        public async Task GetLights() {
            //await BridgeConnector.GetAllLightIDsAsync();
            Lamps.Add(new(1, true, 20, 20, 20));
            Lamps.Add(new(2, true, 100, 50, 10));
           
        }
        [RelayCommand]
        public async Task TurnLightOnOffAsync() {
            await BridgeConnector.TurnLightOnOffAsync(LightId, IsLightOn);
        }
        [RelayCommand]
        public async Task SetLightColor()
        {
            int hue = Hue >= 0 && Hue <= 65535 ? Hue : 0;
            int saturation = Saturation >= 0 && Saturation <= 255 ? Saturation : 0;
            int brightness = Brightness >= 0 && Brightness <= 255 ? Brightness : 0;

            await BridgeConnector.SetLighColorAsync(LightId, hue, saturation, brightness, IsLightOn);
        }

        [RelayCommand]
        public async Task GetSpecificLightInfo()
        {
            var lightInfo = await BridgeConnector.GetLightInfoSpecificAsync(LightId);
            InfoLamp = lightInfo ?? "No info available";
        }

        public async Task AddLamp(Lamp lamp)
        {
            if (Lamps.Contains(lamp)) { return; };
            Lamps.Add(lamp);
            Debug.WriteLine("adding lamp" + lamp);
        }
    }
}
