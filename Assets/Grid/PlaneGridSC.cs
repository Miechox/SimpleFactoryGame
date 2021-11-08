using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGridSC : MonoBehaviour
{

    [SerializeField]
    private Material originalMat;
    [SerializeField]
    private Material electricMat;
    private Renderer _planeRenderer;
    private Renderer Renderer => _planeRenderer != null
        ? _planeRenderer
        : _planeRenderer = GetComponent<Renderer>();

    public ResourcesScript ocupiedByRes { set; get; }

    public bool isElectric = false;

    public void SetPlaneTextrureToYellow()
    {
        Renderer.material = electricMat;
    }
    public void SetMaterialToOriginal()
    {
        Renderer.material = originalMat;
    }
}
