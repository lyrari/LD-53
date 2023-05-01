using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public ScoreTracker tracker;
    bool grabbed;
    Rigidbody rb;

    public DepositBin.BinType MyCorrectBin;

    public float HellTimer = 20f;
    public float timeUntilDestruction;
    public bool TutorialLetter;

    // Any other info about the object should go here - like mail stats, destination

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (MyCorrectBin == DepositBin.BinType.Hell && !TutorialLetter)
        {
            timeUntilDestruction = Time.time + HellTimer;
        } else
        {
            timeUntilDestruction = float.MaxValue;
        }
    }

    private void Update()
    {
        if (Time.time > timeUntilDestruction)
        {
            AkSoundEngine.PostEvent("deliverFail", this.gameObject);
            tracker.failures++;
            Destroy(this.gameObject);
        }
    }

    public void ToggleGrabbed()
    {
        grabbed = !grabbed;
        rb.isKinematic = !rb.isKinematic;

        CreepyTutorialLetter creepletter = this.GetComponent<CreepyTutorialLetter>();
        if (creepletter != null)
        {
            creepletter.OnCreepyLetterPickup();
        }
    }
}
