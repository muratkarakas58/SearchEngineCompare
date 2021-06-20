namespace SearchEngineCompare
{
    partial class Form2
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSearchEngine2 = new System.Windows.Forms.ComboBox();
            this.comboBoxSearchEngine1 = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearchPhrase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearchLoop = new System.Windows.Forms.TextBox();
            this.chartMeasuredParameter1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxShowCount = new System.Windows.Forms.TextBox();
            this.buttonShowChart = new System.Windows.Forms.Button();
            this.chartMeasuredParameter2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMeasuredParameter1Average = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMeasuredParameter2Average = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter1Average)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter2Average)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "İkinci arama Motoru seçimi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "İlk arama Motoru seçimi";
            // 
            // comboBoxSearchEngine2
            // 
            this.comboBoxSearchEngine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchEngine2.FormattingEnabled = true;
            this.comboBoxSearchEngine2.Location = new System.Drawing.Point(324, 90);
            this.comboBoxSearchEngine2.Name = "comboBoxSearchEngine2";
            this.comboBoxSearchEngine2.Size = new System.Drawing.Size(238, 21);
            this.comboBoxSearchEngine2.TabIndex = 11;
            // 
            // comboBoxSearchEngine1
            // 
            this.comboBoxSearchEngine1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchEngine1.FormattingEnabled = true;
            this.comboBoxSearchEngine1.Location = new System.Drawing.Point(19, 90);
            this.comboBoxSearchEngine1.Name = "comboBoxSearchEngine1";
            this.comboBoxSearchEngine1.Size = new System.Drawing.Size(262, 21);
            this.comboBoxSearchEngine1.TabIndex = 10;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(596, 90);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(128, 23);
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "Ara";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxSearchPhrase
            // 
            this.textBoxSearchPhrase.Location = new System.Drawing.Point(102, 31);
            this.textBoxSearchPhrase.Name = "textBoxSearchPhrase";
            this.textBoxSearchPhrase.Size = new System.Drawing.Size(471, 20);
            this.textBoxSearchPhrase.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Aranacak ifade";
            // 
            // textBoxSearchLoop
            // 
            this.textBoxSearchLoop.Location = new System.Drawing.Point(596, 31);
            this.textBoxSearchLoop.Name = "textBoxSearchLoop";
            this.textBoxSearchLoop.Size = new System.Drawing.Size(128, 20);
            this.textBoxSearchLoop.TabIndex = 14;
            this.textBoxSearchLoop.Text = "5";
            // 
            // chartMeasuredParameter1
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMeasuredParameter1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMeasuredParameter1.Legends.Add(legend1);
            this.chartMeasuredParameter1.Location = new System.Drawing.Point(22, 186);
            this.chartMeasuredParameter1.Name = "chartMeasuredParameter1";
            this.chartMeasuredParameter1.Size = new System.Drawing.Size(705, 300);
            this.chartMeasuredParameter1.TabIndex = 15;
            this.chartMeasuredParameter1.Text = "Deneme";
            title1.Name = "Title1";
            title1.Text = "Sonuç bulma süresi. Milisaniye";
            this.chartMeasuredParameter1.Titles.Add(title1);
            this.chartMeasuredParameter1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Gösterilecek kayıt sayısı";
            // 
            // textBoxShowCount
            // 
            this.textBoxShowCount.Location = new System.Drawing.Point(164, 143);
            this.textBoxShowCount.Name = "textBoxShowCount";
            this.textBoxShowCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxShowCount.TabIndex = 17;
            this.textBoxShowCount.Text = "10";
            // 
            // buttonShowChart
            // 
            this.buttonShowChart.Location = new System.Drawing.Point(300, 143);
            this.buttonShowChart.Name = "buttonShowChart";
            this.buttonShowChart.Size = new System.Drawing.Size(162, 23);
            this.buttonShowChart.TabIndex = 18;
            this.buttonShowChart.Text = "Grafikte göster";
            this.buttonShowChart.UseVisualStyleBackColor = true;
            this.buttonShowChart.Click += new System.EventHandler(this.buttonShowChart_Click);
            // 
            // chartMeasuredParameter2
            // 
            chartArea2.Name = "ChartArea1";
            this.chartMeasuredParameter2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartMeasuredParameter2.Legends.Add(legend2);
            this.chartMeasuredParameter2.Location = new System.Drawing.Point(22, 492);
            this.chartMeasuredParameter2.Name = "chartMeasuredParameter2";
            this.chartMeasuredParameter2.Size = new System.Drawing.Size(705, 300);
            this.chartMeasuredParameter2.TabIndex = 19;
            this.chartMeasuredParameter2.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Arama motorunda gösterilen toplam sonuç sayısı. Adet";
            this.chartMeasuredParameter2.Titles.Add(title2);
            // 
            // chartMeasuredParameter1Average
            // 
            chartArea3.Name = "ChartArea1";
            this.chartMeasuredParameter1Average.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartMeasuredParameter1Average.Legends.Add(legend3);
            this.chartMeasuredParameter1Average.Location = new System.Drawing.Point(743, 186);
            this.chartMeasuredParameter1Average.Name = "chartMeasuredParameter1Average";
            this.chartMeasuredParameter1Average.Size = new System.Drawing.Size(279, 300);
            this.chartMeasuredParameter1Average.TabIndex = 20;
            this.chartMeasuredParameter1Average.Text = "chart1";
            title3.Name = "Title1";
            title3.Text = "Ortalama sonuç bulma süresi. Milisaniye";
            this.chartMeasuredParameter1Average.Titles.Add(title3);
            // 
            // chartMeasuredParameter2Average
            // 
            chartArea4.Name = "ChartArea1";
            this.chartMeasuredParameter2Average.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartMeasuredParameter2Average.Legends.Add(legend4);
            this.chartMeasuredParameter2Average.Location = new System.Drawing.Point(743, 492);
            this.chartMeasuredParameter2Average.Name = "chartMeasuredParameter2Average";
            this.chartMeasuredParameter2Average.Size = new System.Drawing.Size(279, 300);
            this.chartMeasuredParameter2Average.TabIndex = 21;
            this.chartMeasuredParameter2Average.Text = "chart1";
            title4.Name = "Title1";
            title4.Text = "Ortalama arama motorunda gösterilen toplam sonuç sayısı. Adet";
            this.chartMeasuredParameter2Average.Titles.Add(title4);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 829);
            this.Controls.Add(this.chartMeasuredParameter2Average);
            this.Controls.Add(this.chartMeasuredParameter1Average);
            this.Controls.Add(this.chartMeasuredParameter2);
            this.Controls.Add(this.buttonShowChart);
            this.Controls.Add(this.textBoxShowCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chartMeasuredParameter1);
            this.Controls.Add(this.textBoxSearchLoop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxSearchEngine2);
            this.Controls.Add(this.comboBoxSearchEngine1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearchPhrase);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter1Average)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMeasuredParameter2Average)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSearchEngine2;
        private System.Windows.Forms.ComboBox comboBoxSearchEngine1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearchPhrase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearchLoop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeasuredParameter1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxShowCount;
        private System.Windows.Forms.Button buttonShowChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeasuredParameter2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeasuredParameter1Average;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMeasuredParameter2Average;
    }
}