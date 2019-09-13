using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WPFUserSearch.Models
{
    public class BaseModel : INotifyPropertyChanged, ICloneable
    {
        #region Fields

        public Action<object, string> PropertyChangedAction;

        #endregion

        #region CTOR

        public BaseModel(Guid pk)
        {
            this.PK = pk;
            this.IsInitialized = true;
        }

        public BaseModel() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region Properties

        private Guid _pk;
        private bool _isInitialized;
        private bool _isSelected;
        private string _name;
        private int _order;
        private string _translationKeyword;

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

        public bool IsInitialized
        {
            get { return this._isInitialized; }
            set
            {
                if (value != this._isInitialized)
                {
                    this._isInitialized = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get { return this._isSelected; }
            set
            {
                if (value != this._isSelected)
                {
                    this._isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return this._name; }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Order
        {
            get { return this._order; }
            set
            {
                if (value != this._order)
                {
                    this._order = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string TranslationKeyword
        {
            get { return this._translationKeyword; }
            set
            {
                if (value != this._translationKeyword)
                {
                    this._translationKeyword = value;
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

            PropertyChangedAction?.Invoke(this, propertyName);
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
