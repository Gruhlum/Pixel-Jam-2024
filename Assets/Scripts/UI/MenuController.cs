using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexTecGames
{
	public class MenuController : MonoBehaviour
	{
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartGame();
            }
        }

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