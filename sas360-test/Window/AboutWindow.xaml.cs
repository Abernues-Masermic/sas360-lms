using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace sas360_test
{
    public partial class AboutWindow : Window
    {


        #region Constructor
        public AboutWindow()
        {
            InitializeComponent();


        }

        #endregion


        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Label_ver.Content = $"Ver. {Constants.version}";
        }

        #endregion

        #region Navigate to link

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });

            e.Handled = true;
        }

        #endregion


        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion



    }

}
