using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlipperPlatform : MonoBehaviour
{
    [SerializeField] Transform rotObj;
    [SerializeField] float rotationDuration = 1f;
    [SerializeField] float intervalAfterLoop = 1f;
    private float endRotation;


    void Start()
    {
        if (rotObj.localEulerAngles.z == 0)
        {
            endRotation = 180;

            Sequence rot = DOTween.Sequence();
            rot.AppendInterval(intervalAfterLoop);
            rot.Append(rotObj.DORotate(new Vector3(0, 0, endRotation), rotationDuration).SetEase(Ease.InOutSine).OnComplete(changeDir));
        }
        else
        {
            endRotation = 0;
            Sequence rot = DOTween.Sequence();
            rot.AppendInterval(intervalAfterLoop);
            rot.Append(rotObj.DORotate(new Vector3(0, 0, endRotation), rotationDuration).SetEase(Ease.InOutSine).OnComplete(changeDir));
        }


        
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(intervalAfterLoop);
        rotObj.DORotate(new Vector3(0, 0, -endRotation), rotationDuration).SetEase(Ease.InOutSine).OnComplete(changeDir);
    }


    public void changeDir()
    {
        if(endRotation == 0)
        {
            endRotation = -180;
        }
        else
        {
            endRotation = 0;
        }

        StartCoroutine(wait());
    }

}
