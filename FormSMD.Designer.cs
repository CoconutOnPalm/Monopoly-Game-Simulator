namespace Monopoly_Game_Simulator
{
    partial class FormSMD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series43 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series44 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series45 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series46 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series47 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series48 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series49 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series50 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series51 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series52 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series53 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series54 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series55 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series56 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSMD));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea10.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea10.AxisY.InterlacedColor = System.Drawing.Color.Navy;
            chartArea10.AxisY.LineColor = System.Drawing.Color.Navy;
            chartArea10.AxisY.Title = "Avarage Profit";
            chartArea10.AxisY2.InterlacedColor = System.Drawing.Color.Maroon;
            chartArea10.AxisY2.LineColor = System.Drawing.Color.Maroon;
            chartArea10.AxisY2.Title = "Avarage Passes";
            chartArea10.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chart1.Legends.Add(legend10);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series43.ChartArea = "ChartArea1";
            series43.IsValueShownAsLabel = true;
            series43.Legend = "Legend1";
            series43.Name = "Avg Profit";
            series44.BorderWidth = 4;
            series44.ChartArea = "ChartArea1";
            series44.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series44.Legend = "Legend1";
            series44.Name = "Avg passes";
            series44.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.chart1.Series.Add(series43);
            this.chart1.Series.Add(series44);
            this.chart1.Size = new System.Drawing.Size(914, 190);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            this.chart1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSMD_KeyDown);
            // 
            // chart2
            // 
            chartArea11.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea11);
            legend11.Name = "Legend1";
            this.chart2.Legends.Add(legend11);
            this.chart2.Location = new System.Drawing.Point(3, 199);
            this.chart2.Name = "chart2";
            series45.BorderWidth = 3;
            series45.ChartArea = "ChartArea1";
            series45.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series45.Legend = "Legend1";
            series45.Name = "Player 1";
            series46.BorderWidth = 3;
            series46.ChartArea = "ChartArea1";
            series46.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series46.Legend = "Legend1";
            series46.Name = "Player 2";
            series47.BorderWidth = 3;
            series47.ChartArea = "ChartArea1";
            series47.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series47.Legend = "Legend1";
            series47.Name = "Player 3";
            series48.BorderWidth = 3;
            series48.ChartArea = "ChartArea1";
            series48.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series48.Legend = "Legend1";
            series48.Name = "Player 4";
            series49.BorderWidth = 3;
            series49.ChartArea = "ChartArea1";
            series49.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series49.Legend = "Legend1";
            series49.Name = "Player 5";
            series50.BorderWidth = 3;
            series50.ChartArea = "ChartArea1";
            series50.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series50.Legend = "Legend1";
            series50.Name = "Player 6";
            this.chart2.Series.Add(series45);
            this.chart2.Series.Add(series46);
            this.chart2.Series.Add(series47);
            this.chart2.Series.Add(series48);
            this.chart2.Series.Add(series49);
            this.chart2.Series.Add(series50);
            this.chart2.Size = new System.Drawing.Size(914, 190);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            title7.Name = "Title1";
            title7.Text = "Players\' money change";
            this.chart2.Titles.Add(title7);
            this.chart2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSMD_KeyDown);
            // 
            // chart3
            // 
            chartArea12.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea12);
            legend12.Name = "Legend1";
            this.chart3.Legends.Add(legend12);
            this.chart3.Location = new System.Drawing.Point(3, 395);
            this.chart3.Name = "chart3";
            series51.BorderWidth = 3;
            series51.ChartArea = "ChartArea1";
            series51.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series51.Legend = "Legend1";
            series51.Name = "Player 1";
            series52.BorderWidth = 3;
            series52.ChartArea = "ChartArea1";
            series52.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series52.Legend = "Legend1";
            series52.Name = "Player 2";
            series53.BorderWidth = 3;
            series53.ChartArea = "ChartArea1";
            series53.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series53.Legend = "Legend1";
            series53.Name = "Player 3";
            series54.BorderWidth = 3;
            series54.ChartArea = "ChartArea1";
            series54.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series54.Legend = "Legend1";
            series54.Name = "Player 4";
            series55.BorderWidth = 3;
            series55.ChartArea = "ChartArea1";
            series55.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series55.Legend = "Legend1";
            series55.Name = "Player 5";
            series56.BorderWidth = 3;
            series56.ChartArea = "ChartArea1";
            series56.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series56.Legend = "Legend1";
            series56.Name = "Player 6";
            this.chart3.Series.Add(series51);
            this.chart3.Series.Add(series52);
            this.chart3.Series.Add(series53);
            this.chart3.Series.Add(series54);
            this.chart3.Series.Add(series55);
            this.chart3.Series.Add(series56);
            this.chart3.Size = new System.Drawing.Size(914, 191);
            this.chart3.TabIndex = 3;
            this.chart3.Text = "chart3";
            title8.Name = "Title1";
            title8.Text = "Players\' debt change";
            this.chart3.Titles.Add(title8);
            this.chart3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSMD_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chart1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chart2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(920, 589);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "png";
            this.saveFileDialog1.Filter = "Images|*.png;*.bmp;*.jpg";
            // 
            // FormSMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 589);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSMD";
            this.Text = "Simulation Output Data";
            this.Load += new System.EventHandler(this.OnFormShow);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSMD_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}