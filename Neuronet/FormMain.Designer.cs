namespace Neuronet
{
  partial class TFormMain
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileSaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.FileCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.TestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.menuStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ActionsToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.TestToolStripMenuItem});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(704, 24);
      this.menuStrip.TabIndex = 0;
      this.menuStrip.Text = "menuStrip1";
      // 
      // FileToolStripMenuItem
      // 
      this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileCreateToolStripMenuItem,
            this.FileOpenToolStripMenuItem,
            this.FileSaveToolStripMenuItem,
            this.FileSaveAsToolStripMenuItem,
            this.FileCloseToolStripMenuItem});
      this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
      this.FileToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
      this.FileToolStripMenuItem.Text = "Нейронная сеть";
      // 
      // FileCreateToolStripMenuItem
      // 
      this.FileCreateToolStripMenuItem.Name = "FileCreateToolStripMenuItem";
      this.FileCreateToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.FileCreateToolStripMenuItem.Text = "Создать";
      this.FileCreateToolStripMenuItem.Click += new System.EventHandler(this.FileCreateToolStripMenuItem_Click);
      // 
      // FileOpenToolStripMenuItem
      // 
      this.FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem";
      this.FileOpenToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.FileOpenToolStripMenuItem.Text = "Открыть";
      this.FileOpenToolStripMenuItem.Click += new System.EventHandler(this.FileOpenToolStripMenuItem_Click);
      // 
      // FileSaveToolStripMenuItem
      // 
      this.FileSaveToolStripMenuItem.Enabled = false;
      this.FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem";
      this.FileSaveToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.FileSaveToolStripMenuItem.Text = "Сохранить";
      this.FileSaveToolStripMenuItem.Click += new System.EventHandler(this.FileSaveToolStripMenuItem_Click);
      // 
      // FileSaveAsToolStripMenuItem
      // 
      this.FileSaveAsToolStripMenuItem.Enabled = false;
      this.FileSaveAsToolStripMenuItem.Name = "FileSaveAsToolStripMenuItem";
      this.FileSaveAsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.FileSaveAsToolStripMenuItem.Text = "Сохранить как";
      this.FileSaveAsToolStripMenuItem.Click += new System.EventHandler(this.FileSaveAsToolStripMenuItem_Click);
      // 
      // FileCloseToolStripMenuItem
      // 
      this.FileCloseToolStripMenuItem.Enabled = false;
      this.FileCloseToolStripMenuItem.Name = "FileCloseToolStripMenuItem";
      this.FileCloseToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.FileCloseToolStripMenuItem.Text = "Закрыть";
      this.FileCloseToolStripMenuItem.Click += new System.EventHandler(this.FileCloseToolStripMenuItem_Click);
      // 
      // ActionsToolStripMenuItem
      // 
      this.ActionsToolStripMenuItem.Enabled = false;
      this.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem";
      this.ActionsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
      this.ActionsToolStripMenuItem.Text = "Действия";
      // 
      // настройкиToolStripMenuItem
      // 
      this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
      this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
      this.настройкиToolStripMenuItem.Text = "Настройки";
      // 
      // TestToolStripMenuItem
      // 
      this.TestToolStripMenuItem.Name = "TestToolStripMenuItem";
      this.TestToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
      this.TestToolStripMenuItem.Text = "Тест";
      this.TestToolStripMenuItem.Click += new System.EventHandler(this.TestToolStripMenuItem_Click);
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "Нейронная сеть (*.nw)|*.txt|Все файлы (*.*)|*.*";
      // 
      // saveFileDialog
      // 
      this.saveFileDialog.Filter = "Нейронная сеть (*.nw)|*.txt|Все файлы (*.*)|*.*";
      // 
      // chart
      // 
      this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
      chartArea1.AxisX.Minimum = 0D;
      chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
      chartArea1.Name = "ChartArea1";
      this.chart.ChartAreas.Add(chartArea1);
      legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
      legend1.Name = "Legend1";
      this.chart.Legends.Add(legend1);
      this.chart.Location = new System.Drawing.Point(48, 43);
      this.chart.Name = "chart";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.Legend = "Legend1";
      series1.LegendText = "СКО, среднее за эпоху";
      series1.Name = "Series1";
      series2.ChartArea = "ChartArea1";
      series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series2.Legend = "Legend1";
      series2.LegendText = "СКО, максимальное за эпоху";
      series2.Name = "Series2";
      this.chart.Series.Add(series1);
      this.chart.Series.Add(series2);
      this.chart.Size = new System.Drawing.Size(608, 300);
      this.chart.TabIndex = 1;
      this.chart.Visible = false;
      // 
      // TFormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(704, 366);
      this.Controls.Add(this.chart);
      this.Controls.Add(this.menuStrip);
      this.MainMenuStrip = this.menuStrip;
      this.Name = "TFormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Neuronet";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileCreateToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileOpenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileSaveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileSaveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem FileCloseToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.ToolStripMenuItem ActionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem TestToolStripMenuItem;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart;
  }
}

