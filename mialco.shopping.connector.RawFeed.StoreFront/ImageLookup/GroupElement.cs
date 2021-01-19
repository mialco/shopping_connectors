using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.RawFeed.StoreFront.ImageLookup
{
	class GroupElement : ImageElementBase
	{
		/// <summary>
		/// This class will calculate in the constructor what is the actual priority of the element 
		/// expecting that the name argument would be the a number, which number will be the priority 
		/// If the name cannot be converted to a number, the priority takes the IntMax value (which is in fact the lowest priority)
		/// If the name is an empty string, the priority will take the value of the Highest priority 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="priority"></param>
		/// <param name="highestPriority"></param>
		public GroupElement(string name, int highestPriority) : base (name,0,highestPriority)
		{
			//Name is Expected to be an integer
			name = name ?? string.Empty;
			var intPriority = int.MaxValue;
			if (name == string.Empty)
			{
				base._priority = highestPriority;			
			}
			else
			{
				int.TryParse(name, out intPriority);
				_priority = intPriority;
			}
			_isHighestPriority =  _priority <= _highestPriority;
		}
	}
}
