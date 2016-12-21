using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProcessPlanner
{
    public partial class MainForm : Form
    {
        private Processor processor = new Processor(50);
        private ResourceQueue videocardQueue = new ResourceQueue(ResourseType.Videocard, 200);
        private Planner planner;
        private static Random r = new Random();
        private List<Process> currentProcesses;
        public static MainForm instance;
        private bool generate = true;

        private void updateQueueListBox()
        {
            queueListBox.Items.Clear();
            foreach(Process p in planner.getArray())
            {
                queueListBox.Items.Add(p.ToString());
            }
        }
       
        public MainForm()
        {
            InitializeComponent();
            planner = new Planner(ref processor, ref videocardQueue);
            planner.processFinishedEvent += (p)=> {
                for(int i = 0; i < currentProcesses.Count; i++)
                    if(currentProcesses[i].getHash() == p.getHash())
                    {
                        currentProcesses.RemoveAt(i);
                        break;
                    }
                updateQueueListBox();
            };
            currentProcesses = new List<Process>();
            instance = this;
        }
        
        static Func<double, double> distributionFunction = (val) =>
        {
            return 1.0 / (1 + Math.Pow(Math.E, -6 * val + 3)); 
        };
        
        Func<int> getRandomExecutionTime = () =>
        {
            double a = r.NextDouble();
            return ((int)(a / distributionFunction(a) * 1000)) / 2 + 1;
        };
        
        private void processorTactModelTimer_Tick(object sender, EventArgs e)
        {
            if (r.NextDouble() > 0.7 && generate)
            {
                //generate = false;
                int a = getRandomExecutionTime();
                Process newProcess = new Process(Process.generateName(), a, a, r.Next(100, 1000));
                newProcess.addAccessMode(Process.AccessMode.Processor, a);
                if (r.NextDouble() > 0.8)
                {
                    a = getRandomExecutionTime();
                    Process p = new Process(Process.generateName(), a, a, r.Next(100, 1000));
                    p.addAccessMode(Process.AccessMode.Processor, a);
                    p.addAccessMode(Process.AccessMode.Videocard, getRandomExecutionTime());
                    planner.addChildProcess(newProcess, p);

                }
                planner.addProcess(newProcess);

                updateQueueListBox();
            }
            processor.nextTick();
            videocardQueue.nextTick();
        }

        private void startExperimentButton_Click(object sender, EventArgs e)
        {
            //String s = "";
            //int[] arr = new int[50];

            //for (int  i = 0; i < 50; i++)
            //{
            //    double a = r.NextDouble();
            //    arr[i] = ((int)(a/distributionFunction(a) * 1000))/14;
            //}
            //Array.Sort(arr);
            //double max = (int)-1e9;
            //for (int i = 0; i < 50; i++)
            //    s += arr[i] + Environment.NewLine;
            //MessageBox.Show(s);
            processorTactModelTimer.Start();
        }

        private void stopExperimentButton_Click(object sender, EventArgs e)
        {
            processorTactModelTimer.Stop();
        }
        public void setProcessStats(Process p)
        {
            currentProcessorStateLabel.Text = p.ToString();
        }
        public void setVideoCardStats(Process p)
        {
            currentVideocardStateLabel.Text = p.ToString();
        }
        public void setStatisticsGlobal(string s)
        {
            statisticsLabel.Text = s;
        }
        public void setMemoryInfo(string text)
        {
            currentMemoryStateLabel.Text = text;
        }
    }
}
