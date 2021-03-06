using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;



namespace ModernUI
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();

            Random rnd = new Random();
            txtbox_ticketID.Text = rnd.Next(100, 100000).ToString();
            combo();
            dataGridAccounts();
            dataGrid();
        }

        void dataGridAccounts()
        {
            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("select id As 'User ID', firstname As 'First Name', lastname As 'Last Name', username As 'Username', role As 'Position' from users", connection);
            connection.Open();
            DataTable dta = new DataTable();
            dta.Load(cmd.ExecuteReader());
            connection.Close();

            dtGridAccounts.DataContext = dta;

        }

        void dataGrid()
        {
            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("select number As 'Ticket Number', issue_title As 'Ticket Category', problem As 'Ticket Problem', details as 'Ticket Details', status As 'Ticket Status', date_created As 'Date Created' from tickets", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            dtGrid.DataContext = dt;
        }


        /// <summary>
        /// function to get the data from the tickets table and show it to the reports. ## reports ticket tab ###
        /// </summary>
        /// <param name="query"></param>
        /// <param name="control"></param>

        void TicketData(string query, int control)
        {

            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    reader.GetString(0);
                    switch (control)
                    {
                        case 0:
                            txtblock_TicketCount.Text = reader.GetString(0);
                            break;
                        case 1:
                            txtblock_OpenTickets.Text = reader.GetString(0);
                            break;
                        case 2:
                            txtblock_PendingTickets.Text = reader.GetString(0);
                            break;
                        case 3:
                            txtblock_ResolvedTickets.Text = reader.GetString(0);
                            break;
                    }

                }
                conn.Close();
            }
            reader.Close();



        }

        /// <summary>
        /// shows the first name and last name of the available staff users in the combobox. ### create ticket tab ###
        /// </summary>
        void combo()
        {
            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";
            MySqlConnection conn = new MySqlConnection(connectionString);


            string query = "SELECT * FROM users WHERE role = 2;";

            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        combobox_AssignedTo.Items.Add(reader.GetString("firstname") + " " + reader.GetString("lastname"));

                    }
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("mali bonak");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /////// TICKET TITLE  //////////////


        /////// TICKET PROBLEM  //////////////

        private void text_TicketProblem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtbox_TicketProblem.Focus();
        }

        private void txtbox_TicketProblem_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtbox_TicketProblem.Text) && txtbox_TicketProblem.Text.Length > 0)
                text_TicketProblem.Visibility = Visibility.Collapsed;
            else
                text_TicketProblem.Visibility = Visibility.Visible;
        }


        /////// TICKET DETAILS  //////////////
        ///
        private void text_TicketDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtbox_TicketDetails.Focus();
        }

        private void txtbox_TicketDetails_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtbox_TicketDetails.Text) && txtbox_TicketDetails.Text.Length > 0)
                text_TicketDetails.Visibility = Visibility.Collapsed;
            else
                text_TicketDetails.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// TICKET INSERT ON DATABASE
        /// </summary>

        private void button_Insert_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// CONDITIONALS. Txtbox must not be empty or on default. Needs sufficient and appropriate input
            /// </summary>
            if (combobox_TicketCategory.Text != "Ticket Category" && txtbox_TicketProblem.Text != ""
                && txtbox_TicketDetails.Text != "" && combobox_AssignedTo.Text != "Assign To ..."
                && combobox_TicketStatus.Text != "Select Status ...")
            {
                try
                {
                    string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

                    MySqlConnection conn = new MySqlConnection(connectionString);

                    string query = "INSERT INTO mydb.tickets(user_id, number, issue_title, problem, details, reported_by_id, reported_by_name, assigned_to, status) VALUES('" + int.Parse(txtbox_ReportedByStaffID.Text) + "','" + txtbox_ticketID.Text + "','" + combobox_TicketCategory.Text + "','" + txtbox_TicketProblem.Text + "','" + txtbox_TicketDetails.Text + "','" + txtbox_ReportedByStaffID.Text + "','" + txtbox_ReportedByStaffName.Text + "','" + combobox_AssignedTo.Text + "','" + combobox_TicketStatus.Text + "')";

                    MySqlCommand command = new MySqlCommand(query, conn);
                    conn.Open();


                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Ticket Submitted Successfully");
                        txtbox_TicketProblem.Clear();
                        txtbox_TicketDetails.Clear();
                        combobox_TicketCategory.SelectedIndex = 0;
                        combobox_AssignedTo.SelectedIndex = 0;
                        combobox_TicketStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Something is wrong! Please check the input carefully");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Fill in approriate input");
            }



        }

        private void button_ViewTickets_Click(object sender, RoutedEventArgs e)
        {
            if (grid_ViewTickets.Visibility == Visibility.Collapsed || grid_ViewTickets.Visibility == Visibility.Hidden)
            {

                grid_CreateTicket.Visibility = Visibility.Collapsed;
                titletext_Create.Visibility = Visibility.Collapsed;
                titletext_Reports.Visibility = Visibility.Collapsed;
                grid_Report.Visibility = Visibility.Collapsed;

                titletext_View.Visibility = Visibility.Visible;
                grid_ViewTickets.Visibility = Visibility.Visible;
                grid_ManageAccounts.Visibility = Visibility.Collapsed;
            }
            ///calls datagrid function
            dataGrid();
        }


        private void button_CreateTicket_Click(object sender, RoutedEventArgs e)
        {
            if (grid_CreateTicket.Visibility == Visibility.Collapsed || grid_CreateTicket.Visibility == Visibility.Hidden)
            {
                grid_CreateTicket.Visibility = Visibility.Visible;
                titletext_View.Visibility = Visibility.Collapsed;
                titletext_Create.Visibility = Visibility.Visible;
                titletext_Reports.Visibility = Visibility.Collapsed;
                grid_Report.Visibility = Visibility.Collapsed;
                grid_ViewTickets.Visibility = Visibility.Collapsed;
                grid_ManageAccounts.Visibility = Visibility.Collapsed;
            }
        }

        private void button_ManageAccounts_Click(object sender, RoutedEventArgs e)
        {
            grid_ManageAccounts.Visibility = Visibility.Visible;
            grid_CreateTicket.Visibility = Visibility.Collapsed;
            titletext_View.Visibility = Visibility.Collapsed;
            titletext_Create.Visibility = Visibility.Collapsed;
            titletext_Reports.Visibility = Visibility.Collapsed;
            grid_Report.Visibility = Visibility.Collapsed;
            grid_ViewTickets.Visibility = Visibility.Collapsed;

            ///calls datagrid function
            dataGridAccounts();
        }

        private void button_SignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void button_Reports_Click(object sender, RoutedEventArgs e)
        {
            if (grid_Report.Visibility == Visibility.Collapsed || grid_Report.Visibility == Visibility.Hidden)
            {
                grid_CreateTicket.Visibility = Visibility.Collapsed;
                titletext_View.Visibility = Visibility.Collapsed;
                titletext_Create.Visibility = Visibility.Collapsed;
                titletext_Reports.Visibility = Visibility.Visible;

                grid_Report.Visibility = Visibility.Visible;
                grid_ViewTickets.Visibility = Visibility.Collapsed;
                grid_ManageAccounts.Visibility = Visibility.Collapsed;

                /// Calls function reportsData when clicked /// ## reports ticket tab ###
                reportsData();
            }
        }

        /// <summary>
        /// this function gets the data from database. ### reports ticket tab ###
        /// </summary>
        void reportsData()
        {
            //All TICKETS PARAMETER ////
            string allQuery = "SELECT COUNT(id) AS TicketCount FROM tickets;";
            int alltxtbox = 0;
            TicketData(allQuery, alltxtbox);

            //OPEN TICKETS PARAMETER ////
            int opentxbox = 1;
            string openTickets = "SELECT COUNT(id) AS TicketCount FROM tickets WHERE status='OPEN';";
            TicketData(openTickets, opentxbox);

            //PENDING TICKETS PARAMETER ////
            int closetxbox = 2;
            string pendingTickets = "SELECT COUNT(id) AS TicketCount FROM tickets WHERE status='PENDING';";
            TicketData(pendingTickets, closetxbox);

            //RESOLVED TICKETS PARAMETER ////
            int servicetxbox = 3;
            string resolvedTickets = "SELECT COUNT(id) AS TicketCount FROM tickets WHERE status='RESOLVED';";
            TicketData(resolvedTickets, servicetxbox);
        }

        void grid()
        {
            titletext_Create.Visibility = Visibility.Visible;
            grid_CreateTicket.Visibility = Visibility.Visible;

            titletext_View.Visibility = Visibility.Collapsed;
            titletext_Reports.Visibility = Visibility.Collapsed;
            grid_Report.Visibility = Visibility.Collapsed;
            grid_ViewTickets.Visibility = Visibility.Collapsed;
            grid_ManageAccounts.Visibility = Visibility.Collapsed;
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid row = (DataGrid)sender;
            DataRowView row_select = row.SelectedItem as DataRowView;

            if (row_select != null)
            {
                grid();

                string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand cmd = new MySqlCommand("select * from tickets WHERE number = @ticketID", connection);

                cmd.Parameters.AddWithValue("@ticketID", row_select["Ticket Number"].ToString());
                connection.Open();

                combobox_TicketCategory.Text = row_select["Ticket Category"].ToString();
                text_TicketProblem.Text = row_select["Ticket Problem"].ToString();
                text_TicketDetails.Text = row_select["Ticket Details"].ToString();
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        /// <summary>
        /// function to get the data from the tickets table and show it to the reports. ## reports ticket tab ###
        /// </summary>
        /// <param name="query"></param>
        /// <param name="control"></param>

    }
}
