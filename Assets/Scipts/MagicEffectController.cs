using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectController : MonoBehaviour
{
    // Reference to base magic effect prefab and empty game object to hold instances once instantiated
    [Tooltip("This is the game object containing the base magic effect")]   // Base magic effect
    [SerializeField] private GameObject baseMagicEffect = null;
    private GameObject magicEffectInstance = null;

    // Sprites for each particle system that uses texture sheet animation for sprite rendering
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customFloorRunes = null;              // Floor runes
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customFloatingFloorRunes = null;      // Floating floor runes
    [Tooltip("Set custom sprite for this effect. \nTo use default sprite leave this field blank.")]
    [SerializeField] private Sprite customRisingRunes = null;             // Rising runes

    // Visual effect variables
    [Tooltip("Set the colour of the summon incantation")]       // Summon
    [SerializeField] private Color summonEffectColour;
    // Set opacity range between range (10% - 70%)
    [Tooltip("Set the opacity percentage of the effect colour of the incantation (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float summonOpacityPercentage = 70.0f;
    // Light intensity
    private Light summonEffectLight = null;
    [Tooltip("Set the intensity of the light (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float summonLightIntensity = 0.0f;
    // Spell effect activation key
    [Tooltip("Set the key used to activate summon spell effect")]
    [SerializeField] private KeyCode summonActivationKey = KeyCode.LeftArrow;
    private string summonEffectTypeName = "Summon_Effect_(Instance)";

    [Tooltip("Set the colour of the attack incantation")]       // Attack
    [SerializeField] private Color attackEffectColour;
    // Set opacity range between range (10% - 70%)
    [Tooltip("Set the opacity percentage of the effect colour of the incantation")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float attackOpacityPercentage = 70.0f;
    // Light intensity
    private Light attackEffectLight = null;
    [Tooltip("Set the intensity of the light (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float attackLightIntensity = 0.0f;
    // Spell effect activation key
    [Tooltip("Set the key used to activate attack spell effect")]
    [SerializeField] private KeyCode attackActivationKey = KeyCode.RightArrow;
    private string attackEffectTypeName = "Attack_Effect_(Instance)";

    [Tooltip("Set the colour of the heal incantation")]         // Heal
    [SerializeField] private Color healEffectColour;
    // Set opacity range between range (10% - 70%)
    [Tooltip("Set the opacity percentage of the effect colour of the incantation")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float healOpacityPercentage = 70.0f;
    // Light intensity
    private Light healEffectLight = null;
    [Tooltip("Set the intensity of the light (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float healLightIntensity = 0.0f;
    // Spell effect activation key
    [Tooltip("Set the key used to activate heal spell effect")]
    [SerializeField] private KeyCode healActivationKey = KeyCode.UpArrow;
    private string healEffectTypeName = "Heal_Effect_(Instance)";

    [Tooltip("Set the colour of the dark incantation")]         // Dark
    [SerializeField] private Color darkEffectColour;
    // Set opacity range between range (10% - 70%)
    [Tooltip("Set the opacity percentage of the effect colour of the incantation")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float darkOpacityPercentage = 70.0f;
    // Light intensity
    private Light darkEffectLight = null;
    [Tooltip("Set the intensity of the light (10% - 70%)")]
    [Range(10.0f, 70.0f)]
    [SerializeField] private float darkLightIntensity = 0.0f;
    // Spell effect activation key
    [Tooltip("Set the key used to activate dark spell effect")]
    [SerializeField] private KeyCode darkActivationKey = KeyCode.DownArrow;
    private string darkEffectTypeName = "Dark_Effect_(Instance)";

    // Particle system module variables
    private ParticleSystem.MainModule magicEffectMainModule;
    private ParticleSystem.TextureSheetAnimationModule magicEffectTextureSheetModule;

    // Audio controls
    [Tooltip("Set audio for the summon incantation on or off")]
    [SerializeField] private bool audioOn = true;
    private AudioSource summonSoundEffect = null;

    // Play Effect Buttons
    private bool summonEditorButtonPressed = false;     // Summon effect
    private bool attackEditorButtonPressed = false;     // Attack effect
    private bool healEditorButtonPressed = false;       // Heal effect
    private bool darkEditorButtonPressed = false;       // Dark effect

    // Player variables
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the player transform
        player = GameObject.FindGameObjectWithTag("Player");

        // Set play effect buttons to false on start (prevent unwanted effect instantiation)
        summonEditorButtonPressed = false;     // Summon effect
        attackEditorButtonPressed = false;     // Attack effect
        healEditorButtonPressed = false;       // Heal effect
        darkEditorButtonPressed = false;       // Dark effect

        // Set effect colour and lock alpha at 70% (this is how the effect looks best)
        summonEffectColour = new Color(summonEffectColour.r, summonEffectColour.g, summonEffectColour.b, summonOpacityPercentage / 100);
        attackEffectColour = new Color(attackEffectColour.r, attackEffectColour.g, attackEffectColour.b, attackOpacityPercentage / 100);
        healEffectColour = new Color(healEffectColour.r, healEffectColour.g, healEffectColour.b, healOpacityPercentage / 100);
        darkEffectColour = new Color(darkEffectColour.r, darkEffectColour.g, darkEffectColour.b, darkOpacityPercentage / 100);

        // Get effect light and set colour and intensity
        summonEffectLight = baseMagicEffect.GetComponentInChildren<Light>();       // Summon effect
        summonEffectLight.color = summonEffectColour;
        summonEffectLight.intensity = summonLightIntensity / 100;

        attackEffectLight = baseMagicEffect.GetComponentInChildren<Light>();       // Attack effect
        attackEffectLight.color = summonEffectColour;
        attackEffectLight.intensity = attackLightIntensity / 100;

        healEffectLight = baseMagicEffect.GetComponentInChildren<Light>();         // Heal effect
        healEffectLight.color = summonEffectColour;
        healEffectLight.intensity = healLightIntensity / 100;

        darkEffectLight = baseMagicEffect.GetComponentInChildren<Light>();         // Dark effect
        darkEffectLight.color = summonEffectColour;
        darkEffectLight.intensity = darkLightIntensity / 100;

        // Set audio on or off
        summonSoundEffect = baseMagicEffect.GetComponentInChildren<AudioSource>();

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
        /*
        // Countdown for light dimming
        if (startTimer == true)
        {
            timer -= Time.deltaTime;
        }
        // Dim light
        if (timer <= 0 && summonEffectLight.spotAngle > 0.0f)
        {
            startTimer = false;
            summonEffectLight.spotAngle -= 0.1f;     // Must refer to newly created prefab instance!!! Fix this. 
        }
        */


        // Trigger summon incantation 
        if (Input.GetKeyDown(summonActivationKey) || summonEditorButtonPressed)
        {
            // Instantiate summon magic effect
            summonEditorButtonPressed = InstantiateMagicEffect(summonEffectColour, summonEffectTypeName, summonEditorButtonPressed);
        }

        // Trigger attack incantation 
        if (Input.GetKeyDown(attackActivationKey) || attackEditorButtonPressed)
        {
            // Instantiate attack magic effect
            attackEditorButtonPressed = InstantiateMagicEffect(attackEffectColour, attackEffectTypeName, attackEditorButtonPressed);
        }

        // Trigger heal incantation 
        if (Input.GetKeyDown(healActivationKey) || healEditorButtonPressed)
        {
            // Instantiate heal magic effect
            healEditorButtonPressed = InstantiateMagicEffect(healEffectColour, healEffectTypeName, healEditorButtonPressed);
        }

        // Trigger dark incantation 
        if (Input.GetKeyDown(darkActivationKey) || darkEditorButtonPressed)
        {
            // Instantiate dark magic effect
            darkEditorButtonPressed = InstantiateMagicEffect(darkEffectColour, darkEffectTypeName, darkEditorButtonPressed);
        }
    }

    // Button functions to play effects in editor
    public void PlaySummonEffect()
    {
        // Set editor summon button pressed bool to true
        summonEditorButtonPressed = true;
    }

    public void PlayAttackEffect()
    {
        // Set editor attack button pressed bool to true
        attackEditorButtonPressed = true;
    }

    public void PlayHealEffect()
    {
        // Set editor heal button pressed bool to true
        healEditorButtonPressed = true;
    }

    public void PlayDarkEffect()
    {
        // Set editor dark button pressed bool to true
        darkEditorButtonPressed = true;
    }


    // Instantiate magic effect function
    private bool InstantiateMagicEffect(Color magicEffectColour, string effectTypeName, bool editorButtonPressed)
    {
	    // Instantiate effect instance
	    if (summonEditorButtonPressed || healEditorButtonPressed || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
	    {
            // Player effects (summon and heal)
		    magicEffectInstance = Instantiate(baseMagicEffect, player.transform);
		    SetEffectInstanceColour(magicEffectColour, magicEffectInstance, effectTypeName);
            SetEffectCustomSprite();
        }
	    else if (attackEditorButtonPressed || darkEditorButtonPressed || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
	    {
            // Get all the enemies in the scene
            foreach (GameObject enemy in EnemyRangeList.enemiesInRange)
            {
                // Attack effects (attack and dark)
	            magicEffectInstance = Instantiate(baseMagicEffect, enemy.transform);
	            SetEffectInstanceColour(magicEffectColour,magicEffectInstance, effectTypeName);
            }
	    }

	    // Start countdown timer for light intensity
	    //startTimer = true;

	    // Reset editor button pressed bool
	    return editorButtonPressed = false;
    }

    // Set the colour of each editable part of particle effect instance passed in
    private void SetEffectInstanceColour(Color magicEffectColour, GameObject effectInstance, string effectTypeName)
    {
	    // Assign name of effect type to instantiated effect instance
	    magicEffectInstance.name = effectTypeName;

	    magicEffectMainModule = magicEffectInstance.GetComponentInChildren<ParticleSystem>().main;

	    ParticleSystem[] allChildParticleSystems = magicEffectInstance.GetComponentsInChildren<ParticleSystem>();

	    foreach (ParticleSystem summonParticleSystem in allChildParticleSystems)
	    {
		    if (!summonParticleSystem.gameObject.CompareTag("Floating_Floor_Runes") || !summonParticleSystem.gameObject.CompareTag("Rising_Runes"))
		    {
			    magicEffectMainModule = summonParticleSystem.GetComponent<ParticleSystem>().main;
			    magicEffectMainModule.startColor = magicEffectColour;
		    }
	    }
    }

    // Set any assigned custom sprites to each editable particel effect that uses texture sheet animaiton module
    private void SetEffectCustomSprite()
    {
        magicEffectTextureSheetModule = magicEffectInstance.GetComponentInChildren<ParticleSystem>().textureSheetAnimation;

        ParticleSystem[] allChildParticleSystems = magicEffectInstance.GetComponentsInChildren<ParticleSystem>();

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
