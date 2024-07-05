using System;
using System.Threading;
using BarrelGame.Scripts.Character;
using BarrelGame.Scripts.Enums;
using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.UIStates;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace BarrelGame.Scripts.UI.InGamePanel
{
    public class InGamePanelModel : IModel
    {
        private float _startTimerValue = 60f;
        private GameStateType _gameStateType;
        
        private ReactiveProperty<float> _timer;
        private ReactiveProperty<int> _score;
        
        public Action<float,float> OnTimerValueChangeAction;
        public Action<int> OnScoreValueChangeAction;
        
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        
        public InGamePanelModel()
        {
            _timer = new ReactiveProperty<float>();
            _score = new ReactiveProperty<int>();

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            
            Timer();
        }
        
        public void AddListeners()
        {
            _timer.OnValueChange += OnTimerValueChange;
            _score.OnValueChange += OnScoreValueChange;
            
            GameStateMachine.Instance.OnGameStateChange += OnGameStateChange;
            CharacterEventBus.Instance.OnCollectablesTake += OnCollectablesTake;
            
            _score.Value = 0;
        }

        public void RemoveListeners()
        {
            _timer.OnValueChange -= OnTimerValueChange;
            _score.OnValueChange -= OnScoreValueChange;
            
            
            GameStateMachine.Instance.OnGameStateChange -= OnGameStateChange;
            CharacterEventBus.Instance.OnCollectablesTake -= OnCollectablesTake;
            
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void OnTimerValueChange(float value)
        {
            OnTimerValueChangeAction?.Invoke(_startTimerValue, value);
        }
        
        private void OnScoreValueChange(int value)
        {
            OnScoreValueChangeAction?.Invoke(value);
            PlayerPrefs.SetInt(PlayerPrefsNaming.SCORE, value);
        }

        private void OnGameStateChange(GameStateType gameStateType)
        {
            _gameStateType = gameStateType;

            if (gameStateType == GameStateType.Win)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private void OnCollectablesTake()
        {
            _score.Value++;
        }
        
        private async UniTask Timer()
        {
            _startTimerValue = 100f;
            _timer.Value = _startTimerValue;

            await UniTask.WaitUntil(() =>
            {
                if (_cancellationToken.IsCancellationRequested) return true;
                
                _timer.Value -= Time.deltaTime;  
                return _timer.Value <= 0;
            }, cancellationToken: _cancellationToken);
            
            if (_cancellationToken.IsCancellationRequested) return;
            
            UIStateMachine.Instance.SetState(new LoseUIState());
            GameStateMachine.Instance.SetState(GameStateType.Lose);
        }
    }
}
