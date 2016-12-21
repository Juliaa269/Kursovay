using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessPlanner
{
    public delegate void processFinished(Process p);
    public delegate void processChildFinished(Process p);
    public class Process
    {
        public enum AccessMode{
            Processor,
            Videocard,
            HardDrive,
            Audiocard
        }
        public enum ProcessState
        {
            Ready,
            Finished,
            WaitingResource,
            WaitingChild
        }
        public String name;
        public int priority;
        int processCycleStage;
        public List<KeyValuePair<AccessMode, int>> accessModes;
        public List<Process> childProcesses;
        static Random rnd;
        public ProcessState thisState;
        public int waitingForChild;
        public int memoryRequired;
        public processFinished processFinishedEvent;
        bool wasOnProcessor = false;
        public processChildFinished processChildFinishedEvent;
        public Process(String id, int priority, int cpuTimeNeeded, int memoryRequired)
        {
            accessModes = new List<KeyValuePair<AccessMode, int>>();
            childProcesses = new List<Process>();
            name = id;
            this.priority = priority;
            processCycleStage = 0;
            thisState = ProcessState.Ready;
            waitingForChild = 0;
            this.memoryRequired = memoryRequired;
            //accessModes.Add(new KeyValuePair<AccessMode, int>(AccessMode.Processor, cpuTimeNeeded));
        }
        public void addAccessMode(AccessMode mode, int timeNeeded)
        {
            accessModes.Add(new KeyValuePair<AccessMode, int>( mode, timeNeeded));
        }
        public void nextAccessMode()
        {
            if(processCycleStage+1 < accessModes.Count)
            {
                processCycleStage++;
                if (accessModes[processCycleStage].Key != AccessMode.Processor)
                    thisState = ProcessState.WaitingResource;
            }
            else
            {
                thisState = ProcessState.Finished;
                processFinishedEvent?.Invoke(this);
            }
        }
        public void assignChild(Process p)
        {
            waitingForChild++;
            thisState = ProcessState.WaitingChild;
            childProcesses.Add(p);
            p.processFinishedEvent += (proccess) =>
            {
                waitingForChild--;
                childProcesses.Remove(p);
                if (childProcesses.Count == 0)
                {
                    thisState = ProcessState.Ready;
                    processChildFinishedEvent?.Invoke(this);
                }
            };
        }
        static int a = 1;
        public static string generateName()
        {
            if(rnd == null)
                rnd = new Random();
            return "P" + (a++).ToString();
        }
        public int changeAccessModeTime(int delta)
        {
            wasOnProcessor = true;
            int currentMode = processCycleStage;
            int newValue = accessModes[processCycleStage].Value + delta;
            accessModes[processCycleStage] = new KeyValuePair<AccessMode, int>(accessModes[processCycleStage].Key,
                newValue);
            if (accessModes[processCycleStage].Value <= 0)
            {
                if(accessModes.Count > processCycleStage + 1)
                {
                    processCycleStage++;
                }
                //nextAccessMode();
                processFinishedEvent?.Invoke(this);
            }
            return accessModes[currentMode].Value;
        }
        public int getAccessModeTimeRemain()
        {
            return accessModes[processCycleStage].Value;
        }
        public AccessMode getCurrentAccessMode()
        {
            return accessModes[processCycleStage].Key;
        }
        public int getAccessModeTime()
        {
            return accessModes[processCycleStage].Value;
        }
        public int getHash()
        {
            return name.GetHashCode() + priority << 7 + accessModes[0].Value >> 3;
        }
        public override string ToString()
        {
            return name + " resourceTime: " + accessModes[processCycleStage].Value.ToString()
                + (thisState == ProcessState.WaitingChild? " Parent":"");
        }
        public Process(Process p)
        {
            this.name = p.name;
            this.priority = p.priority;
            this.accessModes = p.accessModes;

        }
    }

    class Processor
    {
        Process currentProcess;
        public processFinished processFinishedEvent;
        public int processorFreeTime;
        bool newProcessStarted;
        int speed;
        public Processor(int speed = 200)
        {
            processorFreeTime = 0;
            this.speed = speed;
        }
        public void nextTick()
        {
            if(currentProcess != null)
                MainForm.instance.setProcessStats(currentProcess);
            if(currentProcess == null)
            {
                processorFreeTime += speed;
                return;
            }
            if(currentProcess.waitingForChild > 0)
            {
                currentProcess.thisState = Process.ProcessState.WaitingChild;
                Process p = currentProcess;
                currentProcess = null;
                processFinishedEvent?.Invoke(p);
                return;
            }
            if((currentProcess.changeAccessModeTime(-speed)) <= 0){
                    Process p = currentProcess;
                currentProcess = null;
                processFinishedEvent?.Invoke(p);
            }
        }
        public void addProcess(Process p)
        {
            currentProcess = p;
        }
        public bool isFree()
        {
            return currentProcess == null;
        }
    }
}
