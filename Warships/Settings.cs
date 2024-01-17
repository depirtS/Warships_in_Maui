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
        private int LangID;
        public Settings(int langID)
        {
            //pl
            LangID = langID;
            Lang = new string[2, 9];
            Lang[0, 0] = "Graj";
            Lang[0, 1] = "Ustawienia";
            Lang[0, 2] = "Graj z botem";
            Lang[0, 3] = "Graj z graczem";
            Lang[0, 4] = "Plansza 5x5";
            Lang[0, 5] = "Plansza 7x7";
            Lang[0, 6] = "Zatwierdź";
            Lang[0, 7] = "Polski";
            Lang[0, 8] = "Angielski";
            //en
            Lang[1, 0] = "Play";
            Lang[1, 1] = "Settings";
            Lang[1, 2] = "Play with bot";
            Lang[1, 3] = "Play with player";
            Lang[1, 4] = "Board 5x5";
            Lang[1, 5] = "Board 7x7";
            Lang[1, 6] = "Aplly changes";
            Lang[1, 7] = "Polish";
            Lang[1, 8] = "English";
        }

        public void SetLangID(int lang)
        {
            LangID = lang;
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
