using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;


namespace AppMAUITest
{
    public class BodegaDataViewModel
    {
        public ObservableCollection<BodegaDataModel> Bodegas { get; set; }

        public BodegaDataViewModel()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            BodegaResponse oBodega = new();

            using (HttpClient client = new(clientHandler))
            {

                HttpRequestMessage webRequest = new(HttpMethod.Post, AppMAUITest.Properties.Resources.URLWebApi + "/api/CofresUrnas/GetBodegas") { };
                HttpResponseMessage response = client.SendAsync(webRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    HttpContent responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    oBodega = JsonSerializer.Deserialize<BodegaResponse>(responseString);
                }
            }
            Bodegas = new ObservableCollection<BodegaDataModel>();
            if (oBodega != null)
            {
                if (oBodega.respuesta != null)
                {
                    if (Convert.ToInt16(oBodega.respuesta.Codigo) == 0)
                    {
                        if (oBodega.detalle != null)
                        {

                            foreach (BodegaResponseDetalle oBodegaDet in oBodega.detalle)
                            {
                                Bodegas.Add(new BodegaDataModel() { Name = oBodegaDet.nombrebodega, ID = oBodegaDet.codigobodega });
                            }
                        }
                    }
                }
            }
            
        }

    }
}
