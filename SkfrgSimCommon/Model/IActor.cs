using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public interface IActor
	{
		void Tick(int time, IDependencyFactory factory);
	}
}
