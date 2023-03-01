using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerMovement movement;
    private ThirdPersonCam cam;
    public GameObject loseCanvasObj;
    [SerializeField] float invinsiblityTime = 1.5f;
    private bool gotHit = false;
    private MeshRenderer mesh;
    private Material startMat;
    [SerializeField] Material hitEffectMat;

    private void Start()
    {
        cam = Camera.main.GetComponent<ThirdPersonCam>();
        movement = GetComponent<PlayerMovement>();
        mesh = GetComponentInChildren<MeshRenderer>();
        startMat = mesh.material;
    }

    public void Update()
    {
        if (health > numHearts)
        {
            health = numHearts;
        }        

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void GotHit()
    {
        if (!gotHit && health > 0)
        {
            health--;
            movement.Knocked();

            StartCoroutine(Hit());

            ScreenFlasher.Instance.screenFlash();
            CameraShaker.Instance.shakeNow(transform.position, 0.3f);

            if (health <= 0)
            {
                StartCoroutine(DeathScreenDelay());
            }
        }
    }

    IEnumerator DeathScreenDelay()
    {
        movement.Dead();
        cam.Dead();
        yield return new WaitForSeconds(3f);
        loseCanvasObj.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Hit()
    {       
        gotHit = true;
        StartCoroutine(hitEffect());

        yield return new WaitForSeconds(invinsiblityTime);
        gotHit = false;

        StopCoroutine(hitEffect());
        mesh.material = startMat;
    }

    IEnumerator hitEffect()
    {
        while (gotHit)
        {
            mesh.material = hitEffectMat;
            yield return new WaitForSeconds(0.1f);
            mesh.material = startMat;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
