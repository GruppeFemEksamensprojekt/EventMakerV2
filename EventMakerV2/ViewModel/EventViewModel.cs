using EventMakerV2.Common;
using EventMakerV2.Model;
using EventMakerV2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EventMakerV2.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        private Event _selectedEvent;
        private DateTimeOffset _date;
        private TimeSpan _time;

        public EventViewModel()
        {
            EventHandler = new Handler.EventHandler(this);

            EventList = EventCatalogSingleTon.Instance.Events;
            DateTime dt = System.DateTime.Now;
            _date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0, new TimeSpan());
            _time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);


            DeleteEventCommand = new RelayCommand(EventHandler.DeleteEvent, null);
            CreateEventCommand = new RelayCommand(EventHandler.CreateEvent, null);
            ReturnCommand = new RelayCommand(EventHandler.ReturnMethod, null);
            UpdateEventCommand = new RelayCommand(UpdateEventMethod, null);
            GoToCreateEventPageCommand = new RelayCommand(GoToCreateEventPageMethod, null);
            OnPropertyChanged(nameof(EventList));
        }

        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set { _selectedEvent = value; OnPropertyChanged(); }
        }

        public ICommand GoToCreateEventPageCommand { get; set; }

        public Handler.EventHandler EventHandler { get; set; }
        public ICommand CreateEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand ReturnCommand { get; set; }
        public ICommand UpdateEventCommand { get; set; }
        public ObservableCollection<Event> EventList { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTimeOffset Date { get; set; }
        public TimeSpan Time { get; set; }

        public bool EventIsSelected()
        {
            return SelectedEvent != null;
        }
        public void UpdateEventMethod()
        {
            EventCatalogSingleTon.Instance.UpdateEvent(SelectedEvent.Id, SelectedEvent);
        }

        public void GoToCreateEventPageMethod()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(CreateEventPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
