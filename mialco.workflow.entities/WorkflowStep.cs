using mialco.shopping.connector.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.workflow.entities
{
	/// <summary>
	/// Contains data about what is to be executed in this step and 
	/// </summary>
   public  class WorkflowStep: Entity
    {
		public string CurrentChannel { get; set; }
		public IEnumerable<string> NextChannels { get; set; }
		public bool PreserveInputData { get; set; }
		public bool PreserveOutputData { get; set; }
    }
}
