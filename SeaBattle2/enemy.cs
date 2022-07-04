using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle2
{
    public class Bot
    {
        public int[,] myMap = new int[Form1.mapSize, Form1.mapSize];//карта врага
        public int[,] enemyMap = new int[Form1.mapSize, Form1.mapSize];//моя карта

        public Button[,] myButtons = new Button[Form1.mapSize, Form1.mapSize];
        public Button[,] enemyButtons = new Button[Form1.mapSize, Form1.mapSize];

        public Bot(int[,] myMap, int[,] enemyMap, Button[,] myButtons, Button[,] enemyButtons)
        {
            this.myMap = myMap;
            this.enemyMap = enemyMap;
            this.enemyButtons = enemyButtons;
            this.myButtons = myButtons;
        }

        public bool IsInsideMap(int i, int j)
        {
            if (i < 0 || j < 0 || i >= Form1.mapSize || j >= Form1.mapSize)
            {
                return false;
            }
            return true;
        }

        public bool IsEmpty(int i, int j, int length)
        {
            bool isEmpty = true;

            for (int k = j; k < j + length; k++)
            {
                if (myMap[i, k] != 0)
                {
                    isEmpty = false;
                    break;
                }
            }

            return isEmpty;
        }
        public int[,] ConfigureShips()
        {
            
            int location;
            int x;
            int y;
            Random r = new Random();
            bool success = false;
            while (!success)
            {
                location = r.Next(0,2);
                x = r.Next(1, 7);
                y = r.Next(1, 7);
                if (location == 0)
                {
                    myMap[x, y] = 1;
                    myMap[x + 1, y] = 1;
                    myMap[x + 2, y] = 1;
                    myMap[x + 3, y] = 1;
                }
                else
                {
                    myMap[x, y] = 1;
                    myMap[x, y + 1] = 1;
                    myMap[x, y + 2] = 1;
                    myMap[x, y + 3] = 1;
                }
                success = true;
            }

            for (int i = 0; i < 2; i++)
            {
                success = false;
                while (!success)
                {
                    location = r.Next(0, 2);
                    x = r.Next(1, 8);
                    y = r.Next(1, 8);
                    if (location == 0)
                    {
                        if (myMap[x - 1, y - 1] != 1 && myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x + 2, y - 1] !=1 && myMap[x + 3, y - 1] != 1 
                            && myMap[x + 3, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x, y + 1] != 1 && myMap[x + 1, y + 1] != 1 && myMap[x + 2, y + 1] != 1 && myMap[x + 3, y + 1] != 1
                            && myMap[x-1, y] != 1 && myMap[x, y] != 1 && myMap[x+1, y] != 1 && myMap[x+2, y] != 1)
                        {
                            myMap[x, y] = 1;
                            myMap[x + 1, y] = 1;
                            myMap[x + 2, y] = 1;
                            success = true;
                        }
                    }
                    else
                    {
                        if (myMap[x, y-1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x+1, y] != 1 && myMap[x+1, y+1] !=1 && myMap[x+1, y+2] != 1 && myMap[x+1, y+3] !=1
                            && myMap[x, y+3] != 1 && myMap[x - 1, y - 1] != 1 && myMap[x - 1, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x - 1, y + 2] != 1 && myMap[x - 1, y + 3] != 1
                            && myMap[x, y] !=1 && myMap[x, y+1] != 1 && myMap[x, y+2] !=1)
                        {
                            myMap[x, y] = 1;
                            myMap[x, y + 1] = 1;
                            myMap[x, y + 2] = 1;
                            success = true;
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                success = false;
                while (!success)
                {
                    location = r.Next(0, 2);
                    x = r.Next(1, 9);
                    y = r.Next(1, 9);
                    if (location == 0)
                    {
                        if (myMap[x - 1, y - 1] != 1 && myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x + 2, y - 1] != 1
                            && myMap[x + 2, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x, y + 1] != 1 && myMap[x + 1, y + 1] != 1 && myMap[x + 2, y + 1] != 1
                            && myMap[x - 1, y] != 1 && myMap[x, y] != 1 && myMap[x + 1, y] != 1)
                        {
                            myMap[x, y] = 1;
                            myMap[x + 1, y] = 1;
                            success = true;
                        }
                    }
                    else
                    {
                        if (myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x + 1, y] != 1 && myMap[x + 1, y + 1] != 1 && myMap[x + 1, y + 2] != 1
                            && myMap[x, y + 2] != 1 && myMap[x - 1, y - 1] != 1 && myMap[x - 1, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x - 1, y + 2] != 1
                            && myMap[x, y] != 1 && myMap[x, y + 1] != 1)
                        {
                            myMap[x, y] = 1;
                            myMap[x, y + 1] = 1;
                            success = true;
                        }
                    }
                }
            }
            for(int i = 0; i < 4;i++)
            {
                success = false;
                while (!success)
                {
                    x = r.Next(1, 10);
                    y = r.Next(1, 10);
                    if (myMap[x - 1, y - 1] != 1 && myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1
                            && myMap[x - 1, y + 1] != 1 && myMap[x, y + 1] != 1 && myMap[x + 1, y + 1] != 1
                            && myMap[x - 1, y] != 1 && myMap[x, y] != 1 && myMap[x + 1, y] != 1)
                    {
                        myMap[x, y] = 1;
                        success = true;
                    }
                }
            }
            
            return myMap;
        }
        public bool Shoot()
        {
            bool hit = false;
            Random r = new Random();

            int posX = r.Next(1, Form1.mapSize);
            int posY = r.Next(1, Form1.mapSize);

            while (enemyButtons[posX, posY].BackColor == Color.Red || enemyButtons[posX, posY].BackColor == Color.Black)
            {
                posX = r.Next(1, Form1.mapSize);
                posY = r.Next(1, Form1.mapSize);
            }

            if (enemyMap[posX, posY] != 0)
            {
                hit = true;
                enemyMap[posX, posY] = 0;
                enemyButtons[posX, posY].BackColor = Color.Red;
                enemyButtons[posX, posY].Text = "X";
            }
            else
            {
                hit = false;
                enemyButtons[posX, posY].BackColor = Color.Gray;
                enemyButtons[posX, posY].Text = ".";
            }
            if (hit)
                Shoot();
            return hit;
        }
    }
}
