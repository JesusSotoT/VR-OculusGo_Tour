using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainGameController : MonoBehaviour
{
    public GameObject BackgroundEnviroment;
    public GameObject VRPlayerController;
    public Camera mainCamera;
    // Transforms to act as start and end markers for the journey.
    private GameObject startMarker;
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
        startMarker = GameObject.FindGameObjectWithTag("StartMarker");
        cameraFirstPosition = startMarker.gameObject.transform.position;
        // Auto setup Marker with Tag
        endMarkerList = GameObject.FindGameObjectsWithTag("EndMarker");
        Debug.Log("List: " + endMarkerList[0]);
    }
    void Update()
    {
        // Rotate camera face for opposite
        if(isFacingTime){
            float distCoveredRot = (Time.time - startTimeRot) * speed;
            // Set vector for target rotation 
            var targetRotation = Quaternion.LookRotation(targetObject.transform.position - mainCamera.gameObject.transform.position);
            // Rotate camera to opposite with endMarker(target Marker)
            mainCamera.gameObject.transform.rotation = Quaternion.Slerp(mainCamera.gameObject.transform.rotation, targetRotation, distCoveredRot);
        }

        //Move camera to end marker
        if (isCanRun)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed = current distance divided by total distance.
            fracJourney = distCovered / journeyLength;
            // Set position with custom Z
            Vector3 targetPosition = targetObject.gameObject.transform.position;
            Vector3 endMarkerPoint = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            // Set our position as a fraction of the distance between the markers.
            VRPlayerController.gameObject.transform.position = Vector3.Lerp(cameraFirstPosition, endMarkerPoint, fracJourney);
        }

        if(fracJourney >= 0.8)
        {
            StartCoroutine(handleAfterZoom());
            isCanRun = false;
            fracJourney = 0.0f;
        }

    }

    private string nameFormat = "Pointer";
    private GameObject targetObject;
    public void  handlePressedButton(int mode, string sceneName)
    {
        /*
         * Find GameObject with Name to get it to handle
        */
        for (int i = 0; i < endMarkerList.Length; i++ ){
            string nameObject = endMarkerList[i].name;
            if (nameObject.Equals(nameFormat + mode)) {
                targetObject = endMarkerList[i];
            }
        }

        if (!isMoving){
            sceneNameWillBeLoad = sceneName;
            lookAtWithEndMarker();
            // Move Camera to Postion 
            makeZoom(mode);
        }
    }

    IEnumerator handleAfterZoom()
    {
        // Change  to target scene
        SceneManager.LoadScene(sceneNameWillBeLoad, LoadSceneMode.Single);
        // Reset object
        yield return new WaitForSeconds(0.1f);
        previousFirstCameraPosition();
    }

    private void makeZoom(int index)
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        indexItemInEndList = index - 1;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.transform.position, targetObject.gameObject.transform.position);
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
    public bool TestMode;

    public int ObjectTarget;
    public string SceneName;

    private void  OnGUI()
    {
        if(TestMode){
            if (GUI.Button(new Rect(20, 70, 100, 50), "Move Camera"))
            {
                handlePressedButton(ObjectTarget, SceneName);
            }

            if (GUI.Button(new Rect(20, 120, 100, 50), "Facing Camera"))
            {
                handlePressedButton(ObjectTarget, SceneName);
            }

            if (GUI.Button(new Rect(20, 190, 100, 50), "Load scene 1"))
            {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
            }
        }
       

    }

#endif
}
