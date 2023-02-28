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
    public GameObject loseCanvasObj;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
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
        health--;
        movement.Knocked();

        ScreenFlasher.Instance.screenFlash();
        CameraShaker.Instance.shakeNow(transform.position, 0.3f);

        if (health <= 0)
        {
            loseCanvasObj.gameObject.SetActive(true);
        }
    }
}
