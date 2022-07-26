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

        public override void ModifyPoolAppUpdate(PoolApp.AppSpan packet)
        {
            //Console.WriteLine("!! MODIFICATION EVENT !!");
            int leftConsume = MouseClickCounter.ConsumeLeftClickCount();
            int rightConsume = MouseClickCounter.ConsumeRightClickCount();
            //Console.WriteLine("TARGET = " + packet.pageTitle);
            //Console.WriteLine("LEFT = " + leftConsume);
            //Console.WriteLine("RIGHT = " + rightConsume);

            packet.AddStat("lmb", leftConsume);
            packet.AddStat("rmb", rightConsume);
        }
    }
}