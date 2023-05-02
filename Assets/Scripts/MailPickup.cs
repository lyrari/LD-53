using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailPickup : MonoBehaviour
{
    public ScoreTracker scoreTracker;

    public Material ButtonOn;
    public Material ButtonOff;
    public MeshRenderer MailButton;

    public LetterSpawner MailBagPrefab;
    public Transform BagSpawnLocation;

    public float buttonTimeout = 10f; // How long you have to respond to a button before a failure
    bool buttonActive;
    float timeoutTime;

    // Tutorial
    public LetterSpawner TutorialMailBag;
    public GameObject TutorialText;
    bool tutorialClickDone;
    public GameObject CreepyLetter;

    public float mailMinDelay = 30f;
    public float mailVariation = 10f;
    public float nextMailSpawn;

    bool creepyLetterSpawned;
    public float tutTimeUntilCreepyLetterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeoutTime = float.MaxValue;
        nextMailSpawn = float.MaxValue;
        tutTimeUntilCreepyLetterSpawn = float.MaxValue;
        MailButton.material = ButtonOff;
        StartCoroutine(TutorialRing());
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn next mail
        if (Time.time > nextMailSpawn)
        {
            MailReady();
        }

        // Countdown to timeout
        if (Time.time > timeoutTime)
        {
            Timeout();
        }

        // Spawn creepy letter
        if (!creepyLetterSpawned && (Time.time > tutTimeUntilCreepyLetterSpawn || (scoreTracker.successes + scoreTracker.failures >= 5)))
        {
            creepyLetterSpawned = true;
            //AkSoundEngine.PostEvent("mailFaked", this.gameObject); //evilMailAppeared
            CreepyLetter.SetActive(true);
            AkSoundEngine.PostEvent("evilMailAppear", this.gameObject);
        }
        
    }

    // Wait 15 seconds then ring and enable button
    IEnumerator TutorialRing()
    {
        yield return new WaitForSeconds(15f);
        Debug.Log("Tutorial ring - Mail is ready!");
        buttonActive = true;
        MailButton.material = ButtonOn;
        AkSoundEngine.PostEvent("mailReady", this.gameObject);
    }

    // Call this to set nextMailSpawn to right now.
    public void ForceSpawnMail()
    {
        nextMailSpawn = Time.time;
    }

    // Mail is ready, start the countdown
    public void MailReady()
    {
        Debug.Log("Mail ready");
        buttonActive = true;
        MailButton.material = ButtonOn;
        AkSoundEngine.PostEvent("mailReady", this.gameObject);

        nextMailSpawn = float.MaxValue;
        timeoutTime = Time.time + buttonTimeout;

        // TODO: Play ding sound here
    }

    // Failed to hit button in time
    public void Timeout()
    {
        Debug.Log("Mail - Timeout");
        buttonActive = false;
        MailButton.material = ButtonOff;
        timeoutTime = float.MaxValue;
        AkSoundEngine.PostEvent("deliverFail", this.gameObject);

        scoreTracker.failures++;
        nextMailSpawn = Time.time + (5f);

        // TODO: Play failure sound
    }

    public void SetNextMailTime()
    {
        nextMailSpawn = Time.time + mailMinDelay + Random.Range(0, mailVariation);
    }

    // Mail bu tton done, disable timeout
    public void PressMailButton()
    {
        if (!buttonActive) return;

        //tutorial logic
        if (!tutorialClickDone)
        {
            TutorialMailBag.gameObject.SetActive(true);
            TutorialText.SetActive(false);
            buttonActive = false;
            MailButton.material = ButtonOff;

            tutorialClickDone = true;
            tutTimeUntilCreepyLetterSpawn = Time.time + mailMinDelay;
            return;
        }


        Debug.Log("Mail dispensing");
        Instantiate(MailBagPrefab);
        MailBagPrefab.transform.position = BagSpawnLocation.position;


        buttonActive = false;
        MailButton.material = ButtonOff;
        timeoutTime = float.MaxValue;

        nextMailSpawn = Time.time + mailMinDelay + Random.Range(0, mailVariation);

        // TODO: Play success sound
    }
}
