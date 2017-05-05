﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace GitHub.Unity
{
    class GitConfigListTask : ProcessTaskWithListOutput<KeyValuePair<string, string>>
    {
        private readonly string arguments;

        public GitConfigListTask(GitConfigSource configSource, CancellationToken token, ConfigOutputProcessor processor = null)
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
        public override TaskAffinity Affinity { get { return TaskAffinity.Exclusive; } }
    }
}