using System;

using Prism.Interactivity.InteractionRequest;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Data.Models.DB;

namespace WPFUserSearch.Models.Notifications
{
    public class SingleUserNotification : Confirmation, ISingleUserNotification
    {
        #region CTOR

        public SingleUserNotification(Guid pk)
        {
            this.PK = pk;
        }

        public SingleUserNotification() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public Guid PK { get; set; }
        public User User { get; set; }
        public SingleUserViewModeEnum Mode { get; set; }

        #endregion
    }
}