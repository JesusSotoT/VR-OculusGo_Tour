using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSetup : MonoBehaviour {
    public int PointerIndex;

    public void HandlePressedPoint(){
        var controller = gameObject.GetComponent<MainGameController>();
        controller.handlePressedButton(PointerIndex);
    }
}
