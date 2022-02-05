using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PimHoreman
{
    public class GridMapCreaterMenu : MonoBehaviour
    {
        //Can be opened with 'Control + Shift + X'
        [MenuItem("PimHoreman/Level Tools/Grid Map Creator %#x")]
        public static void OpenWindowPanel()
        {
            //Launch Editor Window
            GridMapCreater.LaunchEditor();
        }
    }
}