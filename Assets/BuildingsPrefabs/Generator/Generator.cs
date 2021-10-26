using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    GameMode gameMode;

    private void Awake()
    {
       gameMode = FindObjectOfType<GameMode>().GetComponent<GameMode>();
    }
    public void GeneratorSetup()
    {
        gameMode.AddGenerator(this);
    }

    private void OnDestroy()
    {
        gameMode.goldVal += 30;
    }
}
