﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static Statki.MainWindow;

namespace Statki
{
    public partial class Gracz2 : Window
    {
        public Gracz2()
        {
            InitializeComponent();
            CreateButtons();
            Loaded += MyWindow_Loaded;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var button in gridMain.Children.OfType<Button>().ToList())
            {
                if (MainWindow.player2ships.Contains(Convert.ToString(button.Tag)))
                    ((Gra)gridMain.DataContext).FieldP2[Convert.ToInt32(Convert.ToString(button.Tag).Substring(1))] = 3;
                if (MainWindow.player1shipsE.Contains(Convert.ToString(button.Tag)))
                    button.Content = "";
            }
        }
        int numOfShips = 20;
        List<String> hitBoxes = new List<String>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.turn == 2)
            {
                Button btn = (Button)sender;
                SolidColorBrush ChosenColor;
                if (btn.Content != null)
                {
                    ((Gra)gridMain.DataContext).FieldP1[Convert.ToInt32(Convert.ToString(btn.Tag).Substring(1))] = 1;
                    ChosenColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#e30707");
                }
                else
                {
                    ((Gra)gridMain.DataContext).FieldP1[Convert.ToInt32(Convert.ToString(btn.Tag).Substring(1))] = 2;
                    ChosenColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#515251");
                }
                btn.Background = ChosenColor;
                if (!hitBoxes.Contains(Convert.ToString(btn.Tag)) && btn.Content != null)
                {
                    numOfShips--;
                    hitBoxes.Add(Convert.ToString(btn.Tag));
                }
                if (numOfShips == 0)
                {
                    MainWindow.automaticClose = true;
                    MessageBox.Show("GRACZ 2 WYGRAŁ!");
                    Restart();
                }
                MainWindow.turn = 1;
                TurnLabel.Content = "TURA GRACZA 1";
                ((Gra)this.DataContext).Player1content = "TY STRZELASZ";
            }
        }
        private void CreateButtons()
        {

            YesNoToBooleanConverter converter = new YesNoToBooleanConverter();
            for (int i = 2; i < 12; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Button button = new Button();
                    {
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                    };
                    button.Tag = ("P" + Convert.ToString(j - 1) + Convert.ToString(i - 2));
                    Binding binding = new Binding();
                    binding.Converter = converter;
                    binding.Mode = BindingMode.TwoWay;
                    binding.Path = new PropertyPath("FieldP2[" + Convert.ToString(j - 1) + Convert.ToString(i - 2) + "]");
                    button.SetBinding(Button.BackgroundProperty, binding);
                    button.Click += new RoutedEventHandler(Button_Click);
                    this.gridMain.Children.Add(button);
                }
            }

            for (int i = 2; i < 12; i++)
            {
                for (int j = 13; j < 23; j++)
                {
                    Button button = new Button();
                    {
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                    };
                    button.Tag = ("E" + Convert.ToString(j - 13) + Convert.ToString(i - 2));
                    button.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#09546b");
                    button.Click += new RoutedEventHandler(Button_Click);
                    this.gridMain.Children.Add(button);
                }
            }
        }
        void Restart()
        {
            var response = MessageBox.Show("Czy chcesz zagrać jeszcze raz?", "Wychodzenie...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else
            {
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (automaticClose == false)
            {
                MainWindow.automaticClose = true;
                var response = MessageBox.Show("Do you really want to exit?", "Exiting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                MainWindow.automaticClose = false;
            }
        }
    }
}
