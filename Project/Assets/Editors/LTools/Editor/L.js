import  System;
import System.IO;

class L extends EditorWindow
{
    
    var plusRect = Rect (10,10,180,320);
    var saveRect = Rect (200,10,150,100);
    var addRect = Rect (200,190,150,100);
    
    var savebool : boolean = true;
    
    var hideobj = new Array ();
    var objname : String = "";
    var hideobjlist : Vector2;
    var hide : boolean = true;
    var strg : String = "";//gameobject name
    
    var listNum : String = "";
    var listallobj : GameObject[];
    var listobj : GameObject[];
    
    var saveName : String = "Write Save Name";
    var removeScript : String = "";
    
    var loadfiles : String[];//读取文件列表
    var showLoad : int = 0;//loadfiles的数组编号
    var addtype : int = 0;//添加属性编号
    
    ///AddWindow//
    var highmod : boolean = false;

    function OnGUI ()
    {
        BeginWindows ();

        plusRect =GUILayout.Window (0, plusRect, PlusWindow, "Plug-ins");
        
        if(savebool)
        {
            saveRect = GUILayout.Window (1, saveRect, SaveWindow, "Save & Load List");
        }

        typeRect = GUILayout.Window (2, addRect, AddWindow, "Add...");

        EndWindows ();
    }

    function PlusWindow ()
    {
        /***list select object***/
        hideobjlist = GUILayout.BeginScrollView (hideobjlist,GUILayout.Height (205));
            GUILayout.BeginHorizontal();
                GUILayout.Label(listNum,GUILayout.Width (25));
                GUILayout.TextArea (objname);
            GUILayout.EndHorizontal();
        GUILayout.EndScrollView ();


        if(GUILayout.Button ("list"))//物体列表
        {
            hideobj.Clear();
            objname = "";
            listNum = "";
            for(var gameobject in Selection.gameObjects)
            {
                strg = gameobject.name;
                hideobj.Add(strg);
            }

            for(x=1; x <=3; x++)//过滤掉重复名称的物体
            {
                for ( i=0; i <= hideobj.Count-1; i++)
                {
                    for(o=i+1; o<= hideobj.Count-1; o++)
                    {
                        if(hideobj[i] == hideobj[o])
                        {
                            hideobj.RemoveAt(o);
                        }
                    }
                }
            }

            for(i = 0;i<hideobj.Count;i++)//在列表显示
            {
	      var num = (i+1).ToString("d3");
	      listNum = listNum + num + "\n";
	      objname = objname + hideobj[i] + "\n";
            }
            
            listallobj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            for(o = 0; o < hideobj.Count; o++)
            {
                listallobj[o] = GameObject.Find(hideobj[o]);
            }
            
            listobj = new GameObject[listallobj.Length];

            for (i=0; i < hideobj.Count; i++)
            {
                listobj[i] = (listallobj[i]).gameObject;
            }
            Selection.objects = listobj;
        }
	////////////////////////////////////////////////////////////////
	
	/***Save List***/
        if(GUILayout.Button ("Save/Load List"))
        {
            if(!savebool)
            {
                savebool = true;
            }
            else
            {
                savebool = false;
            }
        }
        ////////////////////////////////////////////////////////////////
	
	/***Clean List***/
        
        if(GUILayout.Button ("Clean List"))
        {
            hideobj.Clear();
            objname = "";
            listobj = new GameObject[0];
	  listNum = "";
        }
        ////////////////////////////////////////////////////////////////
        
        /***Clean List***/
        
        if(GUILayout.Button ("Yoooooo"))
        {
//        	var yOBj : Hashtable = new Hashtable();// = Selection.objects;//.GetComponent("PackedSprite");
        	var a : int = 0;
        	var yScript : PackedSprite[] = new PackedSprite[Selection.gameObjects.Length];
        	for(var gameobject in Selection.gameObjects)
            {
//                yOBj.Add(gameobject);
                yScript[a] = gameobject.GetComponent("PackedSprite");
                a +=1;
            }
            
            for(var h : int; h < 5; h++)
            {
	            for(var b : int; b < yScript.Length; b++)
	            {
	            	if(yScript[b].pixelPerfect != false)
	            	{
		            	yScript[b].pixelPerfect = false;
		            	yScript[b].width = Mathf.Round(yScript[b].width)/2;
		            	yScript[b].height = Mathf.Round(yScript[b].height)/2;
	            	}
	//            	yOBj.width = 
	            }
            }
        }
        ////////////////////////////////////////////////////////////////

	/***Select List Object***/
        GUILayout.Space (10);
        if(GUILayout.Button ("Select List Object"))
        {
            Selection.objects = listobj;
        }
        ////////////////////////////////////////////////////////////////
        
        /***Select Object With Type***/
//        if(GUILayout.Button ("Select Object With Type"))
//        {
//            if(!typebool)
//            {
//                typebool = true;
//            }
//            else
//            {
//                typebool = false;
//            }
//        }
        
        ////////////////////////////////////////////////////////////////

	/***Zero select object***/
        if(GUILayout.Button ("Zero Object"))
        {
            for (var transform in Selection.transforms)
            {
                transform.localPosition = Vector3(0,0,0);
                transform.localRotation = Quaternion.identity;
            }
        }
		if(GUILayout.Button ("Zero Scale"))
        {
            for (var transform in Selection.transforms)
            {
                transform.localScale = Vector3(1,1,1);
            }
        }
        ////////////////////////////////////////////////////////////////
        
	/***List select object***/
        if(GUILayout.Button ("List select Object"))
        {
	  var listx : int = 50;
	  var listy : int = 50;
	  var tanpos : Vector3;
	  
	//**************************************************************
	//****
	//****
	//****
	      for (var transform in Selection.transforms)
	      {
	      	for (i=1;i <= Selection.gameObjects.Length;i++)
		{
	      	transform.localPosition = Vector3(listx + listx+i,0,0);
		}
	      }
	  
        }
        ////////////////////////////////////////////////////////////////

	/***hide select object***/
        if(GUILayout.Button ("Hide Object"))
        {
            if(Selection.gameObjects[0].active == true)
            {
                hide = false;
            }
            else if(Selection.gameObjects[0].active == false)
            {
                hide = true;
            }
            for (var hideo in Selection.gameObjects)
            {
                hideo.SetActiveRecursively(hide);
            }
        }
        ////////////////////////////////////////////////////////////////
        //GUI.DragWindow ();//窗口可拖动
    }
    
    function SaveWindow ()
    {
        saveName = EditorGUILayout.TextField ("Save Name :" , saveName);
        var _FileLocation : String = Application.dataPath + "/Editors/LTools/Editor/SelectList";//存储地址
        var _FileName : String = "/" + saveName + ".txt";//存储名称
        var lay : int = 0;
        var activeDir : String = Application.dataPath + "/Editors/LTools/Editor";
        var newPath = System.IO.Path.Combine(activeDir, "SelectList");
        System.IO.Directory.CreateDirectory(newPath);//如果没有目录创建文件夹
        
        var pool : String = "";
        
        /***Save List***/
        if(GUILayout.Button ("Save"))//存储
        {
            var find  = _FileLocation + _FileName;
            var sw = new StreamWriter(_FileLocation + _FileName);
            sw.Write(objname);
            sw.Close();
        }
        ////////////////////////////////////////////////////////////////
        
        
        /***Load List***/
        var info = new DirectoryInfo(_FileLocation);
        var fileInfo = info.GetFiles("*.txt");

        loadfiles = new String[fileInfo.Length];

        for (i = 0; i < fileInfo.Length; i++) 
        {
            loadfiles[i] = fileInfo[i].ToString();
            loadfiles[i] = loadfiles[i].Replace(_FileLocation + "/", "");
            loadfiles[i] = loadfiles[i].Replace(".txt", "");
        }
        GUILayout.Space (10);
        GUILayout.Label("---------------------------------");
        GUILayout.Space (10);
        GUILayout.Label("Load Files ↓");
        GUILayout.BeginHorizontal();
        showLoad = EditorGUILayout.Popup( showLoad, loadfiles,GUILayout.Width(120));
        if(GUILayout.Button ("Remove",GUILayout.Height(15)))//删除
        {
            var delfile = new FileInfo(_FileLocation + "/" + loadfiles[showLoad] + ".txt");

            if (delfile.Exists)
            {
                delfile.Delete(); //删除单个文件
                showLoad = 0;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space (5);
        
        if(GUILayout.Button ("Load"))//读取
        {
            hideobj.Clear();
            objname = "";
            listNum = "";
            var sellist = new ArrayList();
            if(objname != null)
            {
                objname = "";
            }
            
            try
            {
                var sr = new StreamReader(_FileLocation + "/" + loadfiles[showLoad] + ".txt");
                var st = new StreamReader(_FileLocation + "/" + loadfiles[showLoad] + ".txt");
                var selobj = new StreamReader(_FileLocation + "/" + loadfiles[showLoad] + ".txt");
                pool = sr.ReadLine();
                while(pool != null)
                {
                    pool = sr.ReadLine();
                    lay++;
                }
                
                
                for (i=0; i < lay; i++)
                {
                    objname = objname + st.ReadLine() + "\n";
                    sellist.Add(selobj.ReadLine());
                    listNum = listNum + (i+1).ToString("d3") + "\n";
                }

                sr.Close();
            }
            catch (e)
            {
                Debug.Log("The file could not be read:");
                Debug.Log(e.Message);
            }
            
            
            //根据名称选择物体
            listobj = new GameObject[lay];
            for(o = 0; o < lay; o++)
            {
                listobj[o] = GameObject.Find(sellist[o]);
            }
        }
        ////////////////////////////////////////////////////////////////
        
        //GUI.DragWindow ();
    }

    function AddWindow ()
    {
        var addtypeName : String[] = new String[7];//添加属性列表
        var addtypeCom : Type[] = new Type[7];
        addtypeName[0] = "Physics / Rigidbody";
        addtypeName[1] = "Physics / Character Controller";
        addtypeName[2] = "Physics / Box Collider";
        addtypeName[3] = "Physics / Sphere Collider";
        addtypeName[4] = "Physics / Capsule Collider";
        addtypeName[5] = "Physics / Mesh Collider";
        addtypeName[6] = "Physics / Wheel Collider";
        
        addtypeCom[0] = Rigidbody;
        addtypeCom[1] = CharacterController;
        addtypeCom[2] = BoxCollider;
        addtypeCom[3] = SphereCollider;
        addtypeCom[4] = CapsuleCollider;
        addtypeCom[5] = MeshCollider;
        addtypeCom[6] = WheelCollider;
        
        addtype = EditorGUILayout.Popup( addtype, addtypeName);
        
        if(GUILayout.Button ("Add"))
        {
	  for(var gameobject in Selection.gameObjects)
	  {
	      gameobject.AddComponent(addtypeCom[addtype]);
	  }
        }
        if(GUILayout.Button ("Remove"))
        {
	  for(gameobject in Selection.gameObjects)
	  {
	      DestroyImmediate(gameobject.GetComponent(addtypeCom[addtype]));
	  }
        }
        GUILayout.Space (10);
        GUILayout.Label("---------------------------------");
        highmod = GUILayout.Toggle(highmod, "NB Mode");
        if(highmod)
        {
        GUILayout.Label("Add/Remove By Name :");
        removeScript = GUILayout.TextField (removeScript);
        if(GUILayout.Button ("Add"))
        {
	  for(var gameobject in Selection.gameObjects)
	  {
	      gameobject.AddComponent(removeScript);
	  }
        }
        if(GUILayout.Button ("Remove"))
        {
	  for(gameobject in Selection.gameObjects)
	  {
	      DestroyImmediate(gameobject.GetComponent(removeScript));
	  }
        }
        }
        else
        {
        	
        }
//        GUI.DragWindow ();        
    }

    
//    function TestWindow () {
//        GUILayout.Button ("Hi");
//        GUI.DragWindow ();        
//    }
    @MenuItem ("L Tools/L Tools")
    static function Init ()
    {
        EditorWindow.GetWindow (L);
    }
}