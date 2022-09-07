using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColourController : MonoBehaviour
{
    [SerializeField] private ParticleSystem effectSystem;
    [SerializeField] private Gradient colourGradient;

    private ParticleSystem.MainModule mainModule;

    // Start is called before the first frame update
    void Start()
    {
        mainModule = effectSystem.main;

        mainModule.startColor = colourGradient;
    }

    
}
