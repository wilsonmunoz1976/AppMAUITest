namespace AppMAUITest;

public partial class App : Application
{
	public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cWGhAYVFpR2NbfE52flRDalxWVBYiSV9jS31TfkVnWH5bcHZWQGZaVA==");

        InitializeComponent();

		MainPage = new AppShell();
	}
}
