using System.Collections.Generic;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class TutorialManager : MonoSingleton<TutorialManager>
    {
        [SerializeField, Required] private List<Tutorial> tutorials;

        public void ShowTutorial(int tutorialIndex)
        {
            tutorials[tutorialIndex].Show();
        }

        public void HideTutorial(int tutorialIndex)
        {
            tutorials[tutorialIndex].Hide();
        }
    }
}