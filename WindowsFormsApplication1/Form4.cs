using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public struct verteX
        {
            public int X;
            public int Y;
        }
        public verteX[] vertices = new verteX[maxN];
        public int[,] c = new int[maxN, maxN];
        public int[] d = new int[maxN];
        public bool[] free = new bool[maxN];
        public int[] trace = new int[maxN];
        int sum;
        private const int maxC = 10000000;
        private const int maxN = 1000;
        private bool Connected;
        private int s;
        private int f;
        public int n;
        public int radius;
        int u = 0; int min = maxC;
        private bool end = false;
        private struct Collection
        {
            public int n;
            public int[] S;
        }
        Collection S = new Collection();
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(panel1.Location.X, panel1.Location.Y + panel1.Height);
            button2.Location = new Point(button2.Location.X, panel1.Location.Y + panel1.Height);
            button3.Location = new Point(button3.Location.X, panel1.Location.Y + panel1.Height);
            initPrim();
            
        }
        private void initPrim()
        {
            Connected = true;
            sum = 0;
            S.n = 0;
            S.S = new int[maxN];
            for (int u = 0; u < n; u++)
            {
                free[u] = false;
                d[u] = maxC;
                trace[u] = -1;
            }
            d[0] = 0;

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

            Label lblSum = new Label();
            lblSum.Text = "Sum = " + sum.ToString();
            lblSum.Location = new Point(10, (n + 1) * 30);
            panel2.Controls.Add(lblSum);

            updateLabelD();
            updateLabelGuide();
        }
        
        /*private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myArrowPen = new Pen(Color.Red, 2);
            myArrowPen.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myPen = new Pen(Color.Red, 2);
            Pen myPen2 = new Pen(Color.SkyBlue, 2);
            SolidBrush myBlackBrush = new SolidBrush(Color.Black);
            SolidBrush myPinkBrush = new SolidBrush(Color.Pink);
            SolidBrush myWhiteBrush = new SolidBrush(Color.White);
            int vertexFontSize = 15;
            int edgeFontSize = 11;
            int alignNumber=4;
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
                g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - radius / 2 + alignNumber, vertices[i].Y - radius / 2);
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
                            //Point point1 = new Point (vertices[i].X, vertices[i].Y);
                            //Point point2 = new Point (vertices[j].X, vertices[j].Y);
                            //Point point3 = new Point((vertices[i].X + vertices[j].X) / 2 - distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2, (vertices[i].Y + vertices[j].Y) / 2 - -distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2);
                            //Point[] curvePoints = {point1,point3,point2};
                            //g.DrawCurve(myArrowPen,curvePoints);
                        }
                        Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                        g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, myPoint);
                    }
                }
            for (int i = 0; i < n; i++)
            {
                g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
            }
        }*/
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myArrowPen = new Pen(Color.Red, 2);
            myArrowPen.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myArrowPen2 = new Pen(Color.SkyBlue, 2);
            myArrowPen2.CustomEndCap = new AdjustableArrowCap(6, 6);
            Pen myRedPen = new Pen(Color.Red, 2);
            Pen myBlackPen = new Pen(Color.Black, 2);
            Pen mySkyBluePen = new Pen(Color.SkyBlue, 2);
            Pen myBluePen = new Pen(Color.Blue, 2);
            SolidBrush myBlackBrush = new SolidBrush(Color.Black);
            SolidBrush myGreenBrush = new SolidBrush(Color.DarkGreen);
            SolidBrush myPinkBrush = new SolidBrush(Color.Pink);
            SolidBrush myWhiteBrush = new SolidBrush(Color.White);
            SolidBrush myOrangeBrush = new SolidBrush(Color.DarkOrange);
            SolidBrush myYellowBrush = new SolidBrush(Color.Yellow);
            SolidBrush myRedBrush = new SolidBrush(Color.DarkRed);
            SolidBrush myBlueBrush = new SolidBrush(Color.Blue);
            int vertexFontSize = 15;
            int edgeFontSize = 11;
            Font myFont = new Font("Arial", vertexFontSize);
            Font myFont2 = new Font("Arial", edgeFontSize);
            // Draw border
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 0), new Point(0, panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 0), new Point(panel1.Width, 0));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, panel1.Height), new Point(panel1.Width, panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(panel1.Width, 0), new Point(panel1.Width, panel1.Height));
            // Draw Vertices
            for (int i = 0; i < n; i++)
            {
                if (S.n == 0)
                    g.FillEllipse(myGreenBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else if (u == i)
                    g.FillEllipse(myRedBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else if (c[u, i] != maxC && !free[i])
                    g.FillEllipse(myYellowBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else if (!free[i])
                    g.FillEllipse(myGreenBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                else
                    g.FillEllipse(myPinkBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                g.DrawEllipse(myBluePen, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (c[i, j] < maxC && c[i, j] > 0)
                    {
                        if (c[i, j] == c[j, i]) // undirected graph
                        {
                            if (free[i] == true && free[j] == true && (trace[j]==i||trace[i]==j))
                                g.DrawLine(mySkyBluePen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                            else
                                g.DrawLine(myBlackPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                            Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                            g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, myPoint);
                        }
                        else // directed graph
                        {
                            if (c[j, i] != maxC && j > i) // Draw curve
                            {
                                Point point1 = new Point(vertices[i].X, vertices[i].Y);
                                Point point2 = new Point(vertices[j].X, vertices[j].Y);
                                Point point3 = new Point((vertices[i].X + vertices[j].X) / 2 - minimum(distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2, 50), (vertices[i].Y + vertices[j].Y) / 2 + minimum(distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y) / 2, 50));
                                Point[] curvePoints = { point1, point3, point2 };
                                g.DrawCurve(myArrowPen, curvePoints);
                                g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, point3);
                            }
                            else // Draw Line
                            {
                                g.DrawLine(myArrowPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                                Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                                g.DrawString(c[i, j].ToString(), myFont2, myBlackBrush, myPoint);
                            }
                        }

                    }
                }
            for (int i = 0; i < n; i++)
            {
                if (S.n == 0)
                    g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
                else if (u == i)
                    g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
                else if (c[u, i] != maxC && !free[i])
                    g.DrawString(i.ToString(), myFont, myBlackBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
                else if (!free[i])
                    g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
                else
                    g.DrawString(i.ToString(), myFont, myBlackBrush, vertices[i].X - vertexFontSize / 2, vertices[i].Y - vertexFontSize / 2 - 4);
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
                    MessageBox.Show("Đồ thị không liên thông");
                    button1.Visible = false;
                    button2.Visible = false;
                    end = true;
                    return;
                }
                sum += d[u]; 
                push(u);
                for (int v = 0; v < n; v++)
                    if (!free[v] && d[v] > c[u, v])
                    {
                        d[v] = c[u, v];
                        trace[v] = u;
                    }
                
                updateLabelD();
                updateLabelGuide();
                this.Refresh();
                if (S.n == n)
                {
                    MessageBox.Show("KT Thuật toán với cây bao phủ = "+sum.ToString());
                    button1.Visible = false;
                    button2.Visible = false;
                    end = true;
                    updateLabelD();
                    updateLabelGuide();
                    return;
                }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            solve();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void push(int value)
        {
            S.S[S.n] = value;
            S.n++;
        }
        private void updateLabelD()
        {
            panel2.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                Label lbl = new Label();
                lbl.Text = "d[" + (i).ToString() + "] = " + d[i];
                lbl.Location = new Point(10, i * 30);
                if (trace[i] == u)
                {
                    lbl.Font = new Font("Arial", 10);
                    lbl.ForeColor = Color.Orange;
                }
                panel2.Controls.Add(lbl);
            }
            string temp = "S={";
            for (int i = 0; i < S.n; i++)
            {
                temp += S.S[i].ToString() + ",";
                if (i % 5 == 0 && i > 4)
                    temp += "\n";
            }
            if (S.n > 0) temp = temp.Substring(0, temp.Length - 1);
            temp += "}";
            Label lblS = new Label();
            lblS.Text = temp;
            lblS.Size = new Size(panel2.Width - 10, 20 * (n / 5 + 1));
            lblS.Location = new Point(10, n * 30);
            panel2.Controls.Add(lblS);
            
        }
        private void updateLabelGuide()
        {
            Label lbl = new Label();
            panel3.Controls.Clear();
            if (S.n == 0)
            {
                lbl.Text = "Chọn đỉnh gốc là đỉnh 0. Đỉnh gốc có nhãn bằng 0, các đỉnh còn lại có nhãn là +∞";
            }
            else
            {
                lbl.Text = "- Chọn ra đỉnh có nhãn nhỏ nhất là đỉnh " + u.ToString() + ". Đỉnh này được tô màu đỏ.";
                if (S.n >= 2)
                    lbl.Text += "- Đồng thời cạnh (" + trace[u].ToString() + "," + u.ToString() + ") được thêm vào cây bao phủ nhỏ nhất.\n";
                else
                    lbl.Text +="\n";
                lbl.Text += "- Tất cả các đỉnh chưa được thăm kề với đỉnh " + u.ToString() + " (nếu có, tô màu vàng) được tối ưu lại nhãn\n";
                    for (int i = 0; i < n; i++)
                        if (trace[i] == u)
                        {
                            lbl.Text += "- Đỉnh " + i.ToString() + " được cập nhập lại nhãn =" + d[i].ToString() + "\n";
                        }
                
            }
            lbl.Size = new Size(200, (lbl.Text.Length / 20 + 1) * 20);
            lbl.Font = new Font("Arial", 10);
            panel3.Controls.Add(lbl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (!end)
            {
                solve();
            }
        }
        private int distance(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Floor(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
        }
        private int minimum(int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            sf.FileName = "unknown.bmp";
            sf.ShowDialog();

            var path = sf.FileName;

            int width = panel1.Size.Width;
            int height = panel1.Size.Height;

            Bitmap bm = new Bitmap(width, height);
            panel1.DrawToBitmap(bm, new Rectangle(0, 0, width, height));

            bm.Save(path, ImageFormat.Bmp);
        }
    }
}

