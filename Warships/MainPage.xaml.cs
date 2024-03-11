namespace Warships
{
    public partial class MainPage : ContentPage
    {
        private bool PlayerBot { get; set; }
        private bool WorkMenu {  get; set; }
        private int StepMenu { get; set; }
        private Settings Settings { get; set; }
        private Button SelectedLang { get; set; }

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InitalizeStartValues();
        }

        private void InitalizeStartValues()
        {

            Settings = new Settings(0);
            StepMenu = 0;
            WorkMenu = true;
            
            if(Preferences.Get("LangID", defaultValue: 0) == 0)
            {
                TextPl.BackgroundColor = Colors.DarkGray;
                SelectedLang = TextPl;
            }
            else
            {
                TextEn.BackgroundColor = Colors.DarkGray;
                SelectedLang = TextEn;
            }
            SetMenu();
            MainGrid_SizeChanged(this, null); ;
        }
        private void GoGameOption(object sender, EventArgs e)
        {
            if(WorkMenu == true)
            {
                if (GoGame.Text.Equals(Settings.LangStringValue(0)))
                {
                    ReturnButton.IsVisible = true;
                    GoGame.Text = Settings.LangStringValue(2);
                    Setting.Text = Settings.LangStringValue(3);
                }
                else if (GoGame.Text.Equals(Settings.LangStringValue(6)))
                {
                    if (TextPl.BackgroundColor == Colors.DarkGray)
                        Settings.SetLangID(0);
                    if (TextEn.BackgroundColor == Colors.DarkGray)
                        Settings.SetLangID(1);

                    SetMenu();
                }
                else if (GoGame.Text.Equals(Settings.LangStringValue(2)))
                {
                    PlayerBot = true;
                    ReturnButton.IsVisible = true;
                    GoGame.Text = Settings.LangStringValue(4);
                    Setting.Text = Settings.LangStringValue(5);
                    StepMenu = 1;
                }
                else if (GoGame.Text.Equals(Settings.LangStringValue(4)))
                {
                    if (PlayerBot == true)
                    {
                        WorkMenu = false;
                        Navigation.PushModalAsync(new _5x5(PlayerBot));

                        var timer = Dispatcher.CreateTimer();
                        timer.Interval = TimeSpan.FromSeconds(3);
                        timer.Tick += (s, e) =>
                        {
                            WorkMenu = true;
                            timer.Stop();
                        };
                        timer.Start();
                    }
                    else
                    {
                        WorkMenu = false;
                        Navigation.PushModalAsync(new _5x5(PlayerBot));

                        var timer = Dispatcher.CreateTimer();
                        timer.Interval = TimeSpan.FromSeconds(3);
                        timer.Tick += (s, e) =>
                        {
                            WorkMenu = true;
                            timer.Stop();
                        };
                        timer.Start();
                    }
                }
            }
        }
        private void ReturnMenu(object sender, EventArgs e)
        {
            if(WorkMenu == true){
                if (StepMenu == 0)
                    SetMenu();
                if (StepMenu == 1)
                    SetMenu(Settings.LangStringValue(2), Settings.LangStringValue(3));
            }
        }
        private void SettingOption (object sender, EventArgs e)
        {
            if (Setting.Text.Equals(Settings.LangStringValue(1)))
            {
            GoGame.Text = Settings.LangStringValue(6);
            Setting.IsVisible = false;
            ReturnButton.IsVisible = true;
            TextPl.IsVisible = true;
            TextEn.IsVisible = true;
            }
            else if(Setting.Text.Equals(Settings.LangStringValue(3)))
            {
                StepMenu = 1;
                PlayerBot = false;
                ReturnButton.IsVisible = true;
                GoGame.Text = Settings.LangStringValue(4);
                Setting.Text = Settings.LangStringValue(5);
            }
            else if(Setting.Text.Equals(Settings.LangStringValue(5)))
            {
                if (PlayerBot == true)
                {
                    WorkMenu = false;
                    Navigation.PushModalAsync(new _7x7(PlayerBot));

                    var timer = Dispatcher.CreateTimer();
                    timer.Interval = TimeSpan.FromSeconds(3);
                    timer.Tick += (s, e) =>
                    {
                        WorkMenu = true;
                        timer.Stop();
                    };
                    timer.Start();
                }
                else
                {
                    WorkMenu = false;
                    Navigation.PushModalAsync(new _7x7(PlayerBot));

                    var timer = Dispatcher.CreateTimer();
                    timer.Interval = TimeSpan.FromSeconds(3);
                    timer.Tick += (s, e) =>
                    {
                        WorkMenu = true;
                        timer.Stop();
                    };
                    timer.Start();
                }
            }
        }
        private void InitalizeChangeLanguages()
        {
            GoGame.Text = Settings.LangStringValue(0);
            Setting.Text = Settings.LangStringValue(1);
            TextPl.Text = Settings.LangStringValue(7);
            TextEn.Text = Settings.LangStringValue(8);
            PlayerGuide.Text = Settings.LangStringValue(20);
        }
        private void SetMenu()
        {
            InitalizeChangeLanguages();
            Setting.IsVisible = true;
            ReturnButton.IsVisible = false;
            TextPl.IsVisible = false;
            TextEn.IsVisible = false;
        }
        private void SetMenu(string val1, string val2)
        {
            GoGame.Text = val1;
            Setting.Text = val2;
            StepMenu--;
        }

        private void SelectLanguage_Clicked(object sender, EventArgs e)
        {
            string pl = Settings.LangStringValue(7);
            string en = Settings.LangStringValue(8);
            Button button = (Button)sender;
            switch (button.Text)
            {
                case var text when text == pl:
                    SelectedLang.BackgroundColor = Colors.Gray;
                    SelectedLang = button;
                    SelectedLang.BackgroundColor = Colors.DarkGray;
                    break;
                case var text when text == en:
                    SelectedLang.BackgroundColor = Colors.Gray;
                    SelectedLang = button;
                    SelectedLang.BackgroundColor = Colors.DarkGray;
                    break;
                default:
                    break;
            }
        }

        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            int width = (int)MainGrid.Width;
            int height = (int)MainGrid.Height;
            if (width < height)
            {
                width /= 7;
                ReturnButton.HeightRequest = ReturnButton.WidthRequest = TextPl.HeightRequest = TextEn.HeightRequest = Setting.HeightRequest = GoGame.HeightRequest = width;
                TextPl.WidthRequest = TextEn.WidthRequest = Setting.WidthRequest = GoGame.WidthRequest = width * 3;
                TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = width / 3;
                PlayerGuide.WidthRequest = width * 2;
                PlayerGuide.FontSize = width / 4;

            }
            else
            {
                height /= 7;
                ReturnButton.HeightRequest = ReturnButton.WidthRequest = TextPl.HeightRequest = TextEn.HeightRequest = Setting.HeightRequest = GoGame.HeightRequest = height;
                TextPl.WidthRequest = TextEn.WidthRequest = Setting.WidthRequest = GoGame.WidthRequest = height * 3;
                TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = height / 3;
                PlayerGuide.WidthRequest = height * 2.5;
                PlayerGuide.FontSize = height / 4;
            }
        }
        private void PlayerGuide_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlayerGuide());
        }
    }

    }
