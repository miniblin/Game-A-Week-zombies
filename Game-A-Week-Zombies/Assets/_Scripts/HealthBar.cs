using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{


    public Image healthBar;

    GameObject playerobj;
    void Start()
    {
        playerobj = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.position = playerobj.transform.position;
        healthBar.fillAmount = PlayerController.PLAYER_HEALTH/100;
    }
}