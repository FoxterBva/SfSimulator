using SkfrgSimCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Classes
{
    public class TargetActor : Actor
    {
        public TargetActor() : base(null, null, null)
        {}

        protected override string SelectAbility(EnvironmentContext context)
        {
            return String.Empty;
        }
    }
}
