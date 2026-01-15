using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisconnectedModel_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> listAirPlaneTypes = new List<string>();
        DataSet1 dataSet1 = new DataSet1();

        //--------------------Flight Info------------------------------------------
        DataSet1TableAdapters.FlightInfoTableAdapter flightInfoTableAdapter = 
            new DataSet1TableAdapters.FlightInfoTableAdapter();

        DataSet1.FlightInfoDataTable flightInfoDataTable = new DataSet1.FlightInfoDataTable();

        //--------------------User Info---------------------------------------------
        DataSet1TableAdapters.UserInfoTableAdapter userInfoTableAdapter = 
            new DataSet1TableAdapters.UserInfoTableAdapter();

        DataSet1.UserInfoDataTable userInfoDataTable = new DataSet1.UserInfoDataTable();


        //---------------------Empezamos---------------------------------------



        public MainWindow()
        {
            InitializeComponent();
            listAirPlaneTypes.Clear();
            listAirPlaneTypes.Add("Boeing 737");
            listAirPlaneTypes.Add("Boeing 747");
            listAirPlaneTypes.Add("Boeing 757");
            listAirPlaneTypes.Add("Boeing 767");
            listAirPlaneTypes.Add("Boeing 777");
            listAirPlaneTypes.Add("AirBus 123");
            listAirPlaneTypes.Add("AirBus 124");
            listAirPlaneTypes.Add("AirBus 125");

            comboBoxAirPlaneType.ItemsSource = listAirPlaneTypes;

            RefreshDataGridFlight();
            RefreshDataGridUser();

        }

        #region FlightInfo
        private void RefreshDataGridFlight()
        {
            //flightInfoTableAdapter.Fill(dataSet1.FlightInfo);

            flightInfoTableAdapter.Fill(flightInfoDataTable);
            dataGridFlights.ItemsSource = flightInfoDataTable;

            comboBoxFlights.ItemsSource = flightInfoDataTable;
            comboBoxFlights.DisplayMemberPath = "FlightNumber";
            comboBoxFlights.SelectedValuePath = "Id";

            comboBoxNextFlight.ItemsSource = flightInfoDataTable;
            comboBoxNextFlight.DisplayMemberPath = "FlightNumber";
            comboBoxNextFlight.SelectedValuePath = "Id";
        }

        private void btnAddFlight_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.FlightInfoRow flightInfoRow = dataSet1.FlightInfo.NewFlightInfoRow();
            flightInfoRow.Airline = txtAirline.Text;
            flightInfoRow.FlightNumber = txtFlightNumber.Text;
            flightInfoRow.Destination = txtDestination.Text;
            flightInfoRow.AirPlaneType = comboBoxAirPlaneType.SelectedItem.ToString();

            dataSet1.FlightInfo.Rows.Add(flightInfoRow);
            flightInfoTableAdapter.Update(dataSet1.FlightInfo);

            RefreshDataGridFlight();
        }

        private void btnUpdateFlight_Click(object sender, RoutedEventArgs e)
        {
            int selectedRow = (int)comboBoxFlights.SelectedValue;
            DataRow[] dataRows = flightInfoDataTable.Select($"Id = {selectedRow}");

            dataRows[0]["Airline"] = txtAirline.Text;
            dataRows[0]["FlightNumber"] = txtFlightNumber.Text;
            dataRows[0]["Destination"] = txtDestination.Text;
            dataRows[0]["AirPlaneType"] = comboBoxAirPlaneType.SelectedItem.ToString();

            flightInfoTableAdapter.Update(flightInfoDataTable);

            RefreshDataGridFlight();
        }

        private void btnDeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            int selectedRow = (int)comboBoxFlights.SelectedValue;
            DataRow[] dataRows = flightInfoDataTable.Select($"Id = {selectedRow}");

            if (dataRows.Length > 0)
            {
                dataRows[0].Delete();
                flightInfoTableAdapter.Update(flightInfoDataTable);
                RefreshDataGridFlight();

                txtAirline.Text = "";
                txtDestination.Text = "";
                txtFlightNumber.Text = "";
            }
        }

        private void comboBoxFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedRow = (int)comboBoxFlights.SelectedValue;

            DataRow[] dataRows = flightInfoDataTable.Select($"Id = {selectedRow}");

            if(dataRows.Length > 0)
            {
                DataRow dataRow = dataRows[0];
                txtAirline.Text = dataRow["Airline"].ToString();
                txtFlightNumber.Text = dataRow["FlightNumber"].ToString();
                txtDestination.Text = dataRow["Destination"].ToString();
                comboBoxAirPlaneType.SelectedItem = dataRow["AirPlaneType"].ToString();
            }
        }

        #endregion

        #region UserInfo

        private void RefreshDataGridUser()
        {
            userInfoTableAdapter.Fill(dataSet1.UserInfo);

            userInfoTableAdapter.Fill(userInfoDataTable);

            dataGridUsers.ItemsSource = userInfoDataTable;

            comboBoxUsers.ItemsSource = userInfoDataTable;
            comboBoxUsers.DisplayMemberPath = "Name";
            comboBoxUsers.SelectedValuePath = "Id";
        }
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedRow = (int)comboBoxUsers.SelectedValue;

            DataRow[] dataRows = userInfoDataTable.Select($"Id = {selectedRow}");

            if (dataRows.Length > 0)
            {
                DataRow dataRow = dataRows[0];
                txtName.Text = dataRow["Name"].ToString();
                txtEmail.Text = dataRow["Email"].ToString();
                txtAddress.Text = dataRow["Address"].ToString();
                txtAge.Text = dataRow["Age"].ToString();
                comboBoxNextFlight.SelectedItem = dataRow["FlightInfoId"].ToString();
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.UserInfoRow userInfoRow = dataSet1.UserInfo.NewUserInfoRow();
            userInfoRow.Name = txtName.Text;
            userInfoRow.Email = txtEmail.Text;
            userInfoRow.Address = txtAddress.Text;
            userInfoRow.Age = Convert.ToInt32(txtAge.Text);
            userInfoRow.FlightInfoId = (int)comboBoxNextFlight.SelectedValue;

            dataSet1.UserInfo.Rows.Add(userInfoRow);
            userInfoTableAdapter.Update(dataSet1.UserInfo);

            RefreshDataGridFlight();
        }

        #endregion


    }
}