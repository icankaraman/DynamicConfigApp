using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLibrary.Lib.Services
{
    public abstract class HostedService : IHostedService, IDisposable
    {
        private Task currentTask;
        private readonly CancellationTokenSource ctSource = new CancellationTokenSource();
        
        protected abstract Task ExecuteAsync(CancellationToken cToken); 

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            currentTask = ExecuteAsync(ctSource.Token);

            if (currentTask.IsCompleted)
                return currentTask;

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (currentTask == null)
                return;

            try
            {
                ctSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(currentTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }
    }
}
