using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Prism.Events;
using Prism.Mvvm;

using WPFUserSearch.Infrastructure;
using WPFUserSearch.Infrastructure.EventAggregator;
using WPFUserSearch.Models.Storage;

namespace WPFUserSearch.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Fields

        #endregion

        #region CTOR

        public BaseViewModel(Guid pk)
        {
            this.PK = pk;
        }

        public BaseViewModel() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows simple 'OK' messagebox, without return value
        /// </summary>
        /// <param name="Caption"></param>
        public void MessageBoxShowOK(string caption, string title = null)
        {
            AppStorage.Instance.EventAggregator.GetEvent<BaseEventAggregatorToken>().Publish(new BaseEventAggregatorMessage()
            {
                Title = title ?? "WPF",
                Caption = caption,
                Command = CommandNameEnum.MessageBoxShow,
                MessageBoxInput = MessageBoxInputEnum.OK,
            });
        }

        #endregion

        #region Properties

        private Guid _pk;

        public Guid PK
        {
            get { return this._pk; }
            set
            {
                if (value != this._pk)
                {
                    this._pk = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Implementations

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                DataCheckHasErrors();
            }
        }

        #endregion

        #region INotifyDataErrorInfo

        public virtual void DataCheckHasErrors() { }
        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName)) return null;
            return _validationErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        #endregion
    }
}
