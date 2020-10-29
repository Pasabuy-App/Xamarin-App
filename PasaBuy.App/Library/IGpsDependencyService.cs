using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Library
{
    public interface IGpsDependencyService
    {
        void OpenSettings();
        bool IsGpsEnable();
    }
}
