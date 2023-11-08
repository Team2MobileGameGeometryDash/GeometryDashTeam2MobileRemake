using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coins : MonoBehaviour
{

    [Range(1,3)]
    public int CoinNumber;
    private bool isCollected;

    private void Start()
    {
        isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
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
        if (isCollected) PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + (CoinNumber-1), 1);
    }
   




}
