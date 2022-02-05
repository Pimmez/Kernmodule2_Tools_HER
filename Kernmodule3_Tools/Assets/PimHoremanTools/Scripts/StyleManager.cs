using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions.Generics.Singleton;

public class StyleManager : GenericSingleton<StyleManager, StyleManager>
{
    public ButtonStyle[] buttonStyles;
    
    [HideInInspector]
    public string gridBackgroundImage;
}
[System.Serializable]
public struct ButtonStyle
{
    public Texture2D Icon;
    public string ButtonTexture;

    [HideInInspector]
    public GUIStyle NodeStyle;
}