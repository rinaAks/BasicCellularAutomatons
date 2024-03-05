using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab3cellularAutomatons
{
    public partial class Form1 : Form
    {
        private int[,] cells;
        const int numberHorizontalCells = 14;
        const int numberVerticalCells = 12;

        public Form1()
        {
            InitializeComponent();
            //cells = new byte[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            cells = new int[numberHorizontalCells,numberVerticalCells];
            
            var grid = new MatrixGrid()
            {
                Parent = panel3,
                Dock = DockStyle.Fill,
                GridSize = new Size(numberHorizontalCells, numberVerticalCells), 
                BorderStyle = BorderStyle.FixedSingle, 
                //Location = new Point(100, 100) 
            };
            grid.CellNeeded += grid_CellNeeded;
            grid.CellClick += grid_CellClick;
        }
        
        void grid_CellClick(object sender, MatrixGrid.CellClickEventArgs e)
        {
            cells[e.Cell.X, e.Cell.Y] = (1 - cells[e.Cell.X, e.Cell.Y]);
        }

        void grid_CellNeeded(object sender, MatrixGrid.CellNeededEventArgs e)
        {
            e.BackColor = cells[e.Cell.X, e.Cell.Y] > 0 ? Color.Green : Color.White;
        }

        Random rnd = new Random();

        private void btRandom_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
                cells[i, 0] = rnd.Next(0, 2);

            Invalidate(true);
        }
        int rule;
        string binaryRule;
        private void btStart_Click(object sender, EventArgs e)
        {
            rule = (int)edRule.Value;
            binaryRule = Convert.ToString(rule, 2);
            //binaryRule = IntToBinaryString(rule);

            if(binaryRule.Length < 8) binaryRule = string.Concat(Enumerable.Repeat("0", (8 - binaryRule.Length))) + binaryRule;
            if (binaryRule.Length == 4) binaryRule = "0000" + binaryRule;
            //cells[1, 3] = 1;
            for (int j = 1; j < numberVerticalCells; j++)
            {
                for (int i = 1; i < cells.GetLength(0) - 1; i++)
                {
                    //cells[i, j] = (byte)rnd.Next(0, 2);
                    cells[i, j] = newCell((int)cells[i-1, j - 1], (int)cells[i, j-1], (int)cells[i+1, j - 1]);
                }

            }
            tbOutput.Text = "rule: " + binaryRule;

        }

        int newCell(int left, int middle, int right)
        {
            /*
            if (left == 1 && middle == 1 && right == 1) return (int)(binaryRule[0] - '0');
            else if (left == 1 && middle == 1 && right == 0) return (int)(binaryRule[1] - '0');
            else if (left == 1 && middle == 0 && right == 1) return (int)(binaryRule[2] - '0');
            else if (left == 1 && middle == 0 && right == 0) return (int)(binaryRule[3] - '0');
            else if (left == 0 && middle == 1 && right == 1) return (int)(binaryRule[4] - '0');
            else if (left == 0 && middle == 1 && right == 0) return (int)(binaryRule[5] - '0');
            else if (left == 0 && middle == 0 && right == 1) return (int)(binaryRule[6] - '0');
            else if (left == 0 && middle == 0 && right == 0) return (int)(binaryRule[7] - '0');
            */
            int iRule = binaryRule.Length - 1;
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if(left == i && middle == j && right == k)
                        {
                            return (int)(binaryRule[iRule] - '0');
                        }
                        iRule = iRule - 1;
                    }
                }
            }
            /*
            if (checkI(left, 1, middle, 1, right, 1)) return (int)(binaryRule[0] - '0');
            else if (checkI(left, 1, middle, 1, right, 0)) return (int)(binaryRule[1] - '0');
            else if (checkI(left, 1, middle, 0, right, 1)) return (int)(binaryRule[2] - '0');
            else if (checkI(left, 1, middle, 0, right, 0)) return (int)(binaryRule[3] - '0');
            else if (checkI(left, 0, middle, 1, right, 1)) return (int)(binaryRule[4] - '0');
            else if (checkI(left, 0, middle, 1, right, 0)) return (int)(binaryRule[5] - '0');
            else if (checkI(left, 0, middle, 0, right, 1)) return (int)(binaryRule[6] - '0');
            else if (checkI(left, 0, middle, 0, right, 0)) return (int)(binaryRule[7] - '0');
            */
            return 0;
        }

        //bool checkI(int left, int a, int middle, int b, int right, int c)
        //{
        //    return left == a && middle == b && right == c;
        //}

        public string IntToBinaryString(int number)
        {
            const int mask = 1;
            var binary = string.Empty;
            while (number > 0)
            {
                // Logical AND the number and prepend it to the result string
                binary = (number & mask) + binary;
                number = number >> 1;
            }

            return binary;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j] = 0;
                }
            }

        }
    }
}
