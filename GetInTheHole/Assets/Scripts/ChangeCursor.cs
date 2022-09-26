using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer modifiedCursor;

    private Camera mainCamera;
    private Vector3 modifiedCursorPosition;

    public static ChangeCursor instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    public void DisplayCursor()
    {
        modifiedCursor.transform.localScale = Vector3.one * 0.35f;

        modifiedCursorPosition = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
            2, mainCamera.ScreenToWorldPoint(Input.mousePosition).z);

        modifiedCursor.transform.position = modifiedCursorPosition;
    }

    public void HideCursor()
    {
        modifiedCursor.transform.localScale = Vector3.zero;
    }
}
