using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.InGamePanel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BarrelGame.Scripts.UI.UIStates
{
    public class InGameUIState : UIBaseState
    {
        private IViewModel _viewModel;
        private IModel _model;
        private IView _view;
        
        public override void Init(Transform canvasTransform, object data)
        {
            Addressables.LoadAssetAsync<GameObject>("UI/InGamePanelView").Completed += (asyncOperationHandle) =>            
            {
                if (asyncOperationHandle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Can't load asset (status:{asyncOperationHandle.Status}): UI/InGamePanelView");
                    return;
                }

                _view = Object.Instantiate(asyncOperationHandle.Result.GetComponent<InGamePanelView>(), canvasTransform);
                _view.OnInitComplete += (v) =>
                {
                    _model = new InGamePanelModel();
                    _viewModel = new InGamePanelViewModel((InGamePanelView)_view, (InGamePanelModel)_model);
                    
                    _model.AddListeners();
                    _viewModel.AddListeners();
                };
            };
        }
    }
}