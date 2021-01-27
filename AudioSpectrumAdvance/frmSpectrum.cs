using Mmosoft.Oops;
using Mmosoft.Oops.Animation;
// 
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AudioSpectrumAdvance
{
    public partial class frmSpectrum : Form
    {
        // particle mgr
        private ParticleMgr _particleMgr;

        // animator
        private Animator _animator;

        // audio spectrum analyzer
        private Analyzer _analyzer;

        // img background
        private Image _backgroundImg;
        // rect for moving background
        private Rectangle _bgImageRect;

        public frmSpectrum()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _analyzer = new Analyzer(new BaseSpectrumVisualizer []{ circleSpectrumVisualizer1, horizontalSpectrumVisualizer1 }, comboBox1);
            _analyzer.Enable = true;
            _analyzer.DisplayEnable = true;

            SetupAnimator(20, 20);
        }

        private void frmSpectrum_Shown(object sender, EventArgs e)
        {
            // avatar image for spectrum
            circleSpectrumVisualizer1.Img = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("AudioSpectrumAdvance.Assests.avatar.jpg"));
            // background image
            _backgroundImg = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("AudioSpectrumAdvance.Assests.bg.jpg"));
            // 
            _animator.Start();
        }

        private void SetupAnimator(int x, int y)
        {
            _animator = new Animator() { Loop = true };

            int increaseX = 1;
            int increaseY = 1;

            _animator.Stop();
            _animator.Clear();

            // move top
            _animator.Add(new Step
            {
                Interval = 25,
                TotalStep = y,
                AnimAction = (stepI) => 
                { 
                    _bgImageRect = _bgImageRect.AdjustY(-increaseY); 
                    Invalidate(); 
                }
            });

            // move left
            _animator.Add(new Step
            {
                Interval = 25,
                TotalStep = x,
                AnimAction = (stepX) => 
                { 
                    _bgImageRect = _bgImageRect.AdjustX(-increaseX); 
                    Invalidate(); 
                }
            });

            // move bottom
            _animator.Add(new Step
            {
                Interval = 25,
                TotalStep = y,
                AnimAction = (stepI) => 
                { 
                    _bgImageRect = _bgImageRect.AdjustY(increaseY); 
                    Invalidate(); 
                }
            });

            // move right
            _animator.Add(new Step
            {
                Interval = 25,
                TotalStep = x,
                AnimAction = (stepX) => 
                {
                    _bgImageRect = _bgImageRect.AdjustX(increaseX); 
                    Invalidate(); 
                }
            });
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //
            _bgImageRect = new Rectangle(-25, -25, this.Width + 50, this.Height + 50);
            _particleMgr = new ParticleMgr(50, this.Width, this.Height);

            //
            circleSpectrumVisualizer1.Left = this.Width / 2 - circleSpectrumVisualizer1.Width / 2;
            circleSpectrumVisualizer1.Top = this.Height / 2 - circleSpectrumVisualizer1.Height / 2;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            var g = e.Graphics;
            if (_backgroundImg != null)
                g.DrawImage(_backgroundImg, _bgImageRect);

            _particleMgr.Draw(g);
        }
    }
}
