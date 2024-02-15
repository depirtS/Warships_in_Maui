namespace Warships
{
    public partial class MainPage : ContentPage
    {
        private bool PlayerBot { get; set; }
        private int StepMenu { get; set; }
        private Settings Settings { get; set; }

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Settings = new Settings(0);
            InitalizeStartValues();
        }

        private void InitalizeStartValues()
        {
            StepMenu = 0;
        }
        private void GoGameOption(object sender, EventArgs e)
        {
            if (GoGame.Text.Equals(Settings.LangStringValue(0)))
            {
                ReturnButton.IsVisible = true;
                GoGame.Text = Settings.LangStringValue(2);;
                Setting.Text = Settings.LangStringValue(3);
            }
            else if (GoGame.Text.Equals(Settings.LangStringValue(6)))
            {
                if (CheckPl.IsChecked == true)
                    Settings.SetLangID(0);
                if (CheckEn.IsChecked == true)
                    Settings.SetLangID(1);

                InitalizeChangeLanguages();
                SetMenu();
            }
            else if(GoGame.Text.Equals(Settings.LangStringValue(2)))
            {
                PlayerBot = true;
                ReturnButton.IsVisible = true;
                GoGame.Text = Settings.LangStringValue(4);
                Setting.Text = Settings.LangStringValue(5);
                StepMenu = 1;
            }
            else if(GoGame.Text.Equals(Settings.LangStringValue(4)))
            {
                if(PlayerBot == true)
                {
                    Navigation.PushModalAsync(new _5x5(PlayerBot));
                }
                else
                {
                    Navigation.PushModalAsync(new _5x5(PlayerBot));
                }
            }
        }
        private void ReturnMenu(object sender, EventArgs e)
        {
            if(StepMenu == 0)
                SetMenu();
            if (StepMenu == 1)
                SetMenu(Settings.LangStringValue(2), Settings.LangStringValue(3));
        }
        private void SettingOption (object sender, EventArgs e)
        {
            if (Setting.Text.Equals(Settings.LangStringValue(1)))
            {
            GoGame.Text = Settings.LangStringValue(6);
            Setting.IsVisible = false;
            ReturnButton.IsVisible = true;
            CheckPl.IsVisible = true;
            TextPl.IsVisible = true;
            CheckEn.IsVisible = true;
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
                    Navigation.PushModalAsync(new _7x7(PlayerBot));
                    //_7x7 secondPage = new _7x7();
                    //this.Content = secondPage.Content;
                    //TODO: gra na planszy 7x7 z botem
                }
                else
                {
                    Navigation.PushModalAsync(new _7x7(PlayerBot));
                    //_7x7 secondPage = new _7x7();
                    //this.Content = secondPage.Content;
                    //TODO: gra na planszy 7x7 z graczem
                }
            }
        }
        private void InitalizeChangeLanguages()
        {
            GoGame.Text = Settings.LangStringValue(0);
            Setting.Text = Settings.LangStringValue(1);
            TextPl.Text = Settings.LangStringValue(7);
            TextEn.Text = Settings.LangStringValue(8);
        }
        private void SetMenu()
        {
            InitalizeChangeLanguages();
            Setting.IsVisible = true;
            ReturnButton.IsVisible = false;
            CheckPl.IsVisible = false;
            TextPl.IsVisible = false;
            CheckEn.IsVisible = false;
            TextEn.IsVisible = false;
        }
        private void SetMenu(string val1, string val2)
        {
            GoGame.Text = val1;
            Setting.Text = val2;
            StepMenu--;
        }
    }

    }
