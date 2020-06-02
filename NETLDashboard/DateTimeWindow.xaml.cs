using System.Windows;
using System.Windows.Controls;
using System;

namespace NETLDashboard
{

    
    /// <summary>
    /// Interaction logic for DateTimeWindow.xaml
    /// </summary>
    public partial class DateTimeWindow : Window
    {
        public DateTime startDate;
        public DateTime endDate;
        public DateTimeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startDate = DpStart.SelectedDate.Value.Date; //Gets the selected start date
            endDate = DpEnd.SelectedDate.Value.Date; //Gets the selected end date
            Windough.Close();
        }
    }
}
