using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase
{
	public class EventManager : SingletonMono<EventManager>
	{
		List<BaseEvent> eventList = new List<BaseEvent>();

		public void Register(BaseEvent newEvent)
		{
			Instance.eventList.Add(newEvent);
		}

		public void Unregister(BaseEvent newEvent)
		{
			Instance.eventList.Remove(newEvent);
		}

		public void Clear()
		{
			for (int i = eventList.Count -1; i >= 0; i--)
			{
				Destroy(eventList[i]);
				eventList.RemoveAt(i);
			}
			eventList.Clear();
		}
	}
}
