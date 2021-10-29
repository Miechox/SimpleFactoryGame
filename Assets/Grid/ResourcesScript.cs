using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScript : MonoBehaviour
{
    
    public int resourceAmount { get; set; }

    private void Awake()
    {
        this.resourceAmount = 3000;
    }
    private void Update()
    {
        if(this.resourceAmount<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
