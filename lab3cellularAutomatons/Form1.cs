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
        public static byte[] CreateSpecialByteArray(int length)
        {
            var arr = new byte[length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0x20;
            }
            return arr;
        }

        private byte[,] cells;
        const int numberHorizontalCells = 14;
        const int numberVerticalCells = 12;

        public Form1()
        {
            InitializeComponent();
            //cells = new byte[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            cells = new byte[numberHorizontalCells,numberVerticalCells];
            
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
            cells[e.Cell.X, e.Cell.Y] = (byte)(1 - cells[e.Cell.X, e.Cell.Y]);
        }

        void grid_CellNeeded(object sender, MatrixGrid.CellNeededEventArgs e)
        {
            e.BackColor = cells[e.Cell.X, e.Cell.Y] > 0 ? Color.Green : Color.White;
        }

        Random rnd = new Random();

        private void btRandom_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
                cells[i, 0] = (byte)rnd.Next(0, 2);

            Invalidate(true);
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            cells[1, 3] = 1;
            for (int i = 1; i < cells.GetLength(0)-1; i++)
            {
                for(int j = 1; j < numberVerticalCells; j++)
                {
                    cells[i, j] = (byte)rnd.Next(0, 2);

                }

            }

        }
    }
}
