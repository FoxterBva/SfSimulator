using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSim
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

			Test test = new Test();

			test.EventBasedTest();
		}
	}
}
