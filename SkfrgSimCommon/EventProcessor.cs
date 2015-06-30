using SkfrgSimCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon
{
    public class EventProcessor
    {
        EnvironmentContext context;

        public EventProcessor(EnvironmentContext c)
        {
            context = c;
        }

        public void ProcessEvent(SimEvent evt)
        {
            if (evt.Callback != null)
            {
                evt.ProcessEvent();
            }
            else
            {
                ProcessEventInternal(evt);
            }
        }

        void ProcessEventInternal(SimEvent evt)
        {
            switch (evt.Type)
            {
                case EventType.AbilityDamage:
                    break;
                case EventType.AbilitySelect:
                    break;
                case EventType.DotTick:
                    break;
                case EventType.ImpulseRefresh:
                    break;
                case EventType.RemoveBuff:
                    break;
                case EventType.ResourceGain:

                    break;
            }
        }
    }
}
