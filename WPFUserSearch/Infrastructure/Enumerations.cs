using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUserSearch.Infrastructure
{
    public enum SingleUserViewModeEnum
    {
        Create,
        Update,
    }

    public enum CommandNameEnum
    {
        None,
        MessageBoxShow,
    }

    // MessageBox buttons - available combinations to show
    public enum MessageBoxInputEnum
    {
        None,
        OK,
        YesNo,
        OKCancel,
    }

    // MessageBox buttons - single pressed button
    public enum MessageBoxOutputEnum
    {
        None,
        Yes,
        No,
        OK,
        Cancel,
    }
}
