using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public string name;
    public string color;
    public int xposition;
    public int yposition;
    public int zposition;
}


[System.Serializable]
public class ObjectInfoCollection
{
    public ObjectInfo[] objectInfoArray;

    public static ObjectInfoCollection CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ObjectInfoCollection>(jsonString);
    }
    
}

// Given JSON input:
// {"name":"Dr Charles","lives":3,"health":0.8}
// this example will return a PlayerInfo object with
// name == "Dr Charles", lives == 3, and health == 0.8f.
