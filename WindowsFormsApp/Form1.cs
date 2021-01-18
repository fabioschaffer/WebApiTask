using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WindowsFormsApp {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            NewBook();
        }

        private void NewBook() {
			Uri Url = new Uri(@"http://localhost ....");
			NewBookVM newBookVM = new NewBookVM() {
				Id=1
			};
			using (HttpClient client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true })) {
				client.Timeout = TimeSpan.FromMilliseconds(-1);
				using (MultipartFormDataContent formData = new MultipartFormDataContent()) {
					using (StringContent sc = new StringContent(JsonConvert.SerializeObject(newBookVM), Encoding.UTF8, "application/json")) {
						using (HttpResponseMessage resp = client.PostAsync(new Uri(Url, $"Book/New"), sc).Result) {
							resp.EnsureSuccessStatusCode();
							Task<string> result = resp.Content.ReadAsStringAsync();
							string res = result.Result;
							resp.GetHashCode();
						}
					}
				}
			}
		}
    }
	public class NewBookVM {
		public int Id { get; set; }
	}
}
