using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Service
{
    public class ChiehBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ChiehBackgroundService> _logger;

        public ChiehBackgroundService(IServiceProvider services, ILogger<ChiehBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IChiehRepository>();

                try
                {
                    var now = DateTime.Now;
                    // 民國年
                    int rocYear = now.Year - 1911;
                    // 組合字串：民國年 + 月 + 日 + 時 + 分 + 秒
                    string checkTime = $"{rocYear:D3}{now.Month:D2}{now.Day:D2}{now.Hour:D2}{now.Minute:D2}{now.Second:D2}";

                    var data = await repo.GetDataAsync(checkTime);
                    _logger.LogInformation($"Chieh DB 撈到 {data.Count()} 筆資料 at {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Chieh DB 撈取失敗");
                }
                // 每兩小時撈取一次
                // await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}