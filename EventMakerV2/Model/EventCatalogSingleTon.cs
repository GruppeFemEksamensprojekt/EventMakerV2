using EventMakerV2.Persistancy;
using EventMakerV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EventMakerV2.Model
{
    public class EventCatalogSingleTon
    {
        private static EventCatalogSingleTon _instance = null;

        public ObservableCollection<Event> Events { get; }
        public static EventCatalogSingleTon Instance { get{ return _instance ?? (_instance = new EventCatalogSingleTon()); } }

        private EventCatalogSingleTon()
        {
            Events = new ObservableCollection<Event>();
            LoadEventsAsync();
        }
        public void AddEventToCollection(Event eventObj)
        {
            Events.Add(eventObj);
        }
        public void Remove(Event eventObj)
        {

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
                    var response = client.DeleteAsync($"api/Events/{eventObj.Id}").Result;
                    Events.Remove(eventObj);
                    // var events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result; // IEnumerable
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void UpdateEvent(int selectedEventId, Event eventObj)
        {
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
                    var response = client.PutAsJsonAsync($"api/Events/{selectedEventId}", eventObj).Result;
                    Events.Clear();
                    Instance.LoadEventsAsync();
                    // var events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result; // IEnumerable
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async void LoadEventsAsync()
        {
            //PersistanceService.FileCreationEvents();
            ObservableCollection<Event> events = await PersistanceService.LoadEventsFromJsonAsync();
            //if (events == null)
            //{
            //    EventViewModel.AddDummyData();
            //}
            //else
            //{
            //
            //}
            foreach (var item in events)
            {
                this.Events.Add(item);
            }
        }
    }
}
