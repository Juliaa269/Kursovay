using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessPlanner
{
    enum ResourseType
    {
        HardDrive,
        Videocard,
        Audiocard
    }
    class ResourceQueue
    {
        ResourseType thisResourse;
        public processFinished processFinishedEvent;
        int thisSpeed = 0;
        public ResourceQueue(ResourseType type, int speed = 200)
        {
            thisResourse = type;
            thisSpeed = speed;
        }
        Process currentProcess;

        public void nextTick()
        {

            if (currentProcess?.changeAccessModeTime(-thisSpeed) < 0)
            {
                Process p = currentProcess;
                currentProcess = null;
                processFinishedEvent?.Invoke(p);
            }
            if (currentProcess != null)
                MainForm.instance.setVideoCardStats(currentProcess);
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
