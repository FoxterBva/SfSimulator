using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public class SimEvent : IComparable
    {
        public int Time { get; set; }
        public Action<int> Callback { get; set; }
        public int Priority { get; set; }
        public EventType Type { get; set; }
        public string Parameter { get; set; }

        public void ProcessEvent()
        {
            Callback(Time);
        }

        public int CompareTo(object obj)
        {
            var evt2 = obj as SimEvent;
            if (this.Time == evt2.Time)
                return this.Priority.CompareTo(evt2.Priority);
            else
                return this.Time.CompareTo(evt2.Time);
        }

		public override string ToString()
		{
			return String.Format("[{0:0000.00}] {1} {2} P:'{3}'", (double)Time / 1000, Priority, Type, Parameter);
		}
    }
}
