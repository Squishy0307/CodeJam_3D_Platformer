using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] float timeBtnActivatingNextSpike = 1f;
    [SerializeField] SpikeTrap[] spikes;
   
    private void Start()
    {
        StartCoroutine(spiker());
    }

    IEnumerator spiker()
    {
        while (true)
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].EnableSpike();
                yield return new WaitForSeconds(timeBtnActivatingNextSpike);
                spikes[i].DisableSpike();
            }
        }
    }
}
