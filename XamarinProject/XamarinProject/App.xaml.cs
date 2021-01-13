using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.Views;

[assembly: ExportFont("PressStart2P-Regular.ttf", Alias = "PressStart2P")]

namespace XamarinProject
{
  

    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc1NTc2QDMxMzgyZTM0MmUzMFlqYWF4MWRGU1ZyVlNyT1MzaXVCMWV3ckJYZkR0dzR6L3dSTkVhQWZPOFE9;Mzc1NTc3QDMxMzgyZTM0MmUzMFhYSFhReFpjMzRyb1dHbUFiZklsWWRGczhqQmJrSTN4NFJYYzAxSWg1WFk9;Mzc1NTc4QDMxMzgyZTM0MmUzMFBFNUFkUWp4eDRUK0Q4eVRuVlczSkg3V0szdzAzWmtWNjR4V0pPWExTNVE9;Mzc1NTc5QDMxMzgyZTM0MmUzMEZiUlkyelBpYURjVXMzTXRTQU1TRjRnbXgwZVYzSWEvRFgydG5mZTJ6eUU9;Mzc1NTgwQDMxMzgyZTM0MmUzMGgvVExvNE9GS2J1c25yWmhnclovUE5ncnNPWURNVWE5azluQzN6WjJaVEU9;Mzc1NTgxQDMxMzgyZTM0MmUzMERrZ1MyUFRIWXBqU295bmtnSUNhSzM1RWtTK1FlU0QxR1diaUlNSXduMGM9;Mzc1NTgyQDMxMzgyZTM0MmUzMFZ5Qmd2MnZlUnBkVkZuMGFQeFdUcFBmTlRxa1NEK3FoV1JrcnEzUU45cnM9;Mzc1NTgzQDMxMzgyZTM0MmUzMEluTUdYNzdSWW15T3ZaQU5aOGdTbHpNTUM4dmNpZk5ZVVhXR1NLRWVSOU09;Mzc1NTg0QDMxMzgyZTM0MmUzMG10cXJtRTBTdHBydGJqa3VBcnI0WlhBdzJUbFRINm1QQUtxK09XK0FyNFU9;Mzc1NTg1QDMxMzgyZTM0MmUzMFdrM0RrR01aOS9uQ1RQV2pvOVM1WGVqS0pLVytjbkdqTzhGcDlJUXZoTU09");
            InitializeComponent();

            MainPage = new SplashScreenView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
