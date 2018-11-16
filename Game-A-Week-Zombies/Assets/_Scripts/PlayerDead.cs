using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Invoke("RestartScene", 4);


    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
