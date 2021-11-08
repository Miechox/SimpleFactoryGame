using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubScript : MonoBehaviour
{
    PlayerController plContr;
    private void Start()
    {
        plContr = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        plContr.hubScript = this;
    }
}
