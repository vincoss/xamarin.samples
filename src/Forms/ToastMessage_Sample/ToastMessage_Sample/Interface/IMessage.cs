using System;
using System.Collections.Generic;
using System.Text;


namespace ToastMessage_Sample.Interface
{
    public interface IMessage
    {
        void ShortAlert(string message);
        void LongAlert(string message);
    }
}
