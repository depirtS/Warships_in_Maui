using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    public class Player
    {
        private int[,] OwnFields;
        private bool Board; 
        //true - 5x5 false - 7x7
        //0 - field not selected
        //1 - field selected
        //2 - your attack to enemy field
        private int ReturnFirstIDField(string ID)
        {
            ID = ID.Substring(0,1);
            switch(ID)
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
        private int BoardSize()
        {
            if (Board)
                return 5;
            else
                return 7;
        }
        private int[] RandomID()
        {
            int x = BoardSize();
            Random random = new Random();
            int FirstID = random.Next(0, x);
            int SecondID = random.Next(0, x);

            return new[] { FirstID, SecondID };
        }

        public Player(bool board)
        {
            if(board)
                OwnFields = new int[5, 5];
            else
                OwnFields = new int[7, 7];

            Board = board;
        }

        public void SetOwnFields(params string[] SelectedField)
        {
            int FirstID;
            int SecondID;
            foreach (var ID in SelectedField)
            {
                FirstID = ReturnFirstIDField(ID);
                SecondID = int.Parse(ID.Substring(1, 1));
                OwnFields[FirstID, SecondID] = 1;
            }
        }

        public void SetRandomOwnFields()
        {
            int[] ID = RandomID();
            int FirstID = ID[0];
            int SecondID = ID[1];

            foreach (int test in OwnFields)
            {
                if (test == 1)
                {
                    SetRandomOwnFields();
                    break;
                }
                else
                {
                    string id = "";
                    switch (FirstID)
                    {
                        case 0: id = "A";
                            break;
                        case 1: id = "B";
                            break;
                        case 2: id = "C";
                            break;
                        case 3: id = "D";
                            break;
                        case 4: id = "E";
                            break;
                        case 5: id = "F";
                            break;
                        case 6: id = "G";
                            break;
                        default: id = "N";
                            break;
                    }
                    SetOwnFields(id += SecondID.ToString());
                }
            }
        }

            public void SeeOwnFields(Button field, Player secondPlayer)
        {
            string ID = field.Text;
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1));

            if (secondPlayer.OwnFields[FirstID, SecondID] == 2)
                field.BackgroundColor = Colors.Red;
            else if (OwnFields[FirstID, SecondID] == 1)
                field.BackgroundColor = Colors.Yellow;
            else if (OwnFields[FirstID, SecondID] == 0)
                field.BackgroundColor = Colors.Gray;
        }

        public void SeeGamingFields(Button field)
        {
            string ID = field.Text;
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1));

            if (OwnFields[FirstID, SecondID] == 2)
                field.BackgroundColor = Colors.DarkRed;
            else
                field.BackgroundColor = Colors.Gray;
        }

        public void AttackField(string ID)
        {
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1));
            OwnFields[FirstID, SecondID] = 2;
        }
        
        public void AttackRandomField()
        {
            int[] ID = RandomID();
            int FirstID = ID[0];
            int SecondID = ID[1];

            foreach(int test in OwnFields)
            {
                if(test == 2)
                {
                    AttackRandomField();
                    break;
                }
                else
                {
                    string id = "";
                    switch (FirstID)
                    {
                        case 0: id = "A";
                            break;
                        case 1: id = "B";
                            break;
                        case 2: id = "C";
                            break;
                        case 3: id = "D";
                            break; 
                        case 4: id = "E";
                            break;
                        case 5: id = "F";
                            break;
                        case 6: id = "G";
                            break;
                        default: id = "N";
                            break;
                    }
                    AttackField(id += SecondID.ToString());
                }
            }
        }
    }
}
