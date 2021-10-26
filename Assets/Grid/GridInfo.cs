using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo
{
    public Vector3 gridPoints { get; set; }
    public GameObject occupiedBy;
    public bool isElectricity;

    public GridInfo(int x, int z)
    {
        gridPoints = new Vector3(x, 0, z);
        isElectricity = false;
    }

}
