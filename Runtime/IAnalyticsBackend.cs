namespace Depra.Analytics
{
	public interface IAnalyticsBackend
	{
		void Initialize();

		void SetUserId(string userId);

		void TrackEvent(string eventName);

		void TrackEvent(string eventName, in Params parameters);

		void Flush();
	}
}