using UnityEditor;

namespace PimHoreman.Tools
{
    public class ObjectReplacerMenuItems
    {
        //Can be opened with 'Control + Shift + M'
        [MenuItem("PimHoreman/Level Tools/Object Changer %#m")]
        public static void OpenWindowPanel()
        {
            //Launch Editor Window
            ObjectReplacer.LaunchEditor();
        }
    }
}