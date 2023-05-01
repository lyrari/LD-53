using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public BoolSO m_SpookyMeterActive;
    public FloatSO m_SpookyMeter;

    public Color EvilLightColor;

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

    // Spooky lights out
    public float minLightsOutTime = 45f;
    public float lightsOutVariance = 15f;
    float m_NextLightsOutTime = float.MaxValue;

    Coroutine LightFlickerCoroutine;
    bool LightsOutLastFrame;

    //Substance Parameter Stuff
    //public SubstanceRuntimeGraph testGraph; LAGGY SHIT
    public GameObject bloodyFloor;
    public GameObject cleanFloor;

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

    public void PrepSpookyMeter()
    {
        m_NextLightsOutTime = Time.time + minLightsOutTime + Random.Range(0, lightsOutVariance) - m_SpookyMeter.value / 5;
        RoomMainLight.color = EvilLightColor;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (m_SpookyMeterActive.value && Time.time > m_NextLightsOutTime)
        {
            Debug.Log("Turnout lights");
            LightsOut.value = true;
            PrepSpookyMeter();
        }

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

        // SPOOK

        if (m_SpookyMeterActive.value)
        {
            RoomMainLight.intensity -= (m_SpookyMeter.value / 3000f) * Time.deltaTime;
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
        float lightIntensityFlickerOff = 0;

        if (m_SpookyMeter.value > 40)
        {
            AkSoundEngine.PostEvent("lightFlicker", this.gameObject);
            l.intensity = lightIntensityFlickerOff;
            //testGraph.SetInputFloat("Blood_Intensity", .01f); THIS IS LAGGY
            //testGraph.RenderAsync();
            bloodyFloor.SetActive(true);
            cleanFloor.SetActive(false);
            yield return new WaitForSeconds(.5f);
            l.intensity = lightIntensityInitial;
            AkSoundEngine.PostEvent("lightFlicker", this.gameObject);
            yield return new WaitForSeconds(.5f);
            l.intensity = lightIntensityFlickerOff;
            AkSoundEngine.PostEvent("lightFlicker", this.gameObject);
            //testGraph.SetInputFloat("Blood_Intensity", 0f); THIS IS LAGGY
            //testGraph.RenderAsync();
            bloodyFloor.SetActive(false);
            cleanFloor.SetActive(true);
            yield return new WaitForSeconds(.5f);
            l.intensity = lightIntensityInitial;
        }
        else
        {
            AkSoundEngine.PostEvent("lightFlicker", this.gameObject);
            l.intensity = lightIntensityFlicker;
            yield return new WaitForSeconds(0.1f);
            l.intensity = lightIntensityInitial;
           yield return new WaitForSeconds(0.1f);
            l.intensity = lightIntensityFlicker;
            yield return new WaitForSeconds(0.1f);
            l.intensity = lightIntensityInitial;
        }
        LightFlickerCoroutine = null;
    }
}
