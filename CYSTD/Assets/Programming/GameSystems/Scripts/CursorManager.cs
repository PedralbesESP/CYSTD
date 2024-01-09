using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorManager
{
    public static void Hide()
    {
        Cursor.visible = false;
    }

    public static void Show()
    {
        Cursor.visible = true;
    }
}
