using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtitles
{
    class Subtitle
    {
        public string Place;
        public string Color;
        public int StartTime;
        public int EndTime;
        public string Text;
        public Subtitle(string place, string color, int startTime, int endTime, string text)
        {
            this.Place = place;
            this.Color = color;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Text = text;
        }
    }
}
