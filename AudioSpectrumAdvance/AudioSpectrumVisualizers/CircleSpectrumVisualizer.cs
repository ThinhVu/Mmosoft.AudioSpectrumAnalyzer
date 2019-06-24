using AudioSpectrumAdvance.AudioSpectrumVisualizers;
using Mmosoft.Oops;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AudioSpectrumAdvance
{
    public class CircleSpectrumVisualizer : BaseSpectrumVisualizer
    {
        // the distance from original point to the ring base
        private int _padding;
        // origin point
        private Point _originLocation;
     
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
            : base()
        {
            _overlayBr = new SolidBrush(Color.FromArgb(64, 255, 255, 255));
            _padding = 100;
        }

        public override void Set(byte[] data)
        {
            _originLocation = new Point(this.Width / 2, this.Height / 2);
            _baseLineRect = new Rectangle(_originLocation.X, _originLocation.Y, _padding * 2, _padding * 2)
                .MoveXY(-_padding, -_padding)
                .DecreaseSizeFromCenter(8, 8);

            if (_imgGraphicsPath != null)
                _imgGraphicsPath.Dispose();

            _imgGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            _imgGraphicsPath.AddEllipse(_baseLineRect);

            base.Set(data);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_img != null && _imgGraphicsPath != null)
            {
                g.SetClip(_imgGraphicsPath);
                g.DrawImage(_img, _baseLineRect);
                g.FillRectangle(_overlayBr, _baseLineRect);
            }
        }

        public override Bar[] Transform(byte[] data)
        {
            return Transform(_originLocation, data, _padding);
        }

        // 
        private Bar[] Transform(Point origin, byte[] barValues, int distanceFromOrigin)
        {
            int barCount = barValues.Length;
            var bars = new Bar[barCount];
            double anglePerBar = 2 * Math.PI / barCount;
            double angle = 0;
            for (int i = 0; i < barCount; i++)
            {
                bars[i] = GetBar(origin, angle, barValues[i], distanceFromOrigin);
                angle += anglePerBar;
            }
            return bars;
        }

        private PointF GetPoint(Point origin, double angle, int distance)
        {
            float x = (float)(origin.X - distance * Math.Sin(angle));
            float y = (float)(origin.Y - distance * Math.Cos(angle));

            return new PointF
            {
                X = x,
                Y = y
            };
        }

        private Bar GetBar(Point origin, double angle, byte barValue, int distanceFromOrigin)
        {
            var start = GetPoint(origin, angle, distanceFromOrigin);
            var end = GetPoint(origin, angle, distanceFromOrigin + barValue);
            return new Bar { Start = start, End = end };
        }
    }
}
