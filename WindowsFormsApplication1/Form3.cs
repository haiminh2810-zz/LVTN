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
        public verteX[] vertices = new verteX[100];
        public int[,] c = new int[100, 100];
        public int[] d = new int[100];
        public bool[] free = new bool[100];
        public int[] trace = new int[100];
        private const int maxC = 10000;
        private const int maxN = 100;
        private int s;
        private int f;
        public int n;
        public int radius;
        bool wait = false;
        int u = 0; int min = maxC;
        Form1 form1= new Form1();
        public Form3()
        {
            InitializeComponent();
            
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
                g.DrawString("d = "+(d[i]).ToString(), myFont, myBrush, vertices[i].X - radius / 2 + 4, vertices[i].Y + radius );
            }
            for (int i=0;i<n;i++)
                for (int j=0;j<n;j++)
                    if (c[i, j] != maxC && i != j)
                    {
                        if (free[i]==false||free[j]==false) 
                            g.DrawLine(myPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        else
                            g.DrawLine(myPen2, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        Point myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                        
                        g.DrawString(c[i,j].ToString(), myLineFont, myBrush, myPoint);
                    }
            
            
        }
        private void initDijkstra()
        {

            for (int u = 0; u < n; u++)
            {
                free[u] = false;
                d[u] = maxC;
            }

            s = 0; f = n - 1;
            d[s] = 0;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
            button1.Location = new Point(panel1.Location.X,panel1.Location.Y+panel1.Height);
            initDijkstra();
            //dijkstra();
        }
        /*
        private void dijkstra()
        {   
            do
            {
                u = n; min = maxC;
                for (int i = 0; i < n; i++)
                    if (!free[i] && d[i] < min)
                    {
                        min = d[i];
                        u = i;
                    }
                if (u == n || u == n - 1)
                    break;
                free[u] = true;
                for (int v = 0; v < n; v++)
                    if (!free[v] && d[v] > d[u] + c[u, v])
                    {
                        d[v] = d[u] + c[u, v];
                        trace[v] = u;
                    }
            }
            while (true);
            MessageBox.Show(d[f].ToString());
        }
        */
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
                free[u] = true;
                if (u == n || u == n - 1)
                {
                    this.Refresh();
                    MessageBox.Show("Thuật toán kết thúc với quãng đường = "+d[f].ToString());
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

        
    }
}
