using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestordeCenas : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;

    public void MudarCena()
    {
        SceneManager.LoadScene(1);
    }
}
