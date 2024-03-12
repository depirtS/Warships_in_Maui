namespace Warships;

public partial class _7x7 : ContentPage
{
    private Settings Settings { get; set; }
    private Board Board { get; set; }
    private Player PlayerOne { get; set; }
    private Player PlayerTwo { get; set; }
    private bool Bot { get; set; }
    private bool StopGame { get; set; }
    private bool StepGame { get; set; }
    //true - select
    //false - attack
    private bool PlayerBool { get; set; }
    //true - first player
    //false - second player
    private bool SeeOwnFields { get; set; }
    private IDispatcherTimer Timer { get; set; }
    private int Time { get; set; }
    private int ShipCount { get; set; }
    private int AttackCount { get; set; }
    private List<string> ShipID { get; set; }
    private string AttackID { get; set; }
    private Dictionary<string, Button> buttonDictionary { get; set; }

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
        buttonDictionary = new Dictionary<string, Button>();
        InitalizeBoard();
        DeviceDisplay.MainDisplayInfoChanged += OnOrientationChanged;

        StepGame = true;
        PlayerBool = true;
        SeeOwnFields = true;

        PlayerOne = new Player(false);
        PlayerTwo = new Player(false);

        ShipCount = 15;
        ShipID = new List<string>();

        AttackCount = 1;
        AttackID = "";

        AnnouncementText.Text = Settings.LangStringValue(9);
        YesExit.Text = Settings.LangStringValue(10);
        NoExit.Text = Settings.LangStringValue(11);
        RandomSelectButton.Text = Settings.LangStringValue(12);
        ConfrimSelectButton.Text = Settings.LangStringValue(13);
        SeeMyBoard.Text = Settings.LangStringValue(14);
        Alert.Text = Settings.LangStringValue(15) + " 15";

        StopGame = true;
        InitalizeTimer();
        Time = 121;

        Timer = Dispatcher.CreateTimer();
        Timer.Interval = TimeSpan.FromSeconds(0.01);
        Timer.Tick += (s, e) =>
        {
            MainLayout_SizeChanged(this, null);
            ResponsiveFont(this, null);
            GameControl_ResponsiveFont(this, null);
            StopGame = false;
            Timer.Stop();
        };
        Timer.Start();
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

    private void InitalizeBoard()
    {
        string[] rows = { "A", "B", "C", "D", "E", "F", "G" };
        string[] cols = { "1", "2", "3", "4", "5", "6", "7" };

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                string buttonName = rows[i] + cols[j];
                var button = new Button
                {
                    Text = buttonName,
                    StyleId = buttonName,
                    TextColor = Colors.Transparent,
                    BackgroundColor = Colors.Gray,
                    CornerRadius = 3,
                    MinimumHeightRequest = 0,
                    MinimumWidthRequest = 0,
                    BorderWidth = 2,
                    Margin = new Thickness(1),
                    Padding = new Thickness(1),
                    BorderColor = Colors.Black
            };

                buttonDictionary.Add(button.StyleId, button);

                button.Clicked += Selected_Field;

                GridBoard.Children.Add(button);

                Grid.SetRow(button, i + 1);
                Grid.SetColumn(button, j + 1);

            }
        }
        Board = new Board(false, buttonDictionary);
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
        else if (StopGame == false)
        {
            Time = 121;
            if (StepGame == true)
            {
                SeeOwnFields = true;
                SeeMyBoard.Text = Settings.LangStringValue(14);
                if (PlayerBool == true)
                {
                    for (int i = 0; i < 15; i++)
                        PlayerOne.SetRandomOwnFields();
                    if (Bot == true)
                    {
                        SelectBoardBot();
                    }
                    else
                    {
                        PlayerBool = false;
                        ShipCount = 15;
                        SelectBoardPlayer(PlayerTwo, PlayerOne, "2");
                    }
                }
                else
                {
                    ShipCount = 0;
                    PlayerBool = true;
                    StepGame = false;
                    for (int i = 0; i < 15; i++)
                        PlayerTwo.SetRandomOwnFields();
                    SelectBoardPlayer(PlayerOne, PlayerTwo, "1");
                    Alert.Text = Settings.LangStringValue(18);
                }
            }
            else
            {
                if (PlayerBool == true)
                {
                    PlayerTwo.AttackRandomField(PlayerOne);
                    if (Bot == true)
                    {
                        BotAttack();
                    }
                    else
                    {
                        PlayerAttack(PlayerTwo, PlayerOne, "2");
                        PlayerBool = false;
                    }
                }
                else
                {
                    PlayerOne.AttackRandomField(PlayerTwo);
                    PlayerAttack(PlayerOne, PlayerTwo, "1");
                    PlayerBool = true;
                }
            }
        }
    }

    private void Selected_Field(object sender, EventArgs e)
    {
        if (StepGame == true)
        {
            SetShipID((Button)sender);
        }
        else if (StepGame == false)
        {
            SetAttackID((Button)sender);
        }
    }
    //private bool StepGame {  get; set; }
    //true - select
    //false - attack
    //private bool PlayerBool { get; set; }
    //true - first player
    //false - second player
    private void ConfrimSelectButton_Clicked(object sender, EventArgs e)
    {
        if (ShipCount == 0)
        {
            Time = 121;
            if (StepGame == true)
            {
                SeeOwnFields = true;
                SeeMyBoard.Text = Settings.LangStringValue(14);
                if (PlayerBool == true)
                {
                    Board.ConfrimSelect(PlayerOne,ShipID);
                    if (Bot == true)
                    {
                        SelectBoardBot();
                    }
                    else
                    {
                        PlayerBool = false;
                        ShipCount = 15;
                        SelectBoardPlayer(PlayerTwo, PlayerOne, "2");
                    }
                }
                else
                {
                    Board.ConfrimSelect(PlayerTwo, ShipID);
                    PlayerBool = true;
                    StepGame = false;
                    SelectBoardPlayer(PlayerOne, PlayerTwo, "1");
                    Alert.Text = Settings.LangStringValue(18);
                }
            }
            else if(AttackCount == 0)
            {
                if (PlayerBool == true)
                {
                    Board.ConfrimAttack(PlayerTwo,PlayerOne,AttackID);
                    if (Bot == true)
                    {
                        BotAttack();
                    }
                    else
                    {
                        PlayerAttack(PlayerTwo, PlayerOne, "2");
                        PlayerBool = false;
                    }
                }
                else
                {
                    Board.ConfrimAttack(PlayerOne, PlayerTwo, AttackID);
                    PlayerAttack(PlayerOne, PlayerTwo, "1");
                    PlayerBool = true;
                }
            }
        }
    }

    private void SelectBoardBot()
    {
        Board.SeeGamingBoard(PlayerOne, PlayerTwo);
        PlayerBool = true;
        StepGame = false;
        ShipCount = 0;
        for (int i = 0; i < 15; i++)
            PlayerTwo.SetRandomOwnFields();
        Alert.Text = Settings.LangStringValue(18);
        NextPlayerAlert(Settings.LangStringValue(17) + "1");
    }

    private void BotAttack()
    {
        Board.SeeGamingBoard(PlayerOne, PlayerTwo);
        AttackID = "";
        AttackCount = 1;
        PlayerBool = true;
        PlayerOne.AttackRandomField(PlayerTwo);
        NextPlayerAlert(Settings.LangStringValue(17) + "1");
    }

    private void SelectBoardPlayer(Player player1, Player player2, string playerID)
    {
        Board.SeeGamingBoard(player1, player2);
        Alert.Text = Settings.LangStringValue(15) + " 15";
        NextPlayerAlert(Settings.LangStringValue(17) + playerID);
        ShipID = new List<string>();
    }

    private void PlayerAttack(Player player1, Player player2, string playerID)
    {
        AttackID = "";
        AttackCount = 1;
        Board.SeeGamingBoard(player1, player2);
        NextPlayerAlert(Settings.LangStringValue(17) + playerID);
    }

    private void SetShipID(Button button)
    {
        if (button.BackgroundColor == Colors.Gray)
        {
            if (ShipCount > 0)
            {
                ShipCount--;
                ShipID.Add(button.Text);
                button.BackgroundColor = Colors.Yellow;
                Alert.Text = Settings.LangStringValue(15) + " " + ShipCount.ToString();
            }
        }
        else
        {
            ShipCount++;
            ShipID.Remove(button.Text);
            button.BackgroundColor = Colors.Gray;
            Alert.Text = Settings.LangStringValue(15) + " " + ShipCount.ToString();
        }
    }

    private void SetAttackID(Button button)
    {
        if (button.BackgroundColor == Colors.Gray && AttackCount == 0)
        {
            Button attackButton = buttonDictionary[AttackID];
            attackButton.BackgroundColor = Colors.Gray;
            button.BackgroundColor = Colors.Orange;
            AttackID = button.Text;
        }
        else if (button.BackgroundColor == Colors.Orange)
        {
            AttackCount++;
            AttackID = "";
            button.BackgroundColor = Colors.Gray;
        }
        else if (button.BackgroundColor == Colors.Gray && AttackCount != -1)
        {
            if (AttackCount > 0)
            {
                AttackCount--;
                AttackID = button.Text;
                button.BackgroundColor = Colors.Orange;
            }
        }
    }

    private void NextPlayerAlert(string player)
    {
        var timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(0.01);
        timer.Tick += (s, e) =>
        {
            ExitButton_Clicked(this, null);
            YesExit.IsVisible = false;
            AnnouncementText.Text = player;
            NoExit.Text = "OK";
            EndGame();
            timer.Stop();
        };
        timer.Start();
    }

    private void EndGame()
    {
        if (PlayerOne.HitAttacksID.Count == 15)
        {
            NoExit.IsVisible = false;
            YesExit.IsVisible = true;
            AnnouncementText.Text = Settings.LangStringValue(19) + "1";
            YesExit.Text = "OK";
        }
        if (PlayerTwo.HitAttacksID.Count == 15)
        {
            NoExit.IsVisible = false;
            YesExit.IsVisible = true;
            AnnouncementText.Text = Settings.LangStringValue(19) + "2";
            YesExit.Text = "OK";
        }
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
        AnnouncementText.Text = Settings.LangStringValue(9);
        NoExit.Text = Settings.LangStringValue(11);
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
    private void RandomSelectButton_Clicked(object sender, EventArgs e)
    {
        Time = 0;
        TimeText.Text = "02:00";
        SeeOwnFields = true;
    }

    private void SeeMyBoard_Clicked(object sender, EventArgs e)
    {
        AttackID = "";
        AttackCount = 1;
        if (PlayerBool == true && StepGame == false)
        {
            SeeOwnBoard(PlayerOne, PlayerTwo);
        }
        else if (StepGame == false)
        {
            SeeOwnBoard(PlayerTwo, PlayerOne);
        }
    }

    private void SeeOwnBoard(Player playerOne, Player playerTwo)
    {
        if (SeeOwnFields == true && StepGame == false)
        {
            SeeOwnFields = false;
            AttackCount = -1;
            List<Button> buttonList = Board.FindButton();
            foreach (Button button in buttonList)
                if (button != null)
                    playerOne.SeeOwnFields(button);
            SeeMyBoard.Text = Settings.LangStringValue(16);
        }
        else if (StepGame == false)
        {
            AttackCount = 1;
            SeeOwnFields = true;
            Board.SeeGamingBoard(playerOne, playerTwo);
            SeeMyBoard.Text = Settings.LangStringValue(14);
        }
    }

    private void ResponsiveFont(object sender, EventArgs e)
    {
        int size = (int)AnnouncementBox.Width / 50;
        if (size < 17)
            size = 17;

        AnnouncementText.FontSize = size;
    }

    private void GameControl_ResponsiveFont(object sender, EventArgs e)
    {
        double size = (int)ScrollView.Width / 50;
        if (size < 17)
            size = 17;

        Alert.FontSize = size;
        size /= 1.2;
        RandomSelectButton.FontSize = size;
        ConfrimSelectButton.FontSize = size;
        SeeMyBoard.FontSize = size;
    }

    private void MainLayout_SizeChanged(object sender, EventArgs e)
    {
        if (MainLayout.Width <= 760)
        {
            MainLayout.Orientation = StackOrientation.Vertical;
            GridBoard.Margin = 20;
            GridGameControl.Margin = 20;

        }
        else
        {
            MainLayout.Orientation = StackOrientation.Horizontal;
            GridBoard.Margin = 0;
            GridGameControl.Margin = 0;
        }
        int size;
        if (MainLayout.Width > 800)
        {
            size = (int)(MainLayout.Width / 2) - 20;
            GridBoard.WidthRequest = size;
            GridGameControl.WidthRequest = size;
        }
        else
        {
            GridBoard.WidthRequest = 380;
            GridGameControl.WidthRequest = 380;
            ExitButton.WidthRequest = ExitButton.HeightRequest = 80;
        }
    }

    void OnOrientationChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        var orientation = e.DisplayInfo.Orientation;
        if (orientation == DisplayOrientation.Portrait)
        {
            MainLayout_SizeChanged(this, null);
            ResponsiveFont(this, null);
            GameControl_ResponsiveFont(this, null);
        }
        else if (orientation == DisplayOrientation.Landscape)
        {
            MainLayout_SizeChanged(this, null);
            ResponsiveFont(this, null);
            GameControl_ResponsiveFont(this, null);
        }
    }
}