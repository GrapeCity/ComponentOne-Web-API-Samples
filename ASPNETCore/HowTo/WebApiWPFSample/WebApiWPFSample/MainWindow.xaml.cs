using Microsoft.Win32;
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

using WebApiConsoleSample;

namespace WebApiWPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object MessageBoxButtons { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GenerateExcelFile(object sender, RoutedEventArgs e)
        {
            TextBox tbFileName = this.FindName("tbFileName") as TextBox;

            ComboBox cbFileFormat = this.FindName("cbFileFormat") as ComboBox;
            ComboBox cbDataFileName = this.FindName("cbDataFileName") as ComboBox;
            ComboBox cbTemplateFileName = this.FindName("cbTemplateFileName") as ComboBox;

            if (tbFileName != null && cbFileFormat != null && cbDataFileName != null && cbTemplateFileName != null)
            {
                GenerateExcelFromXMLDto dto = new GenerateExcelFromXMLDto();
                dto.FileName = tbFileName.Text;
                dto.TemplateFileName = cbTemplateFileName.Text;
                dto.Type = cbFileFormat.Text;
                dto.DataFileName = cbDataFileName.Text;

                try
                {

                    var service = new ExcelService(new System.Net.Http.HttpClient());
                    await service.GenerateExcelFromXML(dto);

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }




        }
    }
}
