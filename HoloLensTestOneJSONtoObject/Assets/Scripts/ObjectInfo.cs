using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public string student;
    public string time;
    public int step_id;
    public string transaction_id;
    public string name;
    public string catogory;
    public string value;
    //public Object history;
    //public Object skill_names;
    public string color;//added to paint objects
    public float xposition;//added to locate object
    public float yposition;//added to locate object
    public float zposition;//added to locate object
}


[System.Serializable]
public class ObjectInfoCollection
{
    //array of students
    public ObjectInfo[] detector_results;

    public static ObjectInfoCollection CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ObjectInfoCollection>(jsonString);
    }
    
}

// Given JSON input:
// {"name":"Dr Charles","lives":3,"health":0.8}
// this example will return a PlayerInfo object with
// name == "Dr Charles", lives == 3, and health == 0.8f.
