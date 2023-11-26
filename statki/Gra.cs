using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Statki
{
    public class Gra : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<int> _FieldP1 = new ObservableCollection<int> { };
        ObservableCollection<int> _FieldP2 = new ObservableCollection<int> { };

        public ObservableCollection<int> FieldP1
        {
            get
            {
                return _FieldP1;
            }
            set
            {
                _FieldP1 = value;
                OnPropertyChanged("FieldP");
            }
        }

        public ObservableCollection<int> FieldP2
        {
            get
            {
                return _FieldP2;
            }
            set
            {
                _FieldP2 = value;
                OnPropertyChanged("FieldP");
            }
        }

        public Gra(int[] nFieldP1, int[] nFieldP2)
        {
            foreach (int _field in nFieldP1)
            {
                _FieldP1.Add(_field);
            }
            foreach (int _field in nFieldP2)
            {
                _FieldP2.Add(_field);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        string player1content = "TWOJA TURA";
        string player2content = "GRACZ 1 STRZELA";
        public String Player1content
        {
            get
            {
                return player1content;
            }
            set
            {
                player1content = value;
                OnPropertyChanged("Player1content");
            }
        }
        public String Player2content
        {
            get
            {
                return player2content;
            }
            set
            {
                player2content = value;
                OnPropertyChanged("Player2content");
            }
        }
    }
}
