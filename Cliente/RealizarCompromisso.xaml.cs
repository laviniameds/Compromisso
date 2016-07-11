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
    /// Interaction logic for RealizarCompromisso.xaml
    /// </summary>
    public partial class RealizarCompromisso : Window
    {
        public RealizarCompromisso()
        {
            InitializeComponent();
            Select();
        }

        private string ip = "http://localhost:63308/";

        private async void Select()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/api/Compro/");
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Compromisso> obj = JsonConvert.DeserializeObject<List<Models.Compromisso>>(str);
            dataGrid.ItemsSource = obj;
        }

        private async void realizar_Click(object sender, RoutedEventArgs e)
        {
            object c = ((Button)sender).CommandParameter;
            Models.Compromisso myObject = new Models.Compromisso();
            if (c is Models.Compromisso){myObject = (Models.Compromisso)c;}
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Compromisso f = new Models.Compromisso
            {
                id = Convert.ToInt32(myObject.id),
                descricao = myObject.descricao,
                local = myObject.local,
                data = myObject.data,
                realizado = true
            };
            string s = "=" + JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("/api/Compro/" + f.id, content);
            MessageBox.Show("Realizado com sucesso!");
            Select();
        }
    }
}
