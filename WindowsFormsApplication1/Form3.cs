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
    public partial class Form3 : Form
    {
        public struct verteX
        {
            public int X;
            public int Y;
        }
        Timer timer = new Timer();
        public verteX[] vertices = new verteX[maxN];
        public int[,] c = new int[maxN, maxN];
        public int[] d = new int[maxN];
        public bool[] free = new bool[maxN];
        public int[] trace = new int[maxN];
        private const int maxC = 10000000;
        private const int maxN = 1000;
        private int s;
        private int f;
        public int n;
        public int radius;
        private struct Collection
        {
            public int n;
            public int[] S;
        }
        Collection S = new Collection();
        int u = 0; int min = maxC;
        public Form3()
        {
            InitializeComponent();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Pen myArrowPen = new Pen(Color.Red, 2);
            myArrowPen.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myArrowPen2 = new Pen(Color.SkyBlue, 2);
            myArrowPen2.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myPen = new Pen(Color.Red, 2);
            Pen myPen2 = new Pen(Color.SkyBlue, 2);
            SolidBrush myBlackBrush = new SolidBrush(Color.Black);
            SolidBrush myPinkBrush = new SolidBrush(Color.Pink);
            SolidBrush myWhiteBrush = new SolidBrush(Color.White);
            int vertexFontSize = 15;
            int edgeFontSize = 11;
            int alignNumber = 4;
            Font myFont = new Font("Arial", vertexFontSize);
            Font myFont2 = new Font("Arial", edgeFontSize);
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 0), new Point(0, panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 0), new Point(panel1.Width, 0));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, panel1.Height), new Point(panel1.Width, panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(panel1.Width, 0), new Point(panel1.Width, panel1.Height));
            for (int i = 0; i < n; i++)
            {
                if (!free[i])
                    g.FillEllipse(myBlackBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else
                    g.FillEllipse(myPinkBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (c[i, j] < maxC && c[i, j] > 0)
                    {
                        if (c[i, j] == c[j, i])
                        {
                            if (free[i] == false || free[j] == false)
                                g.DrawLine(myPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                            else
                                g.DrawLine(myPen2, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        }
                        else
                        {
                            if (c[j, i] != maxC&&j>i) // nếu tồn tại cạnh ngược chiều
                            {
                                Point point1 = new Point(vertices[i].X, vertices[i].Y);
                                Point point2 = new Point(vertices[j].X, vertices[j].Y);
                                Point point3 = new Point((vertices[i].X + vertices[j].X) / 2 - distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2, (vertices[i].Y + vertices[j].Y) / 2 - -distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2);
                                Point[] curvePoints = { point1, point3, point2 };
                                if (free[i] == false || free[j] == false)
                                    g.DrawCurve(myArrowPen, curvePoints);
                                else
                                    g.DrawCurve(myArrowPen2, curvePoints);
                                

                                g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, point3);
                            }
                            else
                            {
                                if (free[i] == false || free[j] == false)
                                    g.DrawLine(myArrowPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                                else
                                    g.DrawLine(myArrowPen2, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);

                                Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                                g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, myPoint);
                            }
                        }
                        
                    }
                }
            for (int i = 0; i < n; i++)
            {
                g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
            }
        }
        private void initDijkstra()
        {
            S.n = 0;
            S.S = new int[maxN];
            for (int u = 0; u < n; u++)
            {
                free[u] = false;
                d[u] = maxC;
            }
            s = 0; f = n - 1;
            d[s] = 0;

            for (int i = 0; i < n; i++)
            {
                Label lbl = new Label();
                lbl.Text = "d[" + (i).ToString() + "] = " + d[i];
                lbl.Location = new Point(10, i * 30);
                panel2.Controls.Add(lbl);
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {         
            button1.Location = new Point(panel1.Location.X,panel1.Location.Y+panel1.Height);
            initDijkstra();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            solve();   
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
                free[u] = true; updateLabel(); push(u);
                if (u == n || u == f)
                {   
                    this.Refresh();
                    MessageBox.Show("Thuật toán kết thúc với quãng đường = "+d[f].ToString());
                    button1.Visible = false;
                    return;
                }
                for (int v = 0; v < n; v++)
                    if (!free[v] && d[v] > d[u] + c[u, v])
                    {
                        d[v] = d[u] + c[u, v];
                        trace[v] = u;
                    }
                this.Refresh();
                
        }
        private void updateLabel()
        {
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
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //SolidBrush myBrush = new SolidBrush(Color.Black);
            //Font myFont = new Font("Arial", 15);
            //Graphics g = e.Graphics;
            //for (int i = 0; i < n; i++)
            //{
            //    g.DrawString("d["+(i).ToString()+"] = "+d[i], myFont, myBrush, 30,30*(i+1));
            //}
            //string temp = "S={";
            //for (int i = 0; i < S.n; i++)
            //{
            //    temp += S.S[i].ToString() + ",";
            //}
            //if (S.n > 0) temp = temp.Substring(0, temp.Length - 1);
            //temp += "}";
            
            //g.DrawString(temp, myFont, myBrush, 30, 30 * (n + 1));
            
            
        }
        private void push(int value)
        {
            S.S[S.n] = value;
            S.n++;
        }
        private int distance(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Floor(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
        }
        }
}
