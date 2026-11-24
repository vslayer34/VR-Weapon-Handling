using System;
using DG.Tweening;
using Oculus.Interaction;
using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class GunHandler : MonoBehaviour
    {
        
        [SerializeField]
        private GrabInteractable _grabInteractable;
    
        [SerializeField]
        private RayInteractable _rayInteractable;

        private bool _magazineInserted;
        private bool _isWeaponHeld;

        private float _coolTime;

        [SerializeField]
        private float _rateOfFirePerMinute = 600.0f;

        [SerializeField]
        private AudioSource _fireSound;

        private float _fireRate;

        [SerializeField, Header("Gun Parts")]
        private MagazineSnapPosition _magazine;

        [SerializeField]
        private BoltHandler _bolt;

        [SerializeField]
        private Vector3 _recoileDirection;

        [SerializeField]
        private Transform _gunMesh;

        private bool _isFiring;

        private Tweener _recoileTweener;
        private Tweener _recenterTweener;

        private int _numberOfInteractors;
    
    
    
        // Game Loop Methods---------------------------------------------------------------------------

        private void Start()
        {
            _fireRate = 1.0f / (_rateOfFirePerMinute / 60.0f);
        }
    
        private void OnEnable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action += TestSelect;
            _grabInteractable.WhenSelectingInteractorRemoved.Action += TestUnselect;
    
            _rayInteractable.WhenSelectingInteractorAdded.Action += TestSelect;
            _rayInteractable.WhenSelectingInteractorRemoved.Action += TestUnselect;

            _magazine.OnMagazinePlaced += DetectInsertedMagazine;
        }

        void Update()
        {
            if (!_magazineInserted || !_isWeaponHeld)
            {
                return;
            }

            _coolTime += Time.deltaTime;

            if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                if (_coolTime >= _fireRate)
                {
                    _isFiring = true;
                    _coolTime = 0.0f;
                    FireGun();
                }
                else
                {
                    // if (!_recoileTweener.active)
                    // {
                    //     _recenterTweener = _gunMesh.DOLocalMove(Vector3.zero, _fireRate);
                    // }
                }
            }
        }

        private void OnDisable()
        {
            _grabInteractable.WhenSelectingInteractorAdded.Action -= TestSelect;
            _grabInteractable.WhenSelectingInteractorRemoved.Action -= TestUnselect;
    
            _rayInteractable.WhenSelectingInteractorAdded.Action -= TestSelect;
            _rayInteractable.WhenSelectingInteractorRemoved.Action -= TestUnselect;

            _magazine.OnMagazinePlaced -= DetectInsertedMagazine;
        }

        // Member Methods------------------------------------------------------------------------------

        private void FireGun()
        {
            Debug.Log("Bullet Fired");
            _fireSound.Play();
            _bolt.CycleBolt(_fireRate);

            // if (_recenterTweener.active)
            // {
                
            // }
            // _recoileTweener = _gunMesh.DOLocalMove(_recoileDirection * 0.01f, _fireRate);
        }

        private void TestSelect(GrabInteractor interactor)
        {
            _numberOfInteractors++;
            _isWeaponHeld = true;
        }
    
        private void TestUnselect(GrabInteractor interactor)
        {
            _numberOfInteractors--;
            
            if (_numberOfInteractors == 0)
            {
                _isWeaponHeld = false;
            }
        }
    
        private void TestSelect(RayInteractor interactor)
        {
            Debug.Log("Ray Selected");
        }
    
        private void TestUnselect(RayInteractor interactor)
        {
            Debug.Log("Ray Un-Selected");
        }

        private void DetectInsertedMagazine(bool isMagazineIn)
        {
            _magazineInserted = isMagazineIn;
        }
    }
}