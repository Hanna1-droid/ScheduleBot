﻿using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class ScheduleToMessage
    {
        private readonly List<Event> _schedule;

        public ScheduleToMessage(List<Event> schedule) => _schedule = schedule;

        public string Convert(bool toOrderedList = false)
        {
            string result = string.Empty;

            if (toOrderedList)
            {
                result = OrderedList(result);
            }
            else
            {
                result = UnorderedList(result);
            }

            return result;
        }

        public string OrderedList(string result)
        {
            for (int i = 0; i < _schedule.Count; i++)
            {
                result += $"{i + 1}.\t{ParseEvent(_schedule[i])}\n";
            }

            return result;
        }

        private string ParseEvent(Event @event)
        {
            var parser = new EventToMessage(@event);

            return $"{parser.Parse()}";
        }

        public string UnorderedList(string result)
        {
            foreach(var @event in _schedule)
            {
                result += $"{ParseEvent(@event)}\n";
            }

            return result;
        }
    }
}