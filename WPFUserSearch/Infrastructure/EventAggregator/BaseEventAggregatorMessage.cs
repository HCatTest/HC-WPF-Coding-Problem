using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.ViewModels;

namespace WPFUserSearch.Infrastructure.EventAggregator
{
    public class BaseEventAggregatorToken : PubSubEvent<BaseEventAggregatorMessage> { };

    public class BaseEventAggregatorMessage : ICloneable
    {
        #region CTOR

        public BaseEventAggregatorMessage(Guid pk)
        {
            this.PK = pk;
        }

        public BaseEventAggregatorMessage() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        public Guid PK { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string SenderName { get; set; }
        public string RecepientName { get; set; } = typeof(MainWindowViewModel).Name;
        public object InputParameter0 { get; set; }
        public object InputParameter1 { get; set; }
        public object InputParameter2 { get; set; }
        public object OutputParameter0 { get; set; }
        public object OutputParameter1 { get; set; }
        public object OutputParameter2 { get; set; }

        // Added for convinience, xxxParameterxxx can be used insteda
        public CommandNameEnum Command { get; set; }
        public MessageBoxInputEnum MessageBoxInput { get; set; }
        public MessageBoxOutputEnum MessageBoxOutput { get; set; }

        #endregion

        #region Implementations

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
