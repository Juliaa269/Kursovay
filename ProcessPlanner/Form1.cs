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
        private static Random random = new Random();
        private List<Process> currentProcesses;
        public static MainForm instance;


        public MainForm()
        {
            InitializeComponent();
            planner = new Planner(ref processor, ref videocardQueue);
            planner.processFinishedEvent += (p) => {
                for (int i = 0; i < currentProcesses.Count; i++)
                    if (currentProcesses[i].getHash() == p.getHash())
                    {
                        currentProcesses.RemoveAt(i);
                        break;
                    }
                updateQueueListBox();
            };
            currentProcesses = new List<Process>();
            instance = this;
        }

        private void updateQueueListBox()
        {
            queueListBox.Items.Clear();
            foreach (Process p in planner.getArray())
            {
                queueListBox.Items.Add(p.ToString());
            }
        }

        static double distributionFunction(double val)
        {
            return 1.0 / (1 + Math.Pow(Math.E, -6 * val + 3));
        }

        int getRandomExecutionTime() {
            double a = random.NextDouble();
            return ((int)(a / distributionFunction(a) * 1000)) / 2 + 1;
        }

        private void processorTactModelTimer_Tick(object sender, EventArgs e)
        {
            if (random.NextDouble() > 0.7)  // 0.3
            {
                Process regularProcess = createRegularProcess();

                if (random.NextDouble() > 0.8) // 0.2 * 0.3 = 0.06
                {
                    Process processWithVideoCardUsage = createRegularProcess();
                    processWithVideoCardUsage.addAccessMode(Process.AccessMode.Videocard, getRandomExecutionTime());
                    planner.addChildProcess(regularProcess, processWithVideoCardUsage);
                }
                planner.addProcess(regularProcess);
                updateQueueListBox();
            }
            processor.nextTick();
            videocardQueue.nextTick();
        }

        private Process createRegularProcess()
        {
            return createRegularProcess(getRandomExecutionTime(), random.Next(0, 100), random.Next(100, 100));
        }

        private Process createRegularProcess(int executionTime, int priority, int requiredMemory)
        {
            Process process = new Process(Process.generateName(), executionTime, priority, requiredMemory);
            process.addAccessMode(Process.AccessMode.Processor, executionTime);
            return process;
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
