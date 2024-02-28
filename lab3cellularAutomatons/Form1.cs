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
        private byte[,] cells;

        public Form1()
        {
            InitializeComponent();
            cells = new byte[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            var grid = new MatrixGrid() 
            { 
                Parent = matrixGrid1,
                Dock = DockStyle.Top,
                GridSize = new Size(6, 3), 
                BorderStyle = BorderStyle.FixedSingle, 
                Location = new Point(100, 100) 
            };
            /*var grid = new MatrixGrid() {
                Parent = flowLayoutPanel2,
                Dock = DockStyle.Fill,
                GridSize = new Size(30, 30),
                BorderStyle = BorderStyle.FixedSingle
                ,
                Location = new Point(250, 250)
            };*/

            //matrixGrid1 = grid;

            //создаем матрицу
            /*string[,] matrix = new string[10, 20];
            matrix[1, 4] = "AA";
            matrix[2, 3] = "BB";
            
            //создаем грид
            var grid = new MatrixGrid() { Parent = matrixGrid1, Dock = DockStyle.Fill };
            */
            grid.CellNeeded += grid_CellNeeded;
            grid.CellClick += grid_CellClick;
            //задаем размер грида
            //grid.GridSize = new Size(matrix.GetLength(0), matrix.GetLength(1));

            //grid.Visible = true;

            //присваиваем событие в котором будем отдавать текст ячейки и ее цвет
            /*grid.CellNeeded += (o, e) =>
            {
                e.Value = matrix[e.Cell.X, e.Cell.Y];
                e.BackColor = Color.FromArgb(100, (e.Cell.X * 10) % 256, (e.Cell.Y * 10) % 256, (e.Cell.X * 10) % 256);
                //e.BackColor = Color.Red;
                //e.BackColor = cells[e.Cell.X, e.Cell.Y] > 0 ? Color.Red : Color.White;
            };*/

            //обрабатываем событие клика по ячейке
            /*grid.CellClick += (o, e) =>
            {
                MessageBox.Show(e.Cell.ToString());
                //cells[e.Cell.X, e.Cell.Y] = (byte)(1 - cells[e.Cell.X, e.Cell.Y]);
            };*/
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
    }
}
