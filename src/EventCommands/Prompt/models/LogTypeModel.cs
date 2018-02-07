using DotNetNuke.Services.Log.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Prompt.Event.Commands.Prompt.models
{
    public class LogTypeModel
    {
        public string LogType { get; set; }
        public bool Enabled { get; set; }
        public bool Notify { get; set; }

        public LogTypeModel()
        {
        }
        public LogTypeModel(LogTypeInfo logtype)
        {
            LogType = logtype.LogTypeFriendlyName;
            Enabled = true;
            Notify = false;
        }
    }
}