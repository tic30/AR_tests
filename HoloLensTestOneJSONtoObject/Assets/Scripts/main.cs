using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    public static main instance = null;
    //private GameObject newobject;
    //private Material m1;
    private ObjectInfoCollection objectinfo;
    private Color color_temp;
    private Quaternion spawnRotation = Quaternion.identity;
    private Vector3 spawnPosition;
    private int counter=0;
    private int[] rotator;
    // Use this for initialization
    void Awake() {
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //read in json
        TextAsset targetFile = Resources.Load<TextAsset>("json1");
        objectinfo = ObjectInfoCollection.CreateFromJSON(targetFile.text);

        foreach (ObjectInfo objecti in objectinfo.objectInfoArray)
        {
            //put object into the scene
            GameObject newobject = Resources.Load<GameObject>("Prefabs/" + objecti.name);

            //color
            ColorUtility.TryParseHtmlString(objecti.color, out color_temp);
            //position
            spawnPosition = new Vector3(objecti.xposition, objecti.yposition, objecti.zposition);
            Instantiate(newobject, spawnPosition, spawnRotation);
            //add worldAncherScript
            //newobject.AddComponent<WorldAncherScript>();
            /////????????????????????????????????????????????????????????????????????????????????????????????????
            Renderer rend = newobject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.sharedMaterial.color = color_temp;
            }
            else
            {
                Debug.Log("Error: No Renderer!");
            }
        }

        //random rotate
        int i = 0;
        foreach (GameObject gob in GameObject.FindGameObjectsWithTag("Cube"))
            i++;
        rotator = new int[i];
        for (int j = 0; j < i; j++)
        {
            rotator[j] = (int)Random.Range(10.0f, 50.0f);
        }
    }

    void Update()
    {
        foreach(GameObject gob in GameObject.FindGameObjectsWithTag("Cube"))
        {
            gob.transform.Rotate(new Vector3(rotator[counter], rotator[counter], rotator[counter]) * Time.deltaTime);
            counter++;
        }
        counter = 0;
    }
}
