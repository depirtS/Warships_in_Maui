namespace Warships;

public partial class _5x5 : ContentPage
{
    /// <summary>
    /// A property storing the game settings, represented by an instance of the Settings class.
    /// </summary>
    private Settings Settings { get; set; }

    /// <summary>
    /// A property storing the game board, represented by an instance of the Board class.
    /// </summary>
    private Board Board { get; set; }

    /// <summary>
    /// A property storing information about the first player, represented by an instance of the Player class.
    /// </summary>
    private Player PlayerOne { get; set; }

    /// <summary>
    /// A property storing information about the second player, represented by an instance of the Player class.
    /// </summary>
    private Player PlayerTwo { get; set; }

    /// <summary>
    /// A boolean property indicating whether the second player is a bot.
    /// </summary>
    private bool Bot { get; set; }

    /// <summary>
    /// A boolean property indicating whether the game is paused.
    /// </summary>
    private bool StopGame { get; set; }

    /// <summary>
    /// A boolean property indicating whether it's the game phase.
    /// </summary>
    private bool StepGame { get; set; }

    /// <summary>
    /// A boolean property indicating which player is currently active.
    /// </summary>
    private bool PlayerBool { get; set; }

    /// <summary>
    /// A boolean property indicating whether the player can see their own fields.
    /// </summary>
    private bool SeeOwnFields { get; set; }

    /// <summary>
    /// A property storing information about the game timer, represented by an instance of the IDispatcherTimer interface.
    /// </summary>
    private IDispatcherTimer Timer { get; set; }

    /// <summary>
    /// An integer property storing the game time.
    /// </summary>
    private int Time { get; set; }

    /// <summary>
    /// An integer property storing the number of ships.
    /// </summary>
    private int ShipCount { get; set; }

    /// <summary>
    /// An integer property storing the number of attacks.
    /// </summary>
    private int AttackCount { get; set; }

    /// <summary>
    /// A list property storing the identifiers of the ships.
    /// </summary>
    private List<string> ShipID { get; set; }

    /// <summary>
    /// A string property storing the identifier of the attack.
    /// </summary>
    private string AttackID { get; set; }

    /// <summary>
    /// Represents the radar data for Player One. 
    /// This is a string representation of the sizes and quantities of the detected enemy ships on Player One's board.
    /// Each ship is represented by a series of 'x' characters, where the number of 'x' characters corresponds to the size of the ship.
    /// The quantity of each size of ship is represented by a number followed by an asterisk (*) before the 'x' characters.
    /// For example, "2*xx, 1*xxx" means there are two double-sized ships and one triple-sized ship detected on the board.
    /// </summary>
    private string playerOneRadard { get; set; }

    /// <summary>
    /// Represents the radar data for Player Two. 
    /// This is a string representation of the sizes and quantities of the detected enemy ships on Player Two's board.
    /// Each ship is represented by a series of 'x' characters, where the number of 'x' characters corresponds to the size of the ship.
    /// The quantity of each size of ship is represented by a number followed by an asterisk (*) before the 'x' characters.
    /// For example, "3*xx, 1*xxxx" means there are three double-sized ships and one quadruple-sized ship detected on the board.
    /// </summary>
    private string playerTwoRadard { get; set; }

    /// <summary>
    /// A dictionary property storing information about the buttons on the board. The keys are string identifiers and the values are Button objects.
    /// </summary>
    private Dictionary<string, Button> ButtonDictionary { get; set; }

    /// <summary>
    /// Method invoked when the back button is pressed.
    /// </summary>
    /// <returns>A boolean value indicating whether the back button press has been handled.</returns>
    /// <remarks>
    /// This method calls the ExitButton_Clicked method and returns true to indicate that the back button press has been handled.
    /// </remarks>
    protected override bool OnBackButtonPressed()
    {
        ExitButton_Clicked(this, null);
        return true;
    }

    public _5x5(bool bot)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        Settings = new Settings(0);
        Bot = bot;
        InitializeValue();
    }

    /// <summary>
    /// Initializes the values for the game.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions to initialize the game:
    /// - Sets up an event handler for changes in the main display information.
    /// - Initializes the ButtonDictionary and the game board.
    /// - Sets the initial game state values.
    /// - Creates new Player objects for PlayerOne and PlayerTwo.
    /// - Sets the initial counts for ships and attacks, and initializes the ShipID list.
    /// - Sets the initial text for various game elements using language-specific string values.
    /// - Initializes the game timer and sets the initial game time.
    /// - Creates a new timer that updates the layout and font responsiveness when it ticks, and starts this timer.
    /// </remarks>
    private void InitializeValue()
    {
        DeviceDisplay.MainDisplayInfoChanged += OnOrientationChanged;
        ButtonDictionary = new Dictionary<string, Button>();
        InitalizeBoard();

        playerOneRadard = playerTwoRadard = Settings.LangStringValue(18);

        StepGame = true;
        PlayerBool = true;
        SeeOwnFields = true;

        PlayerOne = new Player(true);
        PlayerTwo = new Player(true);

        ShipCount = 9;
        ShipID = new List<string>();

        AttackCount = 1;
        AttackID = "";

        AnnouncementText.Text = Settings.LangStringValue(9);
        YesExit.Text = Settings.LangStringValue(10);
        NoExit.Text = Settings.LangStringValue(11);
        RandomSelectButton.Text = Settings.LangStringValue(12);
        ConfrimSelectButton.Text = Settings.LangStringValue(13);
        SeeMyBoard.Text = Settings.LangStringValue(14);
        Alert.Text = Settings.LangStringValue(15) + " 9";

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

    /// <summary>
    /// Initializes the game timer.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions to initialize the game timer:
    /// - Creates a new timer using the Dispatcher.CreateTimer method.
    /// - Sets the interval of the timer to one second.
    /// - Sets up an event handler for the Tick event of the timer. This event handler calls the TimerSetTime method.
    /// - Starts the timer.
    /// </remarks>
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

    /// <summary>
    /// Initializes the game board.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions to initialize the game board:
    /// - Defines the rows and columns for the board.
    /// - Iterates over the rows and columns to create a button for each cell on the board. Each button is configured with specific properties and added to the ButtonDictionary.
    /// - Sets up an event handler for the Clicked event of each button.
    /// - Adds each button to the GridBoard and sets its row and column position.
    /// - Creates a new Board object with the ButtonDictionary.
    /// </remarks>
    private void InitalizeBoard()
    {

        string[] rows = { "A", "B", "C", "D", "E" };
        string[] cols = { "1", "2", "3", "4", "5" };

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
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

                ButtonDictionary.Add(button.StyleId, button);

                button.Clicked += Selected_Field;

                GridBoard.Children.Add(button);

                Grid.SetRow(button, i + 1);
                Grid.SetColumn(button, j + 1);
            }
        }
        Board = new Board(true, ButtonDictionary);
    }

    /// <summary>
    /// Manages the game timer and end of time events.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - If the game is not stopped and there is remaining time, it decrements the time and updates the timer display.
    /// - If the game is not stopped and there is no remaining time, it resets the time and performs actions based on the current game phase and player turn.
    /// - In the ship selection phase, it sets up the board for each player. If the player is a bot, it selects the board automatically. Otherwise, it allows the player to select the board.
    /// - In the attack phase, it allows each player to attack the other player's board. If the player is a bot, it performs the attack automatically. Otherwise, it allows the player to perform the attack.
    /// </remarks>
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
                    for (int i = 0; i < 9; i++)
                        PlayerOne.SetRandomOwnFields();
                    playerTwoRadard += PlayerTwo.MarineRadar(PlayerOne);
                    if (Bot == true)
                    {
                        SelectBoardBot();
                    }
                    else
                    {
                        PlayerBool = false;
                        ShipCount = 9;
                        SelectBoardPlayer(PlayerTwo, PlayerOne, "2");
                    }
                }
                else
                {
                    ShipCount = 0;
                    PlayerBool = true;
                    StepGame = false;
                    for (int i = 0; i < 9; i++)
                        PlayerTwo.SetRandomOwnFields();
                    SelectBoardPlayer(PlayerOne, PlayerTwo, "1");
                    playerOneRadard += PlayerOne.MarineRadar(PlayerTwo);
                    Alert.Text = playerOneRadard;
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
                        Alert.Text = playerTwoRadard;
                    }
                }
                else
                {
                    PlayerOne.AttackRandomField(PlayerTwo);
                    PlayerAttack(PlayerOne, PlayerTwo, "1");
                    PlayerBool = true;
                    Alert.Text = playerOneRadard;
                }
            }
        }
    }

    /// <summary>
    /// Handles the player's field selection.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    /// <remarks>
    /// This method performs different actions based on the current game phase:
    /// - If it's the ship selection phase (StepGame is true), it calls the SetShipID method with the selected button.
    /// - If it's the attack phase (StepGame is false), it calls the SetAttackID method with the selected button.
    /// </remarks>
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

    /// <summary>
    /// Handles the click event of the confirm selection button.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    /// <remarks>
    /// This method performs different actions based on the current game state:
    /// - If all ships have been placed (ShipCount is 0), it resets the game time and performs actions based on the current game phase and player turn.
    /// - In the ship selection phase (StepGame is true), it confirms the ship placement for each player. If the player is a bot, it selects the board automatically. Otherwise, it allows the player to select the board.
    /// - In the attack phase (StepGame is false), it allows each player to confirm their attack on the other player's board. If the player is a bot, it performs the attack automatically. Otherwise, it allows the player to perform the attack.
    /// </remarks>
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
                    Board.ConfrimSelect(PlayerOne, ShipID);
                    playerTwoRadard += PlayerTwo.MarineRadar(PlayerOne);
                    if (Bot == true)
                    {
                        SelectBoardBot();
                    }
                    else
                    {
                        PlayerBool = false;
                        ShipCount = 9;
                        SelectBoardPlayer(PlayerTwo, PlayerOne, "2");
                    }
                }
                else
                {
                    Board.ConfrimSelect(PlayerTwo, ShipID);
                    PlayerBool = true;
                    StepGame = false;
                    SelectBoardPlayer(PlayerOne, PlayerTwo, "1");
                    playerOneRadard += PlayerOne.MarineRadar(PlayerTwo);
                    Alert.Text = playerOneRadard;
                }
            }
            else if (AttackCount == 0)
            {
                if (PlayerBool == true)
                {
                    Board.ConfrimAttack(PlayerTwo, PlayerOne, AttackID);
                    if (Bot == true)
                    {
                        BotAttack();
                    }
                    else
                    {
                        PlayerAttack(PlayerTwo, PlayerOne, "2");
                        PlayerBool = false;
                        Alert.Text = playerTwoRadard;
                    }
                }
                else
                {
                    Board.ConfrimAttack(PlayerOne, PlayerTwo, AttackID);
                    PlayerAttack(PlayerOne, PlayerTwo, "1");
                    PlayerBool = true;
                    Alert.Text = playerOneRadard;
                }
            }
        }
    }

    /// <summary>
    /// Handles the bot's board selection.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions to handle the bot's board selection:
    /// - Displays the gaming board.
    /// - Sets PlayerBool to true and StepGame to false.
    /// - Resets the ShipCount to 0.
    /// - Randomly sets the fields for PlayerTwo.
    /// - Updates the Alert text and calls the NextPlayerAlert method.
    /// </remarks>
    private void SelectBoardBot()
    {
        Board.SeeGamingBoard(PlayerOne, PlayerTwo);
        PlayerBool = true;
        StepGame = false;
        ShipCount = 0;
        for (int i = 0; i < 9; i++)
            PlayerTwo.SetRandomOwnFields();
        Alert.Text = Settings.LangStringValue(18) + PlayerOne.MarineRadar(PlayerTwo);
        NextPlayerAlert(Settings.LangStringValue(17) + "1");
    }

    /// <summary>
    /// Handles the bot's attack.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions to handle the bot's attack:
    /// - Displays the gaming board.
    /// - Resets the AttackID and sets the AttackCount to 1.
    /// - Sets PlayerBool to true.
    /// - Calls the AttackRandomField method for PlayerOne.
    /// - Calls the NextPlayerAlert method.
    /// </remarks>
    private void BotAttack()
    {
        Board.SeeGamingBoard(PlayerOne, PlayerTwo);
        AttackID = "";
        AttackCount = 1;
        PlayerBool = true;
        PlayerOne.AttackRandomField(PlayerTwo);
        NextPlayerAlert(Settings.LangStringValue(17) + "1");
    }

    /// <summary>
    /// Handles the player's board selection.
    /// </summary>
    /// <param name="player1">The first player.</param>
    /// <param name="player2">The second player.</param>
    /// <param name="playerID">The ID of the player.</param>
    /// <remarks>
    /// This method performs the following actions to handle the player's board selection:
    /// - Displays the gaming board.
    /// - Updates the Alert text and calls the NextPlayerAlert method.
    /// - Initializes the ShipID list.
    /// </remarks>
    private void SelectBoardPlayer(Player player1, Player player2, string playerID)
    {
        Board.SeeGamingBoard(player1, player2);
        Alert.Text = Settings.LangStringValue(15) + " 9";
        NextPlayerAlert(Settings.LangStringValue(17) + playerID);
        ShipID = new List<string>();
    }

    /// <summary>
    /// Handles the player's attack.
    /// </summary>
    /// <param name="player1">The first player.</param>
    /// <param name="player2">The second player.</param>
    /// <param name="playerID">The ID of the player.</param>
    /// <remarks>
    /// This method performs the following actions to handle the player's attack:
    /// - Resets the AttackID and sets the AttackCount to 1.
    /// - Displays the gaming board.
    /// - Calls the NextPlayerAlert method.
    /// </remarks>
    private void PlayerAttack(Player player1, Player player2, string playerID)
    {
        AttackID = "";
        AttackCount = 1;
        Board.SeeGamingBoard(player1, player2);
        NextPlayerAlert(Settings.LangStringValue(17) + playerID);
    }

    /// <summary>
    /// Sets the ship identifier.
    /// </summary>
    /// <param name="button">The button that was clicked.</param>
    /// <remarks>
    /// This method performs the following actions:
    /// - If the button's background color is gray and there are ships left to place, it decreases the ShipCount, adds the button's text to the ShipID list, changes the button's background color to yellow, and updates the Alert text.
    /// - If the button's background color is not gray, it increases the ShipCount, removes the button's text from the ShipID list, changes the button's background color to gray, and updates the Alert text.
    /// </remarks>
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

    /// <summary>
    /// Sets the attack identifier.
    /// </summary>
    /// <param name="button">Button that was clicked.</param>.
    /// <remarks>
    /// This method does the following:
    /// - If the background colour of the button is grey and no attack file is selected, resets the background colour of the previously attacked button, changes the background colour of the button to orange and sets the AttackID to the button text.
    /// - If the background colour of the button is orange, increases AttackCount, resets AttackID and changes the background colour of the button to grey.
    /// - If the button background colour is grey and no attack file is selected, decreases AttackCount, sets AttackID to the button text and changes the button background colour to orange.
    /// </remarks>
    private void SetAttackID(Button button)
    {
        if (button.BackgroundColor == Colors.Gray && AttackCount == 0)
        {
            Button atackButton = ButtonDictionary[AttackID];
            atackButton.BackgroundColor = Colors.Gray;
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

    /// <summary>
    /// Triggers an alert for the next player's turn.
    /// </summary>
    /// <param name="player">The identifier of the next player.</param>
    /// <remarks>
    /// This method performs the following actions:
    /// - Creates a new timer that triggers an alert when it ticks. The alert includes calling the ExitButton_Clicked method, hiding the YesExit button, updating the AnnouncementText, setting the NoExit text to "OK", calling the EndGame method, and stopping the timer.
    /// </remarks>
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

    /// <summary>
    /// Method ending the game.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - If Player One has hit 9 times, it hides the "No" button, shows the "Yes" button, and displays the appropriate message.
    /// - If Player Two has hit 9 times, it hides the "No" button, shows the "Yes" button, and displays the appropriate message.
    /// </remarks>
    private void EndGame()
    {
        if (PlayerOne.HitAttacksID.Count == 9)
        {
            NoExit.IsVisible = false;
            YesExit.IsVisible = true;
            AnnouncementText.Text = Settings.LangStringValue(19) + "1";
            YesExit.Text = "OK";
        }
        else if (PlayerTwo.HitAttacksID.Count == 9)
        {
            NoExit.IsVisible = false;
            YesExit.IsVisible = true;
            AnnouncementText.Text = Settings.LangStringValue(19) + "2";
            YesExit.Text = "OK";
        }
    }

    /// <summary>
    /// Method handling the click of the exit button.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Hides the main user interface elements and shows a dialog box asking about exit.
    /// - Stops the game.
    /// </remarks>
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

    /// <summary>
    /// Method handling the click of the yes button at exit.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Closes the current modal window.
    /// </remarks>
    private void YesExit_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    /// <summary>
    /// Method handling the click of the no button at exit.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Restores the visibility of the main user interface elements and hides the dialog box.
    /// - Resumes the game.
    /// </remarks>
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

    /// <summary>
    /// Method handling the click of the random select button.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Resets the game time to 0 and displays "02:00" on the time text.
    /// - Sets the visibility of own fields to true.
    /// </remarks>
    private void RandomSelectButton_Clicked(object sender, EventArgs e)
    {
        Time = 0;
        TimeText.Text = "02:00";
        SeeOwnFields = true;
    }

    /// <summary>.
    /// The method that handles the button click see my array.
    /// </summary>.
    /// <remarks>.
    /// This method does the following:
    /// - Resets the attack ID and sets the number of attacks to 1.
    /// - If it is player one's turn and the game is not in the attack stage, allows player one to see his board.
    /// - If it is not player one's turn and the game is not in the attack stage, allows player two to see his own board.
    /// </remarks>.
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

    /// <summary>.
    /// A method that allows the player to see his own fields.
    /// </summary>.
    /// <remarks>.
    /// This method does the following:
    /// - If the visibility of own fields is true and the game is not attack phase, it sets the visibility of own fields to false, sets the number of attacks to -1 and allows the player to see his own fields.
    /// - If the game is not in the attack phase, sets the number of attacks to 1, sets the visibility of own fields to true and allows the player to see the board.
    /// </remarks>.
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

    /// <summary>
    /// Method adjusting the font size to the window size.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Determines the appropriate font size based on the width of the announcement box.
    /// - Sets the font size of the announcement text to the calculated size.
    /// </remarks>
    private void ResponsiveFont(object sender, EventArgs e)
    {
        int size = (int)AnnouncementBox.Width / 50;
        if (size < 17)
            size = 17;

        AnnouncementText.FontSize = size;
    }

    /// <summary>
    /// Method adjusting the font size of game controls to the window size.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Determines the appropriate font size based on the width of the scroll view.
    /// - Sets the font size of the alert and various game control buttons to the calculated size.
    /// </remarks>
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

    /// <summary>
    /// Method handling the change in size of the main layout.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Adjusts the orientation of the main layout and the margins of the board and game control grids based on the width of the main layout.
    /// - Adjusts the width requests of the board and game control grids, and the exit button based on the width of the main layout.
    /// </remarks>
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

    /// <summary>
    /// Method handling the change in screen orientation.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Calls the methods to adjust the main layout size and the font sizes when the screen orientation changes.
    /// </remarks>
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