using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly_Game_Simulator
{
    public partial class FormMMD : Form
    {

        private Bitmap m_image;

        public FormMMD()
        {
            InitializeComponent();
            LoadTileChart();
        }


        private void LoadTileChart()
        {
            MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

            var data = mainWindow.m_gameControlHub.DataCollector;
            var controlHub = mainWindow.m_gameControlHub;

            foreach (var item in data.TileDataTracker )
            {
                decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

                var tileName = controlHub.Tiles[item.Key].Name;
                //var tileName = item.Key;

                chart1.Series[0].Points.AddXY( tileName, item.Value.first / gamecount);
                chart1.Series[1].Points.AddXY( tileName, item.Value.second / gamecount);

                chart1.Series[0].Points.Last().ToolTip = tileName;
                chart1.Series[1].Points.Last().ToolTip = chart1.Series[1].Points.Last().YValues.Last().ToString();
            }

            // set column chart colors
            chart1.Series[0].Points[0].Color = Color.LightGray;
            chart1.Series[0].Points[1].Color = Color.Sienna;
            chart1.Series[0].Points[2].Color = Color.DodgerBlue;
            chart1.Series[0].Points[3].Color = Color.Sienna;
            chart1.Series[0].Points[4].Color = Color.Gray;
            chart1.Series[0].Points[5].Color = Color.MediumSlateBlue;
            chart1.Series[0].Points[6].Color = Color.LightSkyBlue;
            chart1.Series[0].Points[7].Color = Color.SandyBrown;
            chart1.Series[0].Points[8].Color = Color.LightSkyBlue;
            chart1.Series[0].Points[9].Color = Color.LightSkyBlue;

            chart1.Series[0].Points[10].Color = Color.DarkOrange;
            chart1.Series[0].Points[11].Color = Color.DarkMagenta;
            chart1.Series[0].Points[12].Color = Color.Gold;
            chart1.Series[0].Points[13].Color = Color.DarkMagenta;
            chart1.Series[0].Points[14].Color = Color.DarkMagenta;
            chart1.Series[0].Points[15].Color = Color.MediumSlateBlue;
            chart1.Series[0].Points[16].Color = Color.Orange;
            chart1.Series[0].Points[17].Color = Color.DodgerBlue;
            chart1.Series[0].Points[18].Color = Color.Orange;
            chart1.Series[0].Points[19].Color = Color.Orange;

            chart1.Series[0].Points[20].Color = Color.LightGray;
            chart1.Series[0].Points[21].Color = Color.Red;
            chart1.Series[0].Points[22].Color = Color.SandyBrown;
            chart1.Series[0].Points[23].Color = Color.Red;
            chart1.Series[0].Points[24].Color = Color.Red;
            chart1.Series[0].Points[25].Color = Color.MediumSlateBlue;
            chart1.Series[0].Points[26].Color = Color.Yellow;
            chart1.Series[0].Points[27].Color = Color.Yellow;
            chart1.Series[0].Points[28].Color = Color.Gold;
            chart1.Series[0].Points[29].Color = Color.Yellow;

            chart1.Series[0].Points[30].Color = Color.SteelBlue;
            chart1.Series[0].Points[31].Color = Color.Green;
            chart1.Series[0].Points[32].Color = Color.Green;
            chart1.Series[0].Points[33].Color = Color.DodgerBlue;
            chart1.Series[0].Points[34].Color = Color.Green;
            chart1.Series[0].Points[35].Color = Color.MediumSlateBlue;
            chart1.Series[0].Points[36].Color = Color.SandyBrown;
            chart1.Series[0].Points[37].Color = Color.Blue;
            chart1.Series[0].Points[38].Color = Color.Gray;
            chart1.Series[0].Points[39].Color = Color.Blue;
        }


        private void ClearChart()
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();
        }


        public void OnFormShow(object sender, EventArgs e)
        {
            ClearChart();
            LoadTileChart();
        }

        private Bitmap Screenshot()
        {
            Bitmap bitmap = new Bitmap(this.chart1.Width, this.chart1.Height, PixelFormat.Format64bppArgb);
            chart1.DrawToBitmap(bitmap, new Rectangle(0, 0, chart1.Width, chart1.Height));

            return bitmap;
        }

        private void FormMMD_KeyDown(object sender, KeyEventArgs e)
        {
            m_image = Screenshot();

            if (e.Control && e.KeyCode == Keys.S)
            {
                ImageFormat format = ImageFormat.Png;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);

                    switch (ext)
                    {
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        default:
                            MessageBox.Show("Incorrect extention: " + ext);
                            return;
                    }

                    m_image.Save(saveFileDialog1.FileName, format);
                }
            }
        }
    }
}
