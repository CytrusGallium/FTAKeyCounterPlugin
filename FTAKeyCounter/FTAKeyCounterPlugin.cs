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
            //Console.WriteLine(MouseClickCounter.ConsumeLeftClickCount());
            //Console.WriteLine(MouseClickCounter.ConsumeRightClickCount());
            
            int lmb = 0;
            if (packet.stats.ContainsKey("lmb"))
                lmb = packet.stats["lmb"].value;

            int rmb = 0;
            if (packet.stats.ContainsKey("rmb"))
                lmb = packet.stats["rmb"].value;

            packet.AddStat("lmb", lmb + MouseClickCounter.ConsumeLeftClickCount());
            packet.AddStat("rmb", rmb + MouseClickCounter.ConsumeRightClickCount());
        }
    }
}