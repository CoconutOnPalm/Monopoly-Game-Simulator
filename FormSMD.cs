using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly_Game_Simulator
{
    public partial class FormSMD : Form
    {
        public FormSMD()
        {
            InitializeComponent();
            LoadTileChart();
        }


        private void LoadTileChart()
        {
            MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

            var data = mainWindow.m_gameControlHub.DataCollector;
            var controlHub = mainWindow.m_gameControlHub;

            foreach (var item in data.TileDataTracker)
            {
                var tileName = controlHub.Tiles[item.Key].Name;
                //var tileName = item.Key;

                chart1.Series[0].Points.AddXY(tileName, item.Value.first);
                chart1.Series[1].Points.AddXY(tileName, item.Value.second);
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

        private void LoadPlayerMoneyChart()
        {
            MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

            var data = mainWindow.m_gameControlHub.DataCollector;
            var controlHub = mainWindow.m_gameControlHub;

            for (int playerIndex = 0; playerIndex < controlHub.Players.Count; playerIndex++)
            {
                for (int i = 0; i < data.PlayerMoneyTracker[playerIndex].Count; i++)
                {
                    var playerName = controlHub.Players[playerIndex].Name;

                    chart2.Series[playerIndex].Points.AddXY(i, data.PlayerMoneyTracker[playerIndex][i].first);
                    chart2.Series[playerIndex].Name = playerName;
                }
            }
        }

        private void LoadPlayerDebtChart()
        {
            MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

            var data = mainWindow.m_gameControlHub.DataCollector;
            var controlHub = mainWindow.m_gameControlHub;

            for (int playerIndex = 0; playerIndex < controlHub.Players.Count; playerIndex++)
            {
                for (int i = 0; i < data.PlayerMoneyTracker[playerIndex].Count; i++)
                {
                    var playerName = controlHub.Players[playerIndex].Name;

                    chart3.Series[playerIndex].Points.AddXY(i, data.PlayerMoneyTracker[playerIndex][i].second);
                    chart3.Series[playerIndex].Name = playerName;
                }
            }
        }


        private void ClearCharts()
        {
            foreach (var series in chart1.Series)
                series.Points.Clear();

            foreach (var series in chart2.Series)
                series.Points.Clear();

            foreach (var series in chart3.Series)
                series.Points.Clear();

        }


        //private void LoadTable()
        //{
        //    TableLayoutPanel panel = tableLayoutPanel1;
        //    MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

        //    var data = mainWindow.m_gameControlHub.DataCollector;
        //    var controlHub = mainWindow.m_gameControlHub;

        //    // half of tiles
        //    for (int i = 0; i < 20; i++)
        //    {
        //        Label name = (Label)panel.GetControlFromPosition(0, i + 1);
        //        Label profit = (Label)panel.GetControlFromPosition(1, i + 1);
        //        Label passes = (Label)panel.GetControlFromPosition(2, i + 1);

        //        decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

        //        string tileName = controlHub.Tiles[i + 20].Name;
        //        if (tileName.Length > 16)
        //            tileName = new string(tileName.ToCharArray(), 0, 16);

        //        name.Text = tileName;
        //        profit.Text = (data.TileDataTracker[i].first).ToString();
        //        passes.Text = (data.TileDataTracker[i].second).ToString();
        //        //profit.Text = (controlHub.Tiles[i].TotalProfit).ToString();
        //        //passes.Text = (controlHub.Tiles[i].TotalPasses).ToString();
        //    }

        //    // second half
        //    panel = tableLayoutPanel2;

        //    for (int i = 0; i < 20; i++)
        //    {
        //        Label name = (Label)panel.GetControlFromPosition(0, i + 1);
        //        Label profit = (Label)panel.GetControlFromPosition(1, i + 1);
        //        Label passes = (Label)panel.GetControlFromPosition(2, i + 1);

        //        decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

        //        string tileName = controlHub.Tiles[i + 20].Name;
        //        if (tileName.Length > 16)
        //            tileName = new string(tileName.ToCharArray(), 0, 16);

        //        name.Text = tileName;
        //        profit.Text = (data.TileDataTracker[i + 20].first).ToString();
        //        passes.Text = (data.TileDataTracker[i + 20].second).ToString();
        //    }
        //}


        public void OnFormShow(object sender, EventArgs e)
        {
            ClearCharts();

            LoadTileChart();
            LoadPlayerMoneyChart();
            LoadPlayerDebtChart();
        }
    }
}
