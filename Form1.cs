using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_Assignments
{
    public partial class Form1 : Form
    {
        Bitmap off;
        Timer t = new Timer();
        List<PolarCircle> arcs = new List<PolarCircle>();
        DDALine line;
        PointF selectorS;
        PointF selectorE;

        int numberOfSlices = 8;
        int whichArc = -1;
        bool startCutting = false;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                startCutting = false;
                whichArc++;
                if (whichArc >= arcs.Count) { whichArc = 0; }
                selectorE = arcs[whichArc].getLocationInCircle(arcs[whichArc], 450, "middle");
            }
            else if(e.KeyCode == Keys.Left)
            {
                startCutting = false;
                whichArc--;
                if (whichArc < 0) { whichArc = arcs.Count - 1; }
                selectorE = arcs[whichArc].getLocationInCircle(arcs[whichArc], 450, "middle");
            }

            if(e.KeyCode == Keys.Space)
            {
                if (!startCutting && whichArc != -1)
                {
                    startCutting = true;
                    line = new DDALine(selectorS.X, selectorS.Y, selectorE.X, selectorE.Y);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.Width, this.Height);
            createTheCircle();
            selectorS = new PointF(this.Width / 2, this.Height / 2);
            selectorE = new PointF(this.Width / 2, this.Height / 2);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawDouble(e.Graphics);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void T_Tick(object sender, EventArgs e)
        {
            if(startCutting && !line.flagStop)
            {
                arcs[whichArc].xc = line.getNextPoint(arcs[whichArc].xc, arcs[whichArc].yc, 15).X;
                arcs[whichArc].yc = line.getNextPoint(arcs[whichArc].xc, arcs[whichArc].yc, 15).Y;
            }
            drawDouble(this.CreateGraphics());
        }

        void createTheCircle()
        {
            int angle = 0;
            for (int i = 0; i < numberOfSlices; i++)
            {
                arcs.Add(new PolarCircle(this.Width / 2, this.Height / 2, 300, angle, angle + 45));
                angle += 45;
            }
        }

        void drawScene(Graphics g)
        {
            g.Clear(Color.Black);
            
            Brush[] b = { Brushes.Blue , Brushes.Green , Brushes.Yellow , Brushes.Red , Brushes.Indigo , Brushes.Cyan, Brushes.Orange, Brushes.LightCoral};
            for (int i = 0; i < numberOfSlices; i++)
            {
                arcs[i].drawCircle(g, 0.1F, b[i]);
            }
            g.DrawLine(Pens.Gray, selectorS, selectorE);
        }

        void drawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
