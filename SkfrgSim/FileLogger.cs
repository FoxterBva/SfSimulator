using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon;
using System.IO;

namespace SkfrgSim
{
	public class FileLogger : ILogger, IDisposable
	{
		StreamWriter sw;

		public FileLogger(string fileName)
		{
			sw = new StreamWriter(fileName);
		}

		public void Dispose()
		{
			if (sw != null)
				sw.Dispose();
		}

		public void Log(string message)
		{
			sw.WriteLine(message);
		}
	}
}
