using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light TestLight;
    public Light RoomMainLight;
    public Light RoomAmbientLight;

    public Light Flashlight;

    float m_NextLightFlickerTime;
    public float minLightFlicker = 5f;
    public float lightFlickerVariance = 3f;

    public BoolSO LightsOut;
    public Material SwitchOnMaterial;
    public Material SwitchOffMaterial;
    public MeshRenderer LightSwitchRenderer;

    Coroutine LightFlickerCoroutine;
    bool LightsOutLastFrame;
    


    // Start is called before the first frame update
    void Start()
    {
        LightsOut.value = false;
        LightSwitchRenderer.material = SwitchOnMaterial;
        TestLight.gameObject.SetActive(false);
        GenerateNextFlickerTime();
    }

    void GenerateNextFlickerTime()
    {
        m_NextLightFlickerTime = Time.time + minLightFlicker + Random.Range(0, lightFlickerVariance);
    }

    // Update is called once per frame
    
    void Update()
    {
        if (Time.time > m_NextLightFlickerTime)
        {
            LightFlickerCoroutine = StartCoroutine(flickerLight(RoomMainLight));
            GenerateNextFlickerTime();
        }

        // If LightsOut status changed, toggle room lights
        if (LightsOut.value != LightsOutLastFrame)
        {
            RoomLightsChanged();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Flashlight.enabled = !Flashlight.enabled;
            AkSoundEngine.PostEvent("flashLight", this.gameObject);
        }


        LightsOutLastFrame = LightsOut.value;
    }

    public void RoomLightsChanged()
    {
        Debug.Log("RoomLightsChanged - " + LightsOut.value);

        if (LightsOut.value && LightFlickerCoroutine != null)
        {
            StopCoroutine(LightFlickerCoroutine);
        }

        RoomMainLight.enabled = !LightsOut.value;
        RoomAmbientLight.enabled = !LightsOut.value;
        //Flashlight.enabled = LightsOut.value;
        AkSoundEngine.PostEvent("lightSwitch", this.gameObject);

        if (LightsOut.value)
        {
            LightSwitchRenderer.material = SwitchOffMaterial;
        } else
        {
            LightSwitchRenderer.material = SwitchOnMaterial;
        }
        
    }

    IEnumerator flickerLight(Light l)
    {
        if (LightsOut.value) yield break;

        float lightIntensityInitial = l.intensity;
        float lightIntensityFlicker = lightIntensityInitial / 2;

        AkSoundEngine.PostEvent("lightFlicker", this.gameObject);
        l.intensity = lightIntensityFlicker;
        yield return new WaitForSeconds(0.1f);
        l.intensity = lightIntensityInitial;
        yield return new WaitForSeconds(0.1f);
        l.intensity = lightIntensityFlicker;
        yield return new WaitForSeconds(0.1f);
        l.intensity = lightIntensityInitial;

        LightFlickerCoroutine = null;
    }
}
