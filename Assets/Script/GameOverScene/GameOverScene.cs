using System.Collections;
using UnityEngine;

namespace Assets.Script.GameOverScene
{
    public class GameOverScene : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMainMenu();
            }
        }

        public void ReturnToMainMenu()
        {

        }
    }
}