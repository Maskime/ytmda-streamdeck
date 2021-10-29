//  ************************************************************************************************
// 
//   © 2021       Cytiva
// 
//   Description  See class summary below.
// 
//   History      See source code control system.
// 
//  ************************************************************************************************

using System;

namespace YTMDesktop.errors
{
    public class YtmdaExceptionBase:Exception
    {
        protected YtmdaExceptionBase(string message):base(message)
        {
        }
    }
}