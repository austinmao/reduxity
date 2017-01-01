using UnityEngine;
using Redux;
using System;
using Zenject;
using System.Collections.Generic;

namespace Reduxity.Example.Zenject.CharacterMover {

    /// <summary>
    /// Actions that will be dispatched to the Redux store. These may have payloads.
    /// </summary>
    public class Action {
        public class Move: IAction {
            // using Vector2 as input for 2-axis movements. these will be
            // translated to Vector3 in the reducer.
            public Vector2 inputVelocity { get; set; }
            public float fixedDeltaTime { get; set; }
        }

        public class StopMove: IAction {}

        public class Turn: IAction {
            // using Vector2 as input for 2-axis rotations. these will be
            // translated to Quaternions in the reducer.
            public Vector2 inputRotation { get; set; }
            public float fixedDeltaTime { get; set; }
        }

        public class StopTurn: IAction {}
    }

    /// <summary>
    /// Perform actions to modify state
    /// </summary>
    public class Reducer : IReducer {

        /// <summary>
        /// Inject Settings
        /// </summary>
        readonly Settings settings_;
        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public CharacterState Reduce(CharacterState previousState, IAction action) {
            // Debug.Log($"reducing with action: {action} of type {typeof(Action)}");
            if (action is Action.Move) {
                return Move(previousState, (Action.Move)action);
            }

            if (action is Action.StopMove) {
                return StopMove(previousState, (Action.StopMove)action);
            }

            if (action is Action.Turn) {
                return Turn(previousState, (Action.Turn)action);
            }

            if (action is Action.StopTurn) {
                return StopTurn(previousState, (Action.StopTurn)action);
            }

            return previousState;
        }

        // TODO: clone state
        /* calculate distance from velocity and transform */
        private CharacterState Move(CharacterState state, Action.Move action) {
            var inputVelocity = action.inputVelocity;
            var playerVelocity = (inputVelocity.x * state.transformRight) + (inputVelocity.y * state.transformForward);
            var distance = playerVelocity * action.fixedDeltaTime;

            state.isMoving = true;
            state.isTurning = false;
            state.moveDistance = distance;
            // Debug.Log($"in Move, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        private CharacterState StopMove(CharacterState state, Action.StopMove action) {
            state.isMoving = false;
            state.moveDistance = Vector3.zero;
            // Debug.Log($"in StopMove, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        // TODO: get this to work
        private CharacterState Turn(CharacterState state, Action.Turn action) {
            Vector2 rotation = action.inputRotation;
            float time = action.fixedDeltaTime;

            // inputLook.x rotates the character around the vertical axis (with + being right)
            // multiply by lookSpeed to increase the speed
            Vector3 horizontalLook = rotation.x * time * Vector3.up * settings_.xLookSpeed;
            
            state.localRotation *= Quaternion.Euler(horizontalLook);
            state.isTurning = true;
            state.isMoving = false;
            // Debug.Log($"in Turn, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        private CharacterState StopTurn(CharacterState state, Action.StopTurn action) {
            state.isTurning = false;
            // Debug.Log($"in StopTurn, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for character movement
    /// </summary>
    public class Settings {
        public int xLookSpeed = 100;
        public int xMoveSpeed = 1;
        public int yMoveSpeed = 1;
    }
}
