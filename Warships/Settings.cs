using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    /// <summary>
    /// The Settings class is responsible for managing the language settings of the game.
    /// It provides methods and properties to get and set language preferences, 
    /// and to retrieve language-specific string values.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// A two-dimensional string array holding language data.
        /// The first dimension represents different languages, and the second dimension represents different string values in each language.
        /// </summary>
        private string[,] Lang;

        /// <summary>
        /// An integer representing the ID of the currently selected language.
        /// The default value is -1, which indicates that no language has been selected yet.
        /// </summary>
        private static int LangID = -1;

        /// <summary>
        /// Constructor for the Settings class. Initializes the language settings.
        /// </summary>
        /// <param name="langID">The identifier for the language to be set.</param>
        /// <remarks>
        /// This constructor performs the following actions:
        /// - Retrieves the stored language ID from the preferences. If a valid language ID is found, it sets the LangID to this value.
        /// - If no valid language ID is found in the preferences and LangID is still -1, it sets LangID to the provided langID parameter.
        /// - Initializes the Lang two-dimensional string array with language-specific string values for Polish (pl), English (en), and Italian (it).
        /// </remarks>
        public Settings(int langID)
        {
            int preferencesLangID = Preferences.Get("LangID", defaultValue: -1);
            if(preferencesLangID > -1)
                LangID = preferencesLangID;
            else if (LangID == -1)
                LangID = langID;
            //pl
            Lang = new string[3, 28];
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
            Lang[0, 18] = "Radar morski: ";
            Lang[0, 19] = "Wygrywa gracz nr ";
            Lang[0, 20] = "Jak grać?";
            Lang[0, 21] = "\nPodstawowe pole, na którym nie dokonano żadnej akcji.";
            Lang[0, 22] = "Wybieranie planszy\n - wybrane lokalizacje statków. \nPodglądanie planszy\n - pola należące do gracza zawierające statki.";
            Lang[0, 23] = "Podglądanie planszy\n - pola które zostały zaatakowane przez przeciwnika. \nPlansza ataku wroga\n - pola zaatakowane na których był statek wroga.";
            Lang[0, 24] = "Plansza ataku wroga\n - pola zaatakowane, na których nie było statku wroga.";
            Lang[0, 25] = "\n wybrane pole do zaatakowania.";
            Lang[0, 26] = "Włoski";
            Lang[0, 27] = "\n Radar morski - Każdy statek jest reprezentowany przez serię znaków \"x\", gdzie liczba znaków \"x\" odpowiada rozmiarowi statku. Ilość statków każdego rozmiaru jest reprezentowana przez liczbę, po której następuje gwiazdka (*) przed znakami \"x\". Na przykład \"3*xx, 1*xxxx\" oznacza, że na planszy wroga wykryto trzy statki podwójnego rozmiaru i jeden statek poczwórnego rozmiaru.";
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
            Lang[1, 18] = "Marine radar:";
            Lang[1, 19] = "Wins player no. ";
            Lang[1, 20] = "How play?";
            Lang[1, 21] = "\nA basic field where no action has been taken.";
            Lang[1, 22] = "Selecting the board\n - selected locations of ships. \nViewing the board\n - fields owned by the player containing ships.";
            Lang[1, 23] = "Viewing the board\n - fields that were attacked by the enemy. \nEnemy attack board\n - fields attacked on which there was an enemy ship.";
            Lang[1, 24] = "Enemy attack board\n - attacked fields, where there was no enemy ship.";
            Lang[1, 25] = "\n selected field to be attacked.";
            Lang[1, 26] = "Italian";
            Lang[1, 27] = "\n Marine radar - Each ship is represented by a series of 'x' characters, where the number of 'x' characters corresponds to the size of the ship. The quantity of each size of ship is represented by a number followed by an asterisk (*) before the 'x' characters. For example, \"3*xx, 1*xxxx\" means there are three double-sized ships and one quadruple-sized ship detected on the enemy board.";
            //it
            Lang[2, 0] = "Gioca";
            Lang[2, 1] = "Le impostadioni";
            Lang[2, 2] = "Gioca con bot.";
            Lang[2, 3] = "Bioca con il giocatore";
            Lang[2, 4] = "Il cartellone 5x5";
            Lang[2, 5] = "Il cartellone 7x7";
            Lang[2, 6] = "Accetta";
            Lang[2, 7] = "Il polaco";
            Lang[2, 8] = "Il inglese";
            Lang[2, 9] = "Voi finire il gioco?";
            Lang[2, 10] = "Si";
            Lang[2, 11] = "No";
            Lang[2, 12] = "Scegli acaso";
            Lang[2, 13] = "Accetta la scelta";
            Lang[2, 14] = "Visualizza il tuo cartellone";
            Lang[2, 15] = "Scegli la posizione delle navi: ";
            Lang[2, 16] = "Visualizza il cartellone del gioco";
            Lang[2, 17] = "Il turno del giocatore ener. ";
            Lang[2, 18] = "Radar marino: ";
            Lang[2, 19] = "Vince il giocatore ener. ";
            Lang[2, 20] = "Come giocare?";
            Lang[2, 21] = "\nIl campo elementare su quale non e sata svolta nessuna azione.";
            Lang[2, 22] = "La scelta del cartellone\n - le localizzazioni delle navi scelte. \nGuarda il tuo cartello\n - I campi del giocatore che contengono le navi.";
            Lang[2, 23] = "Guarda la tua bacheca\n - I campi attaccati dal nemico. \nIl cartello dell'attacco di nemico\n - Le zone attaccate su quali c'era la nave del nemico.";
            Lang[2, 24] = "Il cartello dell'attacco di nemico\n - Le zone attaccate su quali non c'era la nave del nemico.";
            Lang[2, 25] = "\n La scelta del campo del'attacco.";
            Lang[2, 26] = "Il italiano";
            Lang[2, 27] = "\n Radar marino - Ogni nave è rappresentata da una serie di caratteri \"x\", dove il numero di caratteri \"x\" corrisponde alla dimensione della nave. La quantità di ogni dimensione di nave è rappresentata da un numero seguito da un asterisco (*) prima dei caratteri \"x\". Ad esempio, \"3*xx, 1*xxxx\" significa che sono state rilevate tre navi di dimensioni doppie e una nave di dimensioni quadruple sul tabellone nemico.";
        }

        /// <summary>
        /// Sets the language identifier.
        /// </summary>
        /// <param name="lang">The identifier of the language to be set.</param>
        /// <remarks>
        /// This method sets the LangID to the provided lang parameter and stores this value in the preferences for future use.
        /// </remarks>
        public void SetLangID(int lang)
        {
            LangID = lang;
            Preferences.Set("LangID", lang);
        }

        /// <summary>
        /// Returns a string for the specified value identifier in the currently selected language.
        /// </summary>
        /// <param name="ValueID">The identifier of the value for which the string is to be returned.</param>
        /// <returns>The string corresponding to the specified value identifier in the currently selected language.</returns>
        /// <remarks>
        /// This method retrieves the string from the Lang two-dimensional array using the current LangID and the provided ValueID.
        /// </remarks>
        public string LangStringValue(int ValueID)
        {
            return Lang[LangID, ValueID];
        }
    }

}