using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    private AudioSource ad;

    [SerializeField]
    private float minPitch;
    [SerializeField]
    private float maxPitch;

    private void Awake()
    {
        ad = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ad.pitch = CarController.carSpeed;

        if (CarController.carSpeed < minPitch)
            ad.pitch = minPitch;
        else if(CarController.carSpeed > maxPitch)
            ad.pitch = maxPitch;
    }
}
