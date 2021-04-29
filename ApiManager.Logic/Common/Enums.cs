using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Common
{
    public enum Unit
    {
        Hours = 1,
        Minutes = 2,
        Seconds = 3
    }

    public enum ErrorType
    {
        NONE = 0,
        ALERT = 1,  // Alert: action must be taken immediately
        CRIT = 2,   // Critical: critical conditions
        ERR = 3,    // Error: error conditions
        WARN = 4,   // Warning: warning conditions
        NOTICE = 5, // Notice: normal but significant condition
        INFO = 6,   // Informational: informational messages
        DEBUG = 7,   // Debug: debug messages
        EMERG = 8 // Emergency: system is unusable
    }

    public enum AuthenticationType
    {
        None = 0,
        Basic = 1,
        OAuth2 = 2,
        Windows = 3,
        ApiKey = 4,
        Header = 5,
        BasicWithSHA1 = 6
    }

    public enum ApiType
    {
        Rest = 1,
        GraphQL = 2
    }

    public enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Patch = 4,
        Delete = 5,
        Generic = 6
    }

    public enum GraphQLMethod
    {
        Query = 1,
        Mutation = 2
    }

    public enum TaskType
    {
        API = 1,
        FTP = 2,
        FILE = 3,
        MAIL = 4,
        RECEIVESEND = 5
    }

    public enum ActionType
    {
        API = 1,
        FTP = 2,
        FILE = 3,
        MAIL = 4,
        SQL = 5
    }

    public enum FtpSyncMethod
    {
        Upload = 1,
        Download = 2,
        Synchonize = 3
    }

    public enum FtpConnectionType
    {
        FTP = 1,
        SFTP = 2
    }

    public enum AccessType
    {
        Inbound = 1,
        Outbound = 2
    }
    public enum Period
    {
        Hour = 1,
        Day = 2,
        Week = 3,
        Month = 4
    }


    public enum ScheduleType
    {
        Once = 0,
        Daily = 1,
        Weekly = 2
    }
}
