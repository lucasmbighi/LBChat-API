using System;
using LBChatAPI.Models;

namespace LBChatAPI.Helpers
{
	public static class AppSettingsHelper
	{
		private static AppSettings? GetAppSettings(WebApplicationBuilder? builder) =>
            builder?.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

		public static string GetSecretKey(WebApplicationBuilder? builder) =>
			GetAppSettings(builder)?.SecretKey ?? "";
    }
}