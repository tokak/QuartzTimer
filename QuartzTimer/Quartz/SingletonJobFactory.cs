using Quartz.Spi;
using Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace QuartzTimer.Quartz
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            // Yeni bir scope oluştur
            var scope = _serviceProvider.CreateScope();

            // Job'ı scope içinde çöz
            var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

            // Job'ın kendisini scope ile ilişkilendir (opsiyonel)
            if (job is IDisposable disposableJob)
            {
                // Job dispose edildiğinde scope'u da dispose et
                return new DisposableJobWrapper(job, scope);
            }

            return job;
        }

        public void ReturnJob(IJob job)
        {
            // Job'ı dispose et (eğer IDisposable ise)
            if (job is IDisposable disposableJob)
            {
                disposableJob.Dispose();
            }
        }

        private class DisposableJobWrapper : IJob, IDisposable
        {
            private readonly IJob _job;
            private readonly IServiceScope _scope;

            public DisposableJobWrapper(IJob job, IServiceScope scope)
            {
                _job = job;
                _scope = scope;
            }

            public Task Execute(IJobExecutionContext context)
            {
                return _job.Execute(context);
            }

            public void Dispose()
            {
                _scope.Dispose();
            }
        }
    }
}