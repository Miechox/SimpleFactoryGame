using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubScript : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        playerController.hubScript = this;
    }
    private void Update()
    {
       
    }
  
}
