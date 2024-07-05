using System;
using BarrelGame.Scripts.Interface.UI;
using UnityEngine;

namespace BarrelGame.Scripts.UI.WinPanel
{
    public class WinPanelModel: IModel
    {
        private object _data;
        
        public Action<int> OnScoreDataReceive;
        
        public WinPanelModel(object data)
        {
            _data = data;
        }
        
        public void AddListeners()
        {
            if (_data is int score)
            {
                OnScoreDataSet(score);
            }
        }

        public void RemoveListeners()
        {
        }

        private void OnScoreDataSet(int value)
        {
            OnScoreDataReceive?.Invoke(value);
        }
    }
}