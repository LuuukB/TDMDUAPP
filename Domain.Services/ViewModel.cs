using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDMDUAPP.infrastucture;


namespace TDMDUAPP.Domain.Services
{
    public partial class ViewModel : ObservableObject
    {
        private BridgeConnector BridgeConnector = new BridgeConnector();
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
