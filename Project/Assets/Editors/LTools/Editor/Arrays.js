///2011.12月 1号 下午 12:41	距离可以通过Distance设置

class Arrays extends EditorWindow
{
	var pos = 0;
	var ArrayRect = Rect (10,10,180,320);
	var distance : float;
	
	function OnGUI ()
    {
        BeginWindows ();

        ArrayRect =GUILayout.Window (0, ArrayRect, ArrayWindow, "Plug-ins");

        EndWindows ();
    }
	
	function ArrayWindow()
	{
		distance = EditorGUILayout.FloatField("Distance:", distance);

		if(GUILayout.Button ("list_x"))
		{
			pos = 0;

			for(x=0; x <= Selection.gameObjects.Length-1; x++)
			{
				Selection.gameObjects[x].transform.position.x = pos - 5;
				Selection.gameObjects[x].transform.position.y = Selection.gameObjects[0].transform.position.y;
				Selection.gameObjects[x].transform.position.z = Selection.gameObjects[0].transform.position.z;
				pos = pos + distance ;
			}
		}
		
		if(GUILayout.Button ("list_y"))
		{
			pos = 0;

			for(x=0; x <= Selection.gameObjects.Length-1; x++)
			{
				Selection.gameObjects[x].transform.position.x = Selection.gameObjects[0].transform.position.x;
				Selection.gameObjects[x].transform.position.y = pos + 21;
				Selection.gameObjects[x].transform.position.z = Selection.gameObjects[0].transform.position.z;
				pos = pos + distance ;
			}
		}
		
//		if(GUILayout.Button ("Print"))
//		{
//			for(z=1; z <= 10; z++)
//			{
//				Debug.Log (18.5+21*z);
////				Debug.Log(Selection.gameObjects[0].GetInstanceID());
//			}
//		}
	}
	
    @MenuItem ("L Tools/Arrays")
    static function Init ()
    {
        EditorWindow.GetWindow (Arrays);
    }
}
