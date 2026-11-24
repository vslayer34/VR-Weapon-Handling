using System;
using Oculus.Interaction;
using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class ShowMeshOnHover : MonoBehaviour
    {
        [SerializeField]
        private GrabInteractable _grabInteractable;

        [SerializeField]
        private GameObject _HoverMesh;
    
    
    
        // Game Loop Methods---------------------------------------------------------------------------

        private void OnEnable()
        {
            _HoverMesh.SetActive(false);

            _grabInteractable.WhenInteractorAdded.Action += EnableHoveringMesh;
            _grabInteractable.WhenInteractorRemoved.Action += DisableHoveringMesh;
        }

        private void OnDisable()
        {
            _grabInteractable.WhenInteractorAdded.Action -= EnableHoveringMesh;
            _grabInteractable.WhenInteractorRemoved.Action -= DisableHoveringMesh;
        }

        // Member Methods------------------------------------------------------------------------------

        private void EnableHoveringMesh(GrabInteractor interactor)
        {
            _HoverMesh.SetActive(true);
        }

        private void DisableHoveringMesh(GrabInteractor interactor)
        {
            _HoverMesh.SetActive(false);
        }
    }
}