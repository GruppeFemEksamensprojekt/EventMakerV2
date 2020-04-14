using EventMakerV2.Converter;
using EventMakerV2.Model;
using EventMakerV2.Persistancy;
using EventMakerV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EventMakerV2.Handler
{
    /// <summary>
    /// This class handles database Insert and delete queries
    /// </summary>
    public class EventHandler
    {
        public EventViewModel Reference { get; set; }

        public EventHandler(EventViewModel p)
        {
            Reference = p;
        }
        /// <summary>
        /// Saves/inserts an even obj into the database
        /// </summary>
        public static void SaveEventAsync(Event obj)
        {
            PersistanceService.SaveEventAsJsonAsync(obj);
        }

        public void CommandInvokeHandler()
        {

        }
        
        public void CreateEvent()
        {
            Event obj = new Event() { Name = Reference.Name, Description = Reference.Description, Place = Reference.Place, Date = DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(Reference.Date, Reference.Time) };
            EventCatalogSingleTon.Instance.AddEventToCollection(obj);
            SaveEventAsync(obj);
        }
        public void DeleteEvent()
        {
            if (Reference.SelectedEvent != null)
            {
                EventCatalogSingleTon.Instance.Remove(Reference.SelectedEvent);
                //SaveEventAsync();
            }
        }
        public void ReturnMethod()
        {
            ((Frame)Window.Current.Content).GoBack();
        }
        public void SetSelectedEvent(Event ev)
        {

        }

    }

}
