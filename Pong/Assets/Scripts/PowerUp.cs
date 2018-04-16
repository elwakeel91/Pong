using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Private / Protected variables
    protected Collider col;

    // Inspector Variables
    public float powerUpTime = 5.0f;                    // The time in seconds the power up effect lasts (default 5 seconds)
    public float rotationSpeed = 250.0f;                // The rotation speed (default 250 degrees per second)
    public float bobbleAmplitude = 0.5f;                // The Bobble Amplitude (default 0.5 units)
    public float scaleSpeed = 3.0f;                     // Power Up scaling up / down speed (default 3 units per delta Time)
    public GameObject powerUpVFX;                       // Power up VFX game object

    /// <summary>
    /// Called when the object is enabled
    /// </summary>
    protected void Start()
    {
        // Get the collider of the power up
        col = GetComponent<Collider>();
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    protected void Update()
    {
        // Rotate around the Y axis by a given rotation speed
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        // Bobble the Power Up
        Vector3 position = transform.position;
        position.y -= Mathf.Sin(Time.timeSinceLevelLoad - Time.deltaTime) * bobbleAmplitude;
        position.y += Mathf.Sin(Time.timeSinceLevelLoad) * bobbleAmplitude;
        transform.position = position;
    }

    /// <summary>
    /// Handles Collisions
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter(Collider other)
    {
        // Instantiate our VFX
//        GameObject vfx = Instantiate(powerUpVFX, transform.position, Quaternion.identity) as GameObject;
//        Destroy(vfx, 1.0f);

        // Disable the collider
        col.enabled = false;
        // Change the scale down to zero
        StartCoroutine(Shrink());
    }

    /// <summary>
    /// A coroutine to change the scale to a given target scale
    /// </summary>
    /// <param name="targetScale"></param>
    /// <returns></returns>
    private IEnumerator Shrink()
    {
        // Set the local scale of the object
        Vector3 scale = transform.localScale;

        // Loop until we reach the target scale
        while(scale != Vector3.zero)
        {
            // Lerp towards the target scale by a given scale speed
            scale = Vector3.MoveTowards(scale, Vector3.zero, Time.deltaTime * scaleSpeed);
            transform.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
    }
	
}
