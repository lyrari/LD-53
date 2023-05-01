using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScoreTracker")]
public class ScoreTracker : ScriptableObject
{
    public int successes;
    public int failures;

    public void Reset()
    {
        successes = 0; failures = 0;
    }
}
