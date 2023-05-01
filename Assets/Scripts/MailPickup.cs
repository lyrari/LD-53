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

    public float mailMinDelay = 30f;
    public float mailVariation = 10f;
    public float nextMailSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeoutTime = float.MaxValue;
        nextMailSpawn = float.MaxValue;
        MailButton.material = ButtonOff;
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

        scoreTracker.failures++;
        nextMailSpawn = Time.time + (mailMinDelay / 2);

        // TODO: Play failure sound
    }

    // Mail bu tton done, disable timeout
    public void PressMailButton()
    {
        if (!buttonActive) return;

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
