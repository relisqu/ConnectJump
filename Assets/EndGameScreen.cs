using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("1_Game");
    }

}
