using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.LosePanel;
using BarrelGame.Scripts.UI.WinPanel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BarrelGame.Scripts.UI.UIStates
{
    public class WinUIState: UIBaseState
    {
        private IViewModel _viewModel;
        private IModel _model;
        private IView _view;
        
        public override void Init(Transform canvasTransform, object data)
        {
            Addressables.LoadAssetAsync<GameObject>("UI/WinPanelView").Completed += (asyncOperationHandle) =>            
            {
                if (asyncOperationHandle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Can't load asset (status:{asyncOperationHandle.Status}): UI/WinPanelView");
                    return;
                }

                _view = Object.Instantiate(asyncOperationHandle.Result.GetComponent<WinPanelView>(), canvasTransform);
                _view.OnInitComplete += (v) =>
                {
                    _model = new WinPanelModel(data);
                    _viewModel = new WinPanelViewModel((WinPanelView)_view, (WinPanelModel)_model);
                    
                    _viewModel.AddListeners();
                    _model.AddListeners();
                };
            };
        }
    }
}