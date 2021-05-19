using System;
using System.Collections.Generic;
using System.Text;

namespace TinyIoC_Sample.Services
{
    public interface IDatabaseService
    {
        string Get();
    }

    public class DatabaseService : IDatabaseService
    {
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }
}
