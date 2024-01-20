using MeOS.Core;
using MeOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeOS
{
    internal class TaskScheduler
    {
        public List<Process> processes = new List<Process>();
        


        public void Schedule(string name, string tasks)
        {
            if (tasks == null) { CLI.WriteLine("Tasks is null!", CLI.foreground, CLI.background); return; }
            Process p = new Process(name, tasks);
            processes.Add(p);
        }

        public void ScheduleFunc(string name, Action func) {
            Process p = new Process(name, "$shell callFunc()");
            processes.Add(p);
            RunFunc (() => func());
        }

        public void RunFunc(Action func) {
            func();
        }

        public Process ScheduleReturn(string name, string tasks)
        {
            if (tasks == null) { CLI.WriteLine("Tasks is null!", CLI.foreground, CLI.background); return null; }
            Process p = new Process(name, tasks);
            return p;
        }

        public void ScheduleAndRun(string task)
        {
            string[] tasks = task.Split(' ');
            RunTask(ScheduleReturn(tasks[0], task));
        }
        public void RunAllTasks()
        {
            try
            {
                foreach (Process p in processes.ToList()) // Using ToList() to create a copy for iteration
                {
                    if (p.Tasks != "$shell callFunc()")
                        RunTask(p);
                    else continue;
                }
            }
            catch (Exception ex)
            {
                CLI.WriteLine($"Exception in RunAllTasks: {ex.ToString()}", CLI.foreground, CLI.background);
            }
        }



        public void RunTask(Process p)
        {
            try
            {
                if (p.Tasks != "$shell callFunc()") {
                    CLI.WriteLine(Kernel.cm.processInput(p.Tasks), CLI.foreground, CLI.background);
                    processes.Remove(p);
                }
            }
            catch (Exception e)
            {
            }
        }
        private string[] GetMergedStringArray(string input)
        {
            // Split the input string by spaces
            string[] words = input.Split(' ');

            // Join the words into a single string
            string mergedString = string.Join(" ", words);

            // Create a new array with the merged string as the only item
            string[] resultArray = { mergedString };

            return resultArray;
        }

        private string[] GetModifiedArray(string[] inputArray)
        {
            // Create a new array with the same length plus one for the first line
            string[] resultArray = new string[inputArray.Length + 1];

            // Set the first line to be the combined string of all items
            resultArray[0] = string.Join(" ", inputArray);

            // Copy the original items to the new array starting from index 1
            Array.Copy(inputArray, 0, resultArray, 1, inputArray.Length);

            return resultArray;
        }

    }

    internal class Process
    {
        public string Name { get; }
        public string Tasks { get; }

        public Process(string name, string tasks)
        {
            Name = name;
            Tasks = tasks;
        }

        public Process(string _name) {
            Name = _name;
        }
    }

}
