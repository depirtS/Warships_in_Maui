﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warships
{
    public class Player
    {
        public int[,] OwnFields { get; set; }
        public List<string> HitAttacksID { get; set; }
        private bool Board;
        Random random;
        //true - 5x5 false - 7x7
        //0 - field not selected
        //1 - field selected
        //2 - your attack to enemy field
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
        private int BoardSize()
        {
            if (Board)
                return 5;
            else
                return 7;
        }

        public Player(bool board)
        {
            if (board)
            {
                OwnFields = new int[5, 5];
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        OwnFields[i, j] = 0;
            }
            else
            {
                OwnFields = new int[7, 7];
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 7; j++)
                        OwnFields[i, j] = 0;
            }

            Board = board;
            HitAttacksID = new List<string>();
            random = new Random();
        }

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

        public void SetRandomOwnFields()
        {
            int x = BoardSize();
            int FirstID, SecondID;
            do
            {
                FirstID = random.Next(0, x);
                SecondID = random.Next(0, x);
            } while (OwnFields[FirstID, SecondID] == 1);

            OwnFields[FirstID, SecondID] = 1;
        }

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

        public void AttackField(string ID, Player player)
        {
            int FirstID = ReturnFirstIDField(ID);
            int SecondID = int.Parse(ID.Substring(1, 1)) - 1;
            if (OwnFields[FirstID, SecondID] == 1)
                player.HitAttacksID.Add(ID);
            this.OwnFields[FirstID, SecondID] = 2;
        }

        public void AttackRandomField(Player player)
        {
            int x = BoardSize();
            int FirstID, SecondID;

            do
            {
                FirstID = random.Next(0, x);
                SecondID = random.Next(0, x);
            } while (this.OwnFields[FirstID, SecondID] == 2);

            if(this.OwnFields[FirstID, SecondID] == 1)
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