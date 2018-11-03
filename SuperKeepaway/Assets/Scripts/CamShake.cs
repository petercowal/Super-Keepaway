using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{

    Vector3 originalCameraPosition;

    public float shakeAmt = 0;

    public Camera mainCamera;

   // void OnCollisionEnter2D(Collision2D coll)
   // {

      //  shakeAmt = coll.relativeVelocity.magnitude * .0025f;
       // InvokeRepeating("CameraShake", 0, .01f);
       // Invoke("StopShaking", 0.3f);

   // }

    void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
    }

    public void StartCameraShake(float amt, float duration)
    {
        shakeAmt = amt;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", duration);
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = originalCameraPosition;
            pp.y += quakeAmt; // can also add to x and/or z
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}