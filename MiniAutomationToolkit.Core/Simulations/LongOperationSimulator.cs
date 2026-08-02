using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Simulations
{
    public class LongOperationSimulator
    {
        public string LongOperation() //симуляция долгой операции через thread.sleep - блокировка потока
        {
            System.Threading.Thread.Sleep(2000);
            return "Done";
        }

        public async Task<string> LongOperationAsync() //симуляция долгой операции через await task delay - ждем 2 секунды, не блокируя поток
        {
            await System.Threading.Tasks.Task.Delay(2000);
            return "Done";
        }
    }
}
