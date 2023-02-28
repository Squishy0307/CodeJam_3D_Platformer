using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenFlasher : MonoBehaviour
{
    public static ScreenFlasher Instance;

    [SerializeField] Image flashImg;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        flashImg.enabled = false;

    }

    public void screenFlash()
    {
        flashImg.enabled = true;
        flashImg.DOFade(1,0.08f).SetLoops(2, LoopType.Yoyo).OnComplete(disableImg);
    }

    public void disableImg()
    {
        flashImg.enabled = false;
    }

}
