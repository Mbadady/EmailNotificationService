﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessagingBus
    {
        Task PublishMessage(object message, string topic_queue_name, string connectionString);
    }
}
