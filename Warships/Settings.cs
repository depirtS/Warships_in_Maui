using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    public class Settings
    {
        private string[,] Lang;
        private static int LangID = -1;
        public Settings(int langID)
        {
            int preferencesLangID = Preferences.Get("LangID", defaultValue: -1);
            if(preferencesLangID > -1)
                LangID = preferencesLangID;
            else if (LangID == -1)
                LangID = langID;
            //pl
            Lang = new string[2, 20];
            Lang[0, 0] = "Graj";
            Lang[0, 1] = "Ustawienia";
            Lang[0, 2] = "Graj z botem";
            Lang[0, 3] = "Graj z graczem";
            Lang[0, 4] = "Plansza 5x5";
            Lang[0, 5] = "Plansza 7x7";
            Lang[0, 6] = "Zatwierdź";
            Lang[0, 7] = "Polski";
            Lang[0, 8] = "Angielski";
            Lang[0, 9] = "Czy chcesz opuścić grę?";
            Lang[0, 10] = "Tak";
            Lang[0, 11] = "Nie";
            Lang[0, 12] = "Wybierz losowo";
            Lang[0, 13] = "Zatwierdź wybór";
            Lang[0, 14] = "Wyświetl swoją planszę";
            Lang[0, 15] = "Wybierz rozmieszczenie statków: ";
            Lang[0, 16] = "Wyświetl plansze do gry";
            Lang[0, 17] = "Tura gracza nr ";
            Lang[0, 18] = "Wybierz pole do zaatakowania";
            Lang[0, 19] = "Wygrywa gracz nr ";
            //en
            Lang[1, 0] = "Play";
            Lang[1, 1] = "Settings";
            Lang[1, 2] = "Play with bot";
            Lang[1, 3] = "Play with player";
            Lang[1, 4] = "Board 5x5";
            Lang[1, 5] = "Board 7x7";
            Lang[1, 6] = "Apply changes";
            Lang[1, 7] = "Polish";
            Lang[1, 8] = "English";
            Lang[1, 9] = "Do you want to leave the game?";
            Lang[1, 10] = "Yes";
            Lang[1, 11] = "No";
            Lang[1, 12] = "Random select";
            Lang[1, 13] = "Confirm select";
            Lang[1, 14] = "See your board";
            Lang[1, 15] = "Select ship placement: ";
            Lang[1, 16] = "View your game board";
            Lang[1, 17] = "Player no.'s turn ";
            Lang[1, 18] = "Select field to attack";
            Lang[1, 19] = "Wins player no. ";
        }

        public void SetLangID(int lang)
        {
            LangID = lang;
            Preferences.Set("LangID", lang);
        }

        public string selectedLang()
        {
            switch (LangID)
            {
                case 0: return "pl";
                case 1: return "en"; 
                default: throw new ArgumentException("Invalid Language value");
            }
        }

        public string LangStringValue(int ValueID)
        {
            return Lang[LangID, ValueID];
        }
    }

}