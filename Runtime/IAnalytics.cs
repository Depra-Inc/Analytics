namespace Depra.Analytics
{
	public interface IAnalytics
	{
		void SetUserId(string userId);

		void TrackEvent(string eventName);

		void TrackEvent(string eventName, in Params parameters);

		void Flush();
	}
}