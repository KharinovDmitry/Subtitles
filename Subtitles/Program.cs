using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Subtitles
{
    class Program
    {
        const int ScreenWidth = 110;
        const int ScreenHeight = 30;

        static Timer aTimer;
        static int timer = 0;
        static List<Subtitle> subtitles;

        static void Main(string[] args)
        {
            ReadInput();
            CreateScreen();
            StartTimer();
            Console.ReadKey();
        }

        private static void ReadInput()
        {
            var input = File.ReadAllLines(@"./input.txt");
            subtitles = new List<Subtitle> { };
            foreach (var item in input)
                if (item != "")
                    subtitles.Add(Validate(item));
        }

        private static void StartTimer()
        {
            aTimer = new Timer(1000);
            aTimer.Elapsed += Tick;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        static void Tick(Object source, ElapsedEventArgs e)
        {
            foreach (var subtitle in subtitles)
            {
                if (subtitle.StartTime == timer)
                    WriteSubtitle(subtitle);
                else if (subtitle.EndTime == timer)
                    DeleteSubtitle(subtitle);
            }
            timer++;
        }

        private static void DeleteSubtitle(Subtitle subtitle)
        {
            SetPlace(subtitle.Place, subtitle.Text.Length);
            for (int i = 0; i < subtitle.Text.Length; i++)
                Console.Write(" ");
        }

        private static Subtitle Validate(string input)
        {
            string text;
            string place = "Bottom";
            string color = "White";
            var timeDuration = input.Substring(0, 13).Replace(" ", "").Split('-');
            var timeStart = timeDuration[0].Split(':');
            var timeEnd = timeDuration[1].Split(':');
            int  startTime = int.Parse(timeStart[0]) * 60 +  int.Parse(timeStart[1]);
            int endTime = int.Parse(timeEnd[0]) * 60 + int.Parse(timeEnd[1]);
            if (input.Contains("["))
            { 
                var textInfo = input.Substring(input.IndexOf('[') + 1, input.IndexOf(']') - input.IndexOf('[') - 1).Split(',');
                place = textInfo[0].Replace(" ", "");
                color = textInfo[1].Replace(" ", "");
                text = input.Substring(input.IndexOf(']') + 2, input.Length - input.IndexOf(']') - 2);
            }
            else
            {
                text = input.Substring(14, input.Length - 14);
            }
            return new Subtitle(place, color, startTime, endTime, text);
        }
        private static void WriteSubtitle(Subtitle subtitle)
        {
            SetPlace(subtitle.Place, subtitle.Text.Length);
            SetColor(subtitle.Color);
            Console.WriteLine(subtitle.Text);
        }

        private static void SetColor(string color)
        {
            switch (color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "White":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
        }

        private static void SetPlace(string place, int textLength)
        {
            switch (place)
            {
                case "Top":
                    Console.SetCursorPosition((ScreenWidth - 2) / 2 - textLength / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((ScreenWidth - 2) / 2 - textLength / 2, ScreenHeight - 2);
                    break;
                case "Right":
                    Console.SetCursorPosition(ScreenWidth - 1 - textLength, (ScreenHeight - 2) / 2);
                    break;
                case "Left":
                    Console.SetCursorPosition(1, (ScreenHeight - 2) / 2);
                    break;
                default:
                    break;
            }
        }

        private static void CreateScreen()
        {
            for (int i = 0; i < ScreenWidth; i++)
                Console.Write("-");
            Console.WriteLine();
            for (int i = 0; i < ScreenHeight - 2; i++)
            {
                Console.Write("|");
                for (int j = 0; j < ScreenWidth - 2; j++)
                    Console.Write(" ");
                Console.WriteLine("|");
            }
            for (int i = 0; i < ScreenWidth; i++)
                Console.Write("-");
        }
    }
}
