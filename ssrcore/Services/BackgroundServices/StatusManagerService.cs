using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ssrcore.Services.BackgroundServices
{
    public class StatusManagerService : BackgroundService
    {
        private readonly ILogger<StatusManagerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public StatusManagerService(ILogger<StatusManagerService> logger, 
                                    IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Hosted service executing - {0}", DateTime.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var _serviceRequestService = scope.ServiceProvider.GetRequiredService<IServiceRequestService>();
                    await _serviceRequestService.UpdateStatusExpiredServiceRequest();
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
