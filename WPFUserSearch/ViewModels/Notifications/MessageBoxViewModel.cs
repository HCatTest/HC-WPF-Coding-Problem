using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Models.Notifications;
using WPFUserSearch.ViewModels;

namespace WPFUserSearch.ViewModels
{
    public class MessageBoxViewModel : IInteractionRequestAware, INotifyPropertyChanged
    {
        #region CTOR

        public MessageBoxViewModel()
        {
        }

        #endregion

        #region Methods

        private void ButtonsVisibilitySet(MessageBoxInputEnum messageBoxOperation)
        {
            switch (messageBoxOperation)
            {
                case MessageBoxInputEnum.OK:

                    this.ButtonYesIsVisible = false;
                    this.ButtonNoIsVisible = false;
                    this.ButtonOKIsVisible = true;
                    this.ButtonCancelIsVisible = false;

                    break;

                case MessageBoxInputEnum.OKCancel:

                    this.ButtonYesIsVisible = false;
                    this.ButtonNoIsVisible = false;
                    this.ButtonOKIsVisible = true;
                    this.ButtonCancelIsVisible = true;

                    break;

                case MessageBoxInputEnum.YesNo:

                    this.ButtonYesIsVisible = true;
                    this.ButtonNoIsVisible = true;
                    this.ButtonOKIsVisible = false;
                    this.ButtonCancelIsVisible = false;

                    break;
            }
        }

        #endregion

        #region Properties

        public Action FinishInteraction { get; set; }
        private MessageBoxNotification _notification;

        private bool _buttonYesIsVisible;
        private bool _buttonNoIsVisible;
        private bool _buttonOKIsVisible;
        private bool _buttonCancelIsVisible;

        public bool ButtonYesIsVisible
        {
            get { return this._buttonYesIsVisible; }
            set
            {
                if (value != this._buttonYesIsVisible)
                {
                    this._buttonYesIsVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ButtonNoIsVisible
        {
            get { return this._buttonNoIsVisible; }
            set
            {
                if (value != this._buttonNoIsVisible)
                {
                    this._buttonNoIsVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ButtonOKIsVisible
        {
            get { return this._buttonOKIsVisible; }
            set
            {
                if (value != this._buttonOKIsVisible)
                {
                    this._buttonOKIsVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ButtonCancelIsVisible
        {
            get { return this._buttonCancelIsVisible; }
            set
            {
                if (value != this._buttonCancelIsVisible)
                {
                    this._buttonCancelIsVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public INotification Notification
        {
            get { return this._notification; }
            set
            {
                if (value != this._notification)
                {
                    this._notification = (MessageBoxNotification)value;
                    NotifyPropertyChanged();

                    ButtonsVisibilitySet(_notification.Input);
                }
            }
        }

        #endregion

        #region Commands

        private DelegateCommand<object> _commandInteraction;
        public DelegateCommand<object> CommandInteraction
        {
            get
            {
                return _commandInteraction ?? (_commandInteraction = new DelegateCommand<object>((_o) =>
                {
                    switch(_o.ToString())
                    {
                        case "Yes":

                            _notification.Confirmed = true;
                            _notification.Output = MessageBoxOutputEnum.Yes;

                            break;

                        case "No":

                            _notification.Confirmed = false;
                            _notification.Output = MessageBoxOutputEnum.No;

                            break;

                        case "Cancel":

                            _notification.Confirmed = false;
                            _notification.Output = MessageBoxOutputEnum.Cancel;

                            break;

                        case "OK":

                            _notification.Confirmed = true;
                            _notification.Output = MessageBoxOutputEnum.OK;

                            break;

                    }

                    FinishInteraction?.Invoke();
                }));
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}