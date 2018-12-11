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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BingoData data = new BingoData();
        Random random = new Random();
        List<int> rand;
        int Counter;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnLoad_Player1(object sender, RoutedEventArgs e)
        {
            BingoGrid1.Children.Clear();
            FontGrid(BingoGrid1);
            FillGrid(0, BingoGrid1);
        }
        private void OnLoad_Player2(object sender, RoutedEventArgs e)
        {
            BingoGrid2.Children.Clear();
            FontGrid(BingoGrid2);
            FillGrid(1, BingoGrid2);
        }
        private void OnLoad_Player3(object sender, RoutedEventArgs e)
        {
            BingoGrid3.Children.Clear();
            FontGrid(BingoGrid3);
            FillGrid(2, BingoGrid3);
        }
        private void OnLoad_Player4(object sender, RoutedEventArgs e)
        {
            BingoGrid4.Children.Clear();
            FontGrid(BingoGrid4);
            FillGrid(3, BingoGrid4);
        }
        private void FillGrid(int player, Grid grid)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    TextBlock block = new TextBlock
                    {
                        Text = "" + data.Players[player, i, j],
                        FontSize = 20

                    };
                    block.VerticalAlignment = VerticalAlignment.Center;
                    block.HorizontalAlignment = HorizontalAlignment.Center;
                    if (block.Text == "0")
                        block.Text = "";
                    Grid.SetColumn(block, j);
                    Grid.SetRow(block, i + 1);
                    grid.Children.Add(block);
                }
            }
        }
        private void FontGrid(Grid grid)
        {
            TextBlock LetterB = Textblock("B", 22);
            Grid.SetColumn(LetterB, 0);
            Grid.SetRow(LetterB, 0);
            grid.Children.Add(LetterB);
            TextBlock LetterI = Textblock("I", 22);
            Grid.SetColumn(LetterI, 1);
            Grid.SetRow(LetterI, 0);
            grid.Children.Add(LetterI);
            TextBlock LetterN = Textblock("N", 22);
            Grid.SetColumn(LetterN, 2);
            Grid.SetRow(LetterN, 0);
            grid.Children.Add(LetterN);
            TextBlock LetterG = Textblock("G", 22);
            Grid.SetColumn(LetterG, 3);
            Grid.SetRow(LetterG, 0);
            grid.Children.Add(LetterG);
            TextBlock LetterO = Textblock("O", 22);
            Grid.SetColumn(LetterO, 4);
            Grid.SetRow(LetterO, 0);
            grid.Children.Add(LetterO);
        }
        private TextBlock Textblock(string txt, int size)
        {
            TextBlock textblock =  new TextBlock
            {
                Text = txt,
                FontSize = size,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            return textblock;
        }
        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            rand = Enumerable.Range(1, 75).OrderBy(x => random.Next()).ToList();
            data.FillPlayers();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            string choice = ComboBox1.Text;
            if(choice == null)
            {
                MessageBox.Show("Debes Elegir la Cantidad de jugadores primero");
                return;
            }
            switch (choice)
            {
                case "1 Player":
                    Player1.IsEnabled = true;
                    Player2.IsEnabled = false;
                    Player3.IsEnabled = false;
                    Player4.IsEnabled = false;
                    break;
                case "2 Players":
                    Player1.IsEnabled = true;
                    Player2.IsEnabled = true;
                    Player3.IsEnabled = false;
                    Player4.IsEnabled = false;
                    break;
                case "3 Players":
                    Player1.IsEnabled = true;
                    Player2.IsEnabled = true;
                    Player3.IsEnabled = true;
                    Player4.IsEnabled = false;
                    break;
                case "4 Players":
                    Player1.IsEnabled = true;
                    Player2.IsEnabled = true;
                    Player3.IsEnabled = true;
                    Player4.IsEnabled = true;
                    break;
                default:
                    break;
            }
            ComboBox1.IsEnabled = false;
            Play.IsEnabled = false;
            Call.IsEnabled = true;
        }
        
        private void Call_Click(object sender, RoutedEventArgs e)
        {
            if(Counter >= rand.Count)
            {
                MessageBox.Show("Todos los números han sido jugados");
                return;
            }
            NumberBox.Text = "" + data.AssignLetter(rand[Counter]) + "-" + rand[Counter];
            MarkGrid(rand[Counter]);
            Counter++;
        }
        private void MarkGrid(int N)
        {
            data.Find(N);
        }
    }
   
    public class BingoData
    {
        public int[,,] Players = new int[4, 5, 5];
        public bool[,,] Marked = new bool[4, 5, 5];
        public void Find(int N)
        {
            int col = FindColumn(N);
            for(int k = 0; k < 4; k++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if(Players[k, col, j] == N)
                    {
                        Marked[k, col, j] = true;
                    }
                }
            }
            
        }
        public char AssignLetter(int N)
        {
            if (N > 0 && N < 16)
            {
                return 'B';
            }
            else if (N > 15 && N < 31)
            {
                return 'I';
            }
            else if (N > 30 && N < 46)
            {
                return 'N';
            }
            else if (N > 45 && N < 61)
            {
                return 'G';
            }
            else
            {
                return 'O';
            }
        }
        private int FindColumn(int N)
        {
            if(N > 0 && N < 16)
            {
                return 0;
            }
            else if(N > 15 && N < 31)
            {
                return 1;
            }
            else if(N > 30 && N < 46)
            {
                return 2;
            }
            else if(N > 45 && N < 61)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        public void FillPlayers()
        {
            Random random = new Random();
            for(int k = 0; k < 4; k++)
            {
                List<int> BingoB = Enumerable.Range(1, 15).OrderBy(x => random.Next()).Take(5).ToList();
                List<int> BingoI = Enumerable.Range(16, 15).OrderBy(x => random.Next()).Take(5).ToList();
                List<int> BingoN = Enumerable.Range(31, 15).OrderBy(x => random.Next()).Take(5).ToList();
                List<int> BingoG = Enumerable.Range(46, 15).OrderBy(x => random.Next()).Take(5).ToList();
                List<int> BingoO = Enumerable.Range(61, 15).OrderBy(x => random.Next()).Take(5).ToList();
                for (int i = 0; i < 5; i++)
                {
                    int counter = 0;
                    for(int j = 0; j < 5; j++)
                    {
                        
                        switch (i)
                        {
                            case 0:
                                Players[k, j, i] = BingoB[counter];
                                break;
                            case 1:
                                Players[k, j, i] = BingoI[counter];
                                break;
                            case 2:
                                if(j != 2)
                                    Players[k, j, i] = BingoN[counter];
                                break;
                            case 3:
                                Players[k, j, i] = BingoG[counter];
                                break;
                            case 4:
                                Players[k, j, i] = BingoO[counter];
                                break;
                            default:
                                break;
                        }
                        counter++;
                        
                    }
                }
            }
        }
    }
}
