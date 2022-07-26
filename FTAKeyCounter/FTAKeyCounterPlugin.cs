using System;
using System.Collections.Generic;
using System.Diagnostics;
using FocusTimeAccumulator.Features.Plugins;
using FocusTimeAccumulator.Features.Pool;

namespace FTAKeyCounter
{
    public class FTAKeyCounterPlugin : Plugin
    {
        public override void OnStart()
        {
            Console.WriteLine("+----------------------------+");
            Console.WriteLine("| Key Counter Plugin Enabled |");
            Console.WriteLine("+----------------------------+");

            MouseClickCounter.StartMouseClickCounter();
        }

        public override void OnTick()
        {
            //Console.WriteLine("FTAKeyCounterPlugin : Tick");
        }

        public override void OnProcessChanged(Process process, string prevName, string prevTitle, string appName, string appTitle)
        {
            Console.WriteLine("!! PROCESS CHANGE EVENT !!");
            //Console.WriteLine(prevName);
            //Console.WriteLine(prevTitle);
            //Console.WriteLine(appName);
            //Console.WriteLine(appTitle);
        }

        public override void ModifyPoolAppUpdate(PoolApp.AppSpan packet)
        {
            Console.WriteLine("!! MODIFICATION EVENT !!");
            int leftConsume = MouseClickCounter.ConsumeLeftClickCount();
            int rightConsume = MouseClickCounter.ConsumeRightClickCount();
            Console.WriteLine("TARGET = " + packet.pageTitle);
            Console.WriteLine("LEFT = " + leftConsume);
            Console.WriteLine("RIGHT = " + rightConsume);

            int lmb = 0;
            if (packet.stats.ContainsKey("lmb"))
                lmb = packet.stats["lmb"].value;

            Console.WriteLine("LMB-STAT = " + lmb);

            int rmb = 0;
            if (packet.stats.ContainsKey("rmb"))
                rmb = packet.stats["rmb"].value;

            Console.WriteLine("RMB-STAT = " + rmb);

            packet.AddStat("lmb", lmb + leftConsume);
            packet.AddStat("rmb", rmb + rightConsume);

            Console.WriteLine("POST-LMB-STAT = " + packet.stats["lmb"].value);
            Console.WriteLine("POST-RMB-STAT = " + packet.stats["rmb"].value);
        }
    }
}