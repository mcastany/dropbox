namespace PhoneApp1.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System;
    using System.Xml.Serialization;

    [XmlRoot("root")]
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _path;

        [XmlElement("username")]
        public string UserName { get; set; }

        [XmlElement("path")]
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                this._path = value;
                this.GetFiles(this._path);
                this.RaisePropertyChanged("Path");
            }
        }

        public MainPageViewModel()
        {
            this.Path = "/";
            this.GetUserName();
        }

        [XmlArray("files")]
        [XmlArrayItem("file")]
        public ObservableCollection<Files> Files { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task GetUserName()
        {
            try
            {
                var user = await App.MobileService.InvokeApiAsync<dynamic>("UserInfo", HttpMethod.Get, null);
                this.UserName = user["display_name"];
                this.RaisePropertyChanged("UserName");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        public void UpdatePath(string path)
        {
            this.Path = path;
        }

        private async void GetFiles(string path)
        {
            try
            {
                var param = new Dictionary<string, string> { { "path", path } };

                var content = await App.MobileService.InvokeApiAsync<dynamic>("Files", HttpMethod.Get, param);
                var filesList = new ObservableCollection<Files>();

                foreach (var item in content.contents)
                {
                    filesList.Add(new Files() { Name = item.path, IsFolder = item.is_dir });
                }

                this.Files = filesList;
                this.RaisePropertyChanged("Files");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}