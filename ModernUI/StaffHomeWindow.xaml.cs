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
    /// Interaction logic for StaffHomeWindow.xaml
    /// </summary>
    public partial class StaffHomeWindow : Window
    {
        public StaffHomeWindow()
        {
            InitializeComponent();
            dataGrid();
        }

        void dataGrid()
        {
            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("select number As 'Ticket Number', issue_title As 'Ticket Category', problem As 'Ticket Problem', status As 'Ticket Status', date_created As 'Date Created' from tickets", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            dtGrid.DataContext = dt;
        }
    }
}
