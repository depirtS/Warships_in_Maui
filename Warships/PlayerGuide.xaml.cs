using static System.Net.Mime.MediaTypeNames;

namespace Warships;

/// <summary>
/// Represents a guide for the player in the game.
/// </summary>
public partial class PlayerGuide : ContentPage
{
    /// <summary>
    /// Represents the game settings.
    /// </summary>
    private Settings Settings { get; set; }

    /// <summary>
    /// Initializes a new instance of the PlayerGuide class.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Calls the method to adjust the main grid size.
    /// - Initializes the game settings.
    /// - Calls the method to initialize the language.
    /// </remarks>
    public PlayerGuide()
    {
        InitializeComponent();
        MainGrid_SizeChanged(this, null);
        Settings = new Settings(0);
        InitializeLanguage();
    }

    /// <summary>
    /// Initializes the language for the game.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Sets the text of various game elements based on the language string values in the game settings.
    /// </remarks>
    private void InitializeLanguage()
    {
        Header.Text = Settings.LangStringValue(20);
        GrayFiled.Text = Settings.LangStringValue(21);
        YellowFiled.Text = Settings.LangStringValue(22);
        RedFiled.Text = Settings.LangStringValue(23);
        DarkRedField.Text = Settings.LangStringValue(24);
        OrangeField.Text = Settings.LangStringValue(25);
    }

    /// <summary>
    /// Handles the change in size of the main grid.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Adjusts the size requests and font sizes of various game elements based on the width and height of the main grid.
    /// </remarks>
    private void MainGrid_SizeChanged(object sender, EventArgs e)
    {
        int width = (int)MainGrid.Width;
        int height = (int)MainGrid.Height;
        if (width < height)
        {
            ReturnButton.HeightRequest = ReturnButton.WidthRequest = width / 5;
            GrayFiled.FontSize = YellowFiled.FontSize = RedFiled.FontSize = DarkRedField.FontSize = OrangeField.FontSize = width / 30;
            Header.FontSize = width / 20;
        }
        else
        {
            ReturnButton.HeightRequest = ReturnButton.WidthRequest = height / 5;
            GrayFiled.FontSize = YellowFiled.FontSize = RedFiled.FontSize = DarkRedField.FontSize = OrangeField.FontSize = width / 30;
            Header.FontSize = width / 20;

        }
    }

    /// <summary>
    /// Handles the click of the return button.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// - Navigates back to the previous page.
    /// </remarks>
    private void ReturnButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}