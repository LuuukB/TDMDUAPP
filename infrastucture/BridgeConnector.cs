using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TDMDUAPP.Domain.Model;

namespace TDMDUAPP.infrastucture
{
    public class BridgeConnector : IBridgeConnectorHueLights
    {
        private static readonly HttpClient _httpClient = new() { BaseAddress = new Uri("http://localhost/api/") };//als je met emulator wil connencten gebruik deze anders https://192.168.1.179/api

        private static string? UserName { get; set; }//todo make set private
        public BridgeConnector() 
        {

        }

        public async Task SendApiLinkAsync() 
        {
            Debug.WriteLine("Send LINK");
            var response = await _httpClient.PostAsJsonAsync("", new
            {
                devicetype = "my_hue_app#iphone peter"
            });
            
            response.EnsureSuccessStatusCode(); 
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            string json = await response.Content.ReadAsStringAsync();

            ExtractUserName(json);

           
        }

        public void ExtractUserName(string json)
        {
            //using JsonDocument jsonDocument = JsonDocument.Parse(json);
            //JsonElement root = jsonDocument.RootElement;
            //UserName = root.GetProperty("succes").GetProperty("username").GetString();

            //using JsonDocument jsonDocument = JsonDocument.Parse(json);
            //JsonElement root = jsonDocument.RootElement;

            
            //if (root.TryGetProperty("succes", out JsonElement succesElement))
            //{
            //    string userName = succesElement.GetProperty("username").GetString();
            //    Console.WriteLine(userName); 
            //}

        }

        public void TurnLightOn() 
        {
            
        
        }

        public async Task GetAllLightIDsAsync()
        {
            var response = await _httpClient.GetStringAsync($"/{UserName}/lights");

            Debug.WriteLine(response);


        }
    }
}
