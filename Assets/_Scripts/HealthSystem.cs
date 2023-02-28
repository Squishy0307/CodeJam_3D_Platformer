using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

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

    void Damage()
    {
        health--;

        if (health <= 0)
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.gameObject.SetActive(true);
        }
    }
}
