using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserLogger : MonoBehaviour
{
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] Button _saveButton;
    [SerializeField] Data _data;
    [SerializeField] private GameObject _userPanel;
    [SerializeField] private GameObject _adminPanel;

    public string UserName => _name.text;

    private void Start()
    {
        _saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(OnSaveButtonClick);
    }

    public void OnSaveButtonClick()
    {
        Regex regex = new Regex(@"^[À-ß¨][à-ÿ¸]+ [À-ß¨][à-ÿ¸]+ [À-ß¨][à-ÿ¸]+$");
        Match match = regex.Match(_name.text);

        if (match.Success)
        {
            _data.CreateUser(_name.text, _password.text);

            if (_password.text == "admin1")
            {
                _adminPanel.SetActive(true);
                gameObject.SetActive(false);
            }

            else
            {
                _userPanel.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        else
        {
            _name.image.color = Color.grey;
        }
    }
}