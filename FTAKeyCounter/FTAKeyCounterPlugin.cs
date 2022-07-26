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
        public override void ModifyPoolAppUpdate( PoolApp.AppSpan packet )
        {
            //consume clicks
            int leftConsume = MouseClickCounter.ConsumeLeftClickCount( );
            int rightConsume = MouseClickCounter.ConsumeRightClickCount( );
            //add clicks to data packet
            packet.AddStat( "lmb", leftConsume );
            packet.AddStat( "rmb", rightConsume );
        }
    }
}