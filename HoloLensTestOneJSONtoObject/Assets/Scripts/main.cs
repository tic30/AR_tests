using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class main : MonoBehaviour {
    public static main instance = null;
    private ObjectInfoCollection objectinfo;
    private Color color_temp;
    private Vector3 spawnPosition;
    private int[] rotator;
    private WWW my3w;
    public string url = "http://numbersapi.com/random/date?json";
    public float timeLeft = 5.0f;
    //Random rotation variables:
    //private Quaternion spawnRotation = Quaternion.identity;
    //private int counter=0;

    // Use this for initialization
    void Awake() {
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    void Start()
    {
        //test WWW coroutine
        //loadJSON();//not working

        //read in local json file
        TextAsset targetFile = Resources.Load<TextAsset>("json_test");
        objectinfo = ObjectInfoCollection.CreateFromJSON(targetFile.text);

        foreach (ObjectInfo objecti in objectinfo.detector_results)
        {
            string prefabName = "";
            //find object by name
            switch (objecti.name)
            {
                case "help_model":
                    if (objecti.value == "preferred")
                    {
                        //more code here
                    }
                    else if (objecti.value == "acceptable")
                    {
                        prefabName = "Sphere_green";
                        objecti.color = "#2FF728";
                    }
                    else if (objecti.value == "not acceptable")
                    {
                        prefabName = "Sphere_red";
                        objecti.color = "#f44242";
                    }
                    break;
                case "current_attempt_count":
                    //more code here
                    break;
                case "help_model_try_if_low":
                    prefabName = "Hand";
                    break;
                default:
                    Debug.Log("Invalid name or not yet implemented. See main.cs -> start() function");
                    break;
            }

            //put object into the scene
            GameObject newobject = Resources.Load<GameObject>("Prefabs/" + prefabName);

            //parse color and paint
            ColorUtility.TryParseHtmlString(objecti.color, out color_temp);
            //position object and instantiate
            if (objecti.xposition == 0.0)
            {
                continue;//if not positioned, do not instantiate
            }
            spawnPosition = new Vector3(objecti.xposition, objecti.yposition, objecti.zposition);
            Instantiate(newobject, spawnPosition, newobject.transform.rotation);
            //add worldAncherScript
            //newobject.AddComponent<WorldAncherScript>();
            newobject.AddComponent<WorldAnchor>();
            //WorldAnchorStore failed
            Renderer rend = newobject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.sharedMaterial.color = color_temp;
            }
            else
            {
                Debug.Log("Error: No Renderer for object: " + prefabName);
            }
        }

        //random rotate
        /*
        int i = 0;
        foreach (GameObject gob in GameObject.FindGameObjectsWithTag("Cube"))
            i++;
        rotator = new int[i];
        for (int j = 0; j < i; j++)
        {
            rotator[j] = (int)Random.Range(10.0f, 50.0f);
        }
        */
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0f)
        {
            Debug.Log("Reloading JSON every 5 seconds...");
            loadJSON();
            timeLeft = 5.0f;
        }

        //rotate
        /*
        foreach (GameObject gob in GameObject.FindGameObjectsWithTag("Cube"))
        {
            gob.transform.Rotate(new Vector3(rotator[counter], rotator[counter], rotator[counter]) * Time.deltaTime);
            counter++;
        }
        counter = 0;
        */
    }

    //Coroutine tests
    IEnumerator loadJSON()
    {
        yield return StartCoroutine("reloadPage");
        Debug.Log("Done.");
    }
    IEnumerator reloadPage() {
        my3w = new WWW(url);
        yield return my3w;
        Debug.Log("Done. " + my3w.text);
    }
}
