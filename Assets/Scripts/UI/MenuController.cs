using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexTecGames
{
	public class MenuController : MonoBehaviour
	{
        public void StartGame()
        {
            SceneManager.LoadScene("MainScene");
        }
        public void QuitGame()
        {
#if (UNITY_EDITOR)
            if (Application.isEditor)
            {
                EditorApplication.ExitPlaymode();
                return;
            }
#endif
            Application.Quit();
        }
    }
}