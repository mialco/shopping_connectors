using mialco.shopping.entities.abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAdFeed
{
	public class ExportFeedRaw: Entity
	{
#pragma warning disable CS0108 // 'ExportFeedRaw.Id' hides inherited member 'Entity.Id'. Use the new keyword if hiding was intended.
		int Id { get; set; }
#pragma warning restore CS0108 // 'ExportFeedRaw.Id' hides inherited member 'Entity.Id'. Use the new keyword if hiding was intended.
		string Value { get; set; }
	}
}
