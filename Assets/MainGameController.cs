using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameObject BackgroundEnviroment;
    public GameObject VRPlayerController;


    //Texture list
    public Texture env1_texture;
    public Texture env2_texture;

    // bool variables

    /*
     * Default mode is 1
    */
    private int mode = 1;
     
    /*
     * It is condition for check up to mode
    */
    private int maximum_mode = 2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handlePressedButton(){
        changeEnviroment();
    }


    public void changeEnviroment(){

        // Check mode maximum
        if(mode != maximum_mode){
            mode += 1;
        }else{
            // Reset mode 
            mode = 1;
        }

        // Get Component render from GameObject
        Renderer m_render = BackgroundEnviroment.GetComponent<Renderer>();
        // Get material from render
        switch(mode){
            case 1:
                m_render.material.SetTexture("_MODE1", env1_texture);
                break;
            case 2:
                m_render.material.SetTexture("_MODE2", env2_texture);
                break;
            default : 
                // Do something in here
                break;
        }
    }

}
