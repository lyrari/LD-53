using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepyTutorialLetter : MonoBehaviour
{
    public bool eventTriggered;
    public BoolSO tutorialMode;
    public FloatSO spookyMeter;
    public ScoreTracker m_ScoreTracker;

    public Animator FadeAnimator;
    public GameObject DemonAltar;
    public Timer TimerRef;
    public MailPickup MailPickupRef;
    public GameManager GameManagerRef; // Spook init
    public LightManager LightManagerRef; // PrepSpookyMeter




    // On letter pickup:
    // *Cutscene plays (Fade to red slowly)
    // *Creepy letter sound plays ("demonNote"), screen goes full black (fade in after 5 secs?)
    // *Demon altar appears
    // *Timer starts and appears (5:00 counting down)
    // *Mail timer has to start 
    // *SpookMeter begins rising 
    // *Reset failures to 0
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
        FadeAnimator.SetTrigger("FadeToBlack");
        MailPickupRef.SetNextMailTime();
        GameManagerRef.initSpookyMeter();
        m_ScoreTracker.failures = 0;
        LightManagerRef.PrepSpookyMeter();

        // TODO: Enable demon letters
        StartCoroutine(fadeBackIn());
    }

    IEnumerator fadeBackIn()
    {
        yield return new WaitForSeconds(5f);
        FadeAnimator.SetTrigger("CutToNormal");
        TimerRef.InitTimer();
        DemonAltar.SetActive(true);

        yield return new WaitForSeconds(3f);
        MailPickupRef.ForceSpawnMail();
    }
}
