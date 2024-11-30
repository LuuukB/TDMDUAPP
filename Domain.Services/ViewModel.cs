using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP.Domain.Services
{
    public partial class ViewModel : ObservableObject
    {
        private static IPreferences _Preferences;//todo maak hier een constructor en daar zet je de prefrence in want dan set je het. stupid 
        private BridgeConnector BridgeConnector = new BridgeConnector(_Preferences);
        [RelayCommand]
        public async Task SendApiLink() {
            await BridgeConnector.SendApiLinkAsync();
        }
        [RelayCommand]
        public async Task GetLights() {
            await BridgeConnector.GetAllLightIDsAsync();
        }
        

    }
}
