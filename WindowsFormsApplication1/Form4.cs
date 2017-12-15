using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public struct verteX
        {
            public int X;
            public int Y;
        }
        public verteX[] vertices = new verteX[100];
        public int[,] c = new int[100, 100];
        public int[] d = new int[100];
        public bool[] free = new bool[100];
        public int[] trace = new int[100];
        private const int maxC = 10000;
        private const int maxN = 100;
        private bool Connected;
        private int s;
        private int f;
        public int n;
        public int radius;
        bool wait = false;
        int u = 0; int min = maxC;
        private struct Collection
        {
            public int n;
            public int[] S;
        }
        Collection S = new Collection();
        public Form4()
        {
            InitializeComponent();
            initPrim();
        }
        private void initPrim()
        {
            Connected = true;
            S.n = 0;
            S.S = new int[maxN];
            for (int u = 0; u < n; u++)
            {
                free[u] = false;
                d[u] = maxC;
            }
            d[0] = 0;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(panel1.Location.X, panel1.Location.Y + panel1.Height);
            initPrim();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.Red, 2);
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.Black);
            SolidBrush myBrush2 = new SolidBrush(Color.Pink);
            SolidBrush myBrush3 = new SolidBrush(Color.White);
            Font myLineFont = new Font("Arial", 11);
            myPen.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myPen2 = new Pen(Color.SkyBlue, 2);
            myPen2.CustomEndCap = new AdjustableArrowCap(6, 6);
            Font myFont = new Font("Arial", 15);
            for (int i = 0; i < n; i++)
            {
                if (!free[i])
                    g.FillEllipse(myBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else
                    g.FillEllipse(myBrush2, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));

                g.DrawString((i).ToString(), myFont, myBrush3, vertices[i].X - radius / 2 + 4, vertices[i].Y - radius / 2);
                g.DrawString("d = " + (d[i]).ToString(), myFont, myBrush, vertices[i].X - radius / 2 + 4, vertices[i].Y + radius);
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (c[i, j] != maxC && i != j)
                    {
                        if (free[i] == false || free[j] == false)
                            g.DrawLine(myPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        else
                            g.DrawLine(myPen2, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);

                        g.DrawString(c[i, j].ToString(), myLineFont, myBrush, myPoint);
                    }
        }
        

        private void solve()
        {
            u = n; min = maxC;
                for (int i = 0; i < n; i++)
                    if (!free[i] && d[i] < min)
                    {
                        min = d[i];
                        u = i;
                    }
                free[u] = true;
                if (u == n)
                {
                    Connected = false;
                    MessageBox.Show("KT Thuật toán");
                    return;
                }
                push(u);
                
                for (int v = 0; v < n; v++)
                    if (!free[v] && d[v] > c[u, v])
                    {
                        d[v] = c[u,v];
                        trace[v] = u;
                    }
                this.Refresh();
                panel2.Controls.Clear();
                for (int i = 0; i < n; i++)
                {
                    Label lbl = new Label();
                    lbl.Text = "d[" + (i).ToString() + "] = " + d[i];
                    lbl.Location = new Point(10, i * 30);
                    panel2.Controls.Add(lbl);
                }
                string temp = "S={";
                for (int i = 0; i < S.n; i++)
                {
                    temp += S.S[i].ToString() + ",";
                }
                if (S.n > 0) temp = temp.Substring(0, temp.Length - 1);
                temp += "}";
                Label lblS = new Label();
                lblS.Text = temp;
                lblS.Location = new Point(10, n * 30);
                panel2.Controls.Add(lblS);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            solve();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.Black);
            Font myFont = new Font("Arial", 15);
            Graphics g = e.Graphics;
            for (int i = 0; i < n; i++)
            {
                g.DrawString("d[" + (i).ToString() + "] = " + d[i], myFont, myBrush, 30, 30 * (i + 1));
            }
            string temp = "S={";
            for (int i = 0; i < S.n; i++)
            {
                temp += S.S[i].ToString() + ",";
                if (temp.Length / 10 > 1)
                    temp += "\n";
            }
            if (S.n > 0) temp = temp.Substring(0, temp.Length - 1);
            temp += "}";

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            g.DrawString(temp, myFont, myBrush, 30, 30 * (n + 1),drawFormat);
           
        }
        private void push(int value)
        {
            S.S[S.n] = value;
            S.n++;
        }     
    }
}

