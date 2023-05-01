using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int FailuresForDeath = 3;
    public int SuccessesForWin = 30;

    public GameObject GameOverPanel;
    public TMP_Text GameOverMessage;

    public ScoreTracker m_ScoreTracker;
    public BoolSO m_TutorialMode;
    public BoolSO m_GameOver;

    // SPOOKS
    public FloatSO m_SpookyMeter;
    public BoolSO SpookyMeterActive;

    public GameObject ScreenCoveringEffect;
    public Light SceneLight;
    public Color VictoryColor;
    public Color FailureColor;

    public Timer timerRef;

    // Start is called before the first frame update
    void Start()
    {
        m_SpookyMeter.value = 0;
        SpookyMeterActive.value = false;

        m_GameOver.value = false;
        m_TutorialMode.value = true;
        GameOverPanel.SetActive(false);

        ScreenCoveringEffect.GetComponent<Animator>().SetTrigger("FadeFromBlack");
    }

    public void initSpookyMeter()
    {
        m_SpookyMeter.value = 10f;
        SpookyMeterActive.value = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (m_GameOver.value) {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                m_SpookyMeter.value = 0;
                AkSoundEngine.SetRTPCValue("spookyMeter", m_SpookyMeter.value);
                SpookyMeterActive.value = false;
            }
            return;
        }

        if (SpookyMeterActive.value)
        {
            m_SpookyMeter.value += Time.deltaTime * 0.25f;
            AkSoundEngine.SetRTPCValue("spookyMeter", m_SpookyMeter.value);
        }
       

        if ((!m_TutorialMode.value && m_ScoreTracker.failures >= FailuresForDeath) || timerRef.currentTime < 0)
        {
            Debug.Log("Game over");
            Image screenCoverImage = ScreenCoveringEffect.GetComponent<Image>();
            screenCoverImage.color = FailureColor;
            screenCoverImage.GetComponent<Animator>().SetTrigger("FadeToBlack");
            StartCoroutine(IncreaseLightIntensity(-0.1f));

            m_GameOver.value = true;
            GameOverPanel.SetActive(true);
            GameOverMessage.text = "Delivered to the abyss...";
            AkSoundEngine.PostEvent("gameLoss", this.gameObject);
        }

        if (m_ScoreTracker.successes >= SuccessesForWin) {
            Debug.Log("You win");
            Image screenCoverImage = ScreenCoveringEffect.GetComponent<Image>();
            screenCoverImage.GetComponent<Animator>().SetTrigger("FadeToBlack");
            screenCoverImage.color = VictoryColor;
            StartCoroutine(IncreaseLightIntensity(0.1f));

            m_GameOver.value = true;
            GameOverPanel.SetActive(true);
            GameOverMessage.text = "You won!";
            AkSoundEngine.PostEvent("gameWin", this.gameObject);
        }
    }

    IEnumerator IncreaseLightIntensity(float amount)
    {
        SceneLight.intensity += amount;
        yield return new WaitForSeconds(0.1f);
    }
}
