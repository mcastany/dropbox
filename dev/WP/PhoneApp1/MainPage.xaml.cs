using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhoneApp1.Resources;
using PhoneApp1.ViewModel;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Authenticate();
            this.DataContext = new ViewModel.MainPageViewModel(); ;
        }

        private static async Task Authenticate()
        {
            var provider = "Dropbox";

            // Provide some additional app-specific security for the encryption.
            byte[] entropy = { 1, 8, 3, 6, 5 };

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            while (App.MobileService.CurrentUser == null)
            {
                if (settings.Contains(provider))
                {
                    // Get the encrypted byte array, decrypt and deserialize the user.
                    var encryptedUser = settings[provider] as byte[];
                    var userBytes = ProtectedData.Unprotect(encryptedUser, entropy);
                    App.MobileService.CurrentUser = JsonConvert.DeserializeObject<MobileServiceUser>(Encoding.Unicode.GetString(userBytes, 0, userBytes.Length));
                }

                if (App.MobileService.CurrentUser == null)
                {
                    try
                    {
                        // Login with the identity provider.
                        var user = await App.MobileService.LoginAsync(provider);

                        // Serialize the user into an array of bytes and encrypt with DPAPI.
                        var userBytes = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(user));
                        byte[] encryptedUser = ProtectedData.Protect(userBytes, entropy);

                        // Store the encrypted user credentials in local settings.
                        settings.Add(provider, encryptedUser);
                        settings.Save();

                        App.MobileService.CurrentUser = user;
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var file = (sender as ListBox).SelectedItem as Files;
            if (file != null)
            {
                if (file.IsFolder != true)
                {
                    return;
                }
                var vm = (MainPageViewModel)this.DataContext;

                vm.UpdatePath(file.Name);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (MainPageViewModel)this.DataContext;
            if (vm != null)
            {
                var path = vm.Path;
                if (path != "/")
                {
                    vm.UpdatePath(path.Remove(path.LastIndexOf("/", StringComparison.Ordinal)));
                }
            }
        }
    }
}