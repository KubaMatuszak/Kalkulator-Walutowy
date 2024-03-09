
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kalkulator_Walutowy.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        string item1 = null;
        string item2 = null;
        
        public MainView()
        {
            InitializeComponent();
            string server = "localhost";
            string database = "kursydb";
            string username = "root";
            string password = "";
            string constring = "SERVER="+ server +";" + "DATABASE="+database+";"+"UID="+username+";"+"PASSWORD="+password+";";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            string query = "select * from waluty";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read()) 
            {
                walutaz.Items.Add(reader["nazwa"]);
                walutado.Items.Add(reader["nazwa"]);
            }
            con.Close();
            
            
            
          
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void kwota_GotFocus(object sender, RoutedEventArgs e)
        {
            kwota.Text = "";
        }

        private void btnOk1_Click(object sender, RoutedEventArgs e)
        {
             item1 = walutaz.SelectedItem.ToString();
            ex1.IsExpanded = false;
        }

        private void btnOk2_Click(object sender, RoutedEventArgs e)
        {
             item2 = walutado.SelectedItem.ToString();
            tbPrzeliczanie.Text = "Przeliczam z " + item1 + " na " + item2;
            ex2.IsExpanded = false;
        }

        private void btnWynik_Click(object sender, RoutedEventArgs e)
        {
            string server = "localhost";
            string database = "kursydb";
            string username = "root";
            string password = "";
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection con = new MySqlConnection(constring);
            con.Open();
            char x = '"';
            string query1 = "select kurs from waluty where nazwa="+x+item1+x;
            string query2 = "select kurs from waluty where nazwa="+x+item2+x;
            MySqlCommand cmd1 = new MySqlCommand(query1, con);
            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            MySqlDataReader reader1 = cmd1.ExecuteReader();
            reader1.Read();
            var waluta1 = reader1["kurs"];
            con.Close();
            con.Open();
            MySqlDataReader reader2 = cmd2.ExecuteReader();
            reader2.Read();
            var waluta2 = reader2["kurs"];

            con.Close();
            var msgbox = Math.Round(Int32.Parse(kwota.Text) *(decimal)waluta1 / (decimal)waluta2,2);
            MessageBox.Show($"wynik: {msgbox}");

        }
    }
}
