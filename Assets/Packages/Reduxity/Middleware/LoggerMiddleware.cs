using System;
using Redux;
using UnityEngine;

namespace Reduxity.Middleware {

    /// <summary>
    /// Print Action or State to console depending on LogLevel
    /// </summary>
    public class Logger {

        readonly LoggerSettings settings_;

        /// <summary>
        /// Adjust log level settings in Zenject ScriptableObject Installer
        /// </summary>
        /// <param name="settings">LogLevel settings (off, actionsOnly, verbose)</param>
        public Logger(LoggerSettings settings) {
            settings_ = settings;
        }

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store) {
            return (Dispatcher next) => (IAction action) => {
                #if UNITY_EDITOR
                switch (settings_.Level) {
                    // do default action if off
                    case LoggerSettings.LogLevel.off: {
                        goto default;
                    }

                    // log action only
                    case LoggerSettings.LogLevel.actionsOnly: {
                        Debug.Log($"action: {action}");
                        break;
                    }

                    // log action and state object dump
                    case LoggerSettings.LogLevel.verbose: {
                        Debug.Log($"action: {ObjectDumper.Dump(action)}, state: {ObjectDumper.Dump(store)}");
                        break;
                    }

                    default: {
                        return next(action);
                    }
                };
                #endif

                return next(action);
            };
        }
    }

    [Serializable]
    /// <summary>
    /// Settings adjustable in Zenject's ScriptableObjectInstaller
    /// </summary>
    public class LoggerSettings {
        public enum LogLevel {
            off,
            actionsOnly,
            verbose
        }
        // log actionsOnly by default
        public LogLevel Level = LogLevel.actionsOnly;
    }

    /// <summary>
    /// Static class for non-Dependency Injection setup
    /// </summary>
    public static class StaticLoggerSettings {
        public static LoggerSettings.LogLevel Level = new LoggerSettings.LogLevel {};
    }
}

