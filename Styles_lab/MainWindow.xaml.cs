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
using System.Collections.ObjectModel;

namespace Styles_lab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            
            themeChange.Loaded += ThemeChange_Click;
            themeChange.Click += ThemeChange_Click;
        }

        

        private void ThemeChange_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("LightTheme.xaml", UriKind.Relative);
            if (themeChange.IsChecked)
            {
                uri = new Uri("DarkTheme.xaml", UriKind.Relative);
            }
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt, *.rtf)|*.txt;*.rtf|Документы RichTextFormat (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(rTB.Document.ContentStart, rTB.Document.ContentEnd);
                using (FileStream fs = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
                    {
                        documentTextRange.Load(fs, DataFormats.Rtf);
                    }
                    else
                    {
                        documentTextRange.Load(fs, DataFormats.Text);
                    }
                }
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Документы RichTextFormat (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange documentTextRange = new TextRange(rTB.Document.ContentStart, rTB.Document.ContentEnd);
                using (FileStream fs = File.Create(saveFileDialog.FileName))
                {
                    if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                    {
                        documentTextRange.Save(fs, DataFormats.Rtf);
                    }
                    else
                    {
                        documentTextRange.Save(fs, DataFormats.Text);
                    }
                }
            }
        }
        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = (sender as ComboBox).SelectedItem.ToString();
            if (rTB != null)
            {
                rTB.FontFamily = new FontFamily(fontName);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            double fontSize = Convert.ToDouble((sender as ComboBox).SelectedItem);
            if (rTB != null)
                rTB.FontSize = fontSize;
        }

        private void btn_black_Checked(object sender, RoutedEventArgs e)
        {
            if (btn_blue != null)
            {
                if (!btn_blue.IsPressed && rTB != null)
                    rTB.Foreground = new SolidColorBrush(Colors.Goldenrod);
            }
        }

        private void btn_blue_Checked(object sender, RoutedEventArgs e)
        {
            if (btn_black != null)
            {
                if (!btn_black.IsPressed && rTB != null)
                    rTB.Foreground = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
