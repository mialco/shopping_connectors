using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.abstractions
{
	public interface ITreeNode
	{
		int GetParent();
		int GetId();
	}
}
