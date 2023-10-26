using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coins : MonoBehaviour
{
    public int CoinNumber;
    private bool isCollected;

    private void Start()
    {
        isCollected = false;
    }
    //2D because Player is 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.TryGetComponent(out PlayerController player))
        {
            isCollected = true;
            gameObject.SetActive(false);
        }
    }

    public void ResetCoins()
    {
        isCollected = false;
        gameObject.SetActive(true);
    }

    public void SaveCoins()
    {
        if (isCollected) PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + CoinNumber, 1);
    }
    /*
    Da vedere dove metterlo
    Coins[] coinsList = FindObjectsOfType<Coins>();
    oppure lo si assegna manualmente
        foreach (Coins coin in coinsList)
        {
            coin.ResetCoins(); per la morte del player
            coin.SaveCoins();  Per il fine livello
        }
    */
}
