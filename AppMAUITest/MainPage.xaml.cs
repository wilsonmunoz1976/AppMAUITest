using Syncfusion.Maui.Inputs;

namespace AppMAUITest;

public partial class MainPage : ContentPage
{
	int count = 0;
    ListaCofresUrnasDataViewModel ld = new();


    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        txtTelephoneID.Text = GetIMEI(); // GetIMEI();

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    public static string GetIMEI()
    {
        string imei = DeviceInfo.Current.VersionString;

        return imei;
    }

    //public static string GetDeviceID()
    //{
    //    var context = Android.App.Application.Context;

    //    string id = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

    //    return id;
    //}


    private void grdDatos_CellTapped(object sender, Syncfusion.Maui.DataGrid.DataGridCellTappedEventArgs e)
    {
        ListaCofresUrnasDataModel rData = (ListaCofresUrnasDataModel)e.RowData;
        DisplayAlert("Mensaje", rData.inhumado, "Cancelar");
        
    }

    private void cboBodega_SelectionChanged(object sender, EventArgs e)
    {
        Picker cboBodega = (Picker)sender;
        string sBodega = ((BodegaDataModel)cboBodega.SelectedItem).ID;
        ld.GetCofresUrnas(sBodega, "0", "");
        ld.SelectedCofreUrna = null;
        //grdDatos.SelectedItem = null;
        grdDatos.ItemsSource = ld.CofresUrnas;
        grdDatos.BindingContext = ld;
    }

    private void grdDatos_ItemSelected(object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            ListaCofresUrnasDataModel rData = (ListaCofresUrnasDataModel)e.CurrentSelection[0];
            DisplayAlert("Mensaje", rData.inhumado, "Ok");
        }
    }
}

