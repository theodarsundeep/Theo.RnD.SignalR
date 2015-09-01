using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theo.SignalR.Services
{
    public class TaskService
    {
        string[] Owners = { "Theo", "Sun", "Anu", "Hari", "Man", "Ash", "Kun" };
        string[] TaskNames = { "Build Desgin", "Review Code", "Unit Test", "Code refactoring", "Define Coding Best Practices", "Some other tasks" };

        private readonly List<FakeTask> _tasks;

        public TaskService()
        {
            _tasks = new List<FakeTask>();
            var OwnerCnt = Owners.Length;
            var taskCnt = TaskNames.Length;
            for (int iTskCnt = 0; iTskCnt < 7; iTskCnt++)
            {
                _tasks.Add(new FakeTask()
                {
                    Owner = Owners[iTskCnt % OwnerCnt],
                    Name = TaskNames[iTskCnt % taskCnt] + "-" + iTskCnt,
                    Done = iTskCnt % 3 == 0,
                    TaskID = iTskCnt
                });
            }
        }

        public List<FakeTask> GetAllTasks()
        {
            return _tasks;
        }

        public FakeTask UpdateTask(int tskID)
        {
            var task = _tasks.FirstOrDefault(x => x.TaskID == tskID);
            //Null-conditional operators address many of the situations where code tends to drown in null-checking. 
            //They let you access members and elements only when the receiver is not-null, providing a null result otherwise:
            if (task != null)
            {
                task.Done = true;
            }

            return task;
        }
    }
}
