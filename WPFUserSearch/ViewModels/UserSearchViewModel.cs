using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Infrastructure.EventAggregator;
using WPFUserSearch.Infrastructure.Utilities;
using WPFUserSearch.Models.Notifications;
using WPFUserSearch.Data.Helpers;
using WPFUserSearch.Data.Models.DB;
using WPFUserSearch.Data.Services;

namespace WPFUserSearch.ViewModels
{
    public class UserSearchViewModel : BaseViewModel
    {
        #region Fields

        DataService _dataService;
        SingleUserNotification _singleUserNotification;

        #endregion

        #region CTOR

        public UserSearchViewModel(DataService dataService)
        {
            _dataService = dataService;

            SingleUserNotificationRequest = new InteractionRequest<SingleUserNotification>();

            InitializeBindings();
            InitializeData();
        }

        #endregion

        #region Methods

        private async void InitializeData()
        {
            this.ViewModelIsBusy = true;
            this.UserCollection = new ObservableCollection<User>(await _dataService.GetUsers());
            this.UserAutocompleteCollection = new ObservableCollection<string>(await _dataService.GetUsersFullNames());
            this.UserSelectedItem = null;
            this.UserAutocompleteSelectedItem = null;
            this.UserAutocompleteText = null;
            this.ViewModelIsBusy = false;
        }

        private void InitializeBindings()
        {
            this.CommandApplyFilter = new DelegateCommand(CommandApplyFilter_Execute, CommandApplyFilter_CanExecute);
            this.CommandResetFilter = new DelegateCommand(CommandResetFilter_Execute, CommandResetFilter_CanExecute);
            this.CommandCreateUser = new DelegateCommand(CommandCreateUser_Execute, CommandCreateUser_CanExecute);
            this.CommandUpdateUser = new DelegateCommand(CommandUpdateUser_Execute, CommandUpdateUser_CanExecute);
        }

        private async void ApplyFilter()
        {
            this.ViewModelIsBusy = true;
            this.UserCollection = new ObservableCollection<User>(await _dataService.GetUsers(this.UserAutocompleteText));
            this.ViewModelIsBusy = false;
        }

        private void ResetFilter()
        {
            InitializeData();
        }

        #endregion

        #region Notifications

        private void SingleUserNotificationRequestRaise(BaseEventAggregatorMessage messageInput, SingleUserViewModeEnum mode)
        {
            _singleUserNotification = new SingleUserNotification()
            {
                Title = mode == SingleUserViewModeEnum.Create ?
                    "Create User" :
                    "Update User",
                Mode = mode,
                User = mode == SingleUserViewModeEnum.Create ?
                    new User() { PK_User = Guid.NewGuid(), } :
                    this.UserSelectedItem,
            };

            this.SingleUserNotificationRequest.Raise(_singleUserNotification, Raised =>
            {
                if (Raised.Confirmed) InitializeData();
            });
        }

        #endregion

        #region Properties

        public InteractionRequest<SingleUserNotification> SingleUserNotificationRequest { get; set; }

        private bool _viewModelIsBusy;

        private ObservableCollection<User> _userCollection;
        private User _userSelectedItem;

        private ObservableCollection<string> _userAutocompleteCollection;
        private string _userAutocompleteSelectedItem;
        private string _userAutocompleteText;

        public bool ViewModelIsBusy
        {
            get { return this._viewModelIsBusy; }
            set
            {
                if (value != this._viewModelIsBusy)
                {
                    this._viewModelIsBusy = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<User> UserCollection
        {
            get { return this._userCollection; }
            set
            {
                if (value != this._userCollection)
                {
                    this._userCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public User UserSelectedItem
        {
            get { return this._userSelectedItem; }
            set
            {
                if (value != this._userSelectedItem)
                {
                    this._userSelectedItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> UserAutocompleteCollection
        {
            get { return this._userAutocompleteCollection; }
            set
            {
                if (value != this._userAutocompleteCollection)
                {
                    this._userAutocompleteCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string UserAutocompleteSelectedItem
        {
            get { return this._userAutocompleteSelectedItem; }
            set
            {
                if (value != this._userAutocompleteSelectedItem)
                {
                    this._userAutocompleteSelectedItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string UserAutocompleteText
        {
            get { return this._userAutocompleteText; }
            set
            {
                if (value != this._userAutocompleteText)
                {
                    this._userAutocompleteText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public DelegateCommand CommandApplyFilter { get; set; }
        private void CommandApplyFilter_Execute()
        {
            ApplyFilter();
        }

        private bool CommandApplyFilter_CanExecute()
        {
            return true;
        }

        public DelegateCommand CommandResetFilter { get; set; }
        private void CommandResetFilter_Execute()
        {
            ResetFilter();
        }

        private bool CommandResetFilter_CanExecute()
        {
            return true;
        }

        public DelegateCommand CommandCreateUser { get; set; }
        private void CommandCreateUser_Execute()
        {
            SingleUserNotificationRequestRaise(new BaseEventAggregatorMessage(), SingleUserViewModeEnum.Create);
        }

        private bool CommandCreateUser_CanExecute()
        {
            return true;
        }

        public DelegateCommand CommandUpdateUser { get; set; }
        private void CommandUpdateUser_Execute()
        {
            if (this.UserSelectedItem == null) return;

            SingleUserNotificationRequestRaise(new BaseEventAggregatorMessage(), SingleUserViewModeEnum.Update);
        }

        private bool CommandUpdateUser_CanExecute()
        {
            return true;
        }

        #endregion
    }
}
