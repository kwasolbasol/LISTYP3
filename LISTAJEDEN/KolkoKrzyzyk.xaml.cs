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

namespace LISTAJEDEN
{
    /// <summary>
    /// Interaction logic for KolkoKrzyzyk.xaml
    /// </summary>
    public partial class KolkoKrzyzyk : Window
    {
        public KolkoKrzyzyk()
        {
            InitializeComponent();
        }
        short player = 1;
        int[] t = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        bool endgame = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (player == 1 && ((Button)sender).IsEnabled)
            {
                ((Button)sender).Content = "X";
                ((Button)sender).IsEnabled = false;
                t[Convert.ToInt32(((Button)sender).Tag) - 1] = 1;
                player = 2;
            }
            else if (player == 2 && ((Button)sender).IsEnabled)
            {
                ((Button)sender).Content = "O";
                ((Button)sender).IsEnabled = false;
                t[Convert.ToInt32(((Button)sender).Tag) - 1] = -1;
                player = 1;
            }
            if (CheckWinner(1))
            {
                MessageBox.Show("Gracz X wygrał!");
                endgame = true;
            }
            if (CheckWinner(-1))
            {
                MessageBox.Show("Gracz O Wygrał!");
                endgame = true;
            }
            if (t[0] != 0 && t[1] != 0 && t[2] != 0 && t[3] != 0 && t[4] != 0 && t[5] != 0 && t[6] != 0 &&
                t[7] != 0 && t[8] != 0 && endgame == false)
            {
                MessageBox.Show("Remis!");
                endgame = true;
            }
            if (endgame)
            {
                button1.Content = "";
                button1.IsEnabled = true;
                button2.Content = "";
                button2.IsEnabled = true;
                button3.Content = "";
                button3.IsEnabled = true;
                button4.Content = "";
                button4.IsEnabled = true;
                button5.Content = "";
                button5.IsEnabled = true;
                button6.Content = "";
                button6.IsEnabled = true;
                button7.Content = "";
                button7.IsEnabled = true;
                button8.Content = "";
                button8.IsEnabled = true;
                button9.Content = "";
                button9.IsEnabled = true;
                Array.Clear(t, 0, t.Length);
                endgame = false;
                player = 1;
            }
        }
        private bool CheckWinner(int p)
        {
            if ((t[0] == p && t[1] == p && t[2] == p) ||
                (t[3] == p && t[4] == p && t[5] == p) ||
                (t[6] == p && t[7] == p && t[8] == p) ||
                (t[0] == p && t[3] == p && t[6] == p) ||
                (t[1] == p && t[4] == p && t[7] == p) ||
                (t[2] == p && t[5] == p && t[8] == p) ||
                (t[0] == p && t[4] == p && t[8] == p) ||
                (t[2] == p && t[4] == p && t[6] == p))
                return true;
            else
                return false;
        }
    }
}
