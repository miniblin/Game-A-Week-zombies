using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{


    public Image healthBar;
    PlayerController player;
    void Start()
    {

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
		Debug.Log(PlayerController.PLAYER_HEALTH);
        healthBar.fillAmount = (PlayerController.PLAYER_HEALTH)/100;
    }
}