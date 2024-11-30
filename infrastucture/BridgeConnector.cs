using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TDMDUAPP.Domain.Model;

namespace TDMDUAPP.infrastucture
{
    public class BridgeConnector : IBridgeConnectorHueLights
    {
        private static readonly HttpClient _httpClient = new() { BaseAddress = new Uri("http://localhost/api/") };//als je met emulator wil connencten gebruik deze anders https://192.168.1.179/api
        private IPreferences _preferences;
        //private static string? UserName { get; set; }//todo make set private
        public BridgeConnector(IPreferences preferences) 
        {
            _preferences = preferences;
        }

        public async Task SendApiLinkAsync() 
        {
            Debug.WriteLine("Send LINK");
            var response = await _httpClient.PostAsJsonAsync("", new
            {
                devicetype = "my_hue_app#iphone peter"
            });
            
            response.EnsureSuccessStatusCode(); 
            string json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);

            ExtractUserName(json);
        }

        public void ExtractUserName(string json)
        {
            JsonDocument jsonDocument = JsonDocument.Parse(json);
            var rootArray = jsonDocument.RootElement;
            var rootObject = rootArray[0];
            var successElement = rootObject.GetProperty("success");
            var usernameProperty = successElement.GetProperty("username");
            var userName = usernameProperty.GetString();
            //UserName = usernameProperty.GetString();
            Debug.WriteLine(userName);
            _preferences.Set("username",userName);
        }

        

        public void TurnLightOn() 
        {
            
        
        }

        public async Task GetAllLightIDsAsync()
        {
            //var response = await _httpClient.GetStringAsync($"/{}/lights");

            //Debug.WriteLine(response);


        }
    }
}
