using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessPlanner
{
    class Planner
    {
        private Processor processor;
        private PriorityQueue<Process> processQueue;
        private List<Process> waitingProcesses;
        
        private ResourceQueue videoCard;
        public processFinished processFinishedEvent;
        private Memory memory;

        public int statTotalProcessees = 0;
        public int statTotalFinished = 0;

        private int incomingProcesses = 0;
        private int finishedProcesses = 0;
        private int rejectedOnMemoryAlloc = 0;
        private int executedOnGPU = 0;
        private int totalCalcTime = 0;

        public Planner(ref Processor processor, ref ResourceQueue videoCard)
        {
            memory = new Memory(500, 32);
            this.videoCard = videoCard;
            processQueue = new PriorityQueue<Process>();
            waitingProcesses = new List<Process>();
            this.processor = processor;
            processor.processFinishedEvent += plan;
            videoCard.processFinishedEvent += plan;
        }

        private void plan(Process process)
        {
            MainForm.instance.setStatisticsGlobal(String.Format("Incoming: {0}\nFinished: {1}\nRejected: {2}\nGPU: {3}\nTotal time: {4}\nAverage time: {5}",
                incomingProcesses, finishedProcesses, rejectedOnMemoryAlloc, executedOnGPU,totalCalcTime, totalCalcTime/(1+incomingProcesses)));
            memory.deallocateProcess(process);
            finishedProcesses++;
            if (process.thisState == Process.ProcessState.WaitingChild)
            {

                process.processChildFinishedEvent += (proc) =>
                {
                    waitingProcesses.Remove(proc);
                    addProcess(proc);
                };
                waitingProcesses.Add(process);
            }
            else
            {
                if (process.getAccessModeTimeRemain() > 0)
                {
                    Process.Resources a;
                    switch (a = process.getCurrentAccessMode())
                    {
                        case Process.Resources.Videocard:
                            videoCard.addProcess(process);
                            executedOnGPU++;
                            break;
                        case Process.Resources.Processor:
                            processor.addProcess(process);
                            break;
                    }
                }
            }
            if (!processQueue.isEmpty())
            {
                this.processor.addProcess(processQueue.getTopElement());
            }
        }
        
        private bool isEnoughMemoryForProcess(Process candidate)
        {
            return memory.findMemoryRegion(memory.pagesRequiredForProcess(candidate)) != -1;
        }
        
        private bool checkProcessTreeFitsMemory(Process p)
        {
            bool everythingFitsMemory = true;
            everythingFitsMemory &= isEnoughMemoryForProcess(p);
            foreach(Process child in p.childProcesses) 
            {
                everythingFitsMemory &= isEnoughMemoryForProcess(child);
            }

            return everythingFitsMemory;
        }

        public bool addProcess(Process p)
        {

            if (checkProcessTreeFitsMemory(p))
            {
                incomingProcesses++;
                for(int i = 0; i < p.accessModes.Count; i++)
                {
                    totalCalcTime += p.accessModes[i].Value;
                }
                processQueue.addElement(p, p.priority);
                memory.allocateProcess(p);
                for (int i = 0; i < p.childProcesses?.Count; i++)
                {
                    addProcess(p.childProcesses[i]);
                }
                if (processor.isFree() && !processQueue.isEmpty())
                {
                    plan(processQueue.getTopElement());
                }
                return true;
            }
            rejectedOnMemoryAlloc++;
            return false;
        }

        public List<Process> getArray()
        {
            List<Process> l = new List<Process>();
            for(int i = 0; i < processQueue.getArray().Length; i++)
            {
                if(processQueue.getArray()[i].Key != default(KeyValuePair<Process, int>).Key)
                {
                    l.Add(processQueue.getArray()[i].Key);
                }
            }
            return l;
        }

        public void addChildProcess(Process parent, Process child)
        {
            child.processChildFinishedEvent += (Process proc) =>
            {
                memory.deallocateProcess(proc);
            };
            parent.assignChild(child);
        }
    }
}
