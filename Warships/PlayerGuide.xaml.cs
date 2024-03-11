using static System.Net.Mime.MediaTypeNames;

namespace Warships;

public partial class PlayerGuide : ContentPage
{
    private Settings Settings { get; set; }
    public PlayerGuide()
	{
		InitializeComponent();
        MainGrid_SizeChanged(this, null);
        Settings = new Settings(0);
        InitializeLanguage();
    }

    private void InitializeLanguage()
    {
        Header.Text = Settings.LangStringValue(20);
        GrayFiled.Text = Settings.LangStringValue(21);
        YellowFiled.Text = Settings.LangStringValue(22);
        RedFiled.Text = Settings.LangStringValue(23);
        DarkRedField.Text = Settings.LangStringValue(24);
        OrangeField.Text = Settings.LangStringValue(25);
    }

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

    private void ReturnButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}