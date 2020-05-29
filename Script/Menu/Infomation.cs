using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infomation : MonoBehaviour
{
   private int selectMode = 1;
   private bool isHadEditMap = false;

   private float timer = 0f;

   void Update() {
       if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game") {
           timer += Time.deltaTime;
           if (timer >= 3f) {
               Destroy(gameObject);
           }
       }
   }

   public bool IsHadEditMap
   {
       get { return isHadEditMap; }
       set { isHadEditMap = value; }
   }
    
    public int SelectMode
    {
        get { return selectMode; }
        set { selectMode = value; }
    }
    
}
