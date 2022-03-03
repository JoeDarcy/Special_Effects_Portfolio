using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    // Reference to summon effect prefab and empty game object to hold instance once instantiated
    [Tooltip("This is the game object containing the summon effect")]
    [SerializeField] private GameObject summonEffect = null;
    private GameObject summonIncantationInstance = null;

    // Particle system module variable
    private ParticleSystem.TextureSheetAnimationModule magicEffectTextureSheetModule;

    // Sprites for each particle system that uses texture sheet animation for sprite rendering
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customFloorRunes = null;              // Floor runes
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customFloatingFloorRunes = null;      // Floating floor runes
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customRisingRunes = null;             // Rising runes

    // Spell effect activation key
    [Tooltip("Set the key used to activate spell effect")]
    [SerializeField] private KeyCode activationKey = KeyCode.Space;

    // Visual effect variables
    [Tooltip("Set the colour of the summon incantation")]
    [SerializeField] private Color newEffectColour;
    private ParticleSystem.MainModule summonEffectMainModule;

    // Set opacity range between range (10% - 70%)
    [Tooltip("Set the opacity percentage of the effect colour of the summon incantation")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float summonOpacityPercentage = 70.0f;

    // Light intensity
    [Tooltip("Set the intesity of the light (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float lightIntensity = 0.0f;

    // Audio on / off
    [Tooltip("Set audio for the summon incantation on or off")]
    [SerializeField] private bool audioOn = true;
    private AudioSource summonSoundEffect = null;

    // "Play" effect button bool
    private bool editorPlayButtonPressed = false;

    // "Reset effect values to default" button bool
    private bool editorResetValuesButtonPressed = false;

    // Default values
    private float defaultOpacity = 0.7f;
    private float defaultLightIntensity = 0.7f;

    // Light reference and dimmer timer
    private Light effectLight = null;    


    // Start is called before the first frame update
    void Start()
    {
        // Set play effect button to false on start
        editorPlayButtonPressed = false;
        // Set "Reset effect values" button to false on start
        editorResetValuesButtonPressed = false;


        // Set effect colour and lock alpha at 70% or lower
        ChangeEffectColour(summonOpacityPercentage);

        // Get effect light and set colour and intensity
        effectLight = summonEffect.GetComponentInChildren<Light>();
        effectLight.color = newEffectColour;
        effectLight.intensity = lightIntensity / 100;

        // Set audio on or off
        summonSoundEffect = summonEffect.GetComponentInChildren<AudioSource>();

        if (audioOn)
        {
            summonSoundEffect.enabled = true;
        }
        else
        {
            summonSoundEffect.enabled = false; ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Trigger summon incantation 
        if (Input.GetKeyDown(activationKey) || editorPlayButtonPressed)
	    {
            // Get player location
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Instantiate summon instance
            summonIncantationInstance = Instantiate(summonEffect, player.transform.position, Quaternion.identity);
            SetEffectCustomSprite();

            // Reset editor button pressed bool
            editorPlayButtonPressed = false;
	    }

        // Reset effect values to default
        if (editorResetValuesButtonPressed)
        {
            //magicEffectTextureSheetModule.RemoveSprite(0);  // Set up a sprite to reference as a default on the effect prefab / private non editable
            Debug.Log("Resetting to defualt values");

            // Reset opacity of the base colour
            ChangeEffectColour(defaultOpacity);

            // Reset light intensity to default value
            effectLight.intensity = defaultLightIntensity;

            // Reset effect sprites to default sprites
            customFloorRunes = GameObject.FindGameObjectWithTag("Default_Floor_Rune").GetComponent<SpriteRenderer>().sprite;

            // Reset "Reset values button pressed" bool
            editorResetValuesButtonPressed = false;
        }
    }


    // Change effect colour
    private void ChangeEffectColour(float summonOpacityPercentage)
    {
        newEffectColour = new Color(newEffectColour.r, newEffectColour.g, newEffectColour.b, summonOpacityPercentage / 100);

        // Get reference to the summon effect main modules for colour assignment (exclude "Lock_Colour" tagged particle systems )
        summonEffectMainModule = summonEffect.GetComponentInChildren<ParticleSystem>().main;

        ParticleSystem[] allChildParticleSystems = summonEffect.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem summonParticleSystem in allChildParticleSystems)
        {
            if (!summonParticleSystem.gameObject.CompareTag("Lock_Colour"))
            {
                summonEffectMainModule = summonParticleSystem.GetComponent<ParticleSystem>().main;
                summonEffectMainModule.startColor = newEffectColour;
            }
        }
    }

    // Button function to play effect in editor
    public void PlayEffect()
    {
	    // Set editor "Play" button pressed bool to true
	    editorPlayButtonPressed = true;
    }

    public void ResetEffectValuesToDefault()
    {
        // Set editor "Reset effect values to default" button pressed bool to true
        editorResetValuesButtonPressed = true;
    }

    // Set any assigned custom sprites to each editable particel effect that uses texture sheet animaiton module
    private void SetEffectCustomSprite()
    {
        magicEffectTextureSheetModule = summonIncantationInstance.GetComponentInChildren<ParticleSystem>().textureSheetAnimation;

        ParticleSystem[] allChildParticleSystems = summonIncantationInstance.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem summonParticleSystem in allChildParticleSystems)
        {
            // Floor runes
            if (summonParticleSystem.gameObject.CompareTag("Floor_Runes") && customFloorRunes != null)
            {
                magicEffectTextureSheetModule = summonParticleSystem.GetComponent<ParticleSystem>().textureSheetAnimation;
                magicEffectTextureSheetModule.SetSprite(0, customFloorRunes);
            }

            // Floating floor Runes
            if (summonParticleSystem.gameObject.CompareTag("Floating_Floor_Runes") && customFloatingFloorRunes != null)
            {
                magicEffectTextureSheetModule = summonParticleSystem.GetComponent<ParticleSystem>().textureSheetAnimation;
                magicEffectTextureSheetModule.SetSprite(0, customFloatingFloorRunes);
            }

            // Floating floor Runes
            if (summonParticleSystem.gameObject.CompareTag("Rising_Runes") && customRisingRunes != null)
            {
                magicEffectTextureSheetModule = summonParticleSystem.GetComponent<ParticleSystem>().textureSheetAnimation;
                magicEffectTextureSheetModule.SetSprite(0, customRisingRunes);
            }
        }
    }
}
