using System;
using System.Linq;
using System.Collections.Generic;
using Prime;

namespace Prime
{
	public static class Tasks
	{
		private static List<Task> tasks = new List<Task>();

		private static List<Task> done = new List<Task>();

		public static void Create(Action f, Func<bool> d, Action c)
		{
			tasks.Add(new Task
			{
				Frame = f,
				IsDone = d,
				Callback = c
			});
		}

		internal static void Update()
		{
			foreach ( var t in tasks )
			{
				t.Frame();

				if (t.IsDone())
				{
					t.Callback();
					done.Add(t);
				}
			}
			
			foreach ( var d in done )
			{
				tasks.Remove(d);
			}

			done.Clear();
		}
	}

	struct Task
	{
		public Action Frame;
		public Func<bool> IsDone;
		public Action Callback;
	}
}
