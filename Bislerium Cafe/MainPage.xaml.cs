namespace Bislerium_Cafe;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        InitializeComponent();
        NavigateToMainPage();
	}
    private async void NavigateToMainPage()
    {
        
       await Task.Delay(6000);

       
        Application.Current.MainPage = new NavigationPage(new login());
    }

}

