using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mmosoft.Oops;
using System.Drawing.Drawing2D;

namespace AudioSpectrumAdvance
{
    public class CircleSpectrumVisualizer : Control, IAudioSpectrumVisualizer
    {
        // the distance from original point to the ring base
        private int _padding;

        // bar resources
        private Bar[] _bars;
        private Pen _barPen;
        private Pen _barBgPen;

        // origin point
        private Point _originLocation;
        
        // ring resources
        private Pen _ringBasePen;
        private Rectangle _ringBase;

        // image resources
        private Image _img;
        private GraphicsPath _imgGraphicsPath;
        private Brush _overlayBr;


        /// <summary>
        /// Get or set image
        /// </summary>
        public Image Img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
                Invalidate();
            }
        }

        public CircleSpectrumVisualizer()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            // ring pen
            _ringBasePen = new Pen(Color.White, 2);
            _ringBasePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            // bar pen
            _barPen = new Pen(Color.FromArgb(255, 255, 255, 255), 4);
            _barPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            _padding = 100;

            // bar bg pen
            _barBgPen = new Pen(Color.FromArgb(64, Color.White), 8);
            _barPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            _padding = 100;

            // img
            _overlayBr = new SolidBrush(Color.FromArgb(64, 255, 255, 255));
        }

        public void Set(byte[] data)
        {
            _originLocation = new Point(this.Width / 2, this.Height / 2);
            _ringBase = new Rectangle(_originLocation.X, _originLocation.Y, _padding * 2, _padding * 2)
                .MoveXY(-_padding, -_padding)
                .DecreaseSizeFromCenter(8, 8);

            if (_imgGraphicsPath != null)
                _imgGraphicsPath.Dispose();

            _imgGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            _imgGraphicsPath.AddEllipse(_ringBase);

            // norm data
            byte[] normData = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
                normData[i] = (byte)(data[i] / 2);
            // transform from origin
            _bars = BarTransform.Transform(_originLocation, normData, _padding);

            // call OnPaint
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_bars != null)
            {
                for (int i = 0; i < _bars.Length; i++)
                {
                    var bar = _bars[i];

                    g.DrawEllipse(_ringBasePen, _ringBase);
                    g.DrawLine(_barPen, bar.Start, bar.End);
                    g.DrawLine(_barBgPen, bar.Start, bar.End);
                }
            }

            if (_img != null && _imgGraphicsPath != null)
            {
                g.SetClip(_imgGraphicsPath);
                g.DrawImage(_img, _ringBase);
                g.FillRectangle(_overlayBr, _ringBase);
            }
        }


        // 
        public class BarTransform
        {
            public static Bar[] Transform(Point origin, byte[] barValues, int distanceFromOrigin)
            {
                int barCount = barValues.Length;
                var bars = new Bar[barCount];
                double anglePerBar = 2 * Math.PI / barCount;
                double angle = -Math.PI / 2;
                for (int i = 0; i < barCount; i++)
                {
                    angle += anglePerBar;
                    bars[i] = GetBar(origin, angle, barValues[i], distanceFromOrigin);
                }
                return bars;
            }

            private static PointF GetPoint(Point origin, double angle, int distance)
            {
                float x = (float)(origin.X + distance * Math.Sin(angle));
                float y = (float)(origin.Y + distance * Math.Cos(angle));

                return new PointF
                {
                    X = x,
                    Y = y
                };
            }
            private static Bar GetBar(Point origin, double angle, byte barValue, int distanceFromOrigin)
            {
                var start = GetPoint(origin, angle, distanceFromOrigin);
                var end = GetPoint(origin, angle, distanceFromOrigin + barValue);
                return new Bar { Start = start, End = end };
            }
        }
        public class Bar
        {
            public PointF Start { get; set; }
            public PointF End { get; set; }
        }
    }
}
