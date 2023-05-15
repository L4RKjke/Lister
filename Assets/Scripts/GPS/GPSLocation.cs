using UnityEngine;
using System.Collections;

public class GPSLocation : MonoBehaviour
{
    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    private IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        Debug.Log("Latitude: " + Input.location.lastData.latitude + ", Longitude: " + Input.location.lastData.longitude);
        Latitude = Input.location.lastData.latitude;
        Longitude = Input.location.lastData.longitude;
        Input.location.Stop();
    }
}