using System;
using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class MagazineSnapPosition : MonoBehaviour
    {
        public Action<bool> OnMagazinePlaced;
        private GunHandler _parentGun;
        private MagazineHandler _currentMagazine;




        // Game Loop Methods-----------------------------------------------------------------------
        
        // Member Methods--------------------------------------------------------------------------

        // Getters and Setters---------------------------------------------------------------------

        public GunHandler ParentGun
        {
            get => _parentGun;
            set
            {
                _parentGun = value;
            }
        }

        public MagazineHandler CurrentMagazine
        {
            get => _currentMagazine;
            set
            {
                _currentMagazine = value;

                OnMagazinePlaced?.Invoke(value);
            }
        }
    }
}