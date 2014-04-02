using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   protected static T _instance;
 
   /**
      Returns the _instance of this singleton.
   */
   public static T instance{
		get{
			return Instance;	
		}
	}
   public static T Instance
   {
      get
      {
         if(_instance == null)
         {
            _instance = (T) FindObjectOfType(typeof(T));
            if (_instance == null)
            {
               GameObject go = new GameObject(typeof(T).ToString());
				_instance = go.AddComponent<T>();
            }
         }
 
         return _instance;
      }
   }
}