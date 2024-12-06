using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
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
            lamps = new ObservableCollection<Lamp>();
            //bridgeConnectorHueLights = new BridgeConnector(preferences);
        }
        [ObservableProperty]
        private Lamp _selectedLamp;
        [ObservableProperty]
        private string _lampId;
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
        [ObservableProperty]
        public ObservableCollection<Lamp> lamps;

        [RelayCommand]
        public void SetSelectedLamp(Lamp lamp) {
            SelectedLamp = lamp;
        }

        [RelayCommand]
        public async Task SendApiLink() {
            await BridgeConnector.SendApiLinkAsync();
        }
        [RelayCommand]
        public async Task GetLights() {
            //await BridgeConnector.GetAllLightIDsAsync();
            Lamps.Add(new Lamp()
            {
                LampId = 1,
                Saturation = 20,
                IsOn = true,
                Brightness = 50,
                Hue = 3000
            });
        }
        [RelayCommand]
        public async Task TurnLightOnOffAsync() {
            await BridgeConnector.TurnLightOnOffAsync(LampId, IsLightOn);
        }
        [RelayCommand]
        public async Task SetLightColor()
        {
            int hue = Hue >= 0 && Hue <= 65535 ? Hue : 0;
            int saturation = Saturation >= 0 && Saturation <= 255 ? Saturation : 0;
            int brightness = Brightness >= 0 && Brightness <= 255 ? Brightness : 0;

            if (SelectedLamp == null)
                return;

            await BridgeConnector.SetLighColorAsync(SelectedLamp.LampId.ToString(), SelectedLamp.Hue, SelectedLamp.Saturation, SelectedLamp.Brightness, SelectedLamp.IsOn);
        }

        [RelayCommand]
        public async Task GetSpecificLightInfo()
        {
            var lightInfo = await BridgeConnector.GetLightInfoSpecificAsync(LampId);
            InfoLamp = lightInfo ?? "No info available";
        }

        public async Task AddLamp(Lamp lamp)
        {
            if (lamps.Contains(lamp)) { return; };
            lamps.Add(lamp);
        }
    }
}
