namespace Warships;

public partial class _5x5 : ContentPage
{
    private Settings Settings { get; set; } 
    private bool StopGame {  get; set; }
    protected override bool OnBackButtonPressed()
    {
        ExitButton_Clicked(this,null);
        return true;
    }
	public _5x5()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Settings = new Settings(0);
        InitializeValue();

    }
    private void InitializeValue()
    {
        StopGame = false;
        AnnouncementText.Text = Settings.LangStringValue(9);
        YesExit.Text = Settings.LangStringValue(10);
        NoExit.Text = Settings.LangStringValue(11);
    }
    private void Selected_Field(object sender, EventArgs e)
    {

    }

    private void ExitButton_Clicked(object sender, EventArgs e)
    {
        MainLayout.IsVisible = false;
        AnnouncementBox.IsVisible = true;
        AnnouncementText.IsVisible = true;
        YesExit.IsVisible = true;
        NoExit.IsVisible = true;  
        StopGame = true;
    }
    private void YesExit_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void NoExit_Clicked(object sender, EventArgs e)
    {
        MainLayout.IsVisible = true;
        AnnouncementBox.IsVisible = false;
        AnnouncementText.IsVisible = false;
        YesExit.IsVisible = false;
        NoExit.IsVisible = false;
        StopGame = false;
    }

    private void SetRaitoBoard(object sender, EventArgs e)
    {
        Grid views = (Grid)sender;
        double raito = 1.3;
        if (MainLayout.Height > MainLayout.Width)
        {
            views.HeightRequest = MainLayout.Width / raito;
            views.WidthRequest = MainLayout.Width / raito;
        }
        else
        {
            views.HeightRequest = MainLayout.Height / raito;
            views.WidthRequest = MainLayout.Height / raito;
        }
    }

    private void ResponsiveFont(object sender, EventArgs e)
    {
        int size;
        if(AnnouncementBox.Width < 400)
            size = 14;
        else if(AnnouncementBox.Width < 500)
            size = 17;
        else if (AnnouncementBox.Width < 600)
            size = 19;
        else if(AnnouncementBox.Width < 700)
            size = 22;
        else if(AnnouncementBox.Width < 800)
            size = 24;
        else
            size = 26;
        AnnouncementText.FontSize = size;
    }

}