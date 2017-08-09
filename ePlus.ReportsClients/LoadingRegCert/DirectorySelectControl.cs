using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ePlus.CommonEx.Controls;
using ePlus.MetaData.Client;

namespace LoadingRegCert
{
    public class DirectorySelectControl : MetaPluginDictionarySelectControl
    {
        public DirectorySelectControl()
        {
            this.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
            this.ClearTextOnValidatingIfValueIsEmpty = false;
            this.UseSpaceToOpenPlugin = false;
        }

        public override bool UseEnterToOpenPlugin
        {
            get { return string.IsNullOrEmpty(this.Text); }
        }

        public override string PluginMnemocode
        {
            get
            {
                return string.Empty;
            }
        }

        protected override void SelectEntity()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() != DialogResult.OK) return;
                Value = new DataRowItem(1, Guid.Empty, string.Empty, dialog.SelectedPath);
            }
        }

        protected override void SetFont()
        {
            return;
        }
    }
}
