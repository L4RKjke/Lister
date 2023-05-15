using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YandexMaps;

public class YandexMapsAPI : MonoBehaviour
{
    public RawImage image;
    public Map.TypeMap typeMap;
    public Map.TypeMapLayer mapLayer;

    public void SetGPSlocation(float latitude, float longitude)
    {
        Map.Latitude = latitude;
        Map.Longitude = longitude;
    }

    public void LoadMap()
    {
        Map.EnabledLayer = true;
        Map.Size = 13;
        Map.SetTypeMap = typeMap;
        Map.SetTypeMapLayer = mapLayer;
        Map.Height = 900;
        Map.Width = 520;
        Map.LoadMap();
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        yield return new WaitForSeconds(1f);
        image.texture = Map.GetTexture;
    }
}