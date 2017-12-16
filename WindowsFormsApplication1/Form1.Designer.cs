namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddVertex = new System.Windows.Forms.Button();
            this.btnAddEdge = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSaveGraph = new System.Windows.Forms.Button();
            this.btnLoadGraph = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Location = new System.Drawing.Point(12, 51);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(75, 23);
            this.btnAddVertex.TabIndex = 0;
            this.btnAddVertex.Text = "Thêm Đỉnh";
            this.btnAddVertex.UseVisualStyleBackColor = true;
            this.btnAddVertex.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Location = new System.Drawing.Point(93, 90);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(75, 23);
            this.btnAddEdge.TabIndex = 2;
            this.btnAddEdge.Text = "Thêm Cạnh";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            this.btnAddEdge.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 220);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Dijkstra";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnSaveGraph
            // 
            this.btnSaveGraph.Location = new System.Drawing.Point(93, 12);
            this.btnSaveGraph.Name = "btnSaveGraph";
            this.btnSaveGraph.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGraph.TabIndex = 4;
            this.btnSaveGraph.Text = "Save Graph";
            this.btnSaveGraph.UseVisualStyleBackColor = true;
            this.btnSaveGraph.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnLoadGraph
            // 
            this.btnLoadGraph.Location = new System.Drawing.Point(12, 12);
            this.btnLoadGraph.Name = "btnLoadGraph";
            this.btnLoadGraph.Size = new System.Drawing.Size(75, 23);
            this.btnLoadGraph.TabIndex = 5;
            this.btnLoadGraph.Text = "Load Graph";
            this.btnLoadGraph.UseVisualStyleBackColor = true;
            this.btnLoadGraph.Click += new System.EventHandler(this.button5_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(13, 250);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(155, 23);
            this.button11.TabIndex = 11;
            this.button11.Text = "Prim";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Location = new System.Drawing.Point(174, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 655);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Thêm Cung";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 2;
            this.trackBar1.Location = new System.Drawing.Point(4, 279);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(165, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Di chuyển";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 675);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.btnLoadGraph);
            this.Controls.Add(this.btnSaveGraph);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAddEdge);
            this.Controls.Add(this.btnAddVertex);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "LVTN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddVertex;
        private System.Windows.Forms.Button btnAddEdge;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSaveGraph;
        private System.Windows.Forms.Button btnLoadGraph;
        private System.Windows.Forms.Button button11;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;

    }
}

