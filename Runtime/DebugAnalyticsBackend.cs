using System.Globalization;
using UnityEngine;

namespace Depra.Analytics
{
	public sealed class DebugAnalyticsBackend : IAnalyticsBackend
	{
		void IAnalyticsBackend.Initialize() { }

		void IAnalyticsBackend.SetUserId(string userId)
		{
			Debug.Log($"[DebugAnalytics] SetUserId: {userId}");
		}

		void IAnalyticsBackend.TrackEvent(string eventName)
		{
			Debug.Log($"[DebugAnalytics] TrackEvent: {eventName}");
		}

		void IAnalyticsBackend.TrackEvent(string eventName, in Params parameters)
		{
			Debug.Log($"[DebugAnalytics] TrackEvent: {eventName}");

			foreach (var param in parameters.Items)
			{
				var value = param.Type switch
				{
					Param.ParamType.STRING => param.StringValue,
					Param.ParamType.INT => param.IntValue.ToString(),
					Param.ParamType.BOOL => param.BoolValue.ToString(),
					Param.ParamType.FLOAT => param.FloatValue.ToString(CultureInfo.InvariantCulture),
					_ => "Unknown"
				};

				Debug.Log($"  {param.Key} = {value}");
			}
		}

		void IAnalyticsBackend.Flush() => Debug.Log("[DebugAnalytics] Flush called");
	}
}