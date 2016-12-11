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
using Weather.ServiceReferenceWeather;

namespace Weather
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Service1TownStruct[] weather;
        public ObservableCollection<string> townCollect { get; set; }
        public ObservableCollection<string> dateCollect { get; set; }
        public ObservableCollection<string> timeCollect { get; set; }


        public MainWindow()
        {
            townCollect = new ObservableCollection<string>();
            dateCollect = new ObservableCollection<string>();
            timeCollect = new ObservableCollection<string>();
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var townComboBox = combobox1.SelectedItem.ToString();
            var dateComboBox = combobox2.SelectedItem.ToString();
            var timeBox = combobox3.SelectedItem.ToString();

            var weath = weather.First(t => t.towns == townComboBox).dateStruct.First(d => d.date == dateComboBox).timeStruct.First(t => t.Time == timeBox);

            Label1.Content = weath.weather[0];
            Label2.Content = weath.weather[1];
            Label3.Content = weath.weather[2];

            Label5.Content = weath.weather[3];
            Label6.Content = weath.weather[4];
            Label4.Content = weath.weather[5];      
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GetDate()
        {
            Service1Client serv = new Service1Client();
            weather = serv.GetWeather();

            townCollect.Clear();
            dateCollect.Clear();
            timeCollect.Clear();

            foreach (var item in weather.Select(t => t.towns))
                townCollect.Add(item);

            foreach (var item in weather.Select(t => t.towns))
                townCollect.Add(item);

            foreach (var item in weather.First().dateStruct.Select(t => t.date))
                dateCollect.Add(item);

            foreach (var item in weather.First().dateStruct.First().timeStruct.Select(t => t.Time))
                timeCollect.Add(item);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetDate();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GetDate();
        }

    }
}
