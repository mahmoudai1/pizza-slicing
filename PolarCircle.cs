using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_Assignments
{
    public class PolarCircle
    {
        public float xc, yc, sAngle, eAngle, xcOriginal, ycOriginal;
        public int r;

        public PolarCircle(int xcent, int ycent, int radius, int startAngle, int endAngle)
        {
            xc = xcent;
            yc = ycent;
            xcOriginal = xcent;
            ycOriginal = ycent;
            r = radius;
            sAngle = startAngle;
            eAngle = endAngle;
        }

        public void drawCircle(Graphics g, float gap, Brush b)
        {
            float angle = this.sAngle;
            while (angle < this.eAngle) // smaller value to draw arcs
            {
                float thRadian = (float)(angle * Math.PI / 180);
                float x = (float)(r * Math.Cos(thRadian)) + xc;
                float y = (float)(r * Math.Sin(thRadian)) + yc;
                g.FillEllipse(b, x, y, 5, 5);
                angle += gap;    // for dashed circle
            }
            //g.DrawLine(Pens.White, new PointF(0, 0), getLocationInCircle(this, r));
            g.DrawLine(Pens.White, new PointF(xc, yc), getLocationInCircle(this, r, "start"));
            g.DrawLine(Pens.White, new PointF(xc, yc), getLocationInCircle(this, r, "end"));
            //g.FillEllipse(Brushes.Red, xc, yc, 5, 5);     // To show the mid-point
        }

        public PointF getNextPoint(float angle)
        {
            float thRadian = (float)(angle * Math.PI / 180);
            float x = (float)(r * Math.Cos(thRadian)) + xc;
            float y = (float)(r * Math.Sin(thRadian)) + yc;
            return new PointF(x, y);
        }

        public PointF getLocationInCircle(PolarCircle arc, int customRadius, string pos)
        {
            float thRadian = 0;

            if (pos == "start") { thRadian = (float)(arc.sAngle * Math.PI / 180); }
            else if (pos == "middle") { thRadian = (float)((arc.sAngle + (arc.eAngle - arc.sAngle) / 2) * Math.PI / 180); }
            else if (pos == "end") { thRadian = (float)(arc.eAngle * Math.PI / 180); }

            float x, y;
            if (pos == "middle")
            {
                 x = (float)(customRadius * Math.Cos(thRadian)) + arc.xcOriginal;
                 y = (float)(customRadius * Math.Sin(thRadian)) + arc.ycOriginal;
            }
            else
            {
                 x = (float)(customRadius * Math.Cos(thRadian)) + arc.xc;
                 y = (float)(customRadius * Math.Sin(thRadian)) + arc.yc;
            }
            return new PointF(x, y);
        }
    }
}
