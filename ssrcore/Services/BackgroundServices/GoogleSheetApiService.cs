using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ssrcore.Helpers;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ssrcore.Services.BackgroundServices
{
    public class GoogleSheetApiService : BackgroundService
    {
        private readonly ILogger<GoogleSheetApiService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public GoogleSheetApiService(ILogger<GoogleSheetApiService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Hosted 3 service executing - {0}", DateTime.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var serviceRequests = await _unitOfWork.ServiceRequestRepository.GetAll(new SearchServiceRequestModel());
                    if(serviceRequests != null && serviceRequests.Count > 0)
                    {
                        RequestSheetUtils.ClearAllRow(Constants.GoogleSheet.SHEET_REQUEST_SERVICE);
                        RequestSheetUtils.ClearAllRow(Constants.GoogleSheet.SHEET_FINISHED);
                        RequestSheetUtils.ClearAllRow(Constants.GoogleSheet.SHEET_IN_PROGRESS);
                        RequestSheetUtils.ClearAllRow(Constants.GoogleSheet.SHEET_REJECTED);
                        foreach (var request in serviceRequests)
                        {
                            RequestSheetUtils.Add(request, Constants.GoogleSheet.SHEET_REQUEST_SERVICE);
                            if (request.Status == "In-Progress")
                            {
                                RequestSheetUtils.Add(request, Constants.GoogleSheet.SHEET_IN_PROGRESS);
                            }
                            else if (request.Status == "Rejected")
                            {
                                RequestSheetUtils.Add(request, Constants.GoogleSheet.SHEET_REJECTED);
                            }
                            else if (request.Status == "Finished")
                            {
                                RequestSheetUtils.Add(request, Constants.GoogleSheet.SHEET_FINISHED);
                            }
                        }
                    }
                   
                }

                await Task.Delay(TimeSpan.FromSeconds(60));
            }
        }
    }
}
