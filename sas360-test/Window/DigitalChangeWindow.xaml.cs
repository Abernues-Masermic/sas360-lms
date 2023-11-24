using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace sas360_test
{
    public partial class DigitalChangeWindow : Window
    {
        public string Digital_name { get; set; } = string.Empty;
        public bool Is_activated { get; set; } = new bool();

        public bool Save_changes { get; set; } = new bool();

        #region Constructor
        public DigitalChangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Save_changes = false;
            Label_digital_name.Content = Digital_name;
            Border_digital_state.Background = Is_activated ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
        }

        #endregion

        #region Digital mouse down

        private void Digital_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Is_activated = !Is_activated;

            Border_digital_state.Background = Is_activated ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
        }

        #endregion


        #region Save
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            Save_changes= true;
            Close();
        }

        #endregion

        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Save_changes = false;
            Close();
        }

        #endregion



    }
}
