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
            LoadTables();
        }

        private void LoadTables()
        {
            TableLayoutPanel panel = tableLayoutPanel1;
            MainWindow mainWindow = Application.OpenForms.OfType<MainWindow>().FirstOrDefault();

            var data = mainWindow.m_gameControlHub.DataCollector;
            var controlHub = mainWindow.m_gameControlHub;

            // half of tiles
            for (int i = 0; i < 20; i++)
            {
                Label name = (Label)panel.GetControlFromPosition(0, i + 1);
                Label profit = (Label)panel.GetControlFromPosition(1, i + 1);
                Label passes = (Label)panel.GetControlFromPosition(2, i + 1);

                decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

                string tileName = controlHub.Tiles[i + 20].Name;
                if (tileName.Length > 16)
                    tileName = new string(tileName.ToCharArray(), 0, 16);

                name.Text = tileName; 
                profit.Text = (controlHub.Tiles[i].TotalProfit / gamecount).ToString();
                passes.Text = (controlHub.Tiles[i].TotalPasses / gamecount).ToString();
            }

            // second half
            panel = tableLayoutPanel2;

            for (int i = 0; i < 20; i++)
            {
                Label name = (Label)panel.GetControlFromPosition(0, i + 1);
                Label profit = (Label)panel.GetControlFromPosition(1, i + 1);
                Label passes = (Label)panel.GetControlFromPosition(2, i + 1);

                decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

                string tileName = controlHub.Tiles[i + 20].Name;
                if (tileName.Length > 16)
                    tileName = new string(tileName.ToCharArray(), 0, 16);

                name.Text = tileName;
                profit.Text = (controlHub.Tiles[i + 20].TotalProfit / gamecount).ToString();
                passes.Text = (controlHub.Tiles[i + 20].TotalPasses / gamecount).ToString();
            }
        }
    }
}
