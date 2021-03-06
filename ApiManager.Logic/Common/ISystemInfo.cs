﻿using System;

namespace ApiManager.Logic.Common
{
    public interface ISystemInfo
    {
        int State { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
