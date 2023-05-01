using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class DemonLetterSpawner : MonoBehaviour
{
    public GameObject DemonLetterPrefab;
    public RectTransform LetterSpawnRect;

    public BoolSO m_SpookyMeterEnabled;
    public FloatSO m_SpookyMeter;

    public float demonSpawnDelay = 20f;
    public float demonSpawnVariance = 5f;
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = float.MaxValue;
    }

    public void SetNextDemonSpawn()
    {
        nextSpawnTime = Time.time + demonSpawnDelay + Random.Range(0, demonSpawnVariance) - m_SpookyMeter.value / 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SpookyMeterEnabled && Time.time > nextSpawnTime || Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Spawn demon letter");
            Vector3 position = new Vector3(Random.Range(LetterSpawnRect.rect.xMin, LetterSpawnRect.rect.xMax),
                   0, Random.Range(LetterSpawnRect.rect.yMin, LetterSpawnRect.rect.yMax)) + LetterSpawnRect.transform.position;

            GameObject letter = Instantiate(DemonLetterPrefab, position, Quaternion.identity);
            AkSoundEngine.PostEvent("evilMailAppear", letter);

            SetNextDemonSpawn();
        }
    }
}
