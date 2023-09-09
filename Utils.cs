using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLayer
{
    namespace Utils
    {
        public class Random
        {
            public static int RollDice()
            {
                return s_random.Next(1, 7);
            }

            private static System.Random s_random = new System.Random();
        }

        public class Convert
        {
            public static System.Drawing.Color SysColorToSimColor(Color color)
            {
                switch (color)
                {
                    case Color.Brown:
                        return System.Drawing.Color.Sienna;
                        break;
                    case Color.Cyan:
                        return System.Drawing.Color.LightSkyBlue;
                        break;
                    case Color.Magenta:
                        return System.Drawing.Color.DarkMagenta;
                        break;
                    case Color.Orange:
                        return System.Drawing.Color.Orange;
                        break;
                    case Color.Red:
                        return System.Drawing.Color.Red;
                        break;
                    case Color.Yellow:
                        return System.Drawing.Color.Yellow;
                        break;
                    case Color.Green:
                        return System.Drawing.Color.Green;
                        break;
                    case Color.Blue:
                        return System.Drawing.Color.Blue;
                        break;
                    case Color.Utility:
                        return System.Drawing.Color.FromArgb(220, 242, 221);
                        break;
                    case Color.Other:
                        return System.Drawing.Color.DarkGray;
                        break;
                }
                return System.Drawing.Color.White;
            }
            //// DO NOT USE THIS FUNCTION
            //public static System.Drawing.Color TextToColor(string color_name)
            //{
            //    switch (color_name)
            //    {
            //        case "Brown":
            //            return System.Drawing.Color.Brown;
            //        case "Cyan":
            //            return System.Drawing.Color.Cyan;
            //        case "Magenta":
            //            return System.Drawing.Color.Magenta;
            //        case "Orange":
            //            return System.Drawing.Color.Orange;
            //        case "Red":
            //            return System.Drawing.Color.Red;
            //        case "Yellow":
            //            return System.Drawing.Color.Yellow;
            //        case "Green":
            //            return System.Drawing.Color.Green;
            //        case "Blue":
            //            return System.Drawing.Color.Black;
            //        case "Transport":
            //            return System.Drawing.Color.Gray;
            //        case "PublicService":
            //            return System.Drawing.Color.Gray;
            //        default:
            //            throw new Exception("invalid color name");
            //    }
            //}
        }


    }
}
