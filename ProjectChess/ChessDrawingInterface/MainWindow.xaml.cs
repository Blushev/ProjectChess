using System;
using System.Collections.Generic;
using System.IO;
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
using ChessLogic;

namespace ChessDrawingInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += BoardRenderResize;
        }
        InterfaceBoard newGame;
        bool saved = false;
        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            // byte[,] massive = new byte[8,8];
            ChessLogic.LogicBoard b1 = new ChessLogic.LogicBoard();
            newGame = new InterfaceBoard(b1, chessCanvas, turnCount);
            saved = false;

        }
        private List<string> Read() 
        {
            string filename = "result.txt";
            StreamReader ShapeReader = new StreamReader(filename, Encoding.Default);
            string line;
            List<string> lines = new List<string>();
            using (ShapeReader)
            {
                do
                {
                    line = ShapeReader.ReadLine();
                    if (line == null) continue;
                    lines.Add(line);
                } while (line != null);
                ShapeReader.Close();
            }
            return lines;
        }

        private void ShowResultsClick(object sender, RoutedEventArgs e)
        {
            List<string> lines = Read();
            results.Clear();
            foreach(var line in lines)
            {
                // results.Text = line + "\n";
                results.AppendText(line + "\n");
            }
        }


        private void saveClick(object sender, RoutedEventArgs e)
        {
            if (newGame == null || !newGame.calcBoard.EndGameFlag)
            {
                MessageBox.Show("Игра не началась или не закончилась");
                return;
            }
            if (saved)
                return;

            List<string> lines = Read();

            bool playerExist = false;
            int index = 0;
            foreach (var singLine in lines)
            {
                string[] entries = singLine.Split('\t');

                if (entries[0] == playerName.Text)
                {
                    playerExist = true;
                    int result = int.Parse(entries[1]);
                    result++;
                    lines.Add(entries[0] + '\t' + result.ToString());
                    break;
                }
                index++;
            }
            if (playerExist)
                lines.RemoveAt(index);

            else
                lines.Add(playerName.Text + '\t' + "1");

            var fs = new System.IO.FileStream("result.txt", FileMode.OpenOrCreate, FileAccess.Write);
            var sw = new System.IO.StreamWriter(fs, Encoding.UTF8);

            foreach (var lineWrite in lines)
            {
                 sw.WriteLine(lineWrite);
            }
            sw.Close();
            fs.Close();
            saved = true;
        }

        private void BoardRenderResize (object sender, System.EventArgs e)
        {
            if (newGame != null)
                newGame.RenderBoard();
        }
    }
}
