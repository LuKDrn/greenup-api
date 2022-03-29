using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using GreenUp.Application.Mailing.Dtos;
using GreenUp.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenUp.Application.Mailing
{
    public abstract class SendMailBackgroundJob<TJobArgs> :
        BackgroundJob<TJobArgs>, ITransientDependency
        where TJobArgs : SendMailBackgroundJobArgs
    {
        /// <summary>
        /// Traitement habituel pour tout job d'envoi de mail en masse.
        /// </summary>
        /// <param name="args">
        /// Le contexte et les arguments de ce job.
        /// </param>
        [UnitOfWork]
        public override void Execute(TJobArgs args)
        {
            if (!GreenUpConsts.HangfireEnabled)
                throw new NotSupportedException(
                    "Seul Hangfire est supporté pour l'envoi en masse de courriels, tel qu'il est implémenté.");

            DeleteHangfireRetryingJobsByArgs(args);
        }

        /// <summary>
        /// Abandonne tous (il ne devrait y en avoir qu'un maximum) les jobs
        /// Hangfire devant traiter la candidature donnée.
        /// </summary>
        /// <param name="args">Le contexte et les arguments de ce job.</param>
        /// <returns>Les IDS des jobs</returns>
        public static void DeleteHangfireRetryingJobsByArgs
            (TJobArgs args)
        {
            foreach (var jobId in FindHangfireRetryingJobsIds(args))
                Hangfire.BackgroundJob.Delete(jobId);
        }

        /// <summary>
        /// Cherche tous (il ne devrait y en avoir qu'un maximum) les jobs
        /// Hangfire devant traiter la candidature donnée.
        /// </summary>
        /// <param name="args">Le contexte et les arguments de ce job.</param>
        /// <returns>Les IDS des jobs</returns>
        private static IEnumerable<string>
            FindHangfireRetryingJobsIds(TJobArgs args)
        {
            using var connection = Hangfire.JobStorage.Current.GetConnection();
            var jobIds = connection
                .GetAllItemsFromSet("retries")
                .ToList();
            foreach (var jobId in jobIds)
            {
                var jobObjectId =
                    Hangfire.JobStorage.Current.GetMonitoringApi()
                        .JobDetails(jobId)
                        .Job
                        .Args
                        .Cast<SendMailBackgroundJobArgs>()  // Ce que l'application envoie comme argument à Hangfire est un ID de candidature. C'est pourquoi la classe dérive de BackgroundJob<int>.
                        .Single();
                if (jobObjectId.UserId == args.UserId)
                    yield return jobId;
            }
        }
    }
}
