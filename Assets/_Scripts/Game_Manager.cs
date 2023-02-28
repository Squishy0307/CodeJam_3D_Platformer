using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    [SerializeField] float Timer = 120;
    [SerializeField] List<GameObject> Collectible;
    [SerializeField] Collider WinCol;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Update()
    {
        if(Timer <= 0)
        {
            Debug.Log("GAME OVER!!! GET GUD!");
        }
        else
        {
            Timer -= Time.deltaTime;
        }

        for (int i = 0; i < Collectible.Count; i++)
        {
            if (Collectible[i] == null)
            {
                Collectible.RemoveAt(i);
            }
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Collectible.Count <= 0)
            {
                Debug.Log("WIN");
            }
        }
    }

}
