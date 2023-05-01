using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepyTutorialLetter : MonoBehaviour
{
    bool eventTriggered;
    BoolSO tutorialMode;

    // On letter pickup:
    // Cutscene plays (Fade to red slowly)
    // Creepy letter sound plays ("demonNote"), screen goes full black (fade in after 5 secs?)
    // Demon altar appears
    // Timer starts and appears (5:00 counting down)
    // Mail timer has to start 
    // SpookMeter begins rising 
    // Reset failures to 0
    // Light turning off timer starts
    // Screen fades in over the next couple seconds
    // Enable Demon letters spawning and demon seals (if we get to that)
    public void OnCreepyLetterPickup()
    {
        if (eventTriggered) return;

        eventTriggered = true;
        AkSoundEngine.PostEvent("demonNote", this.gameObject);
        Debug.Log("CREEPY MODE ACTIVATE");

        tutorialMode.value = false;
    }
}
