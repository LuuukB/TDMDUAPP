using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP.Domain.Services
{
    public partial class ViewModel : ObservableObject
    {
        private BridgeConnector BridgeConnector;
       
        public ViewModel(IPreferences preferences) 
        { 
            BridgeConnector = new BridgeConnector(preferences);
        }
        [ObservableProperty]
        private string _lightId;
        [ObservableProperty]
        private bool _isLightOn;
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
    }
}
