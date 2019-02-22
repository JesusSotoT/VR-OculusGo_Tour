using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainGameController : MonoBehaviour
{
    public GameObject BackgroundEnviroment;
    public GameObject VRPlayerController;
    //Texture list
    //public Material[] env_material_list;

    public Camera mainCamera;

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    private GameObject[] endMarkerList;

    // Movement speed in units/sec.
    public float speed;

    // Time when the movement started.
    private float startTime;
    private float fracJourney;

    // Total distance between the markers.
    private float journeyLength = 1;

    private int indexItemInEndList;

    private Vector3 cameraFirstPosition;

    private bool isFacingTime = false;
    private bool isCanRun = false;
    private bool isMoving = false;
    float startTimeRot = 0;

    private string sceneNameWillBeLoad;
    // Use this for initialization
    void Start()
    {
        cameraFirstPosition = startMarker.position;

        // collect object with tag 
        endMarkerList = GameObject.FindGameObjectsWithTag("EndMarker");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate camera face for opposite
        if(isFacingTime){
            float distCoveredRot = (Time.time - startTimeRot) * speed;
            Debug.Log("First function ");
            // Set vector for target rotation 
            var targetRotation = Quaternion.LookRotation(targetObject.transform.position - mainCamera.gameObject.transform.position);
            Debug.Log("targetRotation " + targetRotation +"\n Time: "+Time.deltaTime);
            // Rotate camera to opposite with endMarker(target Marker)
            mainCamera.gameObject.transform.rotation = Quaternion.Slerp(mainCamera.gameObject.transform.rotation, targetRotation, distCoveredRot);
            Debug.Log("transform: "+ mainCamera.gameObject.transform.rotation);

        }

        if (isCanRun)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            fracJourney = distCovered / journeyLength;

            // Set position with custom Z
            Vector3 endMarkerPoint = new Vector3(targetObject.gameObject.transform.position.x, targetObject.gameObject.transform.position.y, targetObject.gameObject.transform.position.z);

            // Set our position as a fraction of the distance between the markers.
            VRPlayerController.gameObject.transform.position = Vector3.Lerp(cameraFirstPosition, endMarkerPoint, fracJourney);

        }

        if(fracJourney >= 0.8)
        {
            handleAfterZoom();
            isCanRun = false;
            fracJourney = 0.0f;
        }

    }

    private string nameFormat = "Pointer";
    private GameObject targetObject;
    public void  handlePressedButton(int mode, string sceneName)
    {
        Debug.Log("Mode: " + mode + "\n Scene Name: " + sceneName);

        /*
         * Find GameObject with Name to get it to handle
        */

       

        for (int i = 0; i < endMarkerList.Length; i++ ){
            string nameObject = endMarkerList[i].name;
            if (nameObject.Equals(nameFormat + mode)) {
                targetObject = endMarkerList[i];
            }
            // Skip if not equals target's name
        }


        if (!isMoving){
            sceneNameWillBeLoad = sceneName;
            lookAtWithEndMarker();
            // Move Camera to Postion 
            makeZoom(mode);
        }
    }

    void handleAfterZoom()
    {
        // Change  to target scene
        SceneManager.LoadScene(sceneNameWillBeLoad, LoadSceneMode.Single);
        // Reset object
        previousFirstCameraPosition();

    }

    private void makeZoom(int index)
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        indexItemInEndList = index - 1;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, targetObject.gameObject.transform.position);
        isCanRun = true;

        isMoving = true;

    }

    private void  lookAtWithEndMarker(){
        isFacingTime = true;
        startTimeRot = Time.time;
    }

    private void previousFirstCameraPosition(){
        isFacingTime = false;
        // Reset camera's rotate 
        mainCamera.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        // Reset controller's position
        VRPlayerController.gameObject.transform.position = Vector3.zero;
        // Object stay => Can make it move
        isMoving = false;
    }

#if UNITY_EDITOR
    /*
     * Variables for testing 
    */

    public int test_mode;
    public string test_sceneName;

    private void  OnGUI()
    {
        //if (GUI.Button(new Rect(20, 20, 50, 50), "Press to chage"))
        //{
        //    // Change texture testing 
        //    //BackgroundEnviroment.GetComponent<Renderer>().sharedMaterial = env_material_list[test_mode-1];
        //}

        if (GUI.Button(new Rect(20, 70, 100, 50), "Move Camera"))
        {
            handlePressedButton(test_mode, test_sceneName);
        }

        if (GUI.Button(new Rect(20, 120, 100, 50), "Facing Camera"))
        {
            handlePressedButton(test_mode, test_sceneName); 
        }

        if (GUI.Button(new Rect(20, 190, 100, 50), "Load scene 1"))
        {
            SceneManager.LoadScene(test_sceneName, LoadSceneMode.Single);
        }

    }

#endif
}
