using EventMakerV2.Model;
using EventMakerV2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EventMakerV2.Persistancy
{
    public class PersistanceService
    {
        //private static string jsonFileNameEvents = "EventsAsJson.dat";
        //public static async void FileCreationEvents()
        //{
        //    var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(jsonFileNameEvents);
        //    if (item == null)
        //    {
        //        StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(jsonFileNameEvents);
        //    }
        //}

        public static async void SaveEventAsJsonAsync(Event eventObj)
        {
            //string usersJsonString = JsonConvert.SerializeObject(events);
            //SerializeEventsFileAsync(usersJsonString, jsonFileNameEvents);


            const string serverUrl = "http://localhost:60743";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                try
                {
                    var response = client.PostAsJsonAsync("api/Events", eventObj).Result;
                    // var events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result; // IEnumerable
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public static async Task<ObservableCollection<Event>> LoadEventsFromJsonAsync()
        {
            //string eventsJsonString = await DeserializeEventsFileAsync(jsonFileNameEvents);
           // return (ObservableCollection<Event>)JsonConvert.DeserializeObject(eventsJsonString, typeof(ObservableCollection<Event>));

            const string serverUrl = "http://localhost:60743";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ObservableCollection<Event> eventList = new ObservableCollection<Event>();

                try
                {
                    var response = await client.GetAsync("api/Events");
                    var events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result; // IEnumerable

                    if (response.IsSuccessStatusCode)
                    {
                        foreach (var singleEvent in events)
                        {
                            eventList.Add(singleEvent);
                        }
                        return eventList;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }
        //public static async void SerializeEventsFileAsync(string eventString, string fileName)
        //{
        //    StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        //    await FileIO.WriteTextAsync(localFile, eventString);
        //}
        //public static async Task<string> DeserializeEventsFileAsync(string fileName)
        //{
        //    StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        //    return await FileIO.ReadTextAsync(localFile);
        //}
    }
}
