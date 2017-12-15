using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public struct verteX
        {
            public int X;
            public int Y;
        }
        public verteX[] vertices = new verteX[maxN];
        public bool isDrawingVertex;
        public bool isDrawingEdge;
        public bool isDrawingArrowEdge;
        public int n,m;
        public int radius=25;
        private int alignNumber=4; // meanless number
        private int vertexFontSize = 15;
        private int edgeFontSize = 11;
        private int temp;
        public int[,] c = new int[maxN, maxN];
        private const int maxC=10000;
        private const int maxN = 1000;
        private int edgeSelect =0;
        private Label label1 = new Label();
        
        public Form1()
        {
            InitializeComponent();
            n = 0; m = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int u = 0; u < maxN; u++)
                for (int v = 0; v < maxN; v++)
                    if (u == v) c[u, v] = 0;
                    else c[u, v] = maxC;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Red, 2);
            SolidBrush myBlackBrush = new SolidBrush(Color.Black);
            SolidBrush myWhiteBrush = new SolidBrush(Color.White);
            Font myFont = new Font("Arial", vertexFontSize);

            g.DrawLine(new Pen(Color.Black,2),new Point (0,0),new Point (0,panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 0), new Point(panel1.Width,0 ));
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, panel1.Height), new Point(panel1.Width, panel1.Height));
            g.DrawLine(new Pen(Color.Black, 2), new Point(panel1.Width, 0), new Point(panel1.Width, panel1.Height));
            for (int i = 0; i < n; i++)
            {
                g.FillEllipse(myBlackBrush, new Rectangle(vertices[i].X - radius, vertices[i].Y - radius, radius * 2, radius * 2));
                Debug.WriteLine((vertices[i].X - radius) + " " + (vertices[i].Y - radius) + " " + radius * 2 + " " + radius * 2);
                g.DrawString(i.ToString(), myFont, myWhiteBrush, vertices[i].X - radius / 2 + alignNumber, vertices[i].Y - radius / 2);
            }
            myPen.CustomEndCap = new AdjustableArrowCap(6, 6);
            myFont = new Font("Arial", edgeFontSize);
            Point myPoint;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (c[i, j] < maxC && c[i, j] > 0)
                    {
                        
                        g.DrawLine(myPen, vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y);
                        g.DrawArc(myPen, vertices[i].X, vertices[i].Y, distance(vertices[i].X, vertices[i].Y, vertices[j].X, vertices[j].Y), 33, 45, 180);
                        
                        
                        myPoint = new Point((vertices[i].X + vertices[j].X) / 2, (vertices[i].Y + vertices[j].Y) / 2);
                        g.DrawString(c[i, j].ToString(), myFont, myBlackBrush, myPoint);
                    }
                }
        }
        private void addVerteX(object sender, MouseEventArgs e)
        {
            vertices[n].X = e.X;
            vertices[n].Y = e.Y;
            n += 1;
            this.Refresh();

        }
        private void addEdge(object sender, MouseEventArgs e)
        {
            
            for (int i = 0; i < n; i++)
            {
                float t = (vertices[i].X - e.X) * (vertices[i].X - e.X) + (vertices[i].Y - e.Y) * (vertices[i].Y - e.Y);
                
                if (t < radius*radius)
                {
                    edgeSelect += 1;
                    if (edgeSelect == 1)
                    {
                        temp = i;
                        break;
                    }
                    else if (edgeSelect == 2)
                    {
                        edgeSelect = 0;
                        String myString = ShowMyDialogBox();
                        if (myString != "Cancel" && myString != "")
                        {
                            c[temp, i] = Int32.Parse(myString);
                            c[i, temp] = Int32.Parse(myString);
                            m += 2;
                        }
                        break;
                    }
                }
            }
            this.Refresh();
        }
        private void addArrowEdge(object sender, MouseEventArgs e)
        {

            for (int i = 0; i < n; i++)
            {
                float t = (vertices[i].X - e.X) * (vertices[i].X - e.X) + (vertices[i].Y - e.Y) * (vertices[i].Y - e.Y);

                if (t < radius * radius)
                {
                    edgeSelect += 1;
                    if (edgeSelect == 1)
                    {
                        temp = i;
                        break;
                    }
                    else if (edgeSelect == 2)
                    {
                        edgeSelect = 0;
                        String myString = ShowMyDialogBox();
                        if (myString != "Cancel" && myString != "")
                        {
                            c[temp, i] = Int32.Parse(myString);
                            m += 1;
                        }
                        break;
                    }
                }
            }
            this.Refresh();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
           
            if (isDrawingVertex) {
                addVerteX(sender, e);
            }
            if (isDrawingEdge)
            {
                addEdge(sender, e);
            }
            if (isDrawingArrowEdge)
            {
                addArrowEdge(sender, e);
            }
        }

        public string ShowMyDialogBox()
        {
            Form2 testDialog = new Form2();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                testDialog.Dispose();
                
                return testDialog.textBox1.Text;
            }
            else
            {
                testDialog.Dispose();
                return "Cancel";
            }
        }
        public void showSolution()
        {
            Form3 form3 = new Form3();
            Graphics g=form3.panel1.CreateGraphics();
            form3.Size = new Size(this.Size.Width, this.Size.Height);
            form3.panel1.Size = new Size(this.panel1.Size.Width,this.panel1.Size.Height);
            form3.panel2.Left = form3.panel1.Left + form3.panel1.Width;
            form3.panel2.AutoScroll = true;
            form3.n = n;
            for (int i = 0; i < n; i++) { form3.vertices[i].X = vertices[i].X; form3.vertices[i].Y = vertices[i].Y; }
            form3.c = c;
            form3.Show();
            form3.radius = radius;
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            showSolution();
            Debug.WriteLine("asd");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();  
            savefile.FileName = "unknown.txt";
            savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName))
                {
                    sw.WriteLine(n);
                    sw.WriteLine(m);
                    for (int i = 0; i < n; i++)
                        sw.WriteLine(vertices[i].X + " " + vertices[i].Y);
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                        {
                            if (c[i, j] < maxC && c[i, j] > 0)
                                sw.WriteLine(i + " " + j+" " + c[i,j]);
                        }
                }
                    
            }
            //MessageBox.Show("Graph have been saved");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = "unknown.txt";
            open.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(open.FileName))
                {
                    n = Int32.Parse(sr.ReadLine());
                    m = Int32.Parse(sr.ReadLine());
                    String[] substrings;
                    for (int i = 0; i < n; i++)
                    {
                        substrings = sr.ReadLine().Split(' ');
                        vertices[i].X = Int32.Parse(substrings[0]);
                        vertices[i].Y = Int32.Parse(substrings[1]);
                    }
                    int vertex1, vertex2, length;
                    for (int i = 0; i < m; i++)
                    {
                        substrings = sr.ReadLine().Split(' ');
                        vertex1 = Int32.Parse(substrings[0]);
                        vertex2 = Int32.Parse(substrings[1]);
                        length = Int32.Parse(substrings[2]);
                        c[vertex1, vertex2] = length;
                    } 
                    this.Refresh();
                    //MessageBox.Show("Graph have been loaded");
                    
                }

            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                vertices[i].X = (int)(Math.Round(vertices[i].X/1.2));
                vertices[i].Y = (int)(Math.Round(vertices[i].Y / 1.2));
            }
            radius = (int)(Math.Round(radius / 1.2));
            this.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.panel1.Left += 50;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.panel1.Left -= 50;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.panel1.Top -= 50;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.panel1.Top += 50;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            Graphics g = form4.panel1.CreateGraphics();
            form4.Size = new Size(this.Size.Width, this.Size.Height);
            form4.panel1.Size = new Size(this.panel1.Size.Width, this.panel1.Size.Height);
            form4.panel2.Left = form4.panel1.Left + form4.panel1.Width;
            form4.panel2.Height = form4.panel1.Height;
            form4.n = n;
            for (int i = 0; i < n; i++) { form4.vertices[i].X = vertices[i].X; form4.vertices[i].Y = vertices[i].Y; }
            form4.c = c;
            form4.Show();
            form4.radius = radius;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            isDrawingArrowEdge = true;
            isDrawingEdge = false;
            isDrawingVertex = false;
            edgeSelect = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            isDrawingVertex = true;
            isDrawingEdge = false;
            isDrawingArrowEdge = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            isDrawingVertex = false;
            isDrawingEdge = true;
            isDrawingArrowEdge = false;
            edgeSelect = 0;
        }
        private int distance(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Floor(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2))));
        }

    }
    
}
