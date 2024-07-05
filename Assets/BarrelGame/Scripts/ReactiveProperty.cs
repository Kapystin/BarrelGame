using System;

namespace BarrelGame.Scripts
{
    public struct ReactiveProperty<T>
    {
        public Action<T> OnValueChange;
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChange?.Invoke(_value);
            }
        }
        
        private T _value;
    }
}