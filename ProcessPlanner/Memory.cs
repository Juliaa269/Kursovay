using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessPlanner
{
    class Memory
    {
        bool[] memoryMap;
        Process currentProcess;
        int blocksCount,
            blocksSize;
        int freeMem;
        class MemoryBlock
        {
            public int leftPtr,
                       rightPtr;
            public MemoryBlock(int leftPtr, int rightPtr)
            {
                this.leftPtr = leftPtr;
                this.rightPtr = rightPtr;
            }
        }
        Dictionary<Process, MemoryBlock> allocatedProcesses;
        public Memory(int blocksNum = 200, int blockSize = 32)
        {
            freeMem = blocksSize * blocksNum;
            allocatedProcesses = new Dictionary<Process, MemoryBlock>();
            memoryMap = new bool[blocksNum];
            blocksCount = blocksNum;
            this.blocksSize = blockSize;
        }
        public int findMemoryRegion(int desiredBlocksNum)
        {
            for(int i = 0; i < blocksCount - desiredBlocksNum; i++)
            {
                bool memoryFreeFlag = true;
                for(int j = i; (j < i + desiredBlocksNum) && memoryFreeFlag; j++)
                {
                    memoryFreeFlag = !memoryMap[j] ? true : false ;
                }
                if (memoryFreeFlag)
                {
                    return i;
                }
            }
            return -1;
        }
        void setBlockAllocated(MemoryBlock block)
        {
            freeMem -= (block.rightPtr - block.leftPtr) * blocksSize;
            for (int i = block.leftPtr; i <= block.rightPtr; i++)
                memoryMap[i] = true;
        }
        void setBlockUnallocated(MemoryBlock block)
        {
            freeMem += (block.rightPtr - block.leftPtr) * blocksSize;
            for (int i = block.leftPtr; i <= block.rightPtr; i++)
                memoryMap[i] = false;
        }
        public bool allocateProcess(Process process)
        {
            int startAllocationBlock, pagesRequired;
            if ((startAllocationBlock = findMemoryRegion((pagesRequired = pagesRequiredForProcess(process)))) != -1)
            {
                if (!allocatedProcesses.ContainsKey(process)) { 
                    MemoryBlock processMemoryCell = new MemoryBlock(startAllocationBlock, startAllocationBlock + pagesRequired);
                    allocatedProcesses.Add(process, processMemoryCell);
                    setBlockAllocated(processMemoryCell);
                    return true;
                }
            }
            return false;
        }
        public int pagesRequiredForProcess(Process process)
        {
            return process.memoryRequired / blocksSize + (process.memoryRequired % blocksSize > 0 ? 1 : 0);
        }
        public bool deallocateProcess(Process process)
        {
            MainForm.instance.setMemoryInfo(String.Format("Total: {0}\nBusy: {1}%", blocksCount * blocksSize, -100.0 * freeMem / (blocksCount * blocksSize)));
            if (allocatedProcesses.ContainsKey(process))
            {
                setBlockUnallocated(allocatedProcesses[process]);
                allocatedProcesses.Remove(process);
                return true;
            }
            return false;
        }
    }
}
