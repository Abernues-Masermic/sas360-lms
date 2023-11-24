
using System.Windows;
using System.Windows.Input;

namespace sas360_test
{
    public partial class SettingModbusVarWindow : Window
    {
        public Modbus_var Modbus_var { get; set; } = new Modbus_var();


        #region Constructor
        public SettingModbusVarWindow()
        {
            InitializeComponent();

            Decimal_addr.Maximum = 10000;
            Decimal_addr.Minimum = 0;

            Decimal_format.Maximum = 100000;
            Decimal_format.Minimum = -100000;
            Decimal_format.Increment = (decimal)0.00001;

            Combobox_type.Items.Add("Byte");
            Combobox_type.Items.Add("UInt16");
            Combobox_type.Items.Add("UInt32");
            Combobox_type.Items.Add("Int8");
            Combobox_type.Items.Add("Int16");
            Combobox_type.Items.Add("Int32");
            Combobox_type.Items.Add("Int64");
            Combobox_type.Items.Add("Single");
        }

        #endregion

        #region Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Decimal_addr.Value = (decimal)Modbus_var.Addr;
            Textbox_name.Text = Modbus_var.Name;
            Textbox_unit.Text = Modbus_var.Unit;
            Decimal_format.Value = (decimal) Modbus_var.Format;
            Combobox_type.Text = Modbus_var.TypeName;
        }

        #endregion

        #region Mover pantalla

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        #endregion

        #region Exit

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #endregion


        #region Save
        private void Button_save_Click(object sender, RoutedEventArgs e)
        {

            Modbus_var.Addr = (double)Decimal_addr.Value!;
            Modbus_var.Name = Textbox_name.Text;
            Modbus_var.Unit = Textbox_unit.Text;
            Modbus_var.Format = (double)Decimal_format.Value!;
            Modbus_var.TypeName = Combobox_type.Text;

            DialogResult = true;
            Close();
        }

        #endregion
    }
}
