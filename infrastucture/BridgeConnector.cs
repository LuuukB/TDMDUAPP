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
            try
            {
                var response = await _httpClient.PostAsJsonAsync("", new
                {
                    devicetype = "my_hue_app#iphone peter"
                });
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);

                ExtractUserName(json);
            }
            catch (Exception ex) {
                Debug.WriteLine("de server staat uit");
            
            }


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


        public async Task<List<string>> GetAllLightIDsAsync()
        {
            Debug.WriteLine("GETTING LIGHTSSS");
            var response = await _httpClient.GetAsync($"{_preferences.Get("username", string.Empty)}/lights");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var lights = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var lightIds = lights.Keys.ToList();
            foreach (var lightId in lightIds) { 
                Debug.WriteLine($"LightIDDDDSSS: {lightId}");
            }
            return lightIds;

        }

        public async Task TurnLightOnOffAsync(string lightID, bool isOn)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{_preferences.Get("username", string.Empty)}/lights/{lightID}/state",
                new { on = isOn }
                );
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
        }

        public async Task SetLighColorAsync(string lightId, int hueOrigen, int saturation, int brightness, bool isOn)
        {
            var response = await _httpClient.PutAsJsonAsync(
              $"{_preferences.Get("username", string.Empty)}/lights/{lightId}/state",
              new
              {
                  on = isOn,
                  sat = saturation,
                  bri = brightness,
                  hue = hueOrigen,

              }
              );
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
        }

        public async Task<string> GetLightInfoSpecificAsync(string lightId)
        {
            var response = await _httpClient.GetAsync(
              $"{_preferences.Get("username", string.Empty)}/lights/{lightId}");

            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
            return json;

        }
    }
}
