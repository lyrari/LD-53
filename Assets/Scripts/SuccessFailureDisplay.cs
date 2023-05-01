using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuccessFailureDisplay : MonoBehaviour
{
    public ScoreTracker scoreTracker;
    public TMP_Text successes;
    public TMP_Text failures;

    private void Start()
    {
        scoreTracker.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        successes.text = "Successes: " + scoreTracker.successes;
        failures.text = "Failures: " + scoreTracker.failures;
    }
}
