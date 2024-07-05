using System;

namespace BarrelGame.Scripts.Interface.UI
{
    public delegate void ViewInitComplete(IView view);
    
    public interface IView
    {
        event ViewInitComplete OnInitComplete; 
        void AddListeners();
        void RemoveListeners();
        
    }
}