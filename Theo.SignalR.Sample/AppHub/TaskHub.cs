using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Timers;
using Theo.SignalR.Services;
using Microsoft.AspNet.SignalR.Hubs;

namespace Theo.SignalR.Sample.AppHub
{
    /// <summary>
    /// Created for learning and understanding Signal R , the detailed explantion of the sample is given @ http://www.codeproject.com/Articles/1018938/SignalR-ASP-NET-Way-of-Socket-Programming
    /// </summary>
    [HubName("taskHub")]
    public class TaskHub : Hub
    {
        //private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(1000);
        //private System.Threading.Timer _timer;
        System.Timers.Timer _timer = new System.Timers.Timer();

        private static int currentID = 0;


        public List<FakeTask> GetTasks()
        {
            return new TaskService().GetAllTasks();
        }

        public void UpdateTasks()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.AutoReset = false;
            _timer.Start();
            //_timer = new System.Threading.Timer(UpdateTaskStatus, null, _updateInterval, _updateInterval);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTaskStatus();
        }

        public void StopUpdateTasks()
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
            Clients.All.stoppedTaskStatus();
        }

        private void UpdateTaskStatus()
        {
            var tS = new TaskService();
            var task = tS.UpdateTask(currentID % 10);
            currentID++;
            Clients.All.updateTaskStatus(task);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}