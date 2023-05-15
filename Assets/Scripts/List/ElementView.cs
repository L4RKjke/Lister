using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent (typeof(RectTransform))]
[RequireComponent(typeof(Button))]
public class ElementView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _idTMP;
    [SerializeField] private TextMeshProUGUI _dateTimeTMP;

    private PoleCard _poleCard;
    private string _lineName;
    private string _number;
    private string _status;
    private string _dateTime;
    private string _executorId;

    public string Longitude { get; private set; }

    public string Latitude { get; private set; }

    private Button _button;

    public string Id { get; set; }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => ShowAllDesctription());
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => ShowAllDesctription());
    }

    public void Init(PoleCard poleCard)
    {
        _poleCard = poleCard;
    }

    public void SetAllDesctription(string number, string LineName, string status, string dataTime, string executorName, string latitude, string longitude)
    {
        _lineName = number;
        _number = LineName;
        _status = status;
        _dateTime = dataTime;
        _executorId = executorName;
        _idTMP.text = _lineName;
        _dateTimeTMP.text = _dateTime;
        Longitude = longitude;
        Latitude = latitude;
    }

    private void ShowAllDesctription()
    {
        _poleCard.gameObject.SetActive(true);
        _poleCard.SetView(_lineName, _number, _status, _dateTime, _executorId, Longitude, Latitude);
    }
}
