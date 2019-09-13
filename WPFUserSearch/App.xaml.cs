using System.Windows;

using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using WPFUserSearch.Models.Storage;
using WPFUserSearch.Views;
using WPFUserSearch.Data.Services;

namespace WPFUserSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            AppStorage.Instance.EventAggregator = Container.Resolve<IEventAggregator>();

            containerRegistry.Register<IDataService, DataService>();

            containerRegistry.RegisterForNavigation<UserSearchView>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate("MainContentRegion", typeof(UserSearchView).Name);
        }
    }
}
