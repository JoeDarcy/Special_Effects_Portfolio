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

    // Visual effect variables
    [Tooltip("Set the colour of the summon incantation")]
    [SerializeField] private Color changeEffectColour;
    private ParticleSystem.MainModule summonEffectMainModule;
    [Tooltip("Set audio for the summon incantation on or off")]
    [SerializeField] private bool audioOn = true;
    private AudioSource summonSoundEffect = null;

    // Play Effect Button
    private bool editorButtonPressed = false;


    // Light reference and dimmer timer
    private Light effectLight = null;
    private float timer = 9.0f;      
    private bool startTimer = false;     


    // Start is called before the first frame update
    void Start()
    {
        // Set play effect button to false on start
        editorButtonPressed = false;

        // Set effect colour and lock alpha at 70%
        changeEffectColour = new Color(changeEffectColour.r, changeEffectColour.g, changeEffectColour.b, 0.7f);

        // Get reference to the summon effect main modules for colour assignment (exclude "Lock_Colour" tagged particle systems )
        summonEffectMainModule = summonEffect.GetComponentInChildren<ParticleSystem>().main;

	    ParticleSystem[] allChildParticleSystems = summonEffect.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem summonParticleSystem in allChildParticleSystems)
        {
	        if (!summonParticleSystem.gameObject.CompareTag("Lock_Colour"))
	        {
		        summonEffectMainModule = summonParticleSystem.GetComponent<ParticleSystem>().main;
		        summonEffectMainModule.startColor = changeEffectColour;
            }
        }

        // Get effect light and set colour and intensity
        effectLight = summonEffect.GetComponentInChildren<Light>();
        effectLight.color = changeEffectColour;
        effectLight.intensity = 1.0f;
        
        
        // Remember to change the light size / intensity over time in Update()

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
        // Countdown for light dimming
	    if (startTimer == true)
	    {
		    timer -= Time.deltaTime;
        }
	    // Dim light
        if (timer <= 0 && effectLight.spotAngle > 0.0f)
        {
            startTimer = false;
	        effectLight.spotAngle -= 0.1f;     // Not working in here but does outside sort of
        }



        // Trigger summon incantation 
        if (Input.GetKeyDown(KeyCode.Space) || editorButtonPressed)
	    {
            // Instantiate summon instance
		    summonIncantationInstance = Instantiate(summonEffect);

            // Start countdown timer for light intensity
            startTimer = true;

            // Reset editor button pressed bool
            editorButtonPressed = false;
	    }
    }


    // Button function to play effect in editor
    public void PlayEffect()
    {
	    // Set editor button pressed bool to true
	    editorButtonPressed = true;
    }
}
