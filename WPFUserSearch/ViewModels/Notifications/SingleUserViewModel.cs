using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Win32;

using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Infrastructure.Utilities;
using WPFUserSearch.Models.Notifications;
using WPFUserSearch.ViewModels;
using WPFUserSearch.Data.Models.DB;
using WPFUserSearch.Data.Services;

namespace WPFUserSearch.ViewModels
{
    public class SingleUserViewModel : BaseViewModel, IInteractionRequestAware, INotifyPropertyChanged
    {
        #region Fields

        DataService _dataService;

        #endregion

        #region CTOR

        public SingleUserViewModel(DataService dataService)
        {
            _dataService = dataService;

            InitializeBindings();
        }

        #endregion

        #region Methods

        private void InitializeData()
        {
            // Data initialization can happen only on Notification property changed

            if (_notification.Mode == SingleUserViewModeEnum.Create)
            {
            }
            else if (_notification.Mode == SingleUserViewModeEnum.Update)
            {
                this.PhotoTMP = _notification.User.Photo;
            }
        }

        private void InitializeBindings()
        {
            this.CommandInsertImage = new DelegateCommand(CommandInsertImage_Execute, CommandInsertImage_CanExecute);
            this.CommandInteractionUpdate = new DelegateCommand(CommandInteractionUpdate_Execute, CommandInteractionUpdate_CanExecute);
            this.CommandInteractionCancel = new DelegateCommand(CommandInteractionCancel_Execute, CommandInteractionCancel_CanExecute);
        }

        private void InsertImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select an image";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (dialog.ShowDialog() == true)
            {
                _notification.User.Photo = ImageUtilities.ConvertBitmapSourceToByteArray(dialog.FileName);
                this.PhotoTMP = _notification.User.Photo;
            }
        }

        private async Task<string> CreateUser()
        {
            return await _dataService.CreateUser(_notification.User);
        }

        private async Task<string> UpdateUser()
        {
            return await _dataService.UpdateUser(_notification.User);
        }

        #endregion

        #region Properties

        public Action FinishInteraction { get; set; }
        private SingleUserNotification _notification;
        private byte[] _photoTMP;

        public INotification Notification
        {
            get { return this._notification; }
            set
            {
                if (value != this._notification)
                {
                    this._notification = (SingleUserNotification)value;
                    NotifyPropertyChanged();

                    InitializeData();
                }
            }
        }

        public byte[] PhotoTMP
        {
            get { return this._photoTMP; }
            set
            {
                if (value != this._photoTMP)
                {
                    this._photoTMP = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public DelegateCommand CommandInsertImage { get; set; }
        private void CommandInsertImage_Execute()
        {
            InsertImage();
        }

        private bool CommandInsertImage_CanExecute()
        {
            return true;
        }

        public DelegateCommand CommandInteractionUpdate { get; set; }
        private async void CommandInteractionUpdate_Execute()
        {
            var result = _notification.Mode == SingleUserViewModeEnum.Create ?
                await CreateUser() :
                await UpdateUser();

            if (string.IsNullOrEmpty(result))
            {
                _notification.Confirmed = false;
                FinishInteraction?.Invoke();
            }
            else if (result == "UpdateSuccess")
            {
                _notification.Confirmed = true;
                FinishInteraction?.Invoke();
            }
            else
            {
                MessageBoxShowOK(result);
            }
        }

        private bool CommandInteractionUpdate_CanExecute()
        {
            return true;
        }

        public DelegateCommand CommandInteractionCancel { get; set; }
        private void CommandInteractionCancel_Execute()
        {
            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }

        private bool CommandInteractionCancel_CanExecute()
        {
            return true;
        }

        #endregion
    }
}