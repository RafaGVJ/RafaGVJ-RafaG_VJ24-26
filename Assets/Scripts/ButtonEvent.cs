using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onClicked;
    
    public void ButtonClicked()
    {
        Debug.Log("Clicked");
        onClicked.Invoke(); 
    }

    
}
