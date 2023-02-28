using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour {

    private CinemachineImpulseSource camShake;

    public static CameraShaker Instance;

    private bool canShakeCam = true;

    /// <summary>
    /// REQUIRED IMPULSE LISTNER on CINEMACHINE CAM!!  PUT IMPULSE SOURCE ON THIS EMPTY OBJ
    /// </summary>

	void Start () {

        camShake = GetComponent<CinemachineImpulseSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        else
        {
            Destroy(gameObject);
        }
    }
	
    public void shakeNow(Vector3 position,float velocity)
    {
        if(canShakeCam)
            camShake.GenerateImpulseAt(position, new Vector3(velocity , velocity , 0));        
    }

    public void hitShake(float shakeDuration)  //USED WHEN TIME SCALE IS 0 
    {
        if (canShakeCam)
        {
            CamShake[] shakers = FindObjectsOfType<CamShake>();
            for (int i = 0; i < shakers.Length; i++)
            {
                if (shakers[i].gameObject.activeInHierarchy == true)
                {
                    shakers[i].ShakeNow(shakeDuration);
                    return;
                }

            }
        }
    }

    public void changeCamShakeSetting(bool CanShake)
    {
        canShakeCam = CanShake;
    }
}
