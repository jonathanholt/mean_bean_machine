using UnityEngine;
 using System.Collections;
 [RequireComponent(typeof(SpriteRenderer))]
 
 public class AutoStretchSprite : MonoBehaviour {
 
     /// <summary> Do you want the sprite to maintain the aspect ratio? </summary>
     public bool anonymouseepAspectRatio = true;
     /// <summary> Do you want it to continually check the screen size and update? </summary>
     public bool ExecuteOnUpdate = true;
	 public float scale;
	 public float widthOffset;
 
     void Start () {
         Resize(anonymouseepAspectRatio);
     }
     
     void FixedUpdate () {
         if (ExecuteOnUpdate)
             Resize(anonymouseepAspectRatio);
     }
 
     /// <summary>
     /// Resize the attached sprite according to the camera view
     /// </summary>
     /// <param name="keepAspect">bool : if true, the image aspect ratio will be retained</param>
     void Resize(bool keepAspect = false)
     {
         SpriteRenderer sr=GetComponent<SpriteRenderer>();
         if(sr==null) return;
 
         transform.localScale=new Vector3(1,1,1);
 
         float width=sr.sprite.bounds.size.x;
         float height=sr.sprite.bounds.size.y;
 
 
         float worldScreenHeight=Camera.main.orthographicSize*2f;
         float worldScreenWidth=worldScreenHeight/Screen.height*Screen.width;
 
         Vector3 xWidth = transform.localScale;
         xWidth.x=((worldScreenHeight / width) / scale) * widthOffset;
         transform.localScale=xWidth;
         //transform.localScale.x = worldScreenWidth / width;
         Vector3 yHeight = transform.localScale;
         yHeight.y=(worldScreenHeight / height) / scale;
         transform.localScale=yHeight;
         //transform.localScale.y = worldScreenHeight / height;
     }
 }