using System;
using System.Diagnostics;

namespace WindowsGame.Master.Managers
{
	public interface IStopwatchManager
	{
		void Initialize();
		void Start();
		void Stop();

		Int64 ElapsedMilliseconds { get; }
	}

	public class StopwatchManager : IStopwatchManager
	{
		private Stopwatch stopwatch;

		public void Initialize()
		{
			stopwatch = new Stopwatch();
		}

		public void Start()
		{
			stopwatch.Start();
		}

		public void Stop()
		{
			stopwatch.Stop();
		}

		public Int64 ElapsedMilliseconds
		{
			get { return stopwatch.ElapsedMilliseconds; }
		}
	}
}
