using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TDMDUAPP.Domain.Model;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP.Domain.Services
{
    public partial class ViewModel : ObservableObject
    {
        private IBridgeConnectorHueLights BridgeConnector;
       
        public ViewModel(IPreferences preferences, IBridgeConnectorHueLights bridgeConnectorHueLights) 
        { 
            bridgeConnectorHueLights = new BridgeConnector(preferences);
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
        [RelayCommand]
        public async Task SendApiLink() {
            await BridgeConnector.SendApiLinkAsync();
        }
        [RelayCommand]
        public async Task GetLights() {
            await BridgeConnector.GetAllLightIDsAsync();
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
    }
}
