using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Redux;

namespace Reduxity {

    /// <summary>
    /// Combine multiple reducers into a single reducer,
    /// while reflecting the shape of the state.
    /// </summary>
    public static class ReducerCombiner<TState> where TState: new()
    {

        /// <summary>
        /// Combine reducers
        /// </summary>
        /// <param name="reducers">The reducers to combine</param>
        /// <returns>A reducer</returns>
        public static Reducer<TState> CombineReducers(Dictionary<string, Func<object, IAction, IState>> reducers)
        {

            return (previousState, action) => {

                var hasChanged = false;
                var nextState = new TState();

                foreach (var reducerPair in reducers) {

                    var reducer = reducerPair.Value;

                    var previousStateForKey = getPropertyByName(previousState, reducerPair.Key);

                    var nextStateForKey = reducer(previousStateForKey, action);

                    if (nextStateForKey == null) {
                        var msg = getUndefinedStateErrorMessage(reducer, action);
                        throw new System.InvalidOperationException(msg);
                    }

                    setPropertyByName(nextState, reducerPair.Key, nextStateForKey);

                    hasChanged = hasChanged || !nextStateForKey.Equals(previousStateForKey);
                }

                return hasChanged ? nextState : previousState;
            };
        }

        private static object getPropertyByName(object obj, string name)
        {
            var propertyInfo = obj.GetType().GetProperty(name);

            return propertyInfo != null
                ? propertyInfo.GetValue(obj, System.Reflection.BindingFlags.Default, null, null, null)
                : null;
        }

        private static void setPropertyByName(object obj, string name, object value) {
            var propertyInfo = obj.GetType().GetProperty(name);
            if (propertyInfo != null) {
                propertyInfo.SetValue(obj, value, System.Reflection.BindingFlags.Default, null, null, null);
            }
        }

        private static string getUndefinedStateErrorMessage (object reducer, object action) {
            var reducerName = reducer.GetType().FullName;
            var actionName = action.GetType().FullName;
            return 
                "Given action " + actionName + ", reducer '" + reducerName + 
                "' returned undefined. " + "To ignore an action, you must explicitly return the previous state.";
        }
    }
}
