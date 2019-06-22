using System.Collections.Generic;
using System.Windows.Controls;

namespace AudioSpectrumAdvance
{
    public partial class HorizontalSpectrumVisualizer : UserControl, IAudioSpectrumVisualizer
    {
        public HorizontalSpectrumVisualizer()
        {
            InitializeComponent();
        }

        public void Set(byte[] data)
        {
            if (data.Length < 64)
                return;
            Bar01.Value = data[0];
            Bar02.Value = data[1];
            Bar03.Value = data[2];
            Bar04.Value = data[3];
            Bar05.Value = data[4];
            Bar06.Value = data[5];
            Bar07.Value = data[6];
            Bar08.Value = data[7];
            Bar09.Value = data[8];
            Bar10.Value = data[9];
            Bar11.Value = data[10];
            Bar12.Value = data[11];
            Bar13.Value = data[12];
            Bar14.Value = data[13];
            Bar15.Value = data[14];
            Bar16.Value = data[15];
            Bar17.Value = data[16];
            Bar18.Value = data[17];
            Bar19.Value = data[18];
            Bar20.Value = data[19];
            Bar21.Value = data[20];
            Bar22.Value = data[21];
            Bar23.Value = data[22];
            Bar24.Value = data[23];
            Bar25.Value = data[24];
            Bar26.Value = data[25];
            Bar27.Value = data[26];
            Bar28.Value = data[27];
            Bar29.Value = data[28];
            Bar30.Value = data[29];
            Bar31.Value = data[30];
            Bar32.Value = data[31];
            Bar33.Value = data[32];
            Bar34.Value = data[33];
            Bar35.Value = data[34];
            Bar36.Value = data[35];
            Bar37.Value = data[36];
            Bar38.Value = data[37];
            Bar39.Value = data[38];
            Bar40.Value = data[39];
            Bar41.Value = data[40];
            Bar42.Value = data[41];
            Bar43.Value = data[42];
            Bar44.Value = data[43];
            Bar45.Value = data[44];
            Bar46.Value = data[45];
            Bar47.Value = data[46];
            Bar48.Value = data[47];
            Bar49.Value = data[48];
            Bar50.Value = data[49];
            Bar51.Value = data[50];
            Bar52.Value = data[51];
            Bar53.Value = data[52];
            Bar54.Value = data[53];
            Bar55.Value = data[54];
            Bar56.Value = data[55];
            Bar57.Value = data[56];
            Bar58.Value = data[57];
            Bar59.Value = data[58];
            Bar60.Value = data[59];
            Bar61.Value = data[60];
            Bar62.Value = data[61];
            Bar63.Value = data[62];
            Bar64.Value = data[63];
        }
    }
}
