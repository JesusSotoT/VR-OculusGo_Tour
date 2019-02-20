using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject BackgroundEnviroment;
    public GameObject VRPlayerController;
    //Texture list
    public Material[] env_material_list;


    public GameObject canvas1;
    public GameObject canvas2;

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform[] endMarkerList;

    // Movement speed in units/sec.
    public float speed;

    // Time when the movement started.
    private float startTime;
    private float fracJourney;
    // Total distance between the markers.
    private float journeyLength = 1;

    private bool isCanRun = false;

    private int indexItemInEndList;

    private Vector3 cameraFirstPosition;

    private int playerMode;

    // Use this for initialization
    void Start()
    {
        cameraFirstPosition = startMarker.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanRun)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            fracJourney = distCovered / journeyLength;

            // Set position with custom Z
            Vector3 endMarkerPoint = new Vector3(endMarkerList[indexItemInEndList].position.x, endMarkerList[indexItemInEndList].position.y, endMarkerList[indexItemInEndList].position.z - 2f);

            // Set our position as a fraction of the distance between the markers.
            startMarker.transform.position = Vector3.Lerp(startMarker.position, endMarkerPoint, fracJourney);
        }

 

    }

    public void  handlePressedButton(int mode)
    {
        playerMode = mode;
        // Move Camera to Postion 
        makeZoom(mode);
        StartCoroutine(handleAfterZoom());  
    }


    IEnumerator handleAfterZoom()
    {
        // Delay to wait zoom process finish
        yield return new WaitForSeconds(0.5f);
        // Loading screen fade
        // Adding Fade script
        var addingGameObject = gameObject.AddComponent<OVRScreenFade>();
        // Change Sphere Texture
        BackgroundEnviroment.GetComponent<Renderer>().sharedMaterial = env_material_list[playerMode - 1];
        isCanRun = false;
        previousFirstCameraPosition();
        yield return new WaitForSeconds(addingGameObject.fadeTime);
        // Remove Component
        Destroy(addingGameObject);

    }

    private void makeZoom(int index)
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarkerList[index-1].position);
        indexItemInEndList = index - 1;
        isCanRun = true;

    }

    private void previousFirstCameraPosition(){
        startMarker.transform.position = Vector3.zero;
    }

#if UNITY_EDITOR
    /*
     * Variables for testing 
    */

    public int test_mode = 1;

    private void  OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 50, 50), "Press to chage"))
        {
            // Change texture testing 
            BackgroundEnviroment.GetComponent<Renderer>().sharedMaterial = env_material_list[test_mode-1];
        }

        if (GUI.Button(new Rect(20, 70, 100, 50), "Move Camera"))
        {
            handlePressedButton(test_mode);
        }
    }

#endif
}
