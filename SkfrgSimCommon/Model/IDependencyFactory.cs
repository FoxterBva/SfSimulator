using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public interface IDependencyFactory
	{
		ILogger GetLogger();
	}

	public class DefaultDependencyFactory : IDependencyFactory
	{
		public ILogger GetLogger()
		{
			return new DefaultLogger();
		}
	}

	public class SilentDependencyFactory : IDependencyFactory
	{
		public ILogger GetLogger()
		{
			return new SilentLogger();
		}
	}
}
