using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.DesignerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly_Game_Simulator
{
    enum SimulationMode
    {
        SingleSim,
        MultiSim
    }


    public partial class MainWindow : Form
    {

        public Color m_defaultTileColor = new Color();
        public Color m_selectedTileColor = new Color();
        public Color m_hoverTileColor = new Color();

        public SimulationLayer.GameControlHub m_gameControlHub;

        private SimulationLayer.Player m_selectedPlayer = SimulationLayer.GameControlHub.emptyPlayer;

        private SimulationMode m_simulationMode = new SimulationMode();

        private FormSMD m_formSMD; // singlegame mode data form (chart button in bottom-right panel)
        private FormMMD m_formMMD; // singlegame mode data form (chart button in bottom-right panel)

        private (SimulationLayer.SimualationExitCode, int) m_simOutput = (SimulationLayer.SimualationExitCode.Error, 0);

        private double m_progressBarValue = 0;


        public MainWindow()
        {
            InitializeComponent();

            m_gameControlHub = new SimulationLayer.GameControlHub();

            m_defaultTileColor = Color.FromArgb(220, 242, 221);
            m_selectedTileColor = Color.Ivory;
            m_hoverTileColor = Color.FromArgb(229, 255, 231);

            m_selectedPlayer = m_gameControlHub.Players.FirstOrDefault();

            LoadPlayerSettingsBox();
            LoadTiles();

            // trigger CheckBox.CheckChanged event
            includeCheckBox3.Checked = false;
            includeCheckBox4.Checked = false;
            includeCheckBox5.Checked = false;
            includeCheckBox6.Checked = false;


            playerNameLabel.Text = m_selectedPlayer.Name;

            m_simulationMode = SimulationMode.MultiSim;

            RefreshSelectedTiles();
            RefreshPlayerPropertiesListBox();
            RefreshPropertyData();

            m_gameControlHub.SimulationProgressIncreased += OnSimulationProgressIncreased;
            m_gameControlHub.f_progressbar = progressBar1;

            string currentDirectory = Directory.GetCurrentDirectory();

            openFileDialog1.InitialDirectory = Path.Combine(currentDirectory, @"Saved Simulations");
            saveFileDialog1.InitialDirectory = Path.Combine(currentDirectory, @"Saved Simulations");
            saveLogFileDialog.InitialDirectory = Path.Combine(currentDirectory, @"Saved Simulations\Logs");

            //saveFileDialog1.InitialDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory() + @"..\Saved Simulations"));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'simulationEntryData_DataSet.PlayerEntryData' table. You can move, or remove it, as needed.
            this.playerEntryDataTableAdapter.Fill(this.simulationEntryData_DataSet.PlayerEntryData);

        }


        private void OnSimulationProgressIncreased(object sender, EventArgs e)
        {
            //progressBar1.Value++;
            //progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value++; });
            //MethodInvoker m = new MethodInvoker(() => progressBar1.Step++);
            //progressBar1.Invoke(m);

            if (progressBar1.InvokeRequired)
            {
                MethodInvoker m = new MethodInvoker(() =>
                {
                    if (progressBar1.Value < 100)
                    {
                        double value = (100.0 / (double)SimulationLayer.GameControlHub.GAMES_PER_SIMULATION);

                        m_progressBarValue += value;

                        // trim
                        if (m_progressBarValue > 100)
                            m_progressBarValue = 100;

                        progressBar1.Value = (int)m_progressBarValue;

                    }
                });
                progressBar1.Invoke(m);
            }
            else
            {
                progressBar1.Value++;
            }
        }


        private void LoadPlayers()
        {
            var player1 = m_gameControlHub.Players[0];
            var player2 = m_gameControlHub.Players[1];
            var player3 = m_gameControlHub.Players[2];
            var player4 = m_gameControlHub.Players[3];
            var player5 = m_gameControlHub.Players[4];
            var player6 = m_gameControlHub.Players[5];

            player1.Playing = true;
            player2.Playing = true;
            player3.Playing = includeCheckBox3.Checked;
            player4.Playing = includeCheckBox4.Checked;
            player5.Playing = includeCheckBox5.Checked;
            player6.Playing = includeCheckBox6.Checked;

            player1.Name = playerNameTextBox1.Text;
            player1.StartMoney = Convert.ToInt32(startMoneyTB1.Text);
            player1.StartDebt = Convert.ToInt32(startDebtTB1.Text);

            player2.Name = playerNameTextBox2.Text;
            player2.StartMoney = Convert.ToInt32(startMoneyTB2.Text);
            player2.StartDebt = Convert.ToInt32(startDebtTB2.Text);

            if (player3.Playing)
            {
                player3.Name = playerNameTextBox3.Text;
                player3.StartMoney = Convert.ToInt32(startMoneyTB3.Text);
                player3.StartDebt = Convert.ToInt32(startDebtTB3.Text);
            }

            if (player4.Playing)
            {
                player4.Name = playerNameTextBox4.Text;
                player4.StartMoney = Convert.ToInt32(startMoneyTB4.Text);
                player4.StartDebt = Convert.ToInt32(startDebtTB4.Text);
            }

            if (player5.Playing)
            {
                player5.Name = playerNameTextBox5.Text;
                player5.StartMoney = Convert.ToInt32(startMoneyTB5.Text);
                player5.StartDebt = Convert.ToInt32(startDebtTB5.Text);
            }

            if (player6.Playing)
            {
                player6.Name = playerNameTextBox6.Text;
                player6.StartMoney = Convert.ToInt32(startMoneyTB6.Text);
                player6.StartDebt = Convert.ToInt32(startDebtTB6.Text);
            }
        }


        private void LoadPlayerSettingsBox()
        {
            var Players = m_gameControlHub.Players;

            playerNameTextBox1.Text = Players[0].Name;
            startMoneyTB1.Text = Players[0].StartMoney.ToString();
            startDebtTB1.Text = Players[0].StartDebt.ToString();

            playerNameTextBox2.Text = Players[1].Name;
            startMoneyTB2.Text = Players[1].StartMoney.ToString();
            startDebtTB2.Text = Players[1].StartDebt.ToString();

            playerNameTextBox3.Text = Players[2].Name;
            startMoneyTB3.Text = Players[2].StartMoney.ToString();
            startDebtTB3.Text = Players[2].StartDebt.ToString();

            playerNameTextBox4.Text = Players[3].Name;
            startMoneyTB4.Text = Players[3].StartMoney.ToString();
            startDebtTB4.Text = Players[3].StartDebt.ToString();

            playerNameTextBox5.Text = Players[4].Name;
            startMoneyTB5.Text = Players[4].StartMoney.ToString();
            startDebtTB5.Text = Players[4].StartDebt.ToString();

            playerNameTextBox6.Text = Players[5].Name;
            startMoneyTB6.Text = Players[5].StartMoney.ToString();
            startDebtTB6.Text = Players[5].StartDebt.ToString();
        }


        private void LoadTiles()
        {
            var Tiles = m_gameControlHub.Tiles.Values.ToList();

            // street tiles

            brownLabel1.Text = Tiles[1].Name;
            brownLabel2.Text = Tiles[3].Name;

            cyanLabel1.Text = Tiles[6].Name;
            cyanLabel2.Text = Tiles[8].Name;
            cyanLabel3.Text = Tiles[9].Name;

            magentaLabel1.Text = Tiles[11].Name;
            magentaLabel2.Text = Tiles[13].Name;
            magentaLabel3.Text = Tiles[14].Name;

            orangeLabel1.Text = Tiles[16].Name;
            orangeLabel2.Text = Tiles[18].Name;
            orangeLabel3.Text = Tiles[19].Name;

            redLabel1.Text = Tiles[21].Name;
            redLabel2.Text = Tiles[23].Name;
            redLabel3.Text = Tiles[24].Name;

            yellowLabel1.Text = Tiles[26].Name;
            yellowLabel2.Text = Tiles[27].Name;
            yellowLabel3.Text = Tiles[29].Name;

            greenLabel1.Text = Tiles[31].Name;
            greenLabel2.Text = Tiles[32].Name;
            greenLabel3.Text = Tiles[34].Name;

            blueLabel1.Text = Tiles[37].Name;
            blueLabel2.Text = Tiles[39].Name;


            // transport tiles

            transportLabel1.Text = Tiles[5].Name;
            transportLabel2.Text = Tiles[15].Name;
            transportLabel3.Text = Tiles[25].Name;
            transportLabel4.Text = Tiles[35].Name;


            // public service tiles

            publicServiceLabel1.Text = Tiles[12].Name;
            publicServiceLabel2.Text = Tiles[28].Name;
        }



        void ImportSimulationFromFile(string filename)
        {
            /// 
            /// FILE STRUCTURE:
            /// 
            /// {Player 1 name}
            /// {Number of properties}
            /// {Porperty name} {Level}
            /// {Property name} {Level}
            /// ...
            /// {Property name} {Level}
            /// ...5
            ///
            /// note: "...n" copies structure above n times
            /// note: "..."  just like in sequence def
            ///

            BinaryReader reader = new BinaryReader(File.OpenRead(filename));

            try
            {
                for (int i = 0; i < 6; i++)
                {
                    string playerName = reader.ReadString();
                    int propCount = reader.ReadInt32();

                    List<Pair<string, int>> properties = new List<Pair<string, int>>(propCount);

                    for (int j = 0; j < propCount; j++)
                    {
                        properties.Add(new Pair<string, int>(reader.ReadString(), reader.ReadInt32()));
                    }

                    foreach (var property in properties)
                    {
                        AssignPropertyToPlayer(m_gameControlHub.Players[i], property.first, property.second);
                    }
                }
            }
            catch (Exception e) // for both IOException and EndOfFileException
            {
                MessageBox.Show("Error: File " + filename + " is corrupted\n" + "Exception: " + e.Message, "Simulation import error");
            }

            RefreshSelectedTiles();
            RefreshPlayerPropertiesListBox();
            RefreshPropertyData();

            reader.Close();
        }


        void ExportSimulationFromFile(string filename)
        {
            /// 
            /// FILE STRUCTURE:
            /// 
            /// {Player 1 name}
            /// {Number of properties}
            /// {Porperty name} {Level}
            /// {Property name} {Level}
            /// ...
            /// {Property name} {Level}
            /// ...5
            ///
            /// note: "...n" copies structure above n times
            /// note: "..."  just like in sequence def
            ///

            BinaryWriter writer = new BinaryWriter(File.OpenWrite(filename));

            try
            {
                for (int i = 0; i < 6; i++)
                {
                    var player = m_gameControlHub.Players[i];

                    writer.Write(player.Name);
                    writer.Write(player.OwnedTiles.Count);

                    foreach (var tileIndex in player.OwnedTiles)
                    {
                        writer.Write(m_gameControlHub.Tiles[tileIndex].Name);
                        writer.Write(m_gameControlHub.Tiles[tileIndex].Level);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not save the simulation\n" + "Exception: " + e.Message, "Simulation export error");
                return;
            }

            writer.Close();
        }



        bool CreateLogFile(string filename)
        {
            /// 
            /// FILE STRUCTURE:
            /// 
            /// [PLAYERS]: Playing {n}
            /// [Player name]: {name}
            /// {
            ///     [Money]: {n}
            ///     [Debt]: {n}
            ///     [Properties]: Count = {n}
            ///     {
            ///         {name}
            ///         {name}
            ///         ...
            ///         {name}
            ///     }
            /// }
            /// ...2-6
            /// [TILES]
            /// [Tile ID]: {id}     [Tile name]: {name}
            /// {
            ///     [Level]: {n}
            ///     [Profit]: {d}
            ///     [Passes]: {d}
            /// }
            /// ...40
            ///
            /// note: "...n" copies structure above n times
            /// note: "..."  just like in sequence def
            ///


            List<string> playerData = new List<string>(6);
            int playing = 0;

            foreach (var player in m_gameControlHub.Players)
            {
                if (player.Playing)
                    playing++;
                else
                    continue;

                string str =
                    "[Player Name]: " + player.Name + '\n' +
                    '{' + '\n' +
                    '\t' + "[Money on start]: " + player.Money.ToString() + '\n' +
                    '\t' + "[Debt on start]: " + player.Debt.ToString() + '\n' +
                    '\t' + "[Properties]: Count = " + player.OwnedTiles.Count.ToString() + '\n';

                if (player.OwnedTiles.Count > 0)
                {
                    string tilestr = "\t{\n";
                    foreach (int i in player.OwnedTiles)
                    {
                        tilestr += "\t\t" + m_gameControlHub.Tiles[i].Name + "\n";
                    }
                    tilestr += "\t}\n";

                    str += tilestr;
                }

                str += '}' + '\n';

                playerData.Add(str);
            }

            string outputText = "[PLAYERS]: \tPlaying = " + playing.ToString() + '\n';

            foreach (var player in playerData)
            {
                outputText += player;
            }

            outputText += "################################\n";

            outputText += "[TILES]\n";

            List<string> tileData = new List<string>(40);

            foreach (var tile in m_gameControlHub.DataCollector.TileDataTracker)
            {
                string str =
                    "[Tile ID]: " + tile.Key.ToString() + " \t" + "[Tile Name]: " + m_gameControlHub.Tiles[tile.Key].Name + "\n" +
                    '{' + '\n' +
                    '\t' + "[Profit]: " + tile.Value.first.ToString() + '\n' +
                    '\t' + "[Passes]: " + tile.Value.second.ToString() + '\n' +
                    '}' + '\n';

                outputText += str;
            }


            try
            {
                File.WriteAllText(filename, outputText);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: could not create the Log file\n" + "Exception: " + e.Message, "Log file error");
            }

            return true;
        }



        private void AssignPropertyToPlayer(SimulationLayer.Player player, string PropertyName, int level = 5)
        {
            SimulationLayer.Tile tile = null;

            foreach (var _t in m_gameControlHub.Tiles)
            {
                if (_t.Value.Name == PropertyName)
                {
                    tile = _t.Value;
                }
            }

            // if tile had an owner before
            if (tile.getOwner() != SimulationLayer.GameControlHub.emptyPlayer)
            {
                tile.getOwner().RemoveOwnership(tile);
            }

            player.AddOwnership(tile, level);
            tile.SetOwner(player);

            RefreshPlayerPropertiesListBox();
            //playerPropertiesListBox.Items.Add(tile.Name);
        }

        private void RemovePropertyFromPlayer(SimulationLayer.Player player, string PropertyName)
        {
            SimulationLayer.Tile tile = null;

            foreach (var _t in m_gameControlHub.Tiles)
            {
                if (_t.Value.Name == PropertyName)
                {
                    tile = _t.Value;
                }
            }

            player.RemoveOwnership(tile);

            RefreshPlayerPropertiesListBox();
        }


        private void RefreshSimulation()
        {
            LoadPlayers();

            foreach (var player in m_gameControlHub.Players)
            {
                Console.WriteLine(player.StartMoney);
                player.Position = 0;
                player.Lost_games = 0;
                player.Won_games = 0;
            }

            foreach (var tile in m_gameControlHub.Tiles)
            {
                tile.Value.TotalProfit = 0;
                tile.Value.TotalPasses = 0;
            }
        }



        private void CheckStreetTile(Panel panel)
        {
            if (panel == null)
            {
                Debugger.Break(); // TODO: remove
                return;
            }

            CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
            Label label = panel.Controls.OfType<Label>().FirstOrDefault();

            if (checkBox == null || label == null)
            {
                Debugger.Break(); // TODO: remove
                return;
            }

            if (checkBox.Checked)
            {
                // deselect
                // remove tile from player's properties
                RemovePropertyFromPlayer(m_selectedPlayer, label.Text);
                panel.BackColor = m_defaultTileColor;
            }
            else
            {
                // select
                // add tile to player's properties
                AssignPropertyToPlayer(m_selectedPlayer, label.Text);
                panel.BackColor = m_selectedTileColor;
            }

            // check box again -> see beggining of OnStreetTileClicked()
            checkBox.Checked = !checkBox.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="status"></param>
        /// <param name="allowPropertyModification">indicates if function can cange player's properties</param>
        private void CheckStreetTile(Panel panel, bool status, bool allowPropertyModification = true)
        {
            if (panel == null)
            {
                Debugger.Break(); // TODO: remove
                return;
            }

            CheckBox checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault();
            Label label = panel.Controls.OfType<Label>().FirstOrDefault();

            // nothing is stored inside this tableLayoutPanel cell
            if (checkBox == null || label == null)
            {
                return;
            }

            checkBox.Checked = status;

            if (status)
            {
                // select
                // add tile to player's properties
                if (allowPropertyModification)
                    AssignPropertyToPlayer(m_selectedPlayer, label.Text);
                panel.BackColor = m_selectedTileColor;
            }
            else
            {
                // deselect
                // remove tile from player's properties
                if (allowPropertyModification)
                    RemovePropertyFromPlayer(m_selectedPlayer, label.Text);
                panel.BackColor = m_defaultTileColor;
            }

        }


        private void CheckStreetTileFromCheckBox(object sender)
        {
            // child -> checkBox or label
            CheckBox child = sender as CheckBox;
            Panel panel = child.Parent as Panel;


            if (child != null && panel != null)
            {
                if (child.Checked)
                {
                    panel.BackColor = m_selectedTileColor;
                }
                else
                {
                    panel.BackColor = m_defaultTileColor;
                }
            }
            else
            {
                MessageBox.Show("CheckStreetBoxFromCheckBox: checkBox or Panel is null", "ERROR");
            }
        }

        private void CheckStreetTileFromLabel(object sender)
        {
            var label = sender as Label;
            var panel = label.Parent;

            if (panel == null)
            {
                throw new Exception("panel has no elements inside -> remove event or add controls");
            }

            var checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault(); // getting checkBox from panel (checkBox's parent)

            if (checkBox != null)
            {
                if (checkBox.Checked)
                {
                    panel.BackColor = m_defaultTileColor;

                    // automatically check the checkBox
                    checkBox.Checked = false;
                }
                else
                {
                    panel.BackColor = m_selectedTileColor;

                    // automatically check the checkBox
                    checkBox.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("CheckStreetBoxFromPanel: could not find CheckBox in Panel's controls", "ERROR");
            }
        }

        private void CheckStreetTileFromPanel(TableLayoutPanel tableLayoutPanel, string tag)
        {
            foreach (Panel panel in tableLayoutPanel.Controls)
            {
                if (panel == null)
                {
                    throw new Exception("panel has no elements inside");
                }

                if (panel.Tag.ToString() != tag)
                    continue;

                var checkBox = panel.Controls.OfType<CheckBox>().FirstOrDefault(); // getting checkBox from panel (checkBox's parent)

                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        panel.BackColor = m_defaultTileColor;

                        // automatically check the checkBox
                        checkBox.Checked = false;
                    }
                    else
                    {
                        panel.BackColor = m_selectedTileColor;

                        // automatically check the checkBox
                        checkBox.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("CheckStreetBoxFromPanel: could not find CheckBox in Panel's controls", "ERROR");
                }
            }
        }


        private void CheckTransportOrServiceTile(Panel panel)
        {
            if (panel != null)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                // replacement for checkBox.Checked, as Transport and Public Service Tiles do not have checkBox inside
                if (panel.BackColor == Color.Ivory)
                {
                    // deselect
                    RemovePropertyFromPlayer(m_selectedPlayer, label.Text);
                    panel.BackColor = m_defaultTileColor;
                }
                else
                {
                    // select
                    AssignPropertyToPlayer(m_selectedPlayer, label.Text);
                    panel.BackColor = m_selectedTileColor;
                }

                m_gameControlHub.UpdateTransportCards(m_selectedPlayer);
                this.RefreshLevelDropdownList();
                this.RefreshPropertyData();
            }
        }

        private void CheckTransportOrServiceTile(Panel panel, bool status, bool allowPropertyModification = true)
        {
            if (panel != null)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                // replacement for checkBox.Checked, as Transport and Public Service Tiles do not have checkBox inside
                if (status)
                {
                    // select
                    // add tile to player's properties
                    if (allowPropertyModification)
                        AssignPropertyToPlayer(m_selectedPlayer, label.Text);
                    panel.BackColor = m_selectedTileColor;
                }
                else
                {
                    // deselect
                    // remove tile from player's properties
                    if (allowPropertyModification)
                        RemovePropertyFromPlayer(m_selectedPlayer, label.Text);
                    panel.BackColor = m_defaultTileColor;
                }

                m_gameControlHub.UpdateTransportCards(m_selectedPlayer);
                this.RefreshLevelDropdownList();
                this.RefreshPropertyData();
            }
        }

        private void CheckTransportOrServiceTileFromLabel(object sender)
        {
            var label = sender as Label;
            var panel = label.Parent;

            if (panel != null)
            {
                // replacement for checkBox.Checked, as Transport and Public Service Tiles do not have checkBox inside
                if (panel.BackColor == Color.Ivory)
                {
                    panel.BackColor = m_defaultTileColor;
                }
                else
                {
                    panel.BackColor = m_selectedTileColor;
                }
            }
        }

        private void CheckTransportOrServiceTileFromPictureBox(object sender)
        {
            var label = sender as PictureBox;
            var panel = label.Parent;

            if (panel != null)
            {
                // replacement for checkBox.Checked, as Transport and Public Service Tiles do not have checkBox inside
                if (panel.BackColor == Color.Ivory)
                {
                    panel.BackColor = m_defaultTileColor;
                }
                else
                {
                    panel.BackColor = m_selectedTileColor;
                }
            }
        }

        private void CheckTransportOrServiceTileFromPanel(TableLayoutPanel tableLayoutPanel, string tag)
        {
            foreach (Panel panel in tableLayoutPanel.Controls)
            {
                if (panel == null)
                {
                    throw new Exception("panel has no elements inside");
                }

                if (panel.Tag.ToString() != tag)
                    continue;

                // replacement for checkBox.Checked, as Transport and Public Service Tiles do not have checkBox inside
                if (panel.BackColor == Color.Ivory)
                {
                    panel.BackColor = m_defaultTileColor;
                }
                else
                {
                    panel.BackColor = m_selectedTileColor;
                }
            }
        }


        public void OnStreetTileClicked(object sender, EventArgs e)
        {
            // reverse checked status so it is checked later without issues
            if (sender is CheckBox)
            {
                ((CheckBox)sender).Checked = !(((CheckBox)sender).Checked);
            }

            Panel panel = (Panel)((Control)sender).Parent;

            if (selectWholeStreet.Checked)
            {
                TableLayoutPanel tlp = panel.Parent as TableLayoutPanel;

                // get if tile is selected by checking panel color
                if (panel.BackColor == m_selectedTileColor)
                {
                    // panel is already selected -> set all to not selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckStreetTile(tile, false);
                        }
                    }
                }
                else
                {
                    // panel is not selected -> set all to selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckStreetTile(tile, true);
                        }
                    }
                }
            }
            else
            {
                CheckStreetTile(panel);
            }
        }

        public void OnTransportTileClicked(object sender, EventArgs e)
        {
            Panel panel = (Panel)((Control)sender).Parent;

            if (selectWholeStreet.Checked)
            {
                TableLayoutPanel tlp = panel.Parent as TableLayoutPanel;

                // get if tile is selected by checking panel color
                if (panel.BackColor == m_selectedTileColor)
                {
                    // panel is already selected -> set all to not selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckTransportOrServiceTile(tile, false);
                        }
                    }
                }
                else
                {
                    // panel is not selected -> set all to selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckTransportOrServiceTile(tile, true);
                        }
                    }
                }
            }
            else
            {
                CheckTransportOrServiceTile(panel);
            }
        }

        public void OnPublicServiceTileClicked(object sender, EventArgs e)
        {
            Panel panel = (Panel)((Control)sender).Parent;

            if (selectWholeStreet.Checked)
            {
                TableLayoutPanel tlp = panel.Parent as TableLayoutPanel;

                // get if tile is selected by checking panel color
                if (panel.BackColor == m_selectedTileColor)
                {
                    // panel is already selected -> set all to not selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckTransportOrServiceTile(tile, false);
                        }
                    }
                }
                else
                {
                    // panel is not selected -> set all to selected
                    foreach (Panel tile in tlp.Controls)
                    {
                        if (tile.Tag.ToString() == panel.Tag.ToString())
                        {
                            CheckTransportOrServiceTile(tile, true);
                        }
                    }
                }
            }
            else
            {
                CheckTransportOrServiceTile(panel);
            }
        }


        public void OnTileMouseEnter(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                var panel = label.Parent as Panel;

                if (panel.BackColor == m_defaultTileColor)
                {
                    panel.BackColor = m_hoverTileColor;
                }
            }
            else if (sender is PictureBox)
            {
                var pictureBox = sender as PictureBox;
                var panel = pictureBox.Parent as Panel;

                if (panel.BackColor == m_defaultTileColor)
                {
                    panel.BackColor = m_hoverTileColor;
                }
            }
        }

        public void OnTileMouseLeave(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                var panel = label.Parent as Panel;

                if (panel.BackColor == m_hoverTileColor)
                {
                    panel.BackColor = m_defaultTileColor;
                }
            }
            else if (sender is PictureBox)
            {
                var pictureBox = sender as PictureBox;
                var panel = pictureBox.Parent as Panel;

                if (panel.BackColor == m_hoverTileColor)
                {
                    panel.BackColor = m_defaultTileColor;
                }
            }
        }

        private void OnTileMouseUpdate(object sender, MouseEventArgs e)
        {
            if (sender is Label)
            {
                var label = sender as Label;
                var panel = label.Parent as Panel;

                if (panel.BackColor == m_defaultTileColor)
                {
                    panel.BackColor = m_hoverTileColor;
                }
            }
            else if (sender is PictureBox)
            {
                var pictureBox = sender as PictureBox;
                var panel = pictureBox.Parent as Panel;

                if (panel.BackColor == m_defaultTileColor)
                {
                    panel.BackColor = m_hoverTileColor;
                }
            }
        }

        private void playerEntryDataBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.playerEntryDataBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.simulationEntryData_DataSet);

        }


        public void OnSelectedPlayerChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            int playerIndex = Convert.ToInt32(button.Text) - 1; // -1 because player indexation in buttons starts from 1

            var player = m_gameControlHub.Players[playerIndex];
            m_selectedPlayer = player;

            playerNameLabel.Text = player.Name;

            RefreshSelectedTiles();
            RefreshPlayerPropertiesListBox();
            RefreshPropertyData();
        }


        private void RefreshPlayerPropertiesListBox()
        {
            playerPropertiesListBox.Items.Clear();

            foreach (var propertyIndex in m_selectedPlayer.OwnedTiles)
            {
                var tile = m_gameControlHub.Tiles[propertyIndex];
                playerPropertiesListBox.Items.Add(tile.Name);
            }

            if (m_selectedPlayer.OwnedTiles.Count > 0)
            {
                playerPropertiesListBox.SelectedItem = playerPropertiesListBox.Items[0];
            }
        }

        /// <summary>
        /// Refreshes price table, value label and color label
        /// </summary>
        /// <remarks>Put this function under RefreshLevelDropdownList()</remarks>
        private void RefreshPropertyData()
        {
            Action ClearData = () =>
            {
                foreach (Label label in tableLayoutPanel5.Controls)
                    label.Text = string.Empty;

                tileValueLabel.Text = string.Empty;
                tileColorPB.BackColor = Color.FromKnownColor(KnownColor.Control);
            };


            Action ClearColors = () =>
            {
                // reset all highlits
                foreach (Label label in tableLayoutPanel5.Controls)
                {
                    label.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            };


            // when there is nothing inside -> clear the table
            if (playerPropertiesListBox.Items.Count == 0)
            {
                ClearData();
                ClearColors();
                return;
            }

            // when nothing is selected -> clear the table
            if (playerPropertiesListBox.SelectedItem == null)
            {
                ClearData();
                ClearColors();
                return;
            }

            string selectedItem = playerPropertiesListBox.SelectedItem as string; // alias
            SimulationLayer.Tile selectedTile = null;

            for (int i = 0; i < m_gameControlHub.Tiles.Count; i++)
            {
                if (m_gameControlHub.Tiles[i].Name == selectedItem)
                {
                    selectedTile = m_gameControlHub.Tiles[i];
                }
            }


            // double check if selected tile is not null
            if (selectedTile == null)
            {
                ClearData();
            }

            int level = 0;
            foreach (Label label in tableLayoutPanel5.Controls)
            {
                // don't display anything if tile is worthless
                if (selectedTile.Price.GetPrice(level) > 0)
                    label.Text = selectedTile.Price.GetPrice(level).ToString();
                else
                    label.Text = string.Empty;
                level++;
            }

            // tile value and street color
            tileValueLabel.Text = selectedTile.Value.ToString();
            tileColorPB.BackColor = SimulationLayer.Utils.Convert.SysColorToSimColor(selectedTile.Color);


            // highlight current price label

            // reset all highlights
            ClearColors();

            // highlight price label
            if (levelSelectorComboBox.SelectedIndex >= 0 && levelSelectorComboBox.SelectedIndex <= 5)
            {
                tableLayoutPanel5.Controls[levelSelectorComboBox.SelectedIndex].BackColor = Color.FromKnownColor(KnownColor.ButtonHighlight);
            }

        }


        private void RefreshLevelDropdownList()
        {
            string selectedItem = playerPropertiesListBox.SelectedItem as string; // alias
            SimulationLayer.Tile selectedTile = null;

            // get selected tile
            for (int i = 0; i < m_gameControlHub.Tiles.Count; i++)
            {
                if (m_gameControlHub.Tiles[i].Name == selectedItem)
                {
                    selectedTile = m_gameControlHub.Tiles[i];
                }
            }

            if (selectedTile == null)
            {
                return;
            }

            // special cases for transport and utility cards
            if (selectedTile.Properties.Contains(SimulationLayer.Property.isTransport))
            {
                levelSelectorComboBox.Enabled = false;
                levelSelectorComboBox.Text = string.Concat("Level ", selectedTile.Level.ToString());
                return;
            }
            else if (selectedTile.Properties.Contains(SimulationLayer.Property.isPublicService))
            {
                levelSelectorComboBox.Enabled = false;
                levelSelectorComboBox.Text = string.Empty;
                return;
            }

            // level dropdown list
            if (maxLevelCheckBox.Checked)
            {
                // is it street tile?
                if (!selectedTile.Properties.Contains(SimulationLayer.Property.isStreetTile))
                {
                    levelSelectorComboBox.Text = string.Empty;
                }
                else
                {
                    levelSelectorComboBox.Text = "Level 5";
                }

                levelSelectorComboBox.Enabled = false;
            }
            else
            {
                if (playerPropertiesListBox.Items.Count == 0)
                {
                    levelSelectorComboBox.Text = string.Empty;
                    levelSelectorComboBox.Enabled = false;
                }
                if (playerPropertiesListBox.SelectedIndex == -1)
                {
                    levelSelectorComboBox.Text = string.Empty;
                    levelSelectorComboBox.Enabled = false;
                }

                // check if property can have level
                bool enabled = true;

                // is it street tile?
                if (!selectedTile.Properties.Contains(SimulationLayer.Property.isStreetTile))
                {
                    enabled = false;
                }
                else
                {
                    // check if player has more properties of that color
                    var color = selectedTile.Color;
                    int tile_count = 0;

                    // brown and blue streets have only 2 members
                    if (color == SimulationLayer.Color.Brown || color == SimulationLayer.Color.Blue)
                    {
                        // add one "virtual" tile so in the end both streets have 3 properties (2 normal + 1 virtual), as other streets have
                        tile_count = 1;
                    }

                    foreach (int porpertyIndex in m_selectedPlayer.OwnedTiles)
                    {
                        if (m_gameControlHub.Tiles[porpertyIndex].Color == color)
                        {
                            tile_count++;
                        }
                    }

                    if (tile_count != 3)
                    {
                        enabled = false;
                    }
                }

                levelSelectorComboBox.Enabled = enabled;
                levelSelectorComboBox.Text = string.Concat("Level ", selectedTile.Level.ToString());
            }
        }


        private void RefreshSelectedTiles()
        {
            List<string> properties = new List<string>();

            foreach (var propIndex in m_selectedPlayer.OwnedTiles)
            {
                properties.Add(m_gameControlHub.Tiles[propIndex].Name);
            }

            // left street panel
            foreach (Panel panel in tableLayoutPanel1.Controls)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                if (properties.Contains(label.Text))
                {
                    CheckStreetTile(panel, true);
                }
            }


            // SELECTING TILES

            // uncheck all tiles
            foreach (Panel panel in tableLayoutPanel1.Controls)
            {
                CheckStreetTile(panel, false, false);
            }
            foreach (Panel panel in tableLayoutPanel2.Controls)
            {
                CheckStreetTile(panel, false, false);
            }
            foreach (Panel panel in tableLayoutPanel3.Controls)
            {
                CheckTransportOrServiceTile(panel, false, false);
            }
            foreach (Panel panel in tableLayoutPanel4.Controls)
            {
                CheckTransportOrServiceTile(panel, false, false);
            }



            // right street panel
            foreach (Panel panel in tableLayoutPanel1.Controls)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                if (properties.Contains(label.Text))
                {
                    CheckStreetTile(panel, true);
                }
            }

            // left street panel
            foreach (Panel panel in tableLayoutPanel2.Controls)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                if (properties.Contains(label.Text))
                {
                    CheckStreetTile(panel, true);
                }
            }

            // transport panel
            foreach (Panel panel in tableLayoutPanel3.Controls)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                if (properties.Contains(label.Text))
                {
                    CheckTransportOrServiceTile(panel, true);
                }
            }

            // public service panel
            foreach (Panel panel in tableLayoutPanel4.Controls)
            {
                Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                if (properties.Contains(label.Text))
                {
                    CheckTransportOrServiceTile(panel, true);
                }
            }




            // ENABLING TILES

            // enable all
            foreach (Panel panel in tableLayoutPanel1.Controls)
            {
                EnableTile(panel, true);
            }
            foreach (Panel panel in tableLayoutPanel2.Controls)
            {
                EnableTile(panel, true);
            }
            foreach (Panel panel in tableLayoutPanel3.Controls)
            {
                EnableTile(panel, true);
            }
            foreach (Panel panel in tableLayoutPanel4.Controls)
            {
                EnableTile(panel, true);
            }


            // wrap in lambda to save space
            Action<TableLayoutPanel> DisableIfOwned = (tablepanel) =>
            {
                foreach (var player in m_gameControlHub.Players)
                {
                    // don't check selected Player
                    if (player == m_selectedPlayer)
                        continue;

                    // find player properties
                    List<string> prop = new List<string>(); // player's properties

                    foreach (int index in player.OwnedTiles)
                    {
                        prop.Add(new string(m_gameControlHub.Tiles[index].Name.ToCharArray()));
                    }

                    foreach (Panel panel in tablepanel.Controls)
                    {
                        Label label = panel.Controls.OfType<Label>().FirstOrDefault();

                        if (prop.Contains(label.Text))
                        {
                            EnableTile(panel, false);
                        }
                    }
                }
            };


            DisableIfOwned(tableLayoutPanel1);
            DisableIfOwned(tableLayoutPanel2);
            DisableIfOwned(tableLayoutPanel3);
            DisableIfOwned(tableLayoutPanel4);
        }

        private void playerPropertiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLevelDropdownList();
            RefreshPropertyData();
        }



        private void EnableTile(Panel panel, bool enable)
        {
            if (panel.Tag != null && panel != null)
            {
                foreach (Control control in panel.Controls)
                {
                    control.Enabled = enable;
                }

                panel.Enabled = enable;
            }
        }


        private void includeCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            // do nothing: you cannot have less than 2 players
        }

        private void includeCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            // do nothing: you cannot have less than 2 players
        }

        private void includeCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                playerNameTextBox3.Text = "Player3";
                startMoneyTB3.Text = "1500";
                startDebtTB3.Text = "0";

                player3Button.Enabled = true;
                playerNameTextBox3.Enabled = true;
                startMoneyTB3.Enabled = true;
                startDebtTB3.Enabled = true;
                
                // select the player
                m_selectedPlayer = m_gameControlHub.Players[2];
                player3Button.Checked = true;

                RefreshSelectedTiles();
                RefreshPlayerPropertiesListBox();
                RefreshPropertyData();

            }
            else // disable every control
            {
                if (player3Button.Checked)
                {
                    player1Button.Checked = true; // select default player
                    player3Button.Checked = false;
                }

                playerNameTextBox3.Text = string.Empty;
                startMoneyTB3.Text = string.Empty;
                startDebtTB3.Text = string.Empty;

                player3Button.Enabled = false;
                playerNameTextBox3.Enabled = false;
                startMoneyTB3.Enabled = false;
                startDebtTB3.Enabled = false;
            }
        }

        private void includeCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                playerNameTextBox4.Text = "Player4";
                startMoneyTB4.Text = "1500";
                startDebtTB4.Text = "0";

                player4Button.Enabled = true;
                playerNameTextBox4.Enabled = true;
                startMoneyTB4.Enabled = true;
                startDebtTB4.Enabled = true;

                // select the player
                m_selectedPlayer = m_gameControlHub.Players[3];
                player4Button.Checked = true;

                RefreshSelectedTiles();
                RefreshPlayerPropertiesListBox();
                RefreshPropertyData();
            }
            else // disable every control
            {
                if (player4Button.Checked)
                {
                    player1Button.Checked = true; // select default player
                    player4Button.Checked = false;
                }

                playerNameTextBox4.Text = string.Empty;
                startMoneyTB4.Text = string.Empty;
                startDebtTB4.Text = string.Empty;

                player4Button.Enabled = false;
                playerNameTextBox4.Enabled = false;
                startMoneyTB4.Enabled = false;
                startDebtTB4.Enabled = false;
            }
        }

        private void includeCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                playerNameTextBox5.Text = "Player5";
                startMoneyTB5.Text = "1500";
                startDebtTB5.Text = "0";

                player5Button.Enabled = true;
                playerNameTextBox5.Enabled = true;
                startMoneyTB5.Enabled = true;
                startDebtTB5.Enabled = true;

                // select the player
                m_selectedPlayer = m_gameControlHub.Players[4];
                player5Button.Checked = true;

                RefreshSelectedTiles();
                RefreshPlayerPropertiesListBox();
                RefreshPropertyData();
            }
            else // disable every control
            {
                if (player5Button.Checked)
                {
                    player1Button.Checked = true; // select default player
                    player5Button.Checked = false;
                }

                playerNameTextBox5.Text = string.Empty;
                startMoneyTB5.Text = string.Empty;
                startDebtTB5.Text = string.Empty;

                player5Button.Enabled = false;
                playerNameTextBox5.Enabled = false;
                startMoneyTB5.Enabled = false;
                startDebtTB5.Enabled = false;
            }
        }

        private void includeCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Checked)
            {
                playerNameTextBox6.Text = "Player6";
                startMoneyTB6.Text = "1500";
                startDebtTB6.Text = "0";

                player6Button.Enabled = true;
                playerNameTextBox6.Enabled = true;
                startMoneyTB6.Enabled = true;
                startDebtTB6.Enabled = true;

                // select the player
                m_selectedPlayer = m_gameControlHub.Players[5];
                player6Button.Checked = true;

                RefreshSelectedTiles();
                RefreshPlayerPropertiesListBox();
                RefreshPropertyData();
            }
            else // disable every control
            {
                if (player6Button.Checked)
                {
                    player1Button.Checked = true; // select default player
                    player6Button.Checked = false;
                }

                playerNameTextBox6.Text = string.Empty;
                startMoneyTB6.Text = string.Empty;
                startDebtTB6.Text = string.Empty;

                player6Button.Enabled = false;
                playerNameTextBox6.Enabled = false;
                startMoneyTB6.Enabled = false;
                startDebtTB6.Enabled = false;
            }
        }

        private void ClearPropertiesButton_Click(object sender, EventArgs e)
        {
            if (m_selectedPlayer != SimulationLayer.GameControlHub.emptyPlayer)
            {
                for (int i = m_selectedPlayer.OwnedTiles.Count - 1; i >= 0; i--)
                {
                    m_selectedPlayer.RemoveOwnership(m_gameControlHub.Tiles[m_selectedPlayer.OwnedTiles.ElementAt(i)]);
                }
            }

            RefreshPlayerPropertiesListBox();
            RefreshLevelDropdownList();
            RefreshPropertyData();
            RefreshSelectedTiles();
        }

        private void levelSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = playerPropertiesListBox.SelectedItem as string; // alias
            SimulationLayer.Tile selectedTile = null;

            for (int i = 0; i < m_gameControlHub.Tiles.Count; i++)
            {
                if (m_gameControlHub.Tiles[i].Name == selectedItem)
                {
                    selectedTile = m_gameControlHub.Tiles[i];
                }
            }

            if (selectedTile == null)
            {
                return;
            }

            switch (levelSelectorComboBox.SelectedIndex)
            {
                case -1:
                    // do nothing
                    break;
                case 0:
                    selectedTile.Level = 0;
                    break;
                case 1:
                    selectedTile.Level = 1;
                    break;
                case 2:
                    selectedTile.Level = 2;
                    break;
                case 3:
                    selectedTile.Level = 3;
                    break;
                case 4:
                    selectedTile.Level = 4;
                    break;
                case 5:
                    selectedTile.Level = 5;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            RefreshLevelDropdownList();
            RefreshPropertyData();
        }

        private void maxLevelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshLevelDropdownList();
        }


        public void PlayerSettingsTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int playerIndex = Convert.ToInt32(textBox.Tag);

            // is empty
            if (textBox.Text.Length == 0)
            {
                m_gameControlHub.Players[playerIndex].StartMoney = 0;
                return;
            }

            // is not text
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                return;
            }

            m_gameControlHub.Players[playerIndex].StartMoney = Convert.ToInt32(textBox.Text);
        }

        public void OnPlayerSettingsLeave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // is empty
            if (textBox.Text.Length == 0)
            {
                textBox.Text = "0";
                return;
            }
        }

        private void singleSimRB_CheckedChanged(object sender, EventArgs e)
        {
            simulatedGamesSelector.Enabled = false;
            m_simulationMode = SimulationMode.SingleSim;
        }

        private void multiSimRB_CheckedChanged(object sender, EventArgs e)
        {
            simulatedGamesSelector.Enabled = true;
            m_simulationMode = SimulationMode.MultiSim;
        }

        private void moneyPerStartSelector_ValueChanged(object sender, EventArgs e)
        {
            SimulationLayer.GameControlHub.START_TILE_PAYOUT = Convert.ToInt32(moneyPerStartSelector.Value);
        }

        private void maxDebtSelector_ValueChanged(object sender, EventArgs e)
        {
            SimulationLayer.GameControlHub.MAX_DEBT = Convert.ToInt32(maxDebtSelector.Value);
        }

        private void simulatedGamesSelector_ValueChanged(object sender, EventArgs e)
        {
            SimulationLayer.GameControlHub.GAMES_PER_SIMULATION = Convert.ToInt32(simulatedGamesSelector.Value);
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            startSimulationButton.Enabled = false;
            maxDebtSelector.Enabled = false;
            moneyPerStartSelector.Enabled = false;
            logDataButton.Enabled = false;
            LoadPlayers();
            m_gameControlHub.DataCollector.Clear();
            progressBar1.Value = 0;
            m_progressBarValue = 0;

            backgroundWorker1.RunWorkerAsync();
            //var output = m_gameControlHub.Run(m_simulationMode == SimulationMode.MultiSim);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            m_simOutput = m_gameControlHub.Run(m_simulationMode == SimulationMode.MultiSim);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (m_simulationMode == SimulationMode.MultiSim)
                tabControl1.SelectedIndex = 0;
            else
                tabControl1.SelectedIndex = 1;

            var output = m_simOutput;

            UpdateSinglegameOutputTab(output.Item1, output.Item2);
            UpdateMultigameOutputTab(output.Item1, output.Item2);
            RefreshSimulation();
            startSimulationButton.Enabled = true;
            maxDebtSelector.Enabled = true;
            moneyPerStartSelector.Enabled = true;

            logDataButton.Enabled = true;

            progressBar1.Value = 100;
        }

        private void UpdateSinglegameOutputTab(SimulationLayer.SimualationExitCode exitCode, int turns)
        {
            if (m_simulationMode == SimulationMode.SingleSim)
            {
                // labels
                switch (exitCode)
                {
                    case SimulationLayer.SimualationExitCode.Default:
                        simStatusLabel.BackColor = Color.Honeydew;
                        simStatusLabel.Text = "Default";
                        break;
                    case SimulationLayer.SimualationExitCode.TurnLimitExceeded:
                        simStatusLabel.BackColor = Color.AliceBlue;
                        simStatusLabel.Text = "Turn limit exceeded";
                        break;
                    case SimulationLayer.SimualationExitCode.BankWentBankrupt:
                        simStatusLabel.BackColor = Color.LightYellow;
                        simStatusLabel.Text = "Bank out of money";
                        break;
                    case SimulationLayer.SimualationExitCode.Error:
                        simStatusLabel.BackColor = Color.MistyRose;
                        simStatusLabel.Text = "ERROR";
                        break;
                }

                if (exitCode == SimulationLayer.SimualationExitCode.TurnLimitExceeded)
                    simEndedInLabel.Text = "infinite turns";
                else
                    simEndedInLabel.Text = turns.ToString() + " turns";

                // players
                simOSMPlayerLabel1.Text = m_gameControlHub.Players[0].Name;
                simOSMPlayerLabel2.Text = m_gameControlHub.Players[1].Name;
                simOSMPlayerLabel3.Text = m_gameControlHub.Players[2].Name;
                simOSMPlayerLabel4.Text = m_gameControlHub.Players[3].Name;
                simOSMPlayerLabel5.Text = m_gameControlHub.Players[4].Name;
                simOSMPlayerLabel6.Text = m_gameControlHub.Players[5].Name;

                simOSMMoneyLabel1.Text = m_gameControlHub.Players[0].Money.ToString();
                simOSMMoneyLabel2.Text = m_gameControlHub.Players[1].Money.ToString();
                simOSMMoneyLabel3.Text = m_gameControlHub.Players[2].Money.ToString();
                simOSMMoneyLabel4.Text = m_gameControlHub.Players[3].Money.ToString();
                simOSMMoneyLabel5.Text = m_gameControlHub.Players[4].Money.ToString();
                simOSMMoneyLabel6.Text = m_gameControlHub.Players[5].Money.ToString();

                simOSMDebtLabel1.Text = m_gameControlHub.Players[0].Debt.ToString();
                simOSMDebtLabel2.Text = m_gameControlHub.Players[1].Debt.ToString();
                simOSMDebtLabel3.Text = m_gameControlHub.Players[2].Debt.ToString();
                simOSMDebtLabel4.Text = m_gameControlHub.Players[3].Debt.ToString();
                simOSMDebtLabel5.Text = m_gameControlHub.Players[4].Debt.ToString();
                simOSMDebtLabel6.Text = m_gameControlHub.Players[5].Debt.ToString();

                Color GetColor(SimulationLayer.Player player)
                {
                    if (!player.Playing)
                        return Color.LightGray;
                    if (player.Lost_games > 0)
                        return Color.Red;
                    if (player.Won_games > 0)
                        return Color.LimeGreen;
                    return Color.White;
                }

                simOSMWinInfoLabel1.BackColor = GetColor(m_gameControlHub.Players[0]);
                simOSMWinInfoLabel2.BackColor = GetColor(m_gameControlHub.Players[1]);
                simOSMWinInfoLabel3.BackColor = GetColor(m_gameControlHub.Players[2]);
                simOSMWinInfoLabel4.BackColor = GetColor(m_gameControlHub.Players[3]);
                simOSMWinInfoLabel5.BackColor = GetColor(m_gameControlHub.Players[4]);
                simOSMWinInfoLabel6.BackColor = GetColor(m_gameControlHub.Players[5]);
            }
        }

        private void UpdateMultigameOutputTab(SimulationLayer.SimualationExitCode exitCode, int turns)
        {
            if (m_simulationMode == SimulationMode.MultiSim)
            {
                // labels
                switch (exitCode)
                {
                    case SimulationLayer.SimualationExitCode.Default:
                        simStatusLabelMM.BackColor = Color.Honeydew;
                        simStatusLabelMM.Text = "Default";
                        break;
                    case SimulationLayer.SimualationExitCode.TurnLimitExceeded:
                        simStatusLabelMM.BackColor = Color.Honeydew;
                        simStatusLabelMM.Text = "Default";
                        break;
                    case SimulationLayer.SimualationExitCode.BankWentBankrupt:
                        simStatusLabelMM.BackColor = Color.Honeydew;
                        simStatusLabelMM.Text = "Default";
                        break;
                    case SimulationLayer.SimualationExitCode.Error:
                        simStatusLabelMM.BackColor = Color.MistyRose;
                        simStatusLabelMM.Text = "ERROR";
                        break;
                }

                decimal gamecount = SimulationLayer.GameControlHub.GAMES_PER_SIMULATION;

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.Series[2].Points.Clear();

                var player = m_gameControlHub.Players[5];
                if (player.Playing)
                {
                    chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                    chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                    chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);
                }

                player = m_gameControlHub.Players[4];
                if (player.Playing)
                {
                    chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                    chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                    chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);
                }

                player = m_gameControlHub.Players[3];
                if (player.Playing)
                {
                    chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                    chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                    chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);
                }

                player = m_gameControlHub.Players[2];
                if (player.Playing)
                {
                    chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                    chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                    chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);
                }


                player = m_gameControlHub.Players[1];
                chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);

                player = m_gameControlHub.Players[0];
                chart1.Series[0].Points.AddXY(player.Name, player.Won_games / gamecount);
                chart1.Series[1].Points.AddXY(player.Name, (gamecount - (player.Won_games + player.Lost_games)) / gamecount);
                chart1.Series[2].Points.AddXY(player.Name, player.Lost_games / gamecount);
            }
        }

        private void openChartButtonSM_Click(object sender, EventArgs e)
        {
            m_formSMD = new FormSMD();
            m_formSMD.Show();
        }

        private void openChartButtonMM_Click(object sender, EventArgs e)
        {
            m_formMMD = new FormMMD();
            m_formMMD.Show();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportSimulationFromFile(openFileDialog1.FileName);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportSimulationFromFile(saveFileDialog1.FileName);
            }
        }

        private void logDataButton_Click(object sender, EventArgs e)
        {
            if (saveLogFileDialog.ShowDialog() == DialogResult.OK)
            {
                CreateLogFile(saveLogFileDialog.FileName);
            }
        }
    }
}
