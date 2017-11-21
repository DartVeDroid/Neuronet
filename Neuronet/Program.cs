using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Neuronet
{
  #region Global
  using tfloat = Double;
  static class Global
  {
    //Function IDs
    public const byte FIDNone = 0;
    public const byte FIDLinear= 1;
    public const byte FIDLogistic = 2;
    public const byte FIDHTangent = 3;
    //Learning rule types
    public const byte LRTStep = 0;
    public const byte LRTMomentum = 1;
    //Learning speed types
    public const byte LSTConst = 0;
    public const byte LSTLinear = 1;
    public const byte LSTSqrt = 2;
    public const byte LSTLogistic = 3;
    //Overall
    public static class Text
    {
      public static char DelimiterValues = ';';
      public static char DelimiterIndexes = '|';
      #region Title
      private static string NNExtension = ".nw";
      public static string TitleDefault = Application.ProductName;
      public static string TitleFileName(string fileName)
      {
        if (Path.GetExtension(fileName) == NNExtension)
        {
          return Path.GetFileNameWithoutExtension(fileName) + " - " + Application.ProductName;
        }
        else
        {
          return Path.GetFileName(fileName) + " - " + Application.ProductName;
        }
      }
      #endregion
    }
    public static TNeuronet Neuronet;
  }
  #endregion
  #region TWeight, TNeuron
  struct TWeight
  {
    public TNeuron Neuron;
    public tfloat Value, PreviousDW;
    public TWeight(string value)
    {
      string[] strings = value.Split(Global.Text.DelimiterValues);
      Value = Convert.ToInt32(strings[0]);
      PreviousDW = Convert.ToInt32(strings[1]);
      Neuron = null;
    }
    public new string ToString()
    {
      return Neuron.IndexLayer.ToString() + Global.Text.DelimiterValues + Neuron.IndexNeuron.ToString() + Global.Text.DelimiterIndexes +
        Value.ToString() + Global.Text.DelimiterValues + PreviousDW.ToString();
    }
  }
  class TNeuron
  {
    private int High = -1, IndexLayerField, IndexNeuronField;
    private TWeight[] Weights = new TWeight[0];
    public TNeuron(int indexLayer, int indexNeuron)
    {
      IndexLayerField = indexLayer;
      IndexNeuronField = indexNeuron;
    }
    public int Count
    {
      set
      {
        Array.Resize(ref Weights, value);
        High = value - 1;
      }
      get
      {
        return Weights.Length;
      }
    }
    public tfloat Out, BPSum, BPSumEpoch;
    public void Add(TWeight[] weights)
    {
      int i1 = Weights.Length, i2=0;
      Count = Count + weights.Length;
      while(i1<=High)
      {
        Weights[i1] = weights[i2];
        i1++;
        i2++;
      }
    }
    public tfloat Summate()
    {
      tfloat sum = 0;
      foreach (TWeight weight in Weights)
      {
        sum += weight.Value * weight.Neuron.Out;
      }
      return sum;
    }
    public void CorrectStartStep(tfloat dCoeff, tfloat dwCoeff)
    {
      for(int i = 0; i <= High; i++)
      {
        Weights[i].Neuron.BPSum += dCoeff * Weights[i].Value;
        Weights[i].Value += dwCoeff * Weights[i].Neuron.Out;
      }
    }
    public void CorrectStartMomentum(tfloat dCoeff, tfloat dwCoeff, tfloat momentum)
    {
      for(int i = 0; i <= High; i++)
      {
        Weights[i].Neuron.BPSum += dCoeff * Weights[i].Value;
        Weights[i].PreviousDW = momentum * Weights[i].PreviousDW + dwCoeff * Weights[i].Neuron.Out;
        Weights[i].Value += Weights[i].PreviousDW;
      }
    }
    public void CorrectContinueStep(tfloat dCoeff, tfloat dwCoeff)
    {
      for (int i = 0; i <= High; i++)
      {
        Weights[i].Neuron.BPSum += dCoeff * Weights[i].Value;
        Weights[i].Value += dwCoeff * Weights[i].Neuron.Out;
      }
      BPSum = 0;
    }
    public void CorrectContinueMomentum(tfloat dCoeff, tfloat dwCoeff, tfloat momentum)
    {
      for (int i = 0; i <= High; i++)
      {
        Weights[i].Neuron.BPSum += dCoeff * Weights[i].Value;
        Weights[i].PreviousDW = momentum * Weights[i].PreviousDW + dwCoeff * Weights[i].Neuron.Out;
        Weights[i].Value += Weights[i].PreviousDW;
      }
      BPSum = 0;
    }
    public int IndexLayer
    {
      get
      {
        return IndexLayerField;
      }
    }
    public int IndexNeuron
    {
      get
      {
        return IndexNeuronField;
      }
    }
  }
  #endregion
  #region TLayer
  class TLayer
  {
    protected byte FID;
    protected int High;
    protected TNeuron[] Neurons;
    public int Count
    {
      get
      {
        return Neurons.Length;
      }
    }
    public TLayer(int count, int indexLayer, byte fid = Global.FIDNone)
    {
      FID = fid;
      Neurons = new TNeuron[count];
      High = count - 1;
      for (int i = 0; i <= High; i++)
      {
        Neurons[i] = new TNeuron(indexLayer, i);
      }
    }
    public TWeight[] GetDendrite(int indexLayer, bool randomizeEqually, tfloat[] areas)
    {
      TWeight[] result = new TWeight[Neurons.Length];
      TWeight bufWeight;
      bufWeight.Value = 0;
      bufWeight.PreviousDW = 0;
      for (int i = 0; i <= High; i++)
      {
        bufWeight.Neuron = Neurons[i];
        result[i] = bufWeight;
      }
      Random rand = new Random();
      if (randomizeEqually)
      {
        tfloat area2 = areas[1] - areas[0];
        for (int i = 0; i <= High; i++)
        {
          result[i].Value = areas[0] + area2 * rand.NextDouble();
        }
      }
      else
      {
        for (int i = 0; i <= High; i++)
        {
          result[i].Value = areas[2 * i] + (areas[2 * i + 1] - areas[2 * i]) * rand.NextDouble();
        }
      }
      return result;
    }
    public void SetOuts(tfloat[] values)
    {
      for(int i=0; i<=High; i++)
      {
        Neurons[i].Out = values[i];
      }
    }
  }
  #region TLayerFunction
  abstract class TLayerFunction : TLayer
  {
    protected byte LRT, LST;
    protected tfloat FP1, FP2, LP1, LP2;
    public TLayerFunction(byte fid, byte lrt, byte lst, tfloat fp1, tfloat fp2, tfloat lp1, tfloat lp2, int count, int indexLayer) : base(count, indexLayer, fid)
    {
      FID = fid;
      LRT = lrt;
      LST = lst;
      switch (LST)
      {
        case Global.LSTConst: Speed = FSpeedConst; break;
        case Global.LSTLinear: Speed = FSpeedLinear; break;
        case Global.LSTSqrt: Speed = FSpeedConst; break;
        case Global.LSTLogistic: Speed = FSpeedConst; break;
      }
      FP1 = fp1;
      FP2 = fp2;
      LP1 = lp1;
      LP2 = lp2;
    }
    public void AddWeights(TWeight[] weights)
    {
      foreach(TNeuron Neuron in Neurons)
      {
        Neuron.Add(weights);
      }
    }
    public abstract void Calculate();
    public tfloat MSDifference(tfloat[] desired)
    {
      tfloat result = 0;
      for(int i=0; i<=High; i++)
      {
        result += Math.Pow(desired[i] - Neurons[i].Out, 2);
      }
      return Math.Sqrt(result/Neurons.Length);
    }
    #region Speed function
    protected delegate tfloat FSpeed(tfloat startSpeed, int epoch);
    protected FSpeed Speed;
    protected static tfloat FSpeedConst(tfloat startSpeed, int epoch) { return startSpeed; }
    protected static tfloat FSpeedLinear(tfloat startSpeed, int epoch) { return startSpeed / epoch; }
    protected static tfloat FSpeedSqrt(tfloat startSpeed, int epoch) { return startSpeed / Math.Sqrt(epoch); }
    protected static tfloat FSpeedLn(tfloat startSpeed, int epoch) { return startSpeed / Math.Log(Math.E - 1 + epoch); }
    #endregion
    #region Correct delegates/functions
    protected delegate void CorrectStartDelegate(tfloat trainingSpeed, tfloat[] desired);
    protected CorrectStartDelegate CorrectStartF;
    protected delegate void CorrectContinueDelegate(tfloat trainingSpeed);
    protected CorrectContinueDelegate CorrectContinueF;
    public void CorrectStart(int epoch, tfloat[] desired)
    {
      tfloat trainingspeed = Speed(LP1, epoch);
      CorrectStartF(trainingspeed, desired);
    }
    public void CorrectContinue(int epoch)
    {
      tfloat trainingspeed = Speed(LP1, epoch);
      CorrectContinueF(trainingspeed);
    }
    #endregion
    public void GetOutput(tfloat[] forValues)
    {
      for (int i=0; i<=High; i++)
      {
        forValues[i] = Neurons[i].Out;
      }
    }
  }
  class TLayerLinear : TLayerFunction
  {
    private tfloat FLinear(tfloat sum, tfloat parameter) => sum * parameter;
    public override void Calculate()
    {
      for(int i=0; i<=High; i++)
      {
        Neurons[i].Out = FLinear(Neurons[i].Summate(),FP1);
      }
    }
    private void CorrectStartStep(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FP1;
        Neurons[i].CorrectStartStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectContinueStep(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FP1;
        Neuron.CorrectContinueStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectStartMomentum(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FP1;
        Neurons[i].CorrectStartMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    private void CorrectContinueMomentum(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FP1;
        Neuron.CorrectContinueMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    public TLayerLinear(byte lrt, byte lst, tfloat fp1, tfloat fp2, tfloat lp1, tfloat lp2, int count, int indexLayer) : base(Global.FIDLinear, lrt, lst, fp1, fp2, lp1, lp2, count, indexLayer)
    {
      switch (lrt)
      {
        case Global.LRTStep:
        {
          CorrectStartF = CorrectStartStep;
          CorrectContinueF = CorrectContinueStep;
          break;
        }
        case Global.LRTMomentum:
        {
          CorrectStartF = CorrectStartMomentum;
          CorrectContinueF = CorrectContinueMomentum;
          break;
        }
      }
    }
  }
  class TLayerLogistic : TLayerFunction
  {
    private tfloat FLogistic(tfloat sum, tfloat parameter) => 1 / (1 + Math.Exp(-parameter * sum));
    private tfloat FDerivativeLogistic(tfloat outvalue) => outvalue * (1 - outvalue);
    public override void Calculate()
    {
      for (int i = 0; i <= High; i++)
      {
        Neurons[i].Out = FLogistic(Neurons[i].Summate(), FP1);
      }
    }
    private void CorrectStartStep(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FDerivativeLogistic(Neurons[i].Out);
        Neurons[i].CorrectStartStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectContinueStep(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FDerivativeLogistic(Neuron.Out);
        Neuron.CorrectContinueStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectStartMomentum(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FDerivativeLogistic(Neurons[i].Out);
        Neurons[i].CorrectStartMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    private void CorrectContinueMomentum(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FDerivativeLogistic(Neuron.Out);
        Neuron.CorrectContinueMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    public TLayerLogistic(byte lrt, byte lst, tfloat fp1, tfloat fp2, tfloat lp1, tfloat lp2, int count, int indexLayer) : base(Global.FIDLogistic, lrt, lst, fp1, fp2, lp1, lp2, count, indexLayer)
    {
      switch (lrt)
      {
        case Global.LRTStep:
        {
          CorrectStartF = CorrectStartStep;
          CorrectContinueF = CorrectContinueStep;
          break;
        }
        case Global.LRTMomentum:
        {
          CorrectStartF = CorrectStartMomentum;
          CorrectContinueF = CorrectContinueMomentum;
          break;
        }
      }
    }
  }
  class TLayerHTangent : TLayerFunction
  {
    private tfloat FHTangent(tfloat sum, tfloat parameter) => Math.Tanh(sum / parameter);
    private tfloat FDerivativeHTangent(tfloat outvalue) => (1 + outvalue) * (1 - outvalue);
    public override void Calculate()
    {
      for (int i = 0; i <= High; i++)
      {
        Neurons[i].Out = FHTangent(Neurons[i].Summate(), FP1);
      }
    }
    private void CorrectStartStep(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FDerivativeHTangent(Neurons[i].Out);
        Neurons[i].CorrectStartStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectContinueStep(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FDerivativeHTangent(Neuron.Out);
        Neuron.CorrectContinueStep(dCoeff, dCoeff * trainingSpeed);
      }
    }
    private void CorrectStartMomentum(tfloat trainingSpeed, tfloat[] desired)
    {
      for (int i = 0; i <= High; i++)
      {
        tfloat dCoeff = (desired[i] - Neurons[i].Out) * FDerivativeHTangent(Neurons[i].Out);
        Neurons[i].CorrectStartMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    private void CorrectContinueMomentum(tfloat trainingSpeed)
    {
      foreach (TNeuron Neuron in Neurons)
      {
        tfloat dCoeff = Neuron.BPSum * FDerivativeHTangent(Neuron.Out);
        Neuron.CorrectContinueMomentum(dCoeff, dCoeff * trainingSpeed, LP2);
      }
    }
    public TLayerHTangent(byte lrt, byte lst, tfloat fp1, tfloat fp2, tfloat lp1, tfloat lp2, int count, int indexLayer) : base(Global.FIDHTangent, lrt, lst, fp1, fp2, lp1, lp2, count, indexLayer)
    {
      switch (lrt)
      {
        case Global.LRTStep:
        {
          CorrectStartF = CorrectStartStep;
          CorrectContinueF = CorrectContinueStep;
          break;
        }
        case Global.LRTMomentum:
        {
          CorrectStartF = CorrectStartMomentum;
          CorrectContinueF = CorrectContinueMomentum;
          break;
        }
      }
    }
  }
  #endregion
  #endregion
  class TNeuronet
  {
    #region Для сохранения и загрузки
    private bool fIsNeedToSave = true;
    public bool IsNeedToSave
    {
      get
      {
        return fIsNeedToSave;
      }
    }
    private string fPathCurrent;
    public string PathCurrent
    {
      get
      {
        return fPathCurrent;
      }
    }
    #endregion
    private int High;
    private TLayer[] Layers;
    public TNeuronet(int count)
    {
      Layers = new TLayer[count];
      High = count - 1;
    }
    public void SetLayer(int index, byte fid, int count, byte lrt = 0, byte lst = 0, tfloat fp1 = 0, tfloat fp2 = 0, tfloat lp1 = 0, tfloat lp2 = 0)
    {
      switch (fid)
      {
        case Global.FIDNone: Layers[index] = new TLayer(count, index); break;
        case Global.FIDLinear: Layers[index] = new TLayerLinear(lrt, lst, fp1, fp2, lp1, lp2, count, index); break;
        case Global.FIDLogistic: Layers[index] = new TLayerLogistic(lrt, lst, fp1, fp2, lp1, lp2, count, index); break;
        case Global.FIDHTangent: Layers[index] = new TLayerHTangent(lrt, lst, fp1, fp2, lp1, lp2, count, index); break;
      }
    }
    public void ConnectLayers(int indexStart, int indexEnd, bool randomizeEqually, tfloat[] areas)
    {
      TWeight[] weights = Layers[indexStart].GetDendrite(indexStart, randomizeEqually, areas);
      ((TLayerFunction)Layers[indexEnd]).AddWeights(weights);
    }
    public void SetInput(tfloat[] values)
    {
      Layers[0].SetOuts(values);
    }
    public void GetOutput(tfloat[] forValues)
    {
      ((TLayerFunction)Layers[High]).GetOutput(forValues);
    }
    public void Calculate()
    {
      for(int i=1; i<=High; i++)
      {
        ((TLayerFunction)Layers[i]).Calculate();
      }
    }
    public bool TrainMLP(List<tfloat[]> inputs, List<tfloat[]> outputs, bool isGraphics, bool isStopEpochs, bool isStopAccuracyAverage, bool isStopAccuracyMaximum, bool isRandomize, out int passesActually, int stopEpochs, tfloat stopAccuracyAverage, tfloat stopAccuracyMaximum, tfloat[] sdAverage, tfloat[] sdMaximum)
    {
      int EpochsCount = inputs.Count, EpochsHigh = EpochsCount - 1, Passes = 0;
      //Инициализация здесь, потому что компилятор - идиотский (не учитывая условий, ругается на эти tfloat'ы в return'е)
      tfloat DAverage = 0, DMaximum = 0;  
      bool StatsAndCheck()
      {
        //Подсчёт
        if (isGraphics || isStopAccuracyAverage || isStopAccuracyMaximum)
        {
          DAverage = 0;
          DMaximum = 0;
          for (int i = 0; i <= EpochsHigh; i++)
          {
            Layers[0].SetOuts(inputs[i]);
            Calculate();
            tfloat DNow = ((TLayerFunction)Layers[High]).MSDifference(outputs[i]);
            DAverage += Math.Pow(DNow, 2);
            if (DNow > DMaximum)
            {
              DMaximum = DNow;
            }
          }
          DAverage = Math.Sqrt(DAverage/EpochsCount);
          if (isGraphics)
          {
            sdAverage[Passes] = DAverage;
            sdMaximum[Passes] = DMaximum;
          }
        }
        //Проверка по точности
        bool resultCheck = (isStopAccuracyAverage || isStopAccuracyMaximum) ? 
          (!isStopAccuracyAverage || (DAverage <= stopAccuracyAverage)) && (!isStopAccuracyMaximum || (DMaximum <= stopAccuracyMaximum))
          : false;
        //Проверка по итерациям
        if (!resultCheck && isStopEpochs)
        {
          resultCheck = Passes == stopEpochs;
        }
        return resultCheck;
      }
      void Randomization()
      {
        if (isRandomize)
        {
          Random Generator = new Random();
          int BufIndex;
          tfloat[] bufValueSet;
          for (int i=0; i<=EpochsHigh; i++)
          {
            BufIndex = Generator.Next(0, EpochsHigh);
            bufValueSet = inputs[i];
            inputs[i] = inputs[BufIndex];
            inputs[BufIndex] = bufValueSet;
            bufValueSet = outputs[i];
            outputs[i] = outputs[BufIndex];
            outputs[BufIndex] = bufValueSet;
          }
        }
      }
      void BackPropagation()
      {
        for(int i=0; i<=EpochsHigh; i++)
        {
          Layers[0].SetOuts(inputs[i]);
          Calculate();
          ((TLayerFunction)Layers[High]).CorrectStart(Passes, outputs[i]);
          for(int j = High - 1; j > 0; j--)
          {
            ((TLayerFunction)Layers[j]).CorrectContinue(Passes);
          }
        }
      }
      if (!StatsAndCheck())
      {
        do
        {
          Passes++;
          Randomization();
          BackPropagation();
        }
        while (!StatsAndCheck());
        passesActually = Passes;
        return (!isStopAccuracyAverage || (DAverage <= stopAccuracyAverage)) && (!isStopAccuracyMaximum || (DMaximum <= stopAccuracyMaximum));
      }
      else
      {
        Passes++;
        passesActually = Passes;
        return StatsAndCheck();
      }
    }
    public bool SaveToFile(string path=null)
    {
      /*Тут будет код*/
      fIsNeedToSave = false;
      if (path!=null)
      {
        fPathCurrent = path;
      }
      return true;
    }
    public static TNeuronet LoadFromFile(string path)
    {
      TNeuronet bufNeuronet = new TNeuronet(0);
      return bufNeuronet;
    }
  }
  static class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TFormMain());
    }
  }
}