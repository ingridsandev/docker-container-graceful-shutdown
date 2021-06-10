using Microsoft.Extensions.Hosting;  
using Microsoft.Extensions.Logging;  
using System.Threading;  
using System.Threading.Tasks;

namespace Clean.Shut.Down.Web.Api
{
    public class ApplicationLifetimeHostedService : IHostedService  
        {
            private readonly IHostApplicationLifetime _applicationLifetime;
            private readonly ILogger<ApplicationLifetimeHostedService> _logger;
            public ApplicationLifetimeHostedService(
                ILogger<ApplicationLifetimeHostedService> logger,   
                IHostApplicationLifetime applicationLifetime)  
            {
                _logger = logger;  
                _applicationLifetime = applicationLifetime;
            }  
  
            public Task StartAsync(CancellationToken cancellationToken)  
            {  
                _logger.LogInformation("StartAsync method called.");  
  
                _applicationLifetime.ApplicationStarted.Register(() => OnStarted());  
                _applicationLifetime.ApplicationStopping.Register(() => OnStopping());  
                _applicationLifetime.ApplicationStopped.Register(() => OnStopped());
                return Task.CompletedTask;
            }  
  
            private void OnStarted()  
            {  
                _logger.LogInformation("DEBUG: Container has started.");
            }  
  
            /// <summary>
            /// Triggered when the application host is starting a graceful shutdown. Shutdown will block until all callbacks registered on this token have completed.
            /// </summary>
            private void OnStopping()  
            {
                // CancellationToken callback when receiving SIGTERM signal
                
                _logger.LogInformation("DEBUG: SIGTERM - Prep to stop container - CancellationToken callback");
                
                // Mocking time to do whatever
                Thread.Sleep(1000);

            }
            
            public Task StopAsync(CancellationToken cancellationToken)  
            {  
                _logger.LogInformation("DEBUG: Container will stop");
                
                return Task.CompletedTask;
            }
  
            /// <summary>
            /// Triggered when the application host has completed a graceful shutdown. The application will not exit until all callbacks registered on this token have completed.
            /// </summary>
            private void OnStopped()  
            {  
                _logger.LogInformation("DEBUG: SIGKILL - Container has stopped");
            }
        } 
}