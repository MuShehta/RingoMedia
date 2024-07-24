using Department.Data;

namespace Department.Services
{
    public class ReminderEmailService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly EmailService _emailService;

        public ReminderEmailService(IServiceScopeFactory scopeFactory, EmailService emailService)
        {
            _scopeFactory = scopeFactory;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DepartmentContext>();

                    var reminders = dbContext.Reminders.Where(r => r.DateTime <= DateTime.Now && !r.IsSent).ToList();

                    foreach (var reminder in reminders)
                    {
                        await _emailService.SendEmailAsync("recipient@example.com", reminder.Title, $"Reminder: {reminder.Title}");
                        reminder.IsSent = true;
                        dbContext.Update(reminder);
                    }

                    await dbContext.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
            }
        }
    }
}
