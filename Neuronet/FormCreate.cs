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
  public partial class TFormCreate : Form
  {
    private TNeuronet BufNeuronet;
    private byte Stage = 0;
    internal TFormCreate(TNeuronet bufNeuronet)
    {
      BufNeuronet = bufNeuronet;
      InitializeComponent();
    }

    private void SetStage(bool Next)
    {
      if (Next)
      {
        Stage++;
      }
      else
      {
        Stage--;
      }
    }
  }
}
