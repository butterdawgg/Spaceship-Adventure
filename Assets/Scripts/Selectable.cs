using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
   public void Select()
    {
       transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void Deselect()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
    
}
