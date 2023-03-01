using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] bool staticSpikes;

    private float startPos;
    [SerializeField] float midPos = 2.25f;
    [SerializeField] float outPos = 3f;

    [SerializeField] Transform spikeObj;
    [SerializeField] Collider collider;
    [SerializeField] float startDelay;
    [SerializeField] float outDelay = 0.5f;
    [SerializeField] float activeTime = 2f;
    [SerializeField] float inActiveTime = 2f;

    [SerializeField] Ease outEase;

    private bool startDelayTriggered;

    private void Start()
    {
        if (!staticSpikes)
        {
            startPos = spikeObj.position.y;

            StartCoroutine(spike());
            collider.enabled = false;
        }
    }


    IEnumerator spike()
    {
        while (true)
        {
            if (!startDelayTriggered)
            {
                startDelayTriggered = true;
                yield return new WaitForSeconds(startDelay);
            }

            spikeObj.DOLocalMoveY(midPos, 0.5f);
            yield return new WaitForSeconds(outDelay);
            spikeObj.DOLocalMoveY(outPos, 0.5f).SetEase(outEase);
            collider.enabled = true;
            yield return new WaitForSeconds(activeTime);
            spikeObj.DOLocalMoveY(startPos, 0.5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.5f);
            collider.enabled = false;
            yield return new WaitForSeconds(inActiveTime - 0.5f);
        }
    }

    public void EnableSpike()
    {
        collider.enabled = true;
        spikeObj.DOLocalMoveY(outPos, 0.5f).SetEase(outEase);
    }

    public void DisableSpike()
    {
        spikeObj.DOLocalMoveY(startPos, 0.5f).SetEase(Ease.Linear).OnComplete(disableCollider);
    }

    public void disableCollider()
    {
        collider.enabled = false;
    }
}
