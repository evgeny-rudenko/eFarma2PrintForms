using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;

namespace ePlus.ReferenceExchange
{
	public class YaroslavlExportAction : IPluginGridAction
	{

		#region IPluginGridAction Members

		public void Execute(IGridController gridController, System.Data.DataRow row)
		{
			
		}

		public bool IsEnabled(IGridController gridController, System.Data.DataRow row)
		{
			return true;
		}

		#endregion
	}
}
