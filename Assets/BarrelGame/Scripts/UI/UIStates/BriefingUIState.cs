using BarrelGame.Scripts.Interface.UI;
using BarrelGame.Scripts.UI.BriefingPanel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BarrelGame.Scripts.UI.UIStates
{
    public class BriefingUIState : UIBaseState
    {
        private IViewModel _viewModel;
        private IModel _model;
        private IView _view;
        
        public override void Init(Transform canvasTransform, object data)
        {
            Addressables.LoadAssetAsync<GameObject>("UI/BriefingPanelView").Completed += (asyncOperationHandle) =>            
            {
                if (asyncOperationHandle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Can't load asset (status:{asyncOperationHandle.Status}): UI/BriefingPanelView");
                    return;
                }

                _view = Object.Instantiate(asyncOperationHandle.Result.GetComponent<BriefingPanelView>(), canvasTransform);
                _view.OnInitComplete += (v) =>
                {
                    _model = new BriefingPanelModel();
                    _viewModel = new BriefingPanelViewModel((BriefingPanelView)_view, (BriefingPanelModel)_model);
                    
                    _model.AddListeners();
                    _viewModel.AddListeners();
                };
            };
        }
    }
}