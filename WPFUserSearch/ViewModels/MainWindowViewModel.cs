using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Models.Storage;
using WPFUserSearch.Models.Notifications;
using WPFUserSearch.Infrastructure;
using WPFUserSearch.Infrastructure.EventAggregator;

namespace WPFUserSearch.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        MessageBoxNotification _messageBoxNotification;

        #endregion

        #region CTOR

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            this.MessageBoxNotificationRequest = new InteractionRequest<MessageBoxNotification>();

            AppStorage.Instance.EventAggregator.GetEvent<BaseEventAggregatorToken>().Subscribe(EventAggregatorMessageProcess);
        }

        #endregion

        #region Notifications

        private void MessageBoxNotificationRequestRaise(BaseEventAggregatorMessage messageInput)
        {
            // Show LoginView in 'login' mode
            _messageBoxNotification = new MessageBoxNotification()
            {
                Title = messageInput.Title ?? "WPFUserSearch",
                Message = messageInput.Caption,
                Input = messageInput.MessageBoxInput,
            };

            this.MessageBoxNotificationRequest.Raise(_messageBoxNotification, Raised =>
            {
                MessageBoxNotificationRequestProcess(messageInput, _messageBoxNotification);
            });
        }

        private void MessageBoxNotificationRequestProcess(BaseEventAggregatorMessage messageInput, MessageBoxNotification notification)
        {
            BaseEventAggregatorMessage messageOutput = messageInput.Clone() as BaseEventAggregatorMessage;

            messageOutput.MessageBoxOutput = notification.Output;
            messageOutput.SenderName = messageInput.RecepientName;
            messageOutput.RecepientName = messageInput.SenderName;

            AppStorage.Instance.EventAggregator.GetEvent<BaseEventAggregatorToken>().Publish(messageOutput);
        }

        #endregion

        #region Properties

        public InteractionRequest<MessageBoxNotification> MessageBoxNotificationRequest { get; set; }

        #endregion

        #region Commands

        private DelegateCommand<object> _commandMessageBoxOK;
        public DelegateCommand<object> CommandMessageBoxOK
        {
            get
            {
                return _commandMessageBoxOK ?? (_commandMessageBoxOK = new DelegateCommand<object>((_o) =>
                {
                    MessageBoxShowOK("Testing");
                }));
            }
        }

        #endregion


        #region EventAggregator

        public void EventAggregatorMessageProcess(BaseEventAggregatorMessage messageInput)
        {
            // Process only relevant messages
            if (messageInput.RecepientName != this.GetType().Name) return;

            switch (messageInput.Command)
            {
                case CommandNameEnum.MessageBoxShow:

                    MessageBoxNotificationRequestRaise(messageInput);
                    break;
            }
        }

        #endregion

    }
}
