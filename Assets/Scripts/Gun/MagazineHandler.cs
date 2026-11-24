using System;
using DG.Tweening;
using Oculus.Interaction;
using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class MagazineHandler : MonoBehaviour
    {
        [SerializeField]
        private MagazineSnapPosition _magazineParent;

        [SerializeField]
        private GrabInteractable _grabInteractable;

        [SerializeField]
        private Rigidbody _rb;

        [SerializeField]
        private Grabbable _grabbable;

        [SerializeField]
        private Collider _collider;



        // Game Loop Methods-----------------------------------------------------------------------

        private void OnEnable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action += SelectiongInteractorAdded_Magazine;
            _grabInteractable.WhenSelectingInteractorRemoved.Action += SelectiongInteractorRemoved_Magazine;
        }

        private void OnDisable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action -= SelectiongInteractorAdded_Magazine;
            _grabInteractable.WhenSelectingInteractorRemoved.Action -= SelectiongInteractorRemoved_Magazine;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out MagazineSnapPosition snapPosition))
            {
                _magazineParent = snapPosition;
                _grabbable.enabled = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                _collider.isTrigger = true;

                transform.SetParent(_magazineParent.transform);
                transform.DOLocalMove(Vector3.zero, 0.5f);
                transform.DOLocalRotate(Vector3.zero, 0.5f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MagazineSnapPosition snapPosition))
            {
                snapPosition.CurrentMagazine = this;
                _magazineParent = snapPosition;
                _grabbable.enabled = false;
                _rb.isKinematic = true;
                _rb.useGravity = false;
                _collider.isTrigger = true;

                transform.SetParent(_magazineParent.transform);
                transform.DOLocalMove(Vector3.zero, 0.5f);
                transform.DOLocalRotate(Vector3.zero, 0.5f);
            }
        }

        // Member Methods--------------------------------------------------------------------------

        private void SelectiongInteractorAdded_Magazine(GrabInteractor interactor)
        {
            // _collider.isTrigger = true;
            if (_magazineParent != null)
            {
                _magazineParent.CurrentMagazine = null;
                _magazineParent = null; 
            }
            _rb.isKinematic = true;
            _rb.useGravity = false;
            transform.SetParent(null);
        }

        private void SelectiongInteractorRemoved_Magazine(GrabInteractor interactor)
        {
            // _collider.isTrigger = false;
            // _rb.useGravity = true;
            // _rb.isKinematic = false;
        }
    }
}