using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data of the Nodes
public class Node : MonoBehaviour
{
    public Rect rectangle;
    public GUIStyle style;
    public Node(Vector2 _position, float _width, float _height, GUIStyle _defaultStyle)
    {
        rectangle = new Rect(_position.x, _position.y, _width, _height);
        style = _defaultStyle;
    }

    public void DrawNode()
    {
        GUI.Box(rectangle, "", style);
    }

    public void SetStyle(GUIStyle _NodeStyle)
    {
        style = _NodeStyle;
    }

    public void DragNode(Vector2 _delta)
    {
        rectangle.position += _delta;
    }
}