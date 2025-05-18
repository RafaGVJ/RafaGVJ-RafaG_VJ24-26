using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestordeCenas : MonoBehaviour
{
    [SerializeField] private string sceneNumber;

    public void MudarCena()
    {
        if (!string.IsNullOrEmpty(sceneNumber))
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            Debug.LogWarning("Nome da cena não definido!");
        }
    }
}
