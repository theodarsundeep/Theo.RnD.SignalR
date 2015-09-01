using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Using static -- to directly use the startic methods.
using static System.DateTime;

namespace Theo.SignalR.Services
{
    class FakeTaskCompletedEventArgs : EventArgs
    {
        public DateTime TaskCompletedTime { get; } = Now;
    }

    /// <summary>
    /// THis is a Fake Learning class , this implement some of the C# 6.0 features listed @ https://msdn.microsoft.com/en-us/magazine/dn802602.aspx
    /// </summary>
    public class FakeTask
    {

        event EventHandler<EventArgs> OnTaskCompleted;
        private bool? _taskDone = false;

        public int TaskID { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }

        //Initializers for auto-properties
        public bool? Done
        {
            get
            {
                return _taskDone;
            }
            set
            {
                _taskDone = value;
                
                //Null-conditional operators address many of the situations where code tends to drown in null-checking. 
                //They let you access members and elements only when the receiver is not-null, providing a null result otherwise:

                OnTaskCompleted?.Invoke(this, new FakeTaskCompletedEventArgs());
            }
        }

        //Getter-only auto-properties
        public DateTime CreatedDate { get; } = Now;

        public string TaskDescription { get { return ToString(); } }

        //Expression bodies on method-like members
        //String interpolation
        //string.format("Task ID - {0} Name - {1} Owner - {2}", TaskID, Name, Owner);
        public override string ToString() => $"Task ID - {TaskID} Name - {Name} Owner - {Owner}";

        //Expression bodies on property-like function members
        public string TaskIdentifier => TaskID + " " + Name;


    }
}
