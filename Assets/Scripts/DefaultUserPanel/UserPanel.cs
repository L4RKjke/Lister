using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _number;
    [SerializeField] private TMP_InputField _lineNumber;
    [SerializeField] private TMP_InputField _status;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Data _data;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private UserLogger _userLogger;

    private void Start()
    {
        _saveButton.onClick.AddListener(OnButtonClick);
    }

    private void OnEnable()
    {
        _name.text = _userLogger.UserName;
    }

    private void OnDisable()
    {
        _saveButton?.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _data.CreateElement(_number.text, _lineNumber.text, _status.text);
    }
}

public class LineInfo
{
    public string LineName;
    public string Number;
    public string Status;
    public string ExecutorId;
    public string Latitude;
    public string Longitude;

    public LineInfo(string lineName, string number, string status, string executorId, string latitude, string lingitude)
    {
        LineName = lineName;
        Number = number;
        Status = status;
        ExecutorId = executorId;
        Latitude = latitude;
        Longitude = lingitude;
    }
}