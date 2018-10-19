using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The code type presented by the panel
/// Numbers, Letters (uppercase), or Both
/// The code type MUST not change
/// </summary>
public enum CodeType
{
    Letters = 0, // Letters (A-Z, uppercase)
    Numbers = 1, // Numbers (0-9)
    AlphaNumeric = 2, // Both letters and numbers values
}

/// <summary>
/// This is the primary behaviour used to control panels and game flow
/// **** YOU ARE NOT ALLOWED TO ALTER THIS CLASS *******
/// </summary>
public class PanelBehaviour : MonoBehaviour
{
    // Values present for each code type
    private static readonly char[] LETTER = { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };
    private static readonly char[] NUMBER = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    /// <summary>
    /// The current state the panel is in
    /// </summary>
    public enum State
    {
        Ready = 0, // Panel is currently in a "safe" place
        Warn = 1, // Panel has reached the warning status
        Boom = 2, // Failed to diffuse the panel in time
    }

    [Header("DO NOT CHANGE - Code style and count required")]
    public CodeType codeType = CodeType.AlphaNumeric; 
    public int codeCount = 5;

    [Header("Disable when you are testing other panels")]
    public bool usePanel = true;

    [Header("Elements to hide when panel is inactive")]
    public Image background;
    public GameObject elements;

    [Header("Game Properties")]
    [Tooltip("Base time until the bomb explodes on this panel")]
    public float boomTime = 30.0f;
    [Tooltip("Minimum range for time to reduce for each bomb diffusal")]
    public float decayMin = 0.1f;
    [Tooltip("Maximum range for time to reduce for each bomb diffusal")]
    public float decayMax = 0.25f;
    [Tooltip("Time to begin warning the player to take action")]
    public float warnTime = 5.0f;

    private State state = State.Ready; // the current state of the panel
    private string code = ""; // the current code used to diffuse the panel
    private float diffuseTime = 0.0f; // the amount of time remaining until the bomb explodes

    /// <summary>
    /// The current state of the panel
    /// </summary>
    public State CurrentState { get { return state; } }
    
    /// <summary>
    /// The amount of time remaining until the bomb explodes
    /// </summary>
    public float TimeRemaining { get { return diffuseTime; } }

    /// <summary>
    /// The current code used to diffuse the panel
    /// </summary>
    public string DiffuseCode { get { return code; } }

    /// <summary>
    /// Returns true when this panel is active
    /// </summary>
    public bool ActivePanel { get { return background.enabled; } }


	// Use this for initialization
	void Start ()
    {
        SetCode(0); // start at the first panel
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!usePanel) return; // Simple solution to not use a panel - THIS IS A HACK :P

        // reduce diffuse time each frame and set the state accordingly
        diffuseTime -= Time.deltaTime;
        if(diffuseTime <= 0.0f)
        {
            state = State.Boom;
        }
        else if(diffuseTime <= warnTime)
        {
            state = State.Warn;
        }
	}

    /// <summary>
    /// Makes the panel visible/invisible
    /// NOTE: I'm doing this instead of disabling the object because a disabled object will NOT update
    /// </summary>
    /// <param name="turnOn">whether to turn the panel on or off</param>
    public void DisplayPanel(bool turnOn)
    {
        background.enabled = turnOn; // .enabled is used for components
        elements.SetActive(turnOn); // SetActive is used for objects
    }

    /// <summary>
    /// Attempts to diffuse the bomb!
    /// </summary>
    /// <param name="diffuseCode">the diffuse code to check against the pass code</param>
    /// <returns>success if the code matches, fail if it is incorrect</returns>
    public bool Diffuse(string diffuseCode)
    {
        if (diffuseCode != code) return false;

        // if successful, reset the code with a slight decay to boom time
        SetCode(Random.Range(decayMin, decayMax));
        return true;
    }

    /// <summary>
    /// Determines the next pass code to use
    /// </summary>
    /// <param name="decayAmount">the amount to reduce from boom time</param>
    private void SetCode(float decayAmount)
    {
        state = State.Ready;
        boomTime -= decayAmount; // reduce boom time here
        diffuseTime = boomTime; // set our time to whatever the current decayed boom time is

        // Determine which array to use for values
        // This is a silly way to do this, but I wanted to expose the direct values you will use
        char[] codeOptions = null;
        if (codeType == CodeType.Letters) codeOptions = LETTER;
        else if (codeType == CodeType.Numbers) codeOptions = NUMBER;
        else
        {
            // For both, make a new array that stores both letters and numbers
            codeOptions = new char[LETTER.Length + NUMBER.Length];
            LETTER.CopyTo(codeOptions, 0);
            NUMBER.CopyTo(codeOptions, LETTER.Length);
        }

        // Loop through and add a new character for each spot on our code
        code = "";
        for(int i = 0; i < codeCount; ++i)
        {
            code += codeOptions[Random.Range(0, codeOptions.Length)];
        }
    }
}
