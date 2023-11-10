using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coins : MonoBehaviour
{

    [Range(1,3)]
    public int CoinNumber;
    private bool isCollected;
    private bool isAlreadyCollected
    {
        get
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + (CoinNumber - 1)) == 1) return true;
            else return false;
        }
    }

    Animator animator;

    private void OnEnable()
    {
        animator.SetBool("isAlreadyCollected", isAlreadyCollected);
    }
    private void Start()
    {
        isCollected = false;
        animator = GetComponent<Animator>();
        animator.SetBool("isAlreadyCollected", isAlreadyCollected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.TryGetComponent(out PlayerController player))
        {
            isCollected = true;
            animator.SetTrigger("Collected");            
        }
    }

    public void CoinDisable()
    {
        gameObject.SetActive(false);
    }

    public void ResetCoins()
    {
        isCollected = false;
        if (gameObject.activeInHierarchy) gameObject.SetActive(false); //If anyone dead in animation progress
        gameObject.SetActive(true);
    }

    public void SaveCoins()
    {
        if (isCollected) PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + (CoinNumber-1), 1);
    }
   




}
