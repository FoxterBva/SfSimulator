using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon
{
	public interface ILogger
	{
		void Log(string message);
	}

	public class DefaultLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}

	public class SilentLogger : ILogger
	{
		public void Log(string message)
		{
			
		}
	}
}
