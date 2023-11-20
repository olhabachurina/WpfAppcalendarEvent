using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppcalendarEvent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Event> Events { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Events = new ObservableCollection<Event>();
            eventsListView.ItemsSource = Events;

        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            DateTime selectedDate = eventDatePicker.SelectedDate ?? DateTime.MinValue;
            calendar.SelectedDate = selectedDate;
            calendar.DisplayDate = selectedDate;
            calendar.SelectedDates.Clear();
            calendar.SelectedDates.Add(selectedDate);
            var eventsForSelectedDate = Events.Where(eventItem => eventItem.Date.Date == selectedDate.Date).ToList();
            eventsListView.ItemsSource = eventsForSelectedDate;
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            string eventName = eventNameTextBox.Text;
            string eventDescription = eventDescriptionTextBox.Text;
            DateTime selectedDate = eventDatePicker.SelectedDate ?? DateTime.MinValue;
            Events.Add(new Event { Date = selectedDate, Name = eventName, Description = eventDescription });
            UpdateEventsListView();
        }

        private void UpdateEventsListView()
        {
            eventsListView.Items.Refresh();
        }
        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            DateTime selectedDate = calendar.SelectedDate ?? DateTime.MinValue;
            eventDatePicker.Text = selectedDate.ToShortDateString();
            calendar.SelectedDate = selectedDate;

            var eventsForSelectedDate = Events.Where(eventItem => eventItem.Date.Date == selectedDate.Date).ToList();


            eventsListView.ItemsSource = eventsForSelectedDate;
        }
        public class Event
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}

