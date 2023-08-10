using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;


namespace AppMAUITest
{
    public class ListaCofresUrnasDataViewModel
    {
        public ObservableCollection<ListaCofresUrnasDataModel> CofresUrnas { get; set; }
        public ListaCofresUrnasDataModel SelectedCofreUrna { get; set; }

        public ListaCofresUrnasDataViewModel()
        {
            //string sBodega = "009";
            //string sEstado = "0";
            //string sUsuario = "";

            //CofresUrnas = GetCofresUrnas(sBodega, sEstado, sUsuario);


        }

        public void GetCofresUrnas(string sBodega, string sEstado, string sUsuario)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            CofreUrnaListaResponse oListadoCofreUrna = new();

            using (HttpClient client = new(clientHandler))
            {

                HttpRequestMessage webRequest = new(HttpMethod.Post, AppMAUITest.Properties.Resources.URLWebApi + "/api/CofresUrnas/GetCofresUrnas/" + sBodega + "?estado=" + sEstado + (sUsuario == "" ? "" : "?usuario=" + sUsuario)) { };
                HttpResponseMessage response = client.SendAsync(webRequest).Result;


                if (response.IsSuccessStatusCode)
                {
                    HttpContent responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    oListadoCofreUrna = JsonSerializer.Deserialize<CofreUrnaListaResponse>(responseString);

                }
            }
            CofresUrnas = new ObservableCollection<ListaCofresUrnasDataModel>();
            if (oListadoCofreUrna != null)
            {
                if (oListadoCofreUrna.respuesta != null)
                {
                    if (Convert.ToInt16(oListadoCofreUrna.respuesta.Codigo) == 0)
                    {
                        if (oListadoCofreUrna.detalle != null)
                        {

                            foreach (CofreUrnaListaResponseDetalle listaCofresUrnasDet in oListadoCofreUrna.detalle)
                            {
                                CofresUrnas.Add(new ListaCofresUrnasDataModel() { codigo = listaCofresUrnasDet.codigo, producto = listaCofresUrnasDet.producto, inhumado = listaCofresUrnasDet.inhumado, nombreProveedor = listaCofresUrnasDet.nombreProveedor });
                            }
                        }
                    }
                }
            }
        }
    }
}
