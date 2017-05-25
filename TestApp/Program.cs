﻿using System;
using Mono.Options;
using System.IO;
using System.Threading;

namespace TestApp
{
    class Program
    {
        static int Main(string[] args)
        {
            int retCode = 0;
            string ret = String.Empty;
            int sleepms = 0;
            var p = new OptionSet();
            p = p
                .Add("r=", (int v) => retCode = v)
                .Add("d=|data=", v => ret = v)
                .Add("f=|file=", v => ret = File.ReadAllText(v))
                .Add("s=|sleep=", (int v) => sleepms = v)
                .Add("h|help", v => p.WriteOptionDescriptions(Console.Out));

            p.Parse(args);

            if (sleepms > 0)
                Thread.Sleep(sleepms);

            Console.WriteLine(ret);
            return retCode;
        }
    }
}