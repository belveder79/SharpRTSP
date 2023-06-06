using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#if WINDOWS_UWP
#if UNITY_2019_4_OR_NEWER
using UnityEngine;
using UnityEditor;
#endif

// THIS IS TO REMOVE NLog dependency
namespace NLog
{
    public class LogManager
    {
        public static Logger GetCurrentClassLogger()
        {
#if UNITY_2019_4_OR_NEWER
            return Logger.Instance();
#else
            return Logger.Instance;
#endif
        }
    }

    public enum LogLevel
    {
        Debug = 0
    };

    public class Logger
#if UNITY_2019_4_OR_NEWER
        : MonoBehaviour
#else
        : IDisposable
#endif
    {
#if UNITY_2019_4_OR_NEWER
        private static Logger _instance = null;
        private static Logger Instance()
        {
            if (!Exists())
            {
                throw new Exception("Logger could not find the Logger object. Please ensure you have added the Logger Prefab to your scene.");
            }
            return _instance;
        }

        /// <summary>
        /// Query the existence of singleton.
        /// </summary>
        internal static bool Exists()
        {
            return _instance != null;
        }
#else
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance { get { return lazy.Value; } }
#endif
        public void Debug(string a) { }
        public void Debug(CultureInfo info, string a, int byteReaden) { }
        public void Debug(CultureInfo info, string a, string b = null) { }
        public void Warn(CultureInfo info, string a, string b = null) { }
        public void Warn(CultureInfo info, string a, int byteReaden) { }
        public void Warn(string a, Exception b = null) { }
        public void Log(string a) { }
        public void Log(LogLevel a, string b, int c) { }
        public void Log(LogLevel a, string b, string c) { }

        public void Log(LogLevel a, string b, string c, string d) { }
        public void Log(LogLevel a, string b) { }
        public bool IsEnabled(LogLevel a) { return false; }

#if UNITY_2019_4_OR_NEWER
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void OnDestroy()
        {
            _instance = null;
        }
#else
        public void Dispose()
        {
        }
#endif
    }
}
#endif // WINDOWS_UWP

namespace Rtsp
{
    public static class RtspUtils
    {
        /// <summary>
        /// Registers the URI.
        /// </summary>
        public static void RegisterUri()
        {
            if (!UriParser.IsKnownScheme("rtsp"))
                UriParser.Register(new HttpStyleUriParser(), "rtsp", 554);
        }
    }
}
