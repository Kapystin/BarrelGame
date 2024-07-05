using System;
using BarrelGame.Scripts.Interface.UI;
using UnityEngine;

namespace BarrelGame.Scripts.UI
{
    public class BaseView : MonoBehaviour, IView
    {
        public event ViewInitComplete OnInitComplete;
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void Start()
        {
            OnInitComplete?.Invoke(this);
        }
        
        public virtual void AddListeners() {}

        public virtual void RemoveListeners() {}

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }
    }
}