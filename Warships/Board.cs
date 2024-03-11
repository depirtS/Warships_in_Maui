using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    public class Board
    {
        protected int SizeBoard;
        private Dictionary<string, Button> ButtonDictionary;

        public Board(bool boardSize,Dictionary<string, Button> buttonDictionary)
        {
            this.ButtonDictionary = buttonDictionary;
            if (boardSize)
                this.SizeBoard = 5;
            else
                this.SizeBoard = 7;
        }

        public List<Button> FindButton()
        {
            string id = "";
            List<Button> buttons = new List<Button>();

            for (int i = 0; i < SizeBoard; i++)
            {
                for (int j = 0; j < SizeBoard; j++)
                {
                    switch (i)
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

                    buttons.Add(ButtonDictionary[(id + (j + 1)).ToString()]);
                }
            }

            return buttons;
        }

        public void SeeGamingBoard(Player player1, Player player2)
        {
            List<Button> buttonList = FindButton();
            foreach (Button button in buttonList)
                if (button != null)
                    player1.SeeGamingFields(button, player2);
        }

        public void ConfrimSelect(Player player, List<string> shipID)
        {
            player.SetOwnFields(shipID.ToArray());
        }

        public void ConfrimAttack(Player player1, Player player2, string attackID)
        {
            player1.AttackField(attackID, player2);
        }

    }
}
