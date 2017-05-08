﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace GitHub.Unity
{
    class GitConfigListTask : ProcessTaskWithListOutput<KeyValuePair<string, string>>
    {
        private readonly string arguments;

        public GitConfigListTask(GitConfigSource configSource, CancellationToken token, BaseOutputListProcessor<KeyValuePair<string, string>> processor = null)
            : base(token, processor ?? new ConfigOutputProcessor())
        {
            var source = "";
            if (configSource != GitConfigSource.NonSpecified)
            {
                source = "--";
                source += configSource == GitConfigSource.Local
                    ? "local"
                    : (configSource == GitConfigSource.User
                        ? "system"
                        : "global");
            }
            arguments = String.Format("config {0} -l", source);
        }

        public override string Name { get { return "git config list"; } }
        public override string ProcessArguments { get { return arguments; } }
        private TaskAffinity affinity = TaskAffinity.Concurrent;
        public override TaskAffinity Affinity { get { return affinity; } set { affinity = value; } }
    }
}