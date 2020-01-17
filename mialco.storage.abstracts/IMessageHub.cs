using mialco.abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.storage.abstracts
{
    public interface IMessageHub
    {
		void PushMessage(WorkflowMessage message);
		WorkflowMessage GetMessage();
    }
}
