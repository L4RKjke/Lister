using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PoleCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private TextMeshProUGUI _tmp2;
    [SerializeField] private TextMeshProUGUI _tmp3;
    [SerializeField] private TextMeshProUGUI _tmp4;
    [SerializeField] private TextMeshProUGUI _tmp5;
    [SerializeField] private TextMeshProUGUI _graph;
    [SerializeField] private Button _map;
    [SerializeField] private Button _graf;
    [SerializeField] private Button _closeMap;
    [SerializeField] private Button _closeGraf;
    [SerializeField] private GameObject _mapPanel;
    [SerializeField] private GameObject _graphPanel;
    [SerializeField] private YandexMapsAPI _yandexMaps;

    public string Longitude { get; private set; }

    public string Latitude { get; private set; }

    private void OnEnable()
    {
        _map.onClick.AddListener(OnMapClick);
        _closeMap.onClick.AddListener(OnCloseMapClick);
        _graf.onClick.AddListener(OnGraphButtonClick);
        _closeGraf.onClick.AddListener(OnCloseGraphButtonClick);
    }

    private void OnDisable()
    {
        _map.onClick.RemoveListener(OnMapClick);
        _closeMap.onClick.RemoveListener(OnCloseMapClick);
        _graf.onClick.RemoveListener(OnGraphButtonClick);
        _closeGraf.onClick.RemoveListener(OnCloseGraphButtonClick);
    }

    private void OnMapClick()
    {
        float.TryParse(Latitude, out float latitude);
        float.TryParse(Longitude, out float longitude);

        _mapPanel.SetActive(true);
        _yandexMaps.SetGPSlocation(latitude, longitude);
        _yandexMaps.LoadMap();
    }

    public void OnCloseMapClick()
    {
        _mapPanel.SetActive(false);
    }

    public void OnCloseGraphButtonClick()
    {
        _graphPanel.SetActive(false);
    }

    public void OnGraphButtonClick()
    {
        _graphPanel.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetView(string id, string name, string status, string DataTime, string nameID, string longitude, string latitude)
    {
        _tmp.text = id;
        _tmp2.text = name;
        _tmp3.text = status;
        _tmp4.text = DataTime;
        _tmp5.text = nameID;
        _graph.text = status;
        Longitude = longitude;
        Latitude = latitude;

        if (float.TryParse(status, out float value))
        {
            if (value > 30)
                _tmp3.color = Color.green;
            else 
                _tmp3.color = Color.red;
        }
    }
}
