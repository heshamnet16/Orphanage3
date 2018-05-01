using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrphanageV3.ViewModel.Tools
{
    public class DownloadViewModel
    {
        public ObservableCollection<DownloadDataModel> DownloadedDataList { get; private set; }

        public delegate void DownloadDataModelDelegate(DownloadDataModel downloadDataModel);

        public event DownloadDataModelDelegate Added;

        public event DownloadDataModelDelegate Removed;

        public event DownloadDataModelDelegate Downloaded;

        private static int counter = 0;

        public DownloadViewModel()
        {
            DownloadedDataList = new ObservableCollection<DownloadDataModel>();
        }

        public async void Add(DownloadDataModel downloadDataModel, Task<byte[]> callback)
        {
            downloadDataModel.Id = counter++;
            DownloadedDataList.Add(downloadDataModel);
            Added?.Invoke(downloadDataModel);
            downloadDataModel.Data = await callback;
            Downloaded?.Invoke(downloadDataModel);
        }

        public void Remove(DownloadDataModel downloadDataModel)
        {
            DownloadedDataList.Remove(downloadDataModel);
            Removed?.Invoke(downloadDataModel);
        }

        public void Save(DownloadDataModel downloadDataModel)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog.FileName = downloadDataModel.Name;
            saveFileDialog.Filter = getFilter(downloadDataModel.DataType);
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowHelp = false;
            saveFileDialog.SupportMultiDottedExtensions = false;
            saveFileDialog.Title = Properties.Resources.SaveAs;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                    File.Delete(saveFileDialog.FileName);
                File.WriteAllBytes(saveFileDialog.FileName, downloadDataModel.Data);
                Remove(downloadDataModel);
            }
        }

        public DownloadDataModel Get(int id)
        {
            if (id >= 0)
            {
                return DownloadedDataList.FirstOrDefault(d => d.Id == id);
            }
            else
            {
                return null;
            }
        }

        private string getFilter(FileExtentionEnum fileExtentionEnum)
        {
            switch (fileExtentionEnum)
            {
                case FileExtentionEnum.docx:
                    return "Word files| *.docx";

                case FileExtentionEnum.jpg:
                    return "JPEG files| *.jpg";

                case FileExtentionEnum.xlsx:
                    return "Excel files| *.xlsx";

                default:
                    return "All files | *.*";
            }
        }
    }
}