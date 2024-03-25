using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharactorUIEvents : MonoBehaviour
{   //受伤的对象与受伤数值
    public static UnityAction<GameObject, int> characterDamaged;
    //恢复的对象与恢复的值
    public static UnityAction<GameObject, int> characterHealed;
    //最大生命值变化
    public static UnityAction<GameObject, int> characterMaxHealthChange;
    //public static UnityAction<Transform, string> controlTextEnable;
}
