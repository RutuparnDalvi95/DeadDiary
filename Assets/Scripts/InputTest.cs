using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour {
   
    // look in the inspector to see the values update
    public float pointerPressure = 0;
    public bool pointerPressed;
 
    public float penPressure = 0;
 
    public bool penTipPressed;
    public bool penIsHovering;
 
    public Vector2 mousePos;
 
    void Update() {
        // "Pen", "Pointer" and "Mouse" must be added to "Supported Devices" in "Project Settings"->"Input System Package"
        // for being available for use
        Pointer pointer = Pointer.current;
        Pen pen = Pen.current;
        Mouse mouse = Mouse.current;
        //
        // only returns a value >0 until pointer is moved after pressing
        pointerPressure = pointer.pressure.ReadValue();
     
        // works
        pointerPressed = pointer.press.isPressed;
 
        // works, iff:
        // - Windows Ink is on in Wacom Tablet Properties
        //   note: all Unity instances must be restarted after toggling this setting
        penPressure = pen.pressure.ReadValue();
        penTipPressed = pen.tip.isPressed;
        penIsHovering = pen.inRange.isPressed;
     
        // works (if Mouse is added to Suported Devices)
        if (mouse != null) {
            mousePos = mouse.position.ReadValue();
         
        }
    }
}
