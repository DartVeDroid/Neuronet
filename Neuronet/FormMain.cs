using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neuronet
{
  using tfloat = Double;
  public partial class TFormMain : Form
  {
    public TFormMain()
    {
      InitializeComponent();
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = !CanSaveGo();
    }

    #region "Нейронная сеть" ("Файл")
    private bool TrySaveAs()
    {
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        if (Global.Neuronet.SaveToFile(saveFileDialog.FileName))
        {
          FileSaveToolStripMenuItem.Enabled = false;
          Text = Global.Text.TitleFileName(saveFileDialog.FileName);
          return true;
        }
        else
        {
          MessageBox.Show("Нейросеть не была сохранена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return false;
        }
      }
      else return false;
    }

    private bool TrySave()
    {
      bool TrueVariant()
      {
        FileSaveToolStripMenuItem.Enabled = false;
        return true;
      }
      if (Global.Neuronet.SaveToFile())
      {
        return TrueVariant();
      }
      else
      {
        if (MessageBox.Show("Файл нейросети недоступен. Выберите место сохранения", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
        {
          return (TrySaveAs() ? TrueVariant() : false);
        }
        else return false;
      }
    }

    private bool CanSaveGo()
    {
      if (Global.Neuronet != null && Global.Neuronet.IsNeedToSave)
      {
        switch (MessageBox.Show("Нейросеть была изменена. Сохранить изменения?", "Сохранить изменения?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
        {
          case DialogResult.Yes: return ((Global.Neuronet.PathCurrent != null) ? TrySave() : TrySaveAs());
          case DialogResult.No: return true;
          default: return false;
        }
      }
      else return true;
    }

    private void SetMenuItemsEnabled(bool valueSave, bool valueOthers)
    {
      FileSaveToolStripMenuItem.Enabled = valueSave;
      FileSaveAsToolStripMenuItem.Enabled = valueOthers;
      FileCloseToolStripMenuItem.Enabled = valueOthers;
      ActionsToolStripMenuItem.Enabled = valueOthers;
    }

    private void FileCreateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (CanSaveGo())
      {
        TNeuronet bufNeuronet = null;
        TFormCreate FormCreate = new TFormCreate(bufNeuronet);
        if (FormCreate.ShowDialog() == DialogResult.OK)
        {
          Global.Neuronet = bufNeuronet;
          SetMenuItemsEnabled(true, true);
          Text = Global.Text.TitleDefault;
        }
      }
    }

    private void FileOpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (CanSaveGo() && (openFileDialog.ShowDialog() == DialogResult.OK))
      {
        TNeuronet bufNeuronet = TNeuronet.LoadFromFile(openFileDialog.FileName);
        if (bufNeuronet != null)
        {
          Global.Neuronet = bufNeuronet;
          SetMenuItemsEnabled(false, true);
          Text = Global.Text.TitleFileName(openFileDialog.FileName);
        }
      }
    }

    private void FileSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TrySave();
    }

    private void FileSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      TrySaveAs();
    }        

    private void FileCloseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (CanSaveGo()) {
        Global.Neuronet = null;
        SetMenuItemsEnabled(false, false);
        Text = Global.Text.TitleDefault;
      }
    }
    #endregion

    private void TestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      #region NN setting
      Global.Neuronet = new TNeuronet(4);
      Global.Neuronet.SetLayer(0, Global.FIDNone, 2);
      Global.Neuronet.SetLayer(1, Global.FIDLogistic, 10, Global.LRTMomentum, Global.LSTLogistic, 1, 1, 0.2, 0);
      Global.Neuronet.ConnectLayers(0, 1, true, new double[] { 0, 1 });
      Global.Neuronet.SetLayer(2, Global.FIDHTangent, 10, Global.LRTMomentum, Global.LSTLogistic, 1, 1, 0.2, 0);
      Global.Neuronet.ConnectLayers(1, 2, true, new double[] { 0, 1 });
      Global.Neuronet.SetLayer(3, Global.FIDLinear, 1, Global.LRTMomentum, Global.LSTLogistic, 1, 1, 0.2, 0);
      Global.Neuronet.ConnectLayers(2, 3, true, new double[] { 0, 1 });
      FileSaveToolStripMenuItem.Enabled = true;
      FileSaveAsToolStripMenuItem.Enabled = true;
      FileCloseToolStripMenuItem.Enabled = true;
      ActionsToolStripMenuItem.Enabled = true;
      #endregion
      #region Data setting
      List<tfloat[]> inputs = new List<tfloat[]>(4);
      inputs.Add(new tfloat[2]);
      inputs[0][0] = 0;
      inputs[0][1] = 0;
      inputs.Add(new tfloat[2]);
      inputs[1][0] = 0;
      inputs[1][1] = 1;
      inputs.Add(new tfloat[2]);
      inputs[2][0] = 1;
      inputs[2][1] = 0;
      inputs.Add(new tfloat[2]);
      inputs[3][0] = 1;
      inputs[3][1] = 1;
      List<tfloat[]> outputs = new List<tfloat[]>(4);
      outputs.Add(new tfloat[2]);
      outputs[0][0] = 0;
      outputs.Add(new tfloat[2]);
      outputs[1][0] = 1;
      outputs.Add(new tfloat[2]);
      outputs[2][0] = 1;
      outputs.Add(new tfloat[2]);
      outputs[3][0] = 0;
      #endregion
      #region Training
      double[] sda = new double[1001];
      double[] sdm = new double[1001];
      DateTime start = DateTime.Now;
      bool result = Global.Neuronet.TrainMLP(inputs, outputs, false, true, false, false, true, out int passes, 10000, 0.1, 0.1, sda, sdm);
      #endregion
      #region Out
      string ms = (DateTime.Now - start).Milliseconds.ToString();
      if (ms.Length == 2)
      {
        ms = "0" + ms;
      }
      else if (ms.Length == 1)
      {
        ms = "00" + ms;
      }
      Text = result.ToString() + ", " + passes.ToString() + " итераций, " + ((DateTime.Now - start).Seconds).ToString() + "." + ms + "с";
      chart.Series[0].Points.Clear();
      chart.Series[1].Points.Clear();
      /*for (int i = 0; i <= passes; i++)
      {
        chart.Series[0].Points.AddXY(i, sda[i]);
        chart.Series[1].Points.AddXY(i, sdm[i]);
      }
      chart.Show();*/
      #endregion
    }
  }
}
