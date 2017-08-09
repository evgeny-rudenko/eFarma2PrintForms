using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Client;
using ePlus.MetaData.Core;

namespace DocsRegistryEx
{
  public class DocRegistryReportAction:IPluginModule
  {
    public void Execute()
    {
      DocsRegistryForm form = new DocsRegistryForm();
      AppManager.RegisterForm(form);
      form.Init();
      form.Show();      
    }
  }
}
