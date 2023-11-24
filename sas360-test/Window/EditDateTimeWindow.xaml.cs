using System;
using System.Windows;


namespace sas360_test
{

    public partial class EditDateTimeWindow : Window
    {
        public bool Save_changes { get; set; } = new bool();

        public string Name_var { get; set; } = string.Empty;
        public DateTime Value_var { get; set; } = new DateTime();



        #region Constructor
        public EditDateTimeWindow()
        {
            InitializeComponent();

            Datetimepicker_edit.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            Datetimepicker_edit.FormatString = Globals.GetTheInstance().DateFormat;
            Datetimepicker_edit.TimeFormat = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            Datetimepicker_edit.TimeFormatString = "HH:mm:ss";
        }

        #endregion


        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Label_internal_config.Content = Name_var;
            Datetimepicker_edit.Value = Value_var;

            Save_changes = false;
        }

        #endregion



        #region Save
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(Datetimepicker_edit.Value.ToString(), out DateTime value))
            {
                Value_var = value;
                Save_changes = true;
            }
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
