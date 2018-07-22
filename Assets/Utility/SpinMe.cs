using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;
	
	void Update () {
        float xDegreesPerFrame = GetDegreesPerFrame(xRotationsPerMinute); // TODO ToGLI STA COSA CHE NON SERVE AD UN CAXZO
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = GetDegreesPerFrame(yRotationsPerMinute); 
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = GetDegreesPerFrame(zRotationsPerMinute); 
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}

    private float GetFramePerMinute()
    {
        var fps = 1 / Time.deltaTime;
        float fpm = (float) fps * 60;
        return fpm;
    }
    

    private float GetDegreesPerMinute(float rotationsPerMinute)
    {
        return rotationsPerMinute * 360;
    }

    private float GetDegreesPerFrame(float rotationsPerMinute)
    {
        // degrees per min over frames per min
        return GetDegreesPerMinute(rotationsPerMinute) / GetFramePerMinute();
    }
}
