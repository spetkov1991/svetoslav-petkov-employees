using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LongestPeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"test.txt");

            var fileLines = File.ReadAllLines(path).ToList();
            var workers = new List<Worker>();

            foreach (string line in fileLines)
            {
                string[] temp = line.Split(',');
                if (temp.Length >= 1)
                {
                    Worker worker = new Worker();
                    worker.EmpID = int.Parse(temp[0].Trim());
                    worker.ProjectId = int.Parse(temp[1].Trim());
                    worker.DateFrom = DateTime.Parse(temp[2].Trim());
                    worker.DateTo = string.IsNullOrWhiteSpace(temp[3]) || temp[3].Trim().Equals("NULL") ? DateTime.Now : DateTime.Parse(temp[3].Trim());
                    workers.Add(worker);
                }
            }

            var duplicateProjectId = from w in workers
                                     join w2 in workers on w.ProjectId equals w2.ProjectId
                                     where w != w2
                                     group w by w.ProjectId into workerGroup
                                     select new
                                     {
                                         ProjectId = workerGroup.Key,
                                         TotalWorkingDays = workerGroup.Sum(x => x.WorkingDays)                                                                                
                                     };
            int longestPeriod = 0;
            int projectId = 0;

            foreach (var item in duplicateProjectId)
            {
                
                if (item.TotalWorkingDays > longestPeriod)
                {
                    longestPeriod = item.TotalWorkingDays;
                    projectId = item.ProjectId;
                }
            }

            Console.WriteLine($"The Project with Project Id {projectId} have longest period : {longestPeriod} days");
            Console.ReadKey(true);
        }
    }
}
