using System;

using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Infrastructure;

namespace WPFUserSearch.Models.Notifications
{
    public class MessageBoxNotification : Confirmation, IMessageBoxNotification
    {
        #region CTOR

        public MessageBoxNotification(Guid pk)
        {
            this.PK = pk;
        }

        public MessageBoxNotification() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public Guid PK { get; set; }

        public string Message { get; set; }
        public MessageBoxOutputEnum Output { get; set; }
        public MessageBoxInputEnum Input { get; set; }

        #endregion
    }
}