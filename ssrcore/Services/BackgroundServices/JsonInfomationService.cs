using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ssrcore.Helpers;
using ssrcore.Repositories;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ssrcore.Services.BackgroundServices
{
    public class JsonInfomationService : BackgroundService
    {
        private readonly ILogger<JsonInfomationService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public JsonInfomationService(ILogger<JsonInfomationService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ReadGoogleSheet.Init();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Hosted 2 service executing - {0}", DateTime.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var _serviceService = scope.ServiceProvider.GetRequiredService<IServiceService>();
                    var _serviceRequestService = scope.ServiceProvider.GetRequiredService<IServiceRequestService>();
                    var serviceList = await  _unitOfWork.ServiceRepository.GetServices();
                    foreach (var service in serviceList)
                    {
                        if (!string.IsNullOrEmpty(service.SheetLink) && service.SheetLink.Contains("spreadsheets/d/"))
                        {

                            //string url = "https://docs.google.com/spreadsheets/d/1skxD2Zt1mdggWnOrb32xeWCNNo5zmy7xCiwIJSDWhpw/edit#gid=221254995";                      
                            string url = service.SheetLink;
                            string[] parts = url.Split("spreadsheets/d/");
                            string result;
                            if (parts[1].Contains("/"))
                            {
                                string[] parts2 = parts[1].Split("/");
                                result = parts2[0];
                            }
                            else
                            {
                                result = parts[1];
                            }
                            dynamic jsonList = ReadGoogleSheet.ReadSheet(result);
                            foreach(var json in jsonList)
                            {
                               await _serviceRequestService.UpdateServiceRequest(json.TicketId, new ServiceRequestModel { JsonInformation = json.JsonInformation});
                            }

                        }
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(8));
            }
        }
    }
}
