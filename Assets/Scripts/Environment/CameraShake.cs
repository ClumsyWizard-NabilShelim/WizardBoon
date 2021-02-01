using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	[HideInInspector] public bool Shake;
	[HideInInspector] public float ShakeMagnitude;
	private Transform Cam;
	private Vector3 OriginalPos;
	[HideInInspector] public float ShakeDuration;
    // Start is called before the first frame update
    void Start()
    {
		Cam = Camera.main.transform;
		OriginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (Shake)
		{
			if(ShakeDuration > 0)
			{
				Cam.localPosition = Vector3.Lerp(Cam.localPosition, OriginalPos + Random.insideUnitSphere * ShakeMagnitude, 30 * Time.deltaTime);
				ShakeDuration -= Time.deltaTime;
			}
			else
			{
				ShakeDuration = 0;
				Cam.localPosition = OriginalPos;
				Shake = false;
			}
		}
    }

	public void ShakeCamera(float duration, float magnitude)
	{
		ShakeDuration = duration;
		ShakeMagnitude = magnitude;
		Shake = true;
	}
}
