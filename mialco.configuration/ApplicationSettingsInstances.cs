using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mialco.configuration
{
	class ApplicationSettingsInstances : IReadOnlyDictionary<string, ApplicationInstanceSettings>
	{
		public ApplicationSettingsInstances(Dictionary<string,ApplicationInstanceSettings> instances)
		{
			_instances = instances ?? new Dictionary<string, ApplicationInstanceSettings>();
			_kvpEnumerator = new InstancesKvpEnumerator(_instances);
		}

		private Dictionary<string, ApplicationInstanceSettings> _instances;
		private IEnumerator<KeyValuePair<string,ApplicationInstanceSettings>> _kvpEnumerator;

		public ApplicationInstanceSettings this[string key] =>  _instances.ContainsKey(key) ? _instances[key] : null;

		public IEnumerable<string> Keys => _instances.Keys;

		public IEnumerable<ApplicationInstanceSettings> Values => _instances.Values;

		public int Count => _instances == null ? 0 : _instances.Count;

		public bool ContainsKey(string key)
		{
			return _instances.ContainsKey(key);
		}

		public IEnumerator<KeyValuePair<string, ApplicationInstanceSettings>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public bool TryGetValue(string key, out ApplicationInstanceSettings value)
		{
			var result = false;
			if (_instances == null)
				value = null;
			else
				result = _instances.TryGetValue(key, out value);
			return result;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			 return _instances.GetEnumerator();
		}


		class InstancesKvpEnumerator : IEnumerator<KeyValuePair<string, ApplicationInstanceSettings>>
		{
			Dictionary<string, ApplicationInstanceSettings> _instances;
			KeyValuePair<string, ApplicationInstanceSettings> _current;

			internal InstancesKvpEnumerator(Dictionary<string, ApplicationInstanceSettings> instances)
			{
				_instances = instances;
				if (_instances != null) _current = _instances.FirstOrDefault();
			}
			public KeyValuePair<string, ApplicationInstanceSettings> Current => _current;

			object IEnumerator.Current => _current;

			public void Dispose()
			{

			}

			public bool MoveNext()
			{
				var result = false;
				try
				{
					result = _instances.GetEnumerator().MoveNext();
					if (result)
						_current = _instances.GetEnumerator().Current;
					else
						_current = _instances.FirstOrDefault();
				}
				catch
				{ }
				return result;
			}

			public void Reset()
			{
				if (_instances != null)
				{
					_current = _instances.FirstOrDefault();
				}
			}
		}
	}





}
