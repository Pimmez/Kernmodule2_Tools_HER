using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace PimHoreman
{
    public class GridMapCreater : EditorWindow
    {
        #region Variables
        //Editor Background Grid Values
        private Vector2 offset;
        private Vector2 drag;

        //Node Values
        private GUIStyle gridEditorField;
        private Vector2 nodePosition;
        private List<List<Node>> nodes;
        private bool isErasing;

        //Menubar values
        Rect menuBar;
        private GUIStyle currentStyle;

        //StyleManager.Instance StyleManager.Instance;
        #endregion

        #region Build-In Methods
        public static void LaunchEditor()
        {
            EditorWindow _editorWin = GetWindow<GridMapCreater>("Grid Map Creator");

            //_editorWin.minSize = new Vector2(250, 250);
            //For older unity versions
            _editorWin.Show();
        }

        private void OnEnable()
        {
            SetUpStyleManager();

            gridEditorField = new GUIStyle();
            Texture2D icon = StyleManager.Instance.buttonStyles[0].Icon;
            StyleManager.Instance.gridBackgroundImage = icon.name;
            //Texture2D icon = Resources.Load("IconTextures/emptyfield") as Texture2D;
            //Texture2D icon = gridTexture;

            gridEditorField.normal.background = icon;
            SetUpNodes();

            //Set Default menu bar item
            currentStyle = StyleManager.Instance.buttonStyles[0].NodeStyle;
        }

        private void OnGUI()
        {
            DrawGrid();
            DrawNodes();
            DrawMenuBar();
            ProcessNodes(Event.current);
            ProcessGrid(Event.current);

            Repaint();
        }
        #endregion

        #region Custom Methods
        //Drawing Grid Inside Editor Window
        private void DrawGrid()
        {
            int widthDivider = Mathf.CeilToInt(position.width / 20);
            int heightDivider = Mathf.CeilToInt(position.height / 20);
            Handles.BeginGUI();

            Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);

            //Press and drag to move the grid
            offset += drag;

            Vector3 newOffset = new Vector3(offset.x % 20, offset.y % 20, 0);
            for (int i = 0; i < widthDivider; i++)
            {
                Handles.DrawLine(new Vector3(20 * i, -20, 0) + newOffset, new Vector3(20 * i, position.height, 0) + newOffset);
            }
            for (int i = 0; i < heightDivider; i++)
            {
                Handles.DrawLine(new Vector3(-20, 20 * i, 0) + newOffset, new Vector3(position.width, 20 * i, 0) + newOffset);
            }

            Handles.color = Color.white;

            Handles.EndGUI();
        }

        private void ProcessGrid(Event _e)
        {
            drag = Vector2.zero;
            switch (_e.type)
            {
                case EventType.MouseDrag:
                    if (_e.button == 0)
                    {
                        OnMouseDrag(_e.delta);
                    }
                    break;
            }
        }

        private void OnMouseDrag(Vector2 _delta)
        {
            drag = _delta;

            //This way the emptyfield grid drags with the editor grid
            for (int i = 0; i < 20; i++)
            {
                for (int x = 0; x < 10; x++)
                {

                    nodes[i][x].DragNode(_delta);
                }
            }

            GUI.changed = true;
        }

        //Setting up node position with texture
        private void SetUpNodes()
        {
            nodes = new List<List<Node>>();
            for (int i = 0; i < 20; i++)
            {
                nodes.Add(new List<Node>());
                for (int x = 0; x < 10; x++)
                {
                    nodePosition.Set(i * 30, x * 30);
                    nodes[i].Add(new Node(nodePosition, 30, 30, gridEditorField));
                }
            }
        }

        //Draw Nodes in the editor field
        private void DrawNodes()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    nodes[i][x].DrawNode();
                }
            }
        }

        //Sets Up StyleManager
        private void SetUpStyleManager()
        {
            try
            {
                //StyleManager.Instance = GameObject.FindGameObjectWithTag("StyleManager.Instance").GetComponent<StyleManager.Instance>();
                for (int i = 0; i < StyleManager.Instance.buttonStyles.Length; i++)
                {
                    StyleManager.Instance.buttonStyles[i].NodeStyle = new GUIStyle();
                    StyleManager.Instance.buttonStyles[i].NodeStyle.normal.background = StyleManager.Instance.buttonStyles[i].Icon;
                }
            }
            catch (Exception _e) { }
        }

        //On Mousedown and drag place and erase icons
        private void ProcessNodes(Event _e)
        {
            int _row = (int)((_e.mousePosition.x - offset.x) / 30);
            int _col = (int)((_e.mousePosition.y - offset.y) / 30);

            if ((_e.mousePosition.x - offset.x) < 0 || (_e.mousePosition.x - offset.x) > 600 || (_e.mousePosition.y - offset.y) < 0 || (_e.mousePosition.y - offset.y) > 300)
            { }
            else
            {
                if (_e.type == EventType.MouseDown)
                {
                    if (nodes[_row][_col].style.normal.background.name == StyleManager.Instance.gridBackgroundImage)
                    {
                        isErasing = false;

                    }
                    else
                    {
                        isErasing = true;
                    }
                    PaintNodes(_row, _col);
                }

                if (_e.type == EventType.MouseDrag)
                {
                    PaintNodes(_row, _col);
                    _e.Use();
                }
            }
        }

        private void PaintNodes(int _row, int _col)
        {
            if(isErasing)
            {
                nodes[_row][_col].SetStyle(gridEditorField);
                GUI.changed = true;

            }
            else
            {
                nodes[_row][_col].SetStyle(currentStyle);
                GUI.changed = true;
            }

        }

        //Draw menu items
        private void DrawMenuBar()
        {
            menuBar = new Rect(0, 0, position.width, 20);
            GUILayout.BeginArea(menuBar, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();
            for (int i = 0; i < StyleManager.Instance.buttonStyles.Length; i++)
            {
                if (GUILayout.Toggle((currentStyle == StyleManager.Instance.buttonStyles[i].NodeStyle), new GUIContent(StyleManager.Instance.buttonStyles[i].ButtonTexture), EditorStyles.toolbarButton, GUILayout.Width(80)))
                {
                    currentStyle = StyleManager.Instance.buttonStyles[i].NodeStyle;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        #endregion
    }
}