using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ASCII_FBX_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string filepath = "";
        private void ReadModelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hello, World!");

            //validate filepath
            if(filepath != "") 
            {
                Model model = ModelRead.Read(filepath);

                //WPF display for the output
                model.Print();
            }
            else
            {
                Console.WriteLine("Empty filepath");
            }


        }
        private void LoadModelButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filepath = openFileDialog.FileName;
                Console.WriteLine($"{filepath}");
            }

        }
    }

}