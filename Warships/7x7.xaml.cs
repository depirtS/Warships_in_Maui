namespace Warships;

public partial class _7x7 : ContentPage
{
    private Settings Settings { get; set; }
    private bool Bot { get; set; }
    private bool StopGame { get; set; }
    private int StepGame { get; set; }
    private IDispatcherTimer Timer { get; set; }
    private int Time { get; set; }
    protected override bool OnBackButtonPressed()
    {
        ExitButton_Clicked(this, null);
        return true;
    }
    public _7x7(bool bot)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Settings = new Settings(0);
        Bot = bot;
        InitializeValue();
    }
    private void InitializeValue()
    {
        StepGame = 0;
        StopGame = false;
        AnnouncementText.Text = Settings.LangStringValue(9);
        YesExit.Text = Settings.LangStringValue(10);
        NoExit.Text = Settings.LangStringValue(11);
        RandomSelectButton.Text = Settings.LangStringValue(12);
        ConfrimSelectButton.Text = Settings.LangStringValue(13);
        SeeMyBoard.Text = Settings.LangStringValue(14);
    }

    private void InitalizeTimer()
    {
        Timer = Dispatcher.CreateTimer();
        Timer.Interval = TimeSpan.FromSeconds(1);
        Timer.Tick += (s, e) =>
        {
            TimerSetTime();
        };
        Timer.Start();
    }

    private void TimerSetTime()
    {
        if (StopGame == false && Time > 0)
        {
            Time--;
            int TimeSec = Time % 60;
            int TimeMin = Time / 60;
            string TimerSet = "";
            if (TimeSec < 10)
                TimerSet = "0" + TimeMin.ToString() + ":0" + TimeSec.ToString();
            else
                TimerSet = "0" + TimeMin.ToString() + ":" + TimeSec.ToString();
            TimeText.Text = TimerSet;
        }
    }
    private void Selected_Field(object sender, EventArgs e)
    {

    }

    private void ExitButton_Clicked(object sender, EventArgs e)
    {
        ScrollView.IsVisible = false;
        MainLayout.IsVisible = false;
        TimeBox.IsVisible = false;
        TimeText.IsVisible = false;
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
        ScrollView.IsVisible = true;
        MainLayout.IsVisible = true;
        TimeBox.IsVisible = true;
        TimeText.IsVisible = true;
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
        if (AnnouncementBox.Width < 400)
            size = 14;
        else if (AnnouncementBox.Width < 500)
            size = 17;
        else if (AnnouncementBox.Width < 600)
            size = 19;
        else if (AnnouncementBox.Width < 700)
            size = 22;
        else if (AnnouncementBox.Width < 800)
            size = 24;
        else
            size = 26;
        AnnouncementText.FontSize = size;
    }

    private void MainLayout_SizeChanged(object sender, EventArgs e)
    {
        if (MainLayout.Width <= 800)
            MainLayout.Orientation = StackOrientation.Vertical;
        else
            MainLayout.Orientation = StackOrientation.Horizontal;

        int size;
        if (MainLayout.Width < 900)
            size = 401;
        else if (MainLayout.Width < 1000)
            size = 450;
        else if (MainLayout.Width < 1100)
            size = 500;
        else if (MainLayout.Width < 1200)
            size = 550;
        else if (MainLayout.Width < 1300)
            size = 600;
        else if (MainLayout.Width < 1400)
            size = 650;
        else
            size = 700;

        GridBoard.WidthRequest = size;
        GridGameControl.WidthRequest = size;

    }
}