using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    [SerializeField] public int completeLevel;

    public GameObject endLevel;

    void OnTriggerEnter(Collider other)
    {
        theScore++;
        scoreText.GetComponent<Text>().text = "" + theScore;
        Destroy(gameObject);
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
