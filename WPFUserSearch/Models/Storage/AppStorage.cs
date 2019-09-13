using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

using Prism.Mvvm;
using Prism.Events;

using WPFUserSearch.Models;

namespace WPFUserSearch.Models.Storage
{
    public sealed class AppStorage : BaseModel
    {
        #region Fields

        #endregion

        #region CTOR

        private static readonly AppStorage instance = new AppStorage();
        static AppStorage() { }
        private AppStorage() { }
        public static AppStorage Instance { get { return instance; } }

        #endregion

        #region Methods

        /// <summary>
        /// Single time initialization
        /// </summary>
        public void Initialize()
        {
        }

        private void InitializeLanguage()
        {
        }

        private void LoadLanguage()
        {
        }

        #endregion

        #region EventHandlers

        #endregion

        #region Properties

        #endregion

        #region EventAggregator

        public IEventAggregator EventAggregator { get; set; }

        #endregion
    }
}