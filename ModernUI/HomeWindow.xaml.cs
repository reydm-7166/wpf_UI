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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
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
            MainWindow.mainConnectionClass.query = "INSERT INTO mydb.tickets(user_id, number, issue_title, problem, details, reported_by_id, reported_by_name, assigned_to, status) VALUES('" + int.Parse(txtbox_ReportedByStaffID.Text) + "','" + txtbox_ticketID.Text + "','" + text_TicketTitle.Text + "','" + text_TicketProblem.Text + "','" + text_TicketDetails.Text + "','" + txtbox_ReportedByStaffID.Text + "','" + txtbox_ReportedByStaffName.Text + "','" + int.Parse(combobox_AssignedTo.Text) + "','" + combobox_TicketStatus.Text + "')";

            MainWindow.mainConnectionClass.conn.Open();
            MySqlCommand command = new MySqlCommand(MainWindow.mainConnectionClass.query, MainWindow.mainConnectionClass.conn);

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted");
                }
                else
                {
                    MessageBox.Show("Data Not Inserted");
                }
                MainWindow.mainConnectionClass.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MainWindow.mainConnectionClass.conn.Close();
        }



        /////// TICKET REPORTED BY THE USER ID  //////////////

    }

    

}
