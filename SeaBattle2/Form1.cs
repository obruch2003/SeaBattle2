using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle2
{
    public partial class Form1 : Form
    {
        public const int mapSize = 11;
        public int cellSize = 30;
        public string alphabet = "АБВГДЕЖЗИК";

        public bool isPlaying = false;
        /*начался ли игровой процесс или нет*/

        public int[,] myMap = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];
       

        public Bot bot;

        public Form1()
        {
            InitializeComponent();
            this.Text = "SeaBattle";
            Init();
        }
        public void Init()
        {
            isPlaying = false;
            CreateMaps();
            bot = new Bot(enemyMap, myMap, enemyButtons, myButtons);
            enemyMap = bot.ConfigureShips();
        }
        public void CreateMaps()
        {
            this.Width = mapSize * 2 * cellSize + 55;
            this.Height = (mapSize + 3) * cellSize + 50;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    /*else
                    {
                        //button.Click += new EventHandler(ConfigureShips);
                    }*/
                    myButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(350 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    else
                    {
                        button.Click += new EventHandler(PlayerShoot);
                    }
                    enemyButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Карта игрока";
            map1.Location = new Point(-30 + mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.Text = "Карта противника";
            map2.Location = new Point(320 + mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map2);

            Button startButton = new Button();
            startButton.BackColor = Color.Red;
            startButton.Size = new Size(100, 40);
            startButton.Text = "В бой";
            startButton.Click += new EventHandler(ConfigureShips);
            startButton.Click += new EventHandler(Start);
            startButton.Location = new Point(290, mapSize * cellSize + 50);
            this.Controls.Add(startButton);
        }
        public void Start(object sender, EventArgs e)
        {
            isPlaying = true;
        }

        public void ConfigureShips(object sender, EventArgs e)
        {
            for(int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    myMap[i, j] = 0;
                    myButtons[i, j].BackColor = Color.White;
                    myButtons[i, j].Text = "";
                }
            }


            int location;
            int x;
            int y;
            Random r = new Random();
            bool success = false;
            while (!success)
            {
                location = r.Next(0, 2);
                x = r.Next(1, 7);
                y = r.Next(1, 7);
                if (location == 0)
                {
                    myMap[x, y] = 1;
                    myMap[x + 1, y] = 1;
                    myMap[x + 2, y] = 1;
                    myMap[x + 3, y] = 1;
                    myButtons[x, y].BackColor = Color.Green;
                    myButtons[x+1, y].BackColor = Color.Green;
                    myButtons[x+2, y].BackColor = Color.Green;
                    myButtons[x+3, y].BackColor = Color.Green;
                }
                else
                {
                    myMap[x, y] = 1;
                    myMap[x, y + 1] = 1;
                    myMap[x, y + 2] = 1;
                    myMap[x, y + 3] = 1;
                    myButtons[x, y].BackColor = Color.Green;
                    myButtons[x, y+1].BackColor = Color.Green;
                    myButtons[x, y+2].BackColor = Color.Green;
                    myButtons[x, y+3].BackColor = Color.Green;
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
                        if (myMap[x - 1, y - 1] != 1 && myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x + 2, y - 1] != 1 && myMap[x + 3, y - 1] != 1
                            && myMap[x + 3, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x, y + 1] != 1 && myMap[x + 1, y + 1] != 1 && myMap[x + 2, y + 1] != 1 && myMap[x + 3, y + 1] != 1
                            && myMap[x - 1, y] != 1 && myMap[x, y] != 1 && myMap[x + 1, y] != 1 && myMap[x + 2, y] != 1)
                        {
                            myMap[x, y] = 1;
                            myMap[x + 1, y] = 1;
                            myMap[x + 2, y] = 1;
                            myButtons[x, y].BackColor = Color.Green;
                            myButtons[x+1, y].BackColor = Color.Green;
                            myButtons[x+2, y].BackColor = Color.Green;
                            success = true;
                        }
                    }
                    else
                    {
                        if (myMap[x, y - 1] != 1 && myMap[x + 1, y - 1] != 1 && myMap[x + 1, y] != 1 && myMap[x + 1, y + 1] != 1 && myMap[x + 1, y + 2] != 1 && myMap[x + 1, y + 3] != 1
                            && myMap[x, y + 3] != 1 && myMap[x - 1, y - 1] != 1 && myMap[x - 1, y] != 1 && myMap[x - 1, y + 1] != 1 && myMap[x - 1, y + 2] != 1 && myMap[x - 1, y + 3] != 1
                            && myMap[x, y] != 1 && myMap[x, y + 1] != 1 && myMap[x, y + 2] != 1)
                        {
                            myMap[x, y] = 1;
                            myMap[x, y + 1] = 1;
                            myMap[x, y + 2] = 1;
                            myButtons[x, y].BackColor = Color.Green;
                            myButtons[x, y+1].BackColor = Color.Green;
                            myButtons[x, y+2].BackColor = Color.Green;
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
                            myButtons[x, y].BackColor = Color.Green;
                            myButtons[x+1, y].BackColor = Color.Green;
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
                            myButtons[x, y].BackColor = Color.Green;
                            myButtons[x, y+1].BackColor = Color.Green;
                            success = true;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
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
                        myButtons[x, y].BackColor = Color.Green;
                        success = true;
                    }
                }
            }
        }

        public void PlayerShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (pressedButton.BackColor != Color.Gray)
            {
                bool playerTurn = Shoot(enemyMap, pressedButton);
                if (!playerTurn)
                    bot.Shoot();
                if (!CheckIfMapIsNotEmpty())
                {
                    this.Controls.Clear();
                    Init();
                }
            }
        }
        public bool CheckIfMapIsNotEmpty()
        {
            bool isEmpty1 = true;
            bool isEmpty2 = true;
            for (int i = 1; i < Form1.mapSize; i++)
            {
                for (int j = 1; j < Form1.mapSize; j++)
                {
                    if (myMap[i, j] != 0)
                        isEmpty1 = false;
                    if (enemyMap[i, j] != 0)
                        isEmpty2 = false;
                }
            }
            if (isEmpty1 || isEmpty2)
                return false;
            else return true;
        }
        public bool Shoot(int[,] map, Button pressedButton)
        {
                bool hit = false;
                if (isPlaying)
                {
                    int delta = 0;
                    if (pressedButton.Location.X > 350)
                        delta = 350;
                    if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] != 0)
                    {
                        hit = true;

                        pressedButton.BackColor = Color.Red;
                        pressedButton.Text = "X";
                    }
                    else
                    {
                        hit = false;

                        pressedButton.BackColor = Color.Gray;
                        pressedButton.Text = ".";
                    }
                }
            return hit;
        }
    }
}
