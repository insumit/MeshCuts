using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayNightController : MonoBehaviour
{
    [Range(0,24)] 
    public float timeOfDay;
    float day = 0;

    public float orbitSpeed = 0.5f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;

    private bool isNight;
    private PhysicallyBasedSky sky;

    Scaling scaling;

    void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * orbitSpeed;
        if (timeOfDay > 24)
        {
            timeOfDay = 0;
            day++;
        }

        //if(day == 0)
        //{
        //    if (timeOfDay > 7 && timeOfDay < 10)
        //        scaling.ChangeSize(true);
        //    else
        //        scaling.ChangeSize(false);
        //}

        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;

        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -150.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150.0f, 0);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 1000;

        CheckDayNightTransition();
    }

    private void CheckDayNightTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
                StartDay();
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
                StartNight();
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.None;
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.None;
    }
}
