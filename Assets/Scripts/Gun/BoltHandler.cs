using System;
using DG.Tweening;
using Oculus.Interaction;
using UnityEngine;

public class BoltHandler : MonoBehaviour
{
    [SerializeField]
    private GrabInteractable _grabInteractable;

    [SerializeField]
    private Transform _bolt;

    [SerializeField, Header("Audio")]
    private AudioSource _boltAudio;

    [SerializeField]
    private AudioClip _openingBoltClip;

    [SerializeField]
    private AudioClip _closingBoltClip;

    private bool _boltMoving;




    // Game Loop Methods---------------------------------------------------------------------------

    private void OnEnable()
    {
        _grabInteractable.WhenSelectingInteractorAdded.Action += SelectingInteractorRemoved_HoldOpen;
        _grabInteractable.WhenSelectingInteractorRemoved.Action += SelectingInteractorRemoved_ReleaseBolt;
    }

    private void OnDisable()
    {
        _grabInteractable.WhenSelectingInteractorAdded.Action -= SelectingInteractorRemoved_HoldOpen;
        _grabInteractable.WhenSelectingInteractorRemoved.Action -= SelectingInteractorRemoved_ReleaseBolt;
    }

    private void Update()
    {
        if (!_boltMoving)
        {
            return;
        }

        // if (transform.localPosition.z < -0.2f)
        // {
        //     PlayBoltHandlingAudio(_openingBoltClip);
        // }

    }

    // Member Methods------------------------------------------------------------------------------

    public void CycleBolt(float time)
    {
        transform.DOLocalMoveZ(-0.0655f, time / 2.0f).OnComplete(() =>
        {
            transform.DOLocalMoveZ(0.0f, time / 2.0f);
        });
    }

    private void SelectingInteractorRemoved_HoldOpen(GrabInteractor interactor)
    {
        _boltMoving = true;
    }

    private void ReleaseBolt()
    {
        if (transform.localPosition.z != 0.0f)
        {
            transform.DOLocalMoveZ(0.0f, 0.075f);
            PlayBoltHandlingAudio(_closingBoltClip);
        }
    }

    private void PlayBoltHandlingAudio(AudioClip clip)
    {
        _boltAudio.clip = clip;
        _boltAudio.Play();
    }

    // Signal Methods------------------------------------------------------------------------------

    private void SelectingInteractorRemoved_ReleaseBolt(GrabInteractor interactor)
    {
        ReleaseBolt();
        _boltMoving = false;
        PlayBoltHandlingAudio(_openingBoltClip);
    }
}