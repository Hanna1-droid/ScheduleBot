﻿using System;

namespace PrimatScheduleBot
{
    public class Mailing : IStopable, IStartable
    {
        private readonly string _chatId;

        public Mailing(string chatId) => _chatId = chatId;

        public void StartMailingList(TimeSpan time, string token)
        {
            var data = new PostSenderData(token, _chatId);
            var scheduler = new Scheduler(_chatId);

            scheduler.Start<PostSender>(time, data);
        }

        private void StopMailingList()
        {
            var scheduler = new Scheduler(_chatId);

            scheduler.TryStop();
        }

        public void Start(TimeSpan time, string token)
        {
            StartMailingList(time, token);

            using var facade = new MailingListFacade();

            facade.InsertOrUpdate(_chatId, time);
        }

        public void Stop()
        {
            StopMailingList();

            using var facade = new MailingListFacade();

            facade.Remove(_chatId);
        }
    }
}