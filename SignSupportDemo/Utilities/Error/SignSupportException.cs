using System;
using SignSupportDemo.SignSupport.API;

namespace SignSupportDemo.Utilities.Error
{
    [Serializable]
    internal class SignSupportException : Exception
    {
        public readonly SignSupportError SignSupportError;

        public SignSupportException(SignSupportError signSupportError)
        {
            this.SignSupportError = signSupportError;
        }
    }
}