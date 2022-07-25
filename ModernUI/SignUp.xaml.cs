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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "SERVER=localhost;DATABASE=mydb;UID=root;PASSWORD=admin;";

            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);

                string query = "INSERT INTO `mydb`.`users` (`firstname`, `lastname`, `username`, `password`, `role`) VALUES('" + txtbox_Firstname.Text + "','" + txtbox_Lastname.Text + "','" + txtbox_Username.Text + "','" + txtbox_Password.Password + "','" + int.Parse(comboBox_Role.Text) + "');";

                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Edited Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

    }
}