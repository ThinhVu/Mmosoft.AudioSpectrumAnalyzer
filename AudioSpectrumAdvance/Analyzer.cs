using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;
//
using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace AudioSpectrumAdvance
{

    internal class Analyzer
    {
        private bool _initialized;          //initialized flag
        private bool _enable;               //enabled status
        private int _lastlevel;             //last output level
        private int _hanctr;                //last output level counter
        private int _deviceIndex;           //used device index
        private int _lines = 64;            // number of spectrum lines

        //
        private DispatcherTimer _t;         //timer that refreshes the display

        //
        private WASAPIPROC _process;        //callback function to obtain data
        public float[] _fft;               //buffer for fft data
        public List<byte> _spectrumdata;   //spectrum data buffer

        //
        private IAudioSpectrumVisualizer[] _spectrumVisualizer;//spectrum dispay control
        private ComboBox _devicelist;       //device list

        //ctor
        public Analyzer(IAudioSpectrumVisualizer[] audioSpectrumVisualizer, ComboBox devicelist)
        {
            _fft = new float[8192];
            _lastlevel = 0;
            _hanctr = 0;
            // init timer
            _t = new DispatcherTimer();
            _t.Tick += _t_Tick;
            _t.Interval = TimeSpan.FromMilliseconds(25); //40hz refresh rate//25
            _t.IsEnabled = false;

            _process = new WASAPIPROC(Process);
            _spectrumdata = new List<byte>();
            _spectrumVisualizer = audioSpectrumVisualizer;
            _devicelist = devicelist;
            _initialized = false;
            Init();
        }

        // flag for display enable
        public bool DisplayEnable { get; set; }

        // flag for enabling and disabling program functionality
        public bool Enable
        {
            get 
            { 
                return _enable; 
            }
            set
            {
                _enable = value;
                if (value)
                {
                    if (!_initialized)
                    {
                        var array = (_devicelist.Items[_devicelist.SelectedIndex] as string).Split(' ');
                        _deviceIndex = Convert.ToInt32(array[0]);
                        bool result = BassWasapi.BASS_WASAPI_Init(_deviceIndex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero);
                        if (!result)
                        {
                            var error = Bass.BASS_ErrorGetCode();
                            MessageBox.Show(error.ToString());
                        }
                        else
                        {
                            _initialized = true;
                        }
                    }
                    BassWasapi.BASS_WASAPI_Start();
                }
                else
                {
                    BassWasapi.BASS_WASAPI_Stop(true);
                }
                System.Threading.Thread.Sleep(500);
                _t.IsEnabled = value;
            }
        }

        // initialization
        private void Init()
        {
            bool result = false;
            for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
            {
                var device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    _devicelist.Items.Add(string.Format("{0} - {1}", i, device.name));
                }
            }
            _devicelist.SelectedIndex = 0;
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            result = Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("Init Error");
        }

        //timer 
        private void _t_Tick(object sender, EventArgs e)
        {
            int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)BASSData.BASS_DATA_FFT8192);  //get channel fft data
            if (ret < -1) return;
            int x, y;
            int b0 = 0;

            //computes the spectrum data, the code is taken from a bass_wasapi sample.
            _spectrumdata.Clear();
            for (x = 0; x < _lines; x++)
            {
                float peak = 0;
                int b1 = (int)Math.Pow(2, x * 10.0 / (_lines - 1));
                if (b1 > 1023) b1 = 1023;
                if (b1 <= b0) b1 = b0 + 1;
                for (; b0 < b1; b0++)
                {
                    if (peak < _fft[1 + b0])
                        peak = _fft[1 + b0];
                }
                y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
                if (y > 255) y = 255;
                if (y < 0) y = 0;
                _spectrumdata.Add((byte)y);
            }

            if (DisplayEnable)
            {
                foreach (var visualizer in _spectrumVisualizer)
                {
                    visualizer.Set(_spectrumdata.ToArray());
                }
            }

            int level = BassWasapi.BASS_WASAPI_GetLevel();
            if (level == _lastlevel && level != 0) _hanctr++;
            _lastlevel = level;

            //Required, because some programs hang the output. If the output hangs for a 75ms
            //this piece of code re initializes the output so it doesn't make a gliched sound for long.
            if (_hanctr > 3)
            {
                _hanctr = 0;
                Free();
                Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                _initialized = false;
                Enable = true;
            }
        }

        // WASAPI callback, required for continuous recording
        private int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        // clean
        public void Free()
        {
            BassWasapi.BASS_WASAPI_Free();
            Bass.BASS_Free();
        }
    }
}
