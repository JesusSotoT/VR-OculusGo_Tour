using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ModelController : MonoBehaviour
{
    public Animator anim;

    private ToggleGroup toggleGroup;

    private GameObject[] togglesCollect;

    private List<string> AnimListName;


    // Use this for initialization
    void Start()
    {
        toggleGroup = gameObject.GetComponent<ToggleGroup>();
        // Collect all toggle in group 
        togglesCollect = GameObject.FindGameObjectsWithTag("TogglesAnim");
        Debug.Log(togglesCollect[1].name);
        CollectAnimListName();

    }
    /*
     * This function collect all name of anim via name of toggles 
     * 
    */
    private void CollectAnimListName(){
        // Create value for array
        AnimListName = new List<string>();
        foreach (GameObject objectAnim in togglesCollect)
        {
            string AnimName = objectAnim.name;
            AnimListName.Add(AnimName);
        }
        Debug.Log(AnimListName[0]);
    }

    /*
     * Handle when press toggle
    */
    public void HandleToggles(){
        // get toggle active 
        Toggle activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();
        string ToggleName = activeToggle.name;
        // Set anim with toggle
        anim.SetBool(ToggleName, true);
        // Disable all
        for (int i = 0; i < AnimListName.Count; i++)
        {
            Debug.Log("Item: " + AnimListName[i]);
            if (AnimListName[i] != ToggleName)
            {
                anim.SetBool(AnimListName[i], false);
            }
        }
    }


}
