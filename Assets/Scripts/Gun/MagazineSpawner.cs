using UnityEngine;

namespace Bassem.Scripts.Gun
{
    public class MagazineSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _newMagazine;

        [SerializeField]
        private Transform _spawnPosition;



        // Game Loop Methods-----------------------------------------------------------------------

        private void Start()
        {
            Invoke(nameof(_newMagazine), 10.0f);
        }

        // Member Methods--------------------------------------------------------------------------

        private void SpawnNewMagazine()
        {
            Instantiate(_newMagazine);
            _newMagazine.transform.position = _spawnPosition.position;
        }
    }
}