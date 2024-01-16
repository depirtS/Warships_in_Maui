namespace Warships
{
    public partial class MainPage : ContentPage
    {
        private bool PlayerBot { get; set; }
        private int StepMenu { get; set; }

        public MainPage()
        {
            InitializeComponent();
            StepMenu = 0;
        }
        private void GoGameOption(object sender, EventArgs e)
        {
            if (GoGame.Text.Equals("Graj"))
            {
                ReturnButton.IsVisible = true;
                GoGame.Text = "Graj z botem";
                Setting.Text = "Graj z graczem";
            }
            else if (GoGame.Text.Equals("Zatwierdź"))
            {
                SetMenu();
                //TODO:: zmiana języka w aplikacji
            }
            else if(GoGame.Text.Equals("Graj z botem"))
            {
                PlayerBot = true;
                ReturnButton.IsVisible = true;
                GoGame.Text = "Plansza 5x5";
                Setting.Text = "Plansza 7x7";
                StepMenu = 1;
            }
            else if(GoGame.Text.Equals("Plansza 5x5"))
            {
                if(PlayerBot == true)
                {
                    //TODO: gra na planszy 5x5 z botem
                }
                else
                {
                    //TODO: gra na planszy 5x5 z graczem
                }
            }
        }
        private void ReturnMenu(object sender, EventArgs e)
        {
            if(StepMenu == 0)
                SetMenu();
            if (StepMenu == 1)
                SetMenu("Graj z botem", "Graj z graczem");
        }
        private void SettingOption (object sender, EventArgs e)
        {
            if (Setting.Text.Equals("Ustawienia"))
            {
            GoGame.Text = "Zatwierdź";
            Setting.IsVisible = false;
            ReturnButton.IsVisible = true;
            CheckPl.IsVisible = true;
            TextPl.IsVisible = true;
            CheckEn.IsVisible = true;
            TextEn.IsVisible = true;
            }
            else if(Setting.Text.Equals("Graj z graczem"))
            {
                StepMenu = 1;
                PlayerBot = false;
                ReturnButton.IsVisible = true;
                GoGame.Text = "Plansza 5x5";
                Setting.Text = "Plansza 7x7";
            }
            else if(Setting.Text.Equals("Plansza 7x7"))
            {
                if (PlayerBot == true)
                {
                    //TODO: gra na planszy 7x7 z botem
                }
                else
                {
                    //TODO: gra na planszy 7x7 z graczem
                }
            }
        }

        private void SetMenu()
        {
            GoGame.Text = "Graj";
            Setting.Text = "Ustawienia";
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
