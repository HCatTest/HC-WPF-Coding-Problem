using System;

using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Data.Models.DB;

namespace WPFUserSearch.Models.Notifications
{
    public interface ISingleUserNotification : IConfirmation
    {
        Guid PK { get; set; }
        User User { get; set; }
        SingleUserViewModeEnum Mode { get; set; }
    }
}
