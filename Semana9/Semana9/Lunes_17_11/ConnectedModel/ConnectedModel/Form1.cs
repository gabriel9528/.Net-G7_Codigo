using System.Data;
using System.Data.SqlClient;

namespace ConnectedModel
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = new SqlConnection();
        private string connectionString = @"Data Source = DESKTOP-2C5V6P1;
                                            Initial Catalog = G7_ConnectedModel;
                                            Integrated Security = true";

        private SqlCommand command;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshDataFlight();
            RefreshDataFlightAirPlaneType();
        }

        private void RefreshDataFlight()
        {
            connection.ConnectionString = connectionString;
            command = connection.CreateCommand();

            try
            {
                connection.Open();
                command.CommandText = "Select * from FlightInfo";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    dataGridViewFlight.DataSource = dataTable;

                    comboBoxSelect.DataSource = dataTable;
                    comboBoxSelect.DisplayMember = "FlightNumber";
                    comboBoxSelect.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
        }

        private void RefreshDataFlightAirPlaneType()
        {
            connection.ConnectionString = connectionString;
            command = connection.CreateCommand();

            try
            {
                connection.Open();
                command.CommandText = "Select * from FlightInfo";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTableFlight = new DataTable();
                    dataTableFlight.Load(reader);

                    comboBoxAirPlaneType.DataSource = dataTableFlight;
                    comboBoxAirPlaneType.DisplayMember = "AirPlaneType";
                    comboBoxAirPlaneType.ValueMember = "Id";
                }
                ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataFlight();
        }

        private void LoadFlightDetails(int flightId)
        {
            connection.ConnectionString = connectionString;
            command = connection.CreateCommand();

            try
            {
                connection.Open();

                string query = "Select * from FlightInfo where Id = @FlightId";
                command.CommandText = query;
                command.Parameters.AddWithValue("@FlightId", flightId);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    textBoxAirline.Text = reader["AirLine"].ToString();
                    textBoxFlightNumber.Text = reader["FlightNumber"].ToString();
                    textBoxDestination.Text = reader["Destination"].ToString();

                    string? airPlaneType = reader["AirPlaneType"].ToString();

                    comboBoxAirPlaneType.SelectedValue = airPlaneType;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
        }

        private bool isAirPlaneTypeSelectionChanged = false;
        private void comboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isAirPlaneTypeSelectionChanged)
                {
                    DataRowView? selectedRow = (DataRowView)comboBoxSelect.SelectedItem;

                    if (selectedRow != null)
                    {
                        int selectedFlightById = Convert.ToInt32(selectedRow["Id"]);
                        if (selectedFlightById > 0)
                        {
                            LoadFlightDetails(selectedFlightById);
                        }

                        string? airPlaneType = selectedRow["AirPlaneType"].ToString();

                        //Buscar el indice en el comboBoxAirPlaneType que coincida con el valor de airPlaneType 
                        for (int i = 0; i < comboBoxAirPlaneType.Items.Count; i++)
                        {
                            DataRowView? dataRow = (DataRowView)comboBoxAirPlaneType.Items[i];
                            if (dataRow["AirPlaneType"].ToString() == airPlaneType)
                            {
                                comboBoxAirPlaneType.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
        }

        private void buttonAddFlight_Click(object sender, EventArgs e)
        {
            string airLine = textBoxAirline.Text;
            string flightNumber = textBoxFlightNumber.Text;
            string destination = textBoxDestination.Text;
            string airPlaneType = "";
            DataRowView selectedRow = (DataRowView)comboBoxAirPlaneType.SelectedItem;
            if (selectedRow != null)
                airPlaneType = selectedRow["AirPlaneType"].ToString();

            if (string.IsNullOrEmpty(airLine)
                || string.IsNullOrEmpty(flightNumber)
                || string.IsNullOrEmpty(destination)
                || string.IsNullOrEmpty(airPlaneType))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            else
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    command = connection.CreateCommand();

                    command = new SqlCommand("Insert into FlightInfo " +
                        "values (@AirLine, @FlightNumber, @Destination, @AirPlaneType)", connection);

                    command.Parameters.AddWithValue("@AirLine", airLine);
                    command.Parameters.AddWithValue("@FlightNumber", flightNumber);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@AirPlaneType", airPlaneType);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Flight add succesfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Dispose();
                    connection.Close();
                    RefreshDataFlight();
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (comboBoxSelect.SelectedValue == null)
            {
                MessageBox.Show("Please select a flight to update");
                return;
            }

            int id = (int)comboBoxSelect.SelectedValue;
            string airLine = textBoxAirline.Text;
            string flightNumber = textBoxFlightNumber.Text;
            string destination = textBoxDestination.Text;
            string airPlaneType = "";


            DataRowView selectedRow = (DataRowView)comboBoxAirPlaneType.SelectedItem;
            if (selectedRow != null)
                airPlaneType = selectedRow["AirPlaneType"].ToString();

            if (string.IsNullOrEmpty(airLine)
                || string.IsNullOrEmpty(flightNumber)
                || string.IsNullOrEmpty(destination)
                || string.IsNullOrEmpty(airPlaneType))
            {
                MessageBox.Show("Please fill all the fields");
                return;
            }
            else
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    command = connection.CreateCommand();

                    command = new SqlCommand("Update FlightInfo set AirLine = @AirLine, FlightNumber = @FlightNumber, " +
                        "Destination = @Destination, AirPlaneType = @AirPlaneType where Id = @Id", connection);

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@AirLine", airLine);
                    command.Parameters.AddWithValue("@FlightNumber", flightNumber);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@AirPlaneType", airPlaneType);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Flight update succesfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Dispose();
                    connection.Close();
                    RefreshDataFlight();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxSelect.SelectedValue == null)
            {
                MessageBox.Show("Please select a flight to update");
                return;
            }

            int id = (int)comboBoxSelect.SelectedValue;

            try
            {
                connection.ConnectionString = connectionString;
                command = connection.CreateCommand();

                command = new SqlCommand("Delete from FlightInfo where Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);


                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Flight delete succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Dispose();
                connection.Close();
                RefreshDataFlight();
            }

        }
    }
}
