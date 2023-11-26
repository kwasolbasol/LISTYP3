using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Windows.Documents;
namespace LISTAJEDEN
{
    public partial class KolkoKrzyzyk : Window, INotifyPropertyChanged
    {
        private GameModel gameModel;
        public string[] CellValues => board.Select(value => value == 1 ? "X" : (value == 2 ? "O" : "")).ToArray();
        private int[] board = new int[9];

        public int[] Board
        {
            get { return board; }
            set
            {
                if (value != board)
                {
                    board = value;
                    OnPropertyChanged(nameof(Board));
                }
            }
        }


        public KolkoKrzyzyk()
        {
            InitializeComponent();
            gameModel = new GameModel();
            DataContext = gameModel;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int index = Convert.ToInt32(button.Tag) - 1;

            if (gameModel.IsValidMove(index))
            {
                gameModel.MakeMove(index);
                gameModel.SwitchPlayer();
                button.Content = gameModel.CurrentPlayerSymbol;

                if (gameModel.CheckWinner())
                {
                    MessageBox.Show($"Gracz {gameModel.CurrentPlayerSymbol} wygrał!");
                    ResetGame();
                }
                else if (gameModel.IsBoardFull())
                {
                    MessageBox.Show("Remis!");
                    ResetGame();
                }
            }
        }
        public void MakeMove(int index)
        {
            board[index] = gameModel.currentPlayer;
            OnPropertyChanged(nameof(CellValues));
            gameModel.SwitchPlayer();
        }
        private void ResetGame()
        {
            gameModel.ResetGame();
            foreach (var button in ((Grid)Content).Children.OfType<Button>())
            {
                button.Content = null;
            }
        }
    }
    public class GameModel : INotifyPropertyChanged
    {
        private int[] board = new int[9];
        private bool endgame = false;
        public short currentPlayer = 1;
        public GameModel()
        {
            ResetGame();
        }
        public string CurrentPlayerSymbol => currentPlayer == 1 ? "X" : "O";
        public bool IsBoardFull() => board.All(cell => cell != 0);
        public bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (AreEqual(board[i * 3], board[i * 3 + 1], board[i * 3 + 2]))
                    return true;
                if (AreEqual(board[i], board[i + 3], board[i + 6]))
                    return true;
            }
            if (AreEqual(board[0], board[4], board[8]) || AreEqual(board[2], board[4], board[6]))
                return true;

            return false;
        }
        private bool AreEqual(int a, int b, int c)
        {
            return a != 0 && a == b && b == c;
        }
        public bool IsValidMove(int index) => !endgame && board[index] == 0;

        public void MakeMove(int index)
        {
            board[index] = currentPlayer;
            OnPropertyChanged(nameof(Board));
        }
        public void SwitchPlayer()
        {
            currentPlayer = (short)(currentPlayer == 1 ? 2 : 1);
            OnPropertyChanged(nameof(CurrentPlayerSymbol));
        }
        public void ResetGame()
        {
            Array.Clear(board, 0, board.Length);
            endgame = false;
            currentPlayer = 1;
            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(CurrentPlayerSymbol));
        }
        public int[] Board => board;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
