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
            //text_TicketAssignedTo.Text = String.Empty;
            //txtbox_TicketAssignedTo.Text = "PREDEFINED"
            Random rnd = new Random();
            txtbox_ticketID.Text = rnd.Next(100, 100000).ToString();
        }



        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            titleText.Text = "Settings";
        }

        

        /////// TICKET TITLE  //////////////

        private void text_TicketTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtbox_TicketTitle.Focus();
        }

        private void txtbox_TicketTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtbox_TicketTitle.Text) && txtbox_TicketTitle.Text.Length > 0)
                text_TicketTitle.Visibility = Visibility.Collapsed;
            else
                text_TicketTitle.Visibility = Visibility.Visible;
        }

        /////// TICKET PROBLEM  //////////////
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

        private void button_Insert_Click(object sender, RoutedEventArgs e)
        {
           

            try
            {
                MainWindow.mainConnectionClass.query = "INSERT INTO mydb.tickets(user_id, number, issue_title, problem, details, reported_by_id, reported_by_name, assigned_to, status) VALUES('" + int.Parse(txtbox_ReportedByStaffID.Text) + "','" + txtbox_ticketID.Text + "','" + text_TicketTitle.Text + "','" + text_TicketProblem.Text + "','" + text_TicketDetails.Text + "','" + txtbox_ReportedByStaffID.Text + "','" + txtbox_ReportedByStaffName.Text + "','" + int.Parse(combobox_AssignedTo.Text) + "','" + combobox_TicketStatus.Text + "')";

                MainWindow.mainConnectionClass.conn.Open();
                MySqlCommand command = new MySqlCommand(MainWindow.mainConnectionClass.query, MainWindow.mainConnectionClass.conn);

            
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Ticket Submitted Successfully");
                }
                else
                {
                    MessageBox.Show("Something is wrong! Please check the input carefully");
                }
                MainWindow.mainConnectionClass.conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Fill up the forms correctly!");
            }

            MainWindow.mainConnectionClass.conn.Close();

            
        }

        private void button_ViewTickets_Click(object sender, RoutedEventArgs e)
        {
            grid_CreateTicket.Visibility = Visibility.Hidden;
        }

        private void button_CreateTicket_Click(object sender, RoutedEventArgs e)
        {
            if(grid_CreateTicket.Visibility == Visibility.Hidden)
            {
                grid_CreateTicket.Visibility = Visibility.Visible;
            }
        }




        /////// TICKET REPORTED BY THE USER ID  //////////////

    }

    

}
