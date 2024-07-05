using System;
using BarrelGame.Scripts.Enums;
using UnityEngine;

namespace BarrelGame.Scripts
{
    public class GameStateMachine
    {
        public static GameStateMachine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateMachine();
                }

                return _instance;
            }
        }
        
        private static GameStateMachine _instance = null;
        
        public Action<GameStateType> OnGameStateChange;

        private GameStateType _currentState;
        
        private GameStateMachine()
        {
            _currentState = GameStateType.Briefing;
        }

        public GameStateType GetCurrentState()
        {
            return _currentState; 
        }
        
        public void SetState(GameStateType state)
        {
            _currentState = state;
            OnGameStateChange?.Invoke(state);
        }
    }
}