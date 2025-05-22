using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
       
        private void OnMouseDown() // quando clica no portal
        {
            int resultado = Random.Range(0, 3); // 0, 1 ou 2

            if (resultado == 0)
            {
                Debug.Log("Reiniciar o jogo");
                SceneManager.LoadScene(1);
            }
            else if (resultado == 1)
            {
                Debug.Log("Fechar o jogo");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
            else
            {
               Debug.Log("Vitória!");
               UIManager.instance.OnWin();
            }
        }
    }

