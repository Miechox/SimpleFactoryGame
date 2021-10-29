using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGridSC : MonoBehaviour
{
    Texture2D planeTexture;
    [SerializeField]
    private Material originalMat;
    [SerializeField]
    private Material electricMat;

    public ResourcesScript ocupiedByRes { set; get; }

    public bool isElectric = false;

 
    public void SetPlaneTextrureToYellow()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = electricMat;
    }
    public void SetMaterialToOriginal()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = originalMat;
    }
}
