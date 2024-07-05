using UnityEngine;

namespace BarrelGame.Scripts.Interface.UI
{
    public abstract class UIBaseState
    {
        public abstract void Init(Transform canvasTransform, object data = null);
    }
}