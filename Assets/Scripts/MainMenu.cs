using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject score;

    private void Start() {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject
        .LeanScale( new Vector3 (1.2f, 1.2f), 0.7f)
        .setLoopPingPong();
    }


    public void Play() 
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f)
            .setOnComplete(onComplete);
        
        
        
    }

    private void onComplete()
    {
        gameManager.Enable();

        score.SetActive(true);

        Destroy(gameObject);
    }
}
