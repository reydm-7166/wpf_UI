﻿using System;
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
            userData.Name = MainWindow.mainConnectionClass.staffFirstName + " " + MainWindow.mainConnectionClass.staffLastName;
            dataGrid();
        }

        public static class userData
        {
            public static string ID  { get; set; }
            public static string Name { get; set; }
            public static string Dog { get; set; }
        }

        /// for buttons. ticket problem and steps to resolve
        /// 
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

        private void text_TicketSolution_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtbox_TicketSolution.Focus();
        }

        private void txtbox_TicketSolution_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtbox_TicketSolution.Text) && txtbox_TicketSolution.Text.Length > 0)
                text_TicketSolution.Visibility = Visibility.Collapsed;
            else
                text_TicketSolution.Visibility = Visibility.Visible;
        }

        /// for buttons. ticket problem and steps to resolve
        void dataGrid()
        {

            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT number As 'Ticket Number', issue_title As 'Ticket Category', problem As 'Ticket Problem', status As 'Ticket Status', date_created As 'Date Created' FROM tickets WHERE assigned_to=@staffName";

            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@staffName", userData.Name);

            connection.Open();


            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            dtGrid.DataContext = dt;
        }

        private void button_ViewTickets_Click(object sender, RoutedEventArgs e)
        {
            grid_ViewTickets.Visibility = Visibility.Visible;
            titletext_View.Visibility = Visibility.Visible;
            button_WorkTicket.FontWeight = FontWeights.SemiBold;

            titletext_Create.Visibility = Visibility.Collapsed;
            grid_CreateTicket.Visibility = Visibility.Collapsed;
        }

        private void button_WorkTicket_Click(object sender, RoutedEventArgs e)
        {

        }

        void workGrid()
        {
            grid_ViewTickets.Visibility = Visibility.Collapsed;
            grid_CreateTicket.Visibility = Visibility.Visible;

            titletext_Create.Visibility = Visibility.Visible;
            titletext_View.Visibility = Visibility.Collapsed;
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid row = (DataGrid)sender;
            DataRowView row_select = row.SelectedItem as DataRowView;

            if (row_select != null)
            {
                workGrid();

                string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand cmd = new MySqlCommand("select * from tickets WHERE number = @ticketID", connection);

                cmd.Parameters.AddWithValue("@ticketID", row_select["Ticket Number"].ToString());
                connection.Open();

                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            combobox_TicketCategory.Text = row_select["Ticket Category"].ToString();
                            text_TicketProblem.Text = row_select["Ticket Problem"].ToString();
                            text_TicketDetails.Text = reader.GetString("details").ToString();
                            txtbox_ticketID.Text = reader.GetInt32("number").ToString();
                            txtbox_AssignedTo.Text = reader.GetString("assigned_to");

                            combobox_TicketStatus.Text = "RESOLVED";

                        }
                        connection.Close();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
        }

        private void button_SignOut_Click(object sender, RoutedEventArgs e)
        {
            userData.ID = "";
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
