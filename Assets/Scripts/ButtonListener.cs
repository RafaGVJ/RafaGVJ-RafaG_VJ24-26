using UnityEngine;

public class ButtonListener : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    private bool IsPlaying = false;
    public void OnButtonClicked()
    {
        
        if (IsPlaying == false)
        {
            Debug.Log("Button was clicked!");
            sound.Play();
            IsPlaying = true;
        }
        else if (IsPlaying == true)
        {
            Debug.Log("Paused");
            sound.Pause();
            IsPlaying = false;
        }

        
        
      
       
    }
    
}
