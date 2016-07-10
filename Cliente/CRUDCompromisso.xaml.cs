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
    /// Interaction logic for CRUDCompromisso.xaml
    /// </summary>
    public partial class CRUDCompromisso : Window
    {
        public CRUDCompromisso()
        {
            InitializeComponent();
            dp.SelectedDate = DateTime.Now;
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

        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Compromisso f = new Models.Compromisso
            {
                id = int.Parse(txtId.Text),
                descricao = txtDesc.Text,
                local = txtLocal.Text,
                data =  Convert.ToDateTime(dp.SelectedDate),
                realizado = false
            };
            List<Models.Compromisso> fl = new List<Models.Compromisso>();
            fl.Add(f);
            string s = "=" + JsonConvert.SerializeObject(fl);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/api/Compro", content);
            Select();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Compromisso f = new Models.Compromisso
            {
                id = int.Parse(txtId.Text),
                descricao = txtDesc.Text,
                local = txtLocal.Text,
                data = Convert.ToDateTime(dp.SelectedDate),
                realizado = false
            };
            string s = "=" + JsonConvert.SerializeObject(f);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("/api/Compro" + f.id, content);
            Select();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            await httpClient.DeleteAsync("/api/Compro/" + txtId.Text);
            Select();
        }
    }
}
