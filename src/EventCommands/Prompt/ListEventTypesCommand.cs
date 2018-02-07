using Dnn.PersonaBar.Library.Prompt;
using Dnn.PersonaBar.Library.Prompt.Attributes;
using Dnn.PersonaBar.Library.Prompt.Models;
using Dnn.Prompt.Event.Commands.Prompt.models;
using DotNetNuke.Services.Log.EventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.Prompt.Event.Commands.Prompt
{
    [ConsoleCommand("list-events", "Prompt_ListEventsCommand_Category", Constants.CommandDescription)]
    public class ListEvents : ConsoleCommandBase
    {
        [FlagParameter("logtype", Constants.FlagLogType, "String")]
        private const string FlagLogType = "logtype";
        [FlagParameter("enabled", Constants.FlagEnabled, "Boolean")]
        private const string FlagEnabled = "enabled";
        [FlagParameter("notify", Constants.FlagNotify, "Boolean")]
        private const string FlagNotify = "notify";

        private string LogType { get; set; }
        private bool? Enabled { get; set; }
        private bool? Notify { get; set; }

        public override void Init(string[] args, DotNetNuke.Entities.Portals.PortalSettings portalSettings,
            DotNetNuke.Entities.Users.UserInfo userInfo, int activeTabId)
        {
            base.Init(args, portalSettings, userInfo, activeTabId);
            LogType = GetFlagValue(FlagLogType, "LogType", "");
            Enabled = GetFlagValue<bool?>(FlagEnabled, "Enabled", null);
            Notify = GetFlagValue<bool?>(FlagNotify, "Notify", null);


        }

        public override ConsoleResultModel Run()
        {
            var logTypes =
                    LogController.Instance.GetLogTypeInfoDictionary()
                        .Values.OrderBy(t => t.LogTypeFriendlyName)
                        .ToList();

            var logModel = (from logtype in logTypes
                           select new LogTypeModel(logtype))
                           .ToList();

            return new ConsoleResultModel
            {
                Data = logModel,
                PagingInfo = new PagingInfo
                {
                    PageNo = 1,
                    TotalPages = 1,
                    PageSize = 5000
                },
                Records = logTypes?.Count ?? 0,
                Output = ""
            };
        }

        public override string LocalResourceFile => "~/DesktopModules/EventCommands/App_LocalResources/Event.Commands.resx";
    }
}