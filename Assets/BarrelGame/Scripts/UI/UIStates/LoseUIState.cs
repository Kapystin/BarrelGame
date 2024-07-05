using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.InGamePanel;
using BarrelGame.Scripts.UI.LosePanel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BarrelGame.Scripts.UI.UIStates
{
    public class LoseUIState: UIBaseState
    {
        private IViewModel _viewModel;
        private IModel _model;
        private IView _view;
        
        public override void Init(Transform canvasTransform, object data)
        {
            Addressables.LoadAssetAsync<GameObject>("UI/LosePanelView").Completed += (asyncOperationHandle) =>            
            {
                if (asyncOperationHandle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Can't load asset (status:{asyncOperationHandle.Status}): UI/LosePanelView");
                    return;
                }

                _view = Object.Instantiate(asyncOperationHandle.Result.GetComponent<LosePanelView>(), canvasTransform);
                _view.OnInitComplete += (v) =>
                {
                    _model = new LosePanelModel();
                    _viewModel = new LosePanelViewModel((LosePanelView)_view, (LosePanelModel)_model);
                    
                    _model.AddListeners();
                    _viewModel.AddListeners();
                };
            };
        }
    }
}