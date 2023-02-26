using Microsoft.Win32;
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
using Path = System.IO.Path;

namespace WPFWord
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
        string path = "";
        private void openfileBtn_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "All Files(*.*)|*.*|Text Files(*.txt)| *.txt";
            openDialog.FilterIndex = 2;


            if (openDialog.ShowDialog()== true)
            {
                using (var sr = File.OpenText(openDialog.FileName))
                {
                    textBox.Text = sr.ReadToEnd();                   
                    path = Path.GetFullPath(openDialog.FileName);
                    linkLbl.Content = path;
                }
            }
            textBox.IsEnabled = true;
            saveBtn.IsEnabled = true;
            checkLbl.IsEnabled = true;
            cutBtn.IsEnabled = true;
            copyBtn.IsEnabled = true;
            pasteBtn.IsEnabled = true;
            selectallBtn.IsEnabled = true;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(path,textBox.Text);
            MessageBox.Show("You have saved the text");
        }
        private void selectallBtn_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectAll();
            textBox.Focus();
        }
        private void cutBtn_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void pasteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != null)
            {
                textBox.Paste();
            }
        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != null)
            {
                textBox.Copy();
            }
        }
        public bool IsClicked { get; set; } = false;


        private void autosaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsClicked = true;
        }
        private void autosaveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsClicked = false;
        }
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsClicked == true)
            {
                File.WriteAllText(path, textBox.Text);
            }
        }

    }
}
