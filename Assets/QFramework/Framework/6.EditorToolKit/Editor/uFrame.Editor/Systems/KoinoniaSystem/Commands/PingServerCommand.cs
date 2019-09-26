﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using QF.GraphDesigner;

namespace QF.GraphDesigner.Unity.KoinoniaSystem.Commands
{
    public class PingServerCommand : IBackgroundCommand
    {
        public BackgroundWorker Worker { get; set; }
        public string Server { get; set; }
        public string Title { get; set; }
    }
}
