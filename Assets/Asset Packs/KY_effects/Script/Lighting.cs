using System.Collections;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public bool flashFlg;

    public bool flashingFlg;
    public float flashingOff;
    public float flashingOffIntensity = 1;
    public float flashingOffPower;
    public float flashTimer = 0.3f;
    public float keepOnTime;
    public float keepTime;

    public float lighting = 1;

    private bool lightKeepFlg;
    private bool lightOffFlg;
    public Light lightPower;
    public float maxLight = 1;
    public float minLight;
    public float revOnTime;

    private void Start()
    {
        lightPower = GetComponent<Light>();

        flash();
        setRev();
        keepOn();
        setFlashingOff();
    }

    private void Update()
    {
        if (flashingFlg)
        {
            if (lightOffFlg)
            {
                lightPower.intensity -= lighting * Time.deltaTime;
                if (lightPower.intensity <= minLight) lightOffFlg = false;
            }
            else
            {
                lightPower.intensity += lighting * Time.deltaTime;
                if (lightPower.intensity > maxLight) lightOffFlg = true;
            }
        }
        else if (lightPower.intensity > 0 && lightPower.enabled && !lightKeepFlg)
        {
            lightPower.intensity -= lighting * Time.deltaTime;
        }

        if (lightKeepFlg && keepTime > 0)
        {
            keepTime -= Time.deltaTime;
            if (keepTime <= 0) lightKeepFlg = false;
        }
    }


    private IEnumerator flash()
    {
        if (flashFlg)
        {
            lightPower.enabled = false;
            yield return new WaitForSeconds(flashTimer);
            lightPower.enabled = true;
        }
    }

    private IEnumerator setRev()
    {
        if (revOnTime > 0)
        {
            yield return new WaitForSeconds(revOnTime);
            lighting *= -1;
        }
    }

    private IEnumerator keepOn()
    {
        if (keepOnTime > 0)
        {
            yield return new WaitForSeconds(keepOnTime);
            lightKeepFlg = true;
        }
    }

    private IEnumerator setFlashingOff()
    {
        if (flashingOff > 0)
        {
            yield return new WaitForSeconds(flashingOff);
            flashingFlg = false;
            if (flashingOffPower > 0)
            {
                lightPower.intensity = flashingOffIntensity;
                lighting = flashingOffPower;
            }
        }
    }
}