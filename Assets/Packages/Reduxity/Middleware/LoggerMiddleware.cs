using System;
using Redux;
using UnityEngine;

namespace Reduxity.Middleware {

    /// <summary>
    /// Print Action to console
    /// </summary>
    public class Logger {

        readonly LoggerSettings settings_;

        public Logger(LoggerSettings settings) {
            settings_ = settings;
        }

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store) {
            return (Dispatcher next) => (IAction action) => {
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

                return next(action);
            };
        }
    }

    [Serializable]
    public class LoggerSettings {
        public enum LogLevel {
            off,
            actionsOnly,
            verbose
        }
        public LogLevel Level;
    }

    /// <summary>
    /// Static class for non-Dependency Injection setup
    /// </summary>
    public static class StaticLoggerSettings {
        public static LoggerSettings.LogLevel Level = new LoggerSettings.LogLevel {};
    }
}

