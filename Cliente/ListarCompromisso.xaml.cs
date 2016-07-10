using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Cliente
{
    /// <summary>
    /// Interaction logic for ListarCompromisso.xaml
    /// </summary>
    public partial class ListarCompromisso : Window
    {
        public ListarCompromisso()
        {
            InitializeComponent();
            Select();
        }

        private string ip = "http://localhost:63308/";

        private async void Select()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/api/Compro");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Compromisso> obj = JsonConvert.DeserializeObject<List<Models.Compromisso>>(str);
            dataGrid.ItemsSource = obj;
        }
    }
}
