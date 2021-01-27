using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmosoft.Oops;

namespace AudioSpectrumAdvance
{
    public class ParticleMgr
    {
        private const int MAXIMUM_SIZE = 5;
        private const int MAXIMUM_MOVING_STEP = 5;
        private System.Drawing.Point _origin;
        private Particle[] _particles;
        private int _width;
        private int _height;
        
        
        //
        private readonly Random r = new Random();
        private readonly int[] direction = new int[] { -1, 1 };

        public ParticleMgr(int numberOfParticle, int width, int height)
        {
            _width = width;
            _height = height;
            _origin = new Point(width/2, height/2);
            InitParitcle(numberOfParticle);
        }

        private void InitParitcle(int pNum)
        {
            
            _particles = new Particle[pNum];
            for (int i = 0; i < pNum; i++)
            {
                _particles[i] = InitNewParticle();
            }
        }

        

        private void ResetParticleIfNeeded(ref Particle particle)
        {
            if (   particle.Alpha <= 0 
                || particle.Region.X < 0 
                || particle.Region.Y < 0 
                || particle.Region.X > _width 
                || particle.Region.Y > _height)
            {
                particle = InitNewParticle();
            }
        }

        private Particle InitNewParticle()
        {
            
            int size = r.Next(3, MAXIMUM_SIZE);
            return new Particle()
            {
                Alpha = 255,
                Region = new Rectangle(_origin.X, _origin.Y, size, size),
                Direction = new Point
                {
                    X = direction[r.Next(direction.Length)] * r.Next(1, MAXIMUM_MOVING_STEP),
                    Y = direction[r.Next(direction.Length)] * r.Next(1, MAXIMUM_MOVING_STEP)
                }
            };
        }

        private void Update()
        {
            Point direction;
            for (int i = 0; i < _particles.Length; i++)
            {
                direction = _particles[i].Direction;

                // update alpha and region
                _particles[i].Alpha -= 1;
                _particles[i].Region = _particles[i].Region.AdjustXY(direction.X, direction.Y);

                ResetParticleIfNeeded(ref _particles[i]);
            }
        }

        public void Draw(Graphics g)
        {
            Update();

            SolidBrush br = new SolidBrush(Color.White);
            foreach (var particle in _particles)
            {
                br.Color = Color.FromArgb(particle.Alpha, br.Color);
                g.FillEllipse(br, particle.Region);
            }

            br.Dispose();
        }
    }

    public class Particle
    {
        public Rectangle Region { get; set; }
        public int Alpha { get; set; }
        public Point Direction { get; set; }
    }
}
