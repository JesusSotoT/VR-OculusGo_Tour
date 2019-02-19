using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject BackgroundEnviroment;
    public GameObject VRPlayerController;
    //Texture list
    public Material env1_material;
    public Material env2_material;

    // bool variables


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handlePressedButton(int mode){
        changeEnviroment(mode);
    }

    public void changeEnviroment(int mode){
        switch(mode){
            case 1:
                //BackgroundEnviroment.GetComponent<Renderer>().material.mainTexture = env1_texture;
                Debug.Log("Mode 1");
                break;
            case 2:
                // Set new texture for material 
                //BackgroundEnviroment.GetComponent<Renderer>().material.mainTexture = env1_texture;
                Debug.Log("Mode 2");
                break;
            default : 
                // Do something in here
                break;
        }
    }

    private void makeZoom(){
        // Get button position 
        Vector3 btnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // Get Camera object from player controller
        Camera mainCamera = VRPlayerController.GetComponent<Camera>();
        // Get first position of camera 
        Vector3 cameraFirstPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        // Math distance to camera's position to button's position
        float distance = Vector3.Distance(cameraFirstPosition, btnPosition);
        // Set speed to move
        float speed = 2f * Time.deltaTime;
        // Move camera to near object
        mainCamera.transform.position = Vector3.Lerp(cameraFirstPosition, btnPosition, speed);
    }

    /*
     * Variables for testing 
    */

    public int test_mode = 1;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(20,20, 50,50),"Press to chage")){
            // Change texture testing 
            BackgroundEnviroment.GetComponent<Renderer>().sharedMaterial = env2_material;
        }
    }

}
