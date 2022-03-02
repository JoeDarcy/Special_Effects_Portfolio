using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectController : MonoBehaviour
{
    // Reference to base magic effect prefab and empty game object to hold instances once instantiated
    [Tooltip("This is the game object containing the base magic effect")]   // Base magic effect
    [SerializeField] private GameObject baseMagicEffect = null;
    private GameObject magicEffectInstance = null;

    // Visual effect variables
    [Tooltip("Set the colour of the summon incantation")]       // Summon
    [SerializeField] private Color summonEffectColour;
    // Spell effect activation key
    [Tooltip("Set the key used to activate summon spell effect")]
    [SerializeField] private KeyCode summonActivationKey = KeyCode.LeftArrow;
    private ParticleSystem.MainModule magicEffectMainModule;
    private string summonEffectTypeName = "Summon_Effect_(Instance)";

    [Tooltip("Set the colour of the attack incantation")]       // Attack
    [SerializeField] private Color attackEffectColour;
    // Spell effect activation key
    [Tooltip("Set the key used to activate attack spell effect")]
    [SerializeField] private KeyCode attackActivationKey = KeyCode.RightArrow;
    private string attackEffectTypeName = "Attack_Effect_(Instance)";

    [Tooltip("Set the colour of the heal incantation")]         // Heal
    [SerializeField] private Color healEffectColour;
    // Spell effect activation key
    [Tooltip("Set the key used to activate heal spell effect")]
    [SerializeField] private KeyCode healActivationKey = KeyCode.UpArrow;
    private string healEffectTypeName = "Heal_Effect_(Instance)";

    [Tooltip("Set the colour of the dark incantation")]         // Dark
    [SerializeField] private Color darkEffectColour;
    // Spell effect activation key
    [Tooltip("Set the key used to activate dark spell effect")]
    [SerializeField] private KeyCode darkActivationKey = KeyCode.DownArrow;
    private string darkEffectTypeName = "Dark_Effect_(Instance)";

    // Audio controls
    [Tooltip("Set audio for the summon incantation on or off")]
    [SerializeField] private bool audioOn = true;
    private AudioSource summonSoundEffect = null;

    // Play Effect Buttons
    private bool summonEditorButtonPressed = false;     // Summon effect
    private bool attackEditorButtonPressed = false;     // Attack effect
    private bool healEditorButtonPressed = false;       // Heal effect
    private bool darkEditorButtonPressed = false;       // Dark effect


    // Light reference and dimmer timer
    private Light summonEffectLight = null;
    //private float timer = 9.0f;
    //private bool startTimer = false;

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
        summonEffectColour = new Color(summonEffectColour.r, summonEffectColour.g, summonEffectColour.b, 0.7f);
        attackEffectColour = new Color(attackEffectColour.r, attackEffectColour.g, attackEffectColour.b, 0.7f);
        healEffectColour = new Color(healEffectColour.r, healEffectColour.g, healEffectColour.b, 0.7f);
        darkEffectColour = new Color(darkEffectColour.r, darkEffectColour.g, darkEffectColour.b, 0.7f);

        // Get effect light and set colour and intensity
        summonEffectLight = baseMagicEffect.GetComponentInChildren<Light>();
        summonEffectLight.color = summonEffectColour;
        summonEffectLight.intensity = 1.0f;

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
		    if (!summonParticleSystem.gameObject.CompareTag("Lock_Colour"))
		    {
			    magicEffectMainModule = summonParticleSystem.GetComponent<ParticleSystem>().main;
			    magicEffectMainModule.startColor = magicEffectColour;
		    }
	    }
    }
}
