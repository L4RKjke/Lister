using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;

public class Data : MonoBehaviour
{
    [SerializeField] private GPSLocation _location;
    [SerializeField] private UserLogger _userLogger;

    private DatabaseReference _database;
    private string _userID;
    private List<Pole> _poles = new List<Pole>();
    public Action<Pole> ElementLoaded;

    public int ListCount => _poles.Count;

    private void Start()
    {
        _userID = SystemInfo.deviceUniqueIdentifier;
        _database = FirebaseDatabase.DefaultInstance.RootReference;
        LoadElements();
    }

    public Pole GetElement(int id)
    {
        return _poles[id];
    }

    public void CreateUser(string name, string password)
    {
        Role role = Role.User;

        if (password == "admin1")
            role = Role.Admin;       

        User newUser = new User(name, password, role);
        string json = JsonUtility.ToJson(newUser);

        _database.Child("Users").Child(_userID).SetRawJsonValueAsync(json);
    }

    private void GetUser()
    {

    }

    private IEnumerator GetUserNameRoutine(Action<string> callback)
    {
        string id = _userID;

        var userData = _database.Child("Users").Child(id).Child("Name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if (userData != null)
        {
            DataSnapshot snapshot = userData.Result;
            callback?.Invoke(snapshot.Value.ToString());
        }
    }

    public void CreateElement(string lineName, string number, string status)
    {
        LineInfo newPole = new LineInfo(lineName, number, status, _userLogger.UserName, _location.Latitude.ToString(), _location.Longitude.ToString());
        string json = JsonUtility.ToJson(newPole);

        _database.Child("PoleList").Child(lineName).SetRawJsonValueAsync(json);
    }

    public void LoadElements()
    {
        StartCoroutine(GetAllElementsData());
    }

    private IEnumerator GetData(string targetString,string desription , Action<string> lineNumber = null)
    {
        var elementData = _database.Child("PoleList").Child(targetString).Child(desription).GetValueAsync();

        yield return new WaitUntil(predicate: () => elementData.IsCompleted);

        if (elementData != null)
        {
            DataSnapshot snapshot = elementData.Result;
            lineNumber?.Invoke(snapshot.Value.ToString());
        }
    }

    private IEnumerator GetAllElementsData()
    {
        var elementData = _database.Child("PoleList").GetValueAsync();

        yield return new WaitUntil(predicate: () => elementData.IsCompleted);

        if (elementData != null)
        {
            DataSnapshot snapshot = elementData.Result;

            foreach (var item in snapshot.Children)
            {
                if (item == null) continue;

                Pole newPole = new Pole(
                    item.Child("LineName").Value.ToString(),
                    item.Child("Number").Value.ToString(),
                    item.Child("Status").Value.ToString(),
                    item.Child("Latitude").Value.ToString(),
                    item.Child("Longitude").Value.ToString(),
                    item.Child("ExecutorId").Value.ToString()
                    ); ;

                _poles.Add(newPole);
                ElementLoaded?.Invoke(newPole);
            }
        }
    }
}

public class Pole
{
    public string PillarName { get; private set; }

    public string LineName { get; private set; }

    public string Status { get; private set; }

    public string ExecutorName { get; private set; }

    public string DataTime { get; private set; }

    public string Latitude { get; private set; }

    public string Longitude { get; private set; }

    public Pole(string pillarName, string lineName, string status, string latitude, string lingitude, string executorName = null)
    {
        ExecutorName = executorName;
        PillarName = pillarName;
        LineName = lineName;
        DataTime = DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
        Status = status;
        Latitude = latitude;
        Longitude = lingitude;
    }
}

public class User
{
    public string Name;
    public string Password; 
    public string Role;

    public User(string name, string password, Role role)
    {
        Name = name;
        Password = password;
        Role = role.ToString();
    }
}

public enum Role
{
    Admin,
    User
}