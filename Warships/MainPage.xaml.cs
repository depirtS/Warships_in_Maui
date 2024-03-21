namespace Warships
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// A boolean property indicating whether the player is a bot.
        /// </summary>
        private bool PlayerBot { get; set; }

        /// <summary>
        /// A boolean property indicating whether the menu is active.
        /// </summary>
        private bool WorkMenu { get; set; }

        /// <summary>
        /// An integer property specifying the current step of the menu.
        /// </summary>
        private int StepMenu { get; set; }

        /// <summary>
        /// A property storing the game settings, represented by an instance of the Settings class.
        /// </summary>
        private Settings Settings { get; set; }

        /// <summary>
        /// A property storing the button corresponding to the selected language in the settings.
        /// </summary>
        private Button SelectedLang { get; set; }

        /// <summary>
        /// A property storing a list of buttons corresponding to the available languages in the settings.
        /// </summary>
        private List<Button> LangButton { get; set; }


        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InitalizeStartValues();
        }

        /// <summary>
        /// Initializes the start values for the application.
        /// </summary>
        /// <remarks>
        /// This method performs the following initializations:
        /// - Defines a list of language abbreviations.
        /// - Initializes the LangButton list with buttons found by name using the language abbreviations.
        /// - Initializes a new Settings object with a parameter of 0.
        /// - Sets StepMenu to 0 and WorkMenu to true.
        /// - Iterates over the LangButton list. If the stored language preference matches the current ID, it sets the button's background color to dark gray and assigns it to SelectedLang and stop iterates.
        /// - Calls the SetMenu method to set up the menu.
        /// - Calls the MainGrid_SizeChanged method with null event arguments.
        /// </remarks>
        private void InitalizeStartValues()
        {
            string[] shortLang = { "Pl", "En", "It" };
            LangButton = new List<Button>();
            foreach (var item in shortLang)
            {
                LangButton.Add(this.FindByName<Button>("Text" + item));
            }
            Settings = new Settings(0);
            StepMenu = 0;
            WorkMenu = true;

            int ID = 0;
            foreach (var item in LangButton)
            {
                if (Preferences.Get("LangID", defaultValue: 0) == ID)
                {
                    item.BackgroundColor = Colors.DarkGray;
                    SelectedLang = item;
                    break;
                }
                ID++;
            }
            SetMenu();
            MainGrid_SizeChanged(this, null);
        }

        /// <summary>
        /// Handles the game option logic based on the current state of the game and the text of the GoGame button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// This method performs different actions based on the text of the GoGame button:
        /// - If the text is equal to the first language string value, it makes the ReturnButton visible and updates the text of the GoGame and Setting buttons.
        /// - If the text is equal to the sixth language string value, it sets the language ID based on the selected language button and calls the SetMenu method.
        /// - If the text is equal to the second language string value, it sets PlayerBot to true, makes the ReturnButton visible, updates the text of the GoGame and Setting buttons, and sets StepMenu to 1.
        /// - If the text is equal to the fourth language string value, it starts a new game and sets up a timer to set WorkMenu back to true after 3 seconds.
        /// </remarks>
        private void GoGameOption(object sender, EventArgs e)
        {
            if (WorkMenu == true)
            {
                if (GoGame.Text.Equals(Settings.LangStringValue(0)))
                {
                    ReturnButton.IsVisible = true;
                    GoGame.Text = Settings.LangStringValue(2);
                    Setting.Text = Settings.LangStringValue(3);
                }
                else if (GoGame.Text.Equals(Settings.LangStringValue(6)))
                {
                    int ID = 0;
                    foreach (var item in LangButton)
                    {
                        if (item.BackgroundColor == Colors.DarkGray)
                            Settings.SetLangID(ID);
                        ID++;
                    }

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

        /// <summary>
        /// Handles the return to the menu.
        /// If WorkMenu is true, it sets the menu based on the value of StepMenu.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// If WorkMenu is true and StepMenu is 0, it calls SetMenu with no arguments.
        /// If WorkMenu is true and StepMenu is 1, it calls SetMenu with language-specific values.
        /// </remarks>
        private void ReturnMenu(object sender, EventArgs e)
        {
            if (WorkMenu == true)
            {
                if (StepMenu == 0)
                    SetMenu();
                if (StepMenu == 1)
                    SetMenu(Settings.LangStringValue(2), Settings.LangStringValue(3));
            }
        }

        /// <summary>
        /// Handles the settings option logic based on the current state of the game and the text of the Setting button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// This method performs different actions based on the text of the Setting button:
        /// - If the text equals the first language string value, it updates the GoGame button text, hides the Setting button, shows the ReturnButton and ScrollSettings, and hides PlayerGuide.
        /// - If the text equals the third language string value, it sets StepMenu to 1, PlayerBot to false, shows the ReturnButton, updates the GoGame and Setting buttons text.
        /// - If the text equals the fifth language string value, it starts a new game and sets up a timer to set WorkMenu back to true after 3 seconds.
        /// </remarks>
        private void SettingOption(object sender, EventArgs e)
        {
            if (WorkMenu == true)
            {
                if (Setting.Text.Equals(Settings.LangStringValue(1)))
                {
                    GoGame.Text = Settings.LangStringValue(6);
                    Setting.IsVisible = false;
                    ReturnButton.IsVisible = true;
                    ScrollSettings.IsVisible = true;
                    PlayerGuide.IsVisible = false;
                }
                else if (Setting.Text.Equals(Settings.LangStringValue(3)))
                {
                    StepMenu = 1;
                    PlayerBot = false;
                    ReturnButton.IsVisible = true;
                    GoGame.Text = Settings.LangStringValue(4);
                    Setting.Text = Settings.LangStringValue(5);
                }
                else if (Setting.Text.Equals(Settings.LangStringValue(5)))
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

        }

        /// <summary>
        /// Initializes the language change functionality.
        /// </summary>
        /// <remarks>
        /// This method updates the text of various elements in the application to reflect the selected language. It uses the Settings object to retrieve the appropriate language string values based on the current language setting.
        /// The elements updated include:
        /// - The GoGame button
        /// - The Setting button
        /// - The TextPl, TextEn, and TextIt elements
        /// - The PlayerGuide text
        /// </remarks>
        private void InitalizeChangeLanguages()
        {
            GoGame.Text = Settings.LangStringValue(0);
            Setting.Text = Settings.LangStringValue(1);
            TextPl.Text = Settings.LangStringValue(7);
            TextEn.Text = Settings.LangStringValue(8);
            TextIt.Text = Settings.LangStringValue(26);
            PlayerGuide.Text = Settings.LangStringValue(20);
        }

        /// <summary>
        /// Sets up the menu for the application.
        /// </summary>
        /// <remarks>
        /// This method performs the following actions to set up the menu:
        /// - Calls the InitalizeChangeLanguages method to update the text of various elements to reflect the selected language.
        /// - Makes the Setting button and PlayerGuide text visible.
        /// - Hides the ReturnButton and ScrollSettings.
        /// </remarks>
        private void SetMenu()
        {
            InitalizeChangeLanguages();
            Setting.IsVisible = true;
            PlayerGuide.IsVisible = true;
            ReturnButton.IsVisible = false;
            ScrollSettings.IsVisible = false;
        }

        /// <summary>
        /// Sets up the menu for the application with specified values.
        /// </summary>
        /// <param name="val1">The text to be set for the GoGame button.</param>
        /// <param name="val2">The text to be set for the Setting button.</param>
        /// <remarks>
        /// This method updates the text of the GoGame and Setting buttons with the provided values and decrements the StepMenu value by one.
        /// </remarks>
        private void SetMenu(string val1, string val2)
        {
            GoGame.Text = val1;
            Setting.Text = val2;
            StepMenu--;
        }

        /// <summary>
        /// Handles the click event of the language selection button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Retrieves the language string values for Polish (pl), English (en), and Italian (it).
        /// - Casts the sender of the event to a Button object.
        /// - Changes the background color of the currently selected language button to gray.
        /// - Assigns the clicked button to the SelectedLang button and changes its background color to dark gray.
        /// </remarks>
        private void SelectLanguage_Clicked(object sender, EventArgs e)
        {
            string pl = Settings.LangStringValue(7);
            string en = Settings.LangStringValue(8);
            string it = Settings.LangStringValue(26);
            Button button = (Button)sender;
            SelectedLang.BackgroundColor = Colors.Gray;
            SelectedLang = button;
            SelectedLang.BackgroundColor = Colors.DarkGray;
        }

        /// <summary>
        /// Handles the size changed event of the main grid.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Retrieves the width and height of the main grid.
        /// - If the width is less than the height, it adjusts the size and font size of various elements based on the width.
        /// - If the width is not less than the height, it adjusts the size and font size of various elements based on the height.
        /// - If the current language ID is 2 (Italian), it further adjusts the font size of certain elements.
        /// </remarks>
        private void MainGrid_SizeChanged(object sender, EventArgs e)
        {
            int width = (int)MainGrid.Width;
            int height = (int)MainGrid.Height;
            if (width < height)
            {
                width /= 7;
                ReturnButton.HeightRequest = ReturnButton.WidthRequest = TextIt.HeightRequest = TextPl.HeightRequest = TextEn.HeightRequest = Setting.HeightRequest = GoGame.HeightRequest = width;
                TextIt.WidthRequest = TextPl.WidthRequest = TextEn.WidthRequest = Setting.WidthRequest = GoGame.WidthRequest = width * 3;
                TextIt.FontSize = TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = width / 3;
                PlayerGuide.WidthRequest = width * 2.5;
                PlayerGuide.FontSize = width / 4;
                ScrollSettings.HeightRequest = width * 4;
                if (Preferences.Get("LangID", defaultValue: 0) == 2)
                {
                    TextIt.FontSize = TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = width / 3.5;
                }

            }
            else
            {
                height /= 7;
                ReturnButton.HeightRequest = ReturnButton.WidthRequest = TextIt.HeightRequest = TextPl.HeightRequest = TextEn.HeightRequest = Setting.HeightRequest = GoGame.HeightRequest = height;
                TextIt.WidthRequest = TextPl.WidthRequest = TextEn.WidthRequest = Setting.WidthRequest = GoGame.WidthRequest = height * 3;
                TextIt.FontSize = TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = height / 3;
                PlayerGuide.WidthRequest = height * 2.5;
                PlayerGuide.FontSize = height / 4;
                ScrollSettings.HeightRequest = height * 3;
                if (Preferences.Get("LangID", defaultValue: 0) == 2)
                {
                    TextIt.FontSize = TextPl.FontSize = TextEn.FontSize = Setting.FontSize = GoGame.FontSize = height / 3.5;
                }
            }
        }

        /// <summary>
        /// Handles the click event of the player guide button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        /// <remarks>
        /// This method navigates to the PlayerGuide page when the player guide button is clicked.
        /// </remarks>
        private void PlayerGuide_Clicked(object sender, EventArgs e)
        {
            if (WorkMenu)
            {
                WorkMenu = false;
                Navigation.PushModalAsync(new PlayerGuide());

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