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
    }

  
}
