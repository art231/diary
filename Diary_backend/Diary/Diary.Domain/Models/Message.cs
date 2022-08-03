using System;

namespace Diary.Domain.Models
{
    public abstract record Message
    {
        private readonly string _messageType;
        private readonly DateTimeOffset _timestamp;

        protected Message()
        {
            _messageType = GetType().Name;
            _timestamp = DateTimeOffset.Now;
        }

        public string GetMessageType()
        {
            return _messageType;
        }

        public DateTimeOffset GetTimestamp()
        {
            return _timestamp;
        }
    }
}
