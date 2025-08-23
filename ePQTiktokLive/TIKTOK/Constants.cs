﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePQTiktokLive.TIKTOK
{
    public static class Constants
    {
        /// <summary>
        /// Web-URL for TikTok
        /// </summary>
        public const string TIKTOK_URL_WEB = @"https://www.tiktok.com/";
        /// <summary>
        /// WebCast-BaseURL for TikTok
        /// </summary>
        public const string TIKTOK_URL_WEBCAST = @"https://webcast.tiktok.com/webcast/";
        /// <summary>
        /// Signing API by Isaac Kogan
        /// </summary>
        public const string TIKTOK_SIGN_API = @"https://tiktok.eulerstream.com/webcast/fetch";
        /// <summary>
        /// User-Agent for WebClients (Http/WebSocket)
        /// </summary>
        public const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36";
        /// <summary>
        /// Browser version for WebClients parameter (Http/WebSocket)
        /// </summary>
        public const string BROWSER_VERSION = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36";/// <summary>
                                                                                                                                                                /// <summary>
                                                                                                                                                                /// Default TimeOut for Connections
                                                                                                                                                                /// </summary>
        public const float DEFAULT_TIMEOUT = 20f;
        /// <summary>
        /// Default Polling-Time for Socket-Connection
        /// </summary>
        public const float DEFAULT_POLLTIME = 1f;
        /// <summary>
        /// Default Reconnection-Interval
        /// </summary>
        public const float DEFAULT_RECONNECT_TIMEOUT = 1f;

        
        public static readonly IReadOnlyDictionary<string, object> DEFAULT_CLIENT_PARAMS = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>()
        {
            { "aid",  1988 },
            { "app_language", "en-US" },
            { "app_name", "tiktok_web" },
            { "browser_language", "en" },
            { "browser_name", "Mozilla" },
            { "browser_online", true },
            { "browser_platform", "Win32" },
            { "browser_version", BROWSER_VERSION },
            { "cookie_enabled", true },
            { "cursor", "" },
            { "internal_ext", "" },
            { "device_platform", "web" },
            { "focus_state", true },
            { "from_page", "user" },
            { "history_len", 4 },
            { "is_fullscreen", false },
            { "is_page_visible", true },
            { "did_rule", 3 },
            { "fetch_rule", 1 },
            { "last_rtt", 0 },
            { "live_id", 12 },
            { "resp_content_type", "protobuf" },
            { "screen_height", 1080 },
            { "screen_width", 1920 },
            { "tz_name", "Europe/Berlin" },
            { "referer", "https, //www.tiktok.com/" },
            { "root_referer", "https, //www.tiktok.com/" },
            { "msToken", "" },
            { "webcast_sdk_version", "1.3.0" },
            { "update_version_code", "1.3.0" }
        });

        /// <summary>
        /// Default Headers for HTTP-Request
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> DEFAULT_HTTP_HEADERS = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
        {
            //{ "Connection", "keep-alive" },
            { "authority", "www.tiktok.com" },
            { "Cache-Control", "no-cache" },
           // { "Accept", "text/html,application/json,application/protobuf" },
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" },
            { "Referer", TIKTOK_URL_WEB },
            { "Accept-Language", "en; q=0.8" }
        });

        /// <summary>
        /// Default Headers for Websocket-Client
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> DEFAULT_SOCKET_HEADERS = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>()
        {
            { "authority", "www.tiktok.com" },
            { "Cache-Control", "max-age=0" },
            { "Accept", "text/html,application/json,application/protobuf" },
            { "User-Agent", USER_AGENT },
            { "Referer", TIKTOK_URL_WEB },
            { "Origin", TIKTOK_URL_WEB },
            { "Accept-Language", "en; q=0.8" }
        });

        public static readonly KeyValuePair<string, string> COMPRESSION_HEADER = new KeyValuePair<string, string>("Accept-Encoding", "gzip, deflate");
    }
}
