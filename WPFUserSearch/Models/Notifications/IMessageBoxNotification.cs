using System;

using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Infrastructure;

namespace WPFUserSearch.Models.Notifications
{
    public interface IMessageBoxNotification : IConfirmation
    {
        Guid PK { get; set; }

        string Message { get; set; }
        MessageBoxInputEnum Input { get; set; }
        MessageBoxOutputEnum Output { get; set; }
    }
}
