using System;
using Oculus.Interaction;
using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class Reciever : MonoBehaviour
    {
        [SerializeField]
        private GrabInteractable _grabInteractable;

        private int _numberOfInteractors;

        [SerializeField, Header("Hand Sources")]
        private Transform _1HandGrabSource;

        [SerializeField]
        private Transform _2HandGrabSource;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action += HandAddedOnReciever;
            _grabInteractable.WhenSelectingInteractorRemoved.Action += HandRemovedFromReciever;
        }

        private void OnDisable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action -= HandAddedOnReciever;
            _grabInteractable.WhenSelectingInteractorRemoved.Action -= HandRemovedFromReciever;
        }

        // Member Methods--------------------------------------------------------------------------

        private void HandAddedOnReciever(GrabInteractor interactor)
        {
            NumberOfInteractors++;

            switch (NumberOfInteractors)
            {
                case 0:
                    break;
                case 1:
                    _grabInteractable.InjectOptionalGrabSource(_1HandGrabSource);
                    break;
                case 2:
                    _grabInteractable.InjectOptionalGrabSource(_2HandGrabSource);
                    break;
            }
        }

        private void HandRemovedFromReciever(GrabInteractor interactor)
        {
            NumberOfInteractors--;

            switch (NumberOfInteractors)
            {
                case 0:
                    break;
                case 1:
                    _grabInteractable.InjectOptionalGrabSource(_1HandGrabSource);
                    break;
                case 2:
                    break;
            }
        }

        // Getters and Setters---------------------------------------------------------------------

        public int NumberOfInteractors
        {
            get => _numberOfInteractors;
            set
            {
                _numberOfInteractors = Mathf.Clamp(value, 0, 2);
            }
        }
    }
}