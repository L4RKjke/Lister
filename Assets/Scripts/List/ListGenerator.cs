using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ListGenerator : MonoBehaviour
{
    [SerializeField] private ElementView _elementView;
    [SerializeField] private GameObject _content;
    [SerializeField] private Data _data;
    [SerializeField] private PoleCard _card;

    private List<ElementView> _itemList = new List<ElementView>();

    private void Start()
    {
        _data.ElementLoaded += AddNewItem;
    }

    private void OnDisable()
    {
        _data.ElementLoaded -= AddNewItem;
    }

    public void Reload()
    {
        RemoveElements();
        _data.LoadElements();
    }

    public void AddNewItem(Pole pole)
    {
        var newItem = Instantiate(_elementView);

        _itemList.Add(newItem);
        newItem.Init(_card);
        newItem.SetAllDesctription(pole.PillarName, pole.LineName, pole.Status, pole.DataTime, pole.ExecutorName, pole.Latitude, pole.Longitude);
        newItem.transform.SetParent(_content.transform);
        newItem.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    private void RemoveElements()
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            if (_itemList[i].gameObject != null)
                Destroy(_itemList[i].gameObject);
        }

        _itemList.Clear();
    }
}

public class ListItem
{
    public int Id { get; set; }

    public string DateTime { get; private set; }

    public ListItem (int id)
    {
        Id = Mathf.Clamp(id, 0, 1);
    }

    public void UpdateInfo(string dateTime = "00/00/00 00:00")
    {
        DateTime = dateTime;
    }

    public override string ToString()
    {
        return (Id.ToString() + ", " +  DateTime.ToString());
    }
}