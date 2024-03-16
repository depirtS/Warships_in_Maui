using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    /// <summary>
    /// Represents a player in the game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Represents the player's own fields in the game.
        /// </summary>
        public int[,] OwnFields { get; set; }

        /// <summary>
        /// List of IDs for the player's hit attacks.
        /// </summary>
        public List<string> HitAttacksID { get; set; }

        /// <summary>
        /// Represents the size of the game board.
        /// </summary>
        private int Board;

        /// <summary>
        /// Used for generating random numbers.
        /// </summary>
        private Random random;

        /// <summary>
        /// Returns the first ID field based on the provided ID.
        /// </summary>
        /// <param name="ID">The ID to process.</param>
        /// <returns>The first ID field.</returns>
        private int ReturnFirstIDField(string ID)
        {
            ID = ID.Substring(0, 1);
            switch (ID)
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                default: return -1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="board">If true, initializes a 5x5 board. If false, initializes a 7x7 board.</param>
        public Player(bool board)
        {
            if (board)
            {
                OwnFields = new int[5, 5];
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        OwnFields[i, j] = 0;
                Board = 5;

            }
            else
            {
                OwnFields = new int[7, 7];
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        OwnFields[i, j] = 0;
                Board = 7;
            }

            HitAttacksID = new List<string>();
            random = new Random();
        }

        /// <summary>
        /// Sets the player's own fields based on the provided field IDs.
        /// </summary>
        /// <param name="SelectedField">The field IDs to set.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - For each provided field ID, it determines the corresponding indices in the OwnFields array and sets the value at those indices to 1.
        /// </remarks>
        public void SetOwnFields(params string[] SelectedField)
        {
            int FirstID;
            int SecondID;
            foreach (var ID in SelectedField)
            {
                FirstID = ReturnFirstIDField(ID);
                SecondID = int.Parse(ID.Substring(1, 1)) - 1;
                OwnFields[FirstID, SecondID] = 1;
            }
        }

        /// <summary>
        /// Sets a random field in the player's own fields.
        /// </summary>
        /// <remarks>
        /// This method performs the following actions:
        /// - Generates random indices until it finds a field in the OwnFields array that has not been set (value is not 1), and sets the value at those indices to 1.
        /// </remarks>
        public void SetRandomOwnFields()
        {
            int FirstID, SecondID;
            do
            {
                FirstID = random.Next(0, Board);
                SecondID = random.Next(0, Board);
            } while (OwnFields[FirstID, SecondID] == 1);

            OwnFields[FirstID, SecondID] = 1;
        }

        /// <summary>
        /// Allows the player to see their own fields.
        /// </summary>
        /// <param name="field">The button representing the field to check.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Determines the corresponding indices in the OwnFields array based on the text of the provided button.
        /// - Changes the background color of the button based on the value at those indices in the OwnFields array.
        /// </remarks>
        public void SeeOwnFields(Button field)
        {
            string ID = field.Text;
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1)) - 1;

            if (OwnFields[FirstID, SecondID] == 2)
                field.BackgroundColor = Colors.Red;
            else if (OwnFields[FirstID, SecondID] == 1)
                field.BackgroundColor = Colors.Yellow;
            else if (OwnFields[FirstID, SecondID] == 0)
                field.BackgroundColor = Colors.Gray;
        }

        /// <summary>
        /// Allows the player to see the gaming fields.
        /// </summary>
        /// <param name="field">The button representing the field to check.</param>
        /// <param name="player">The player whose fields to check.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Determines the corresponding indices in the OwnFields array based on the text of the provided button.
        /// - Changes the background color of the button based on the value at those indices in the player's OwnFields array.
        /// - If the player has hit the field, changes the background color of the button to red.
        /// </remarks>
        public void SeeGamingFields(Button field, Player player)
        {
            string ID = field.Text;
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1)) - 1;

            if (player.OwnFields[FirstID, SecondID] == 2)
                field.BackgroundColor = Colors.DarkRed;
            else
                field.BackgroundColor = Colors.Gray;
            for (int i = 0; i < 15; i++)
            {
                if (HitAttacksID.Count > i)
                    if (HitAttacksID[i] == ID)
                        field.BackgroundColor = Colors.Red;
            }
        }

        /// <summary>
        /// Attacks a field on the player's board.
        /// </summary>
        /// <param name="ID">The ID of the field to attack.</param>
        /// <param name="player">The player who is being attacked.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Determines the corresponding indices in the OwnFields array based on the provided ID.
        /// - If the field has been selected by the player, adds the ID to the player's HitAttacksID list.
        /// - Sets the value at those indices in the OwnFields array to 2, indicating that the field has been attacked.
        /// </remarks>
        public void AttackField(string ID, Player player)
        {
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1)) - 1;
            if (OwnFields[FirstID, SecondID] == 1)
                player.HitAttacksID.Add(ID);
            this.OwnFields[FirstID, SecondID] = 2;
        }

        /// <summary>
        /// Attacks a random field on the player's board.
        /// </summary>
        /// <param name="player">The player who is being attacked.</param>
        /// <remarks>
        /// This method performs the following actions:
        /// - Generates random indices until it finds a field in the OwnFields array that has not been attacked (value is not 2), and sets the value at those indices to 2.
        /// - If the field has been selected by the player, adds the ID to the player's HitAttacksID list.
        /// </remarks>
        public void AttackRandomField(Player player)
        {
            int x = Board;
            int FirstID, SecondID;

            do
            {
                FirstID = random.Next(0, x);
                SecondID = random.Next(0, x);
            } while (this.OwnFields[FirstID, SecondID] == 2);

            if (this.OwnFields[FirstID, SecondID] == 1)
            {
                string id = "";
                switch (FirstID)
                {
                    case 0:
                        id = "A";
                        break;
                    case 1:
                        id = "B";
                        break;
                    case 2:
                        id = "C";
                        break;
                    case 3:
                        id = "D";
                        break;
                    case 4:
                        id = "E";
                        break;
                    case 5:
                        id = "F";
                        break;
                    case 6:
                        id = "G";
                        break;
                }
                player.HitAttacksID.Add(id + (SecondID + 1).ToString());
            }
            this.OwnFields[FirstID, SecondID] = 2;

        }

    }
}