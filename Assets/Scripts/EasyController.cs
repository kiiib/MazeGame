using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyController : MonoBehaviour
{
    float speed = 0.01f;
    private GameObject head, player;
    SteamVR_TrackedObject trackdeObjec;
    SteamVR_Controller.Device device;
    bool is_trigging = false; // 兩隻手都按放開應該會GG XDD

    public GameObject Player;

    void Awake()
    {
        this.head = GameObject.Find("Camera (eye)");
        this.player = GameObject.Find("player");

        //抓取手把上面的組件  
        var controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += Trigger;
        controller.TriggerUnclicked += UnTrigger;
        this.trackdeObjec = this.GetComponent<SteamVR_TrackedObject>();
    }

    void Trigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Trigger has been pressed: " + this.name);
        is_trigging = true;

    }

    void UnTrigger(object sender, ClickedEventArgs e)
    {
        Debug.Log("Trigger has been unpressed");
        is_trigging = false;
    }
    // Use this for initialization
    void Start()
    {

    }
    private bool IsDown = false;
    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)this.trackdeObjec.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Trigger is pressed!"); //控制台输出Trigger is pressed!
            device.TriggerHapticPulse(500); //手柄震动函数，500代表振幅,是一个ushort类型
        }

        //print(this.head.transform.eulerAngles.y);
        var Camera = this.head.transform.eulerAngles.y;
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            IsDown = false;
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            IsDown = true;
        }


        if (IsDown)
        {
            Debug.Log("按下了 Touchpad ");

            //方法返回一個坐標 接觸圓盤位置
            Vector2 cc = device.GetAxis();
            Debug.Log(cc);
			var degree = Mathf.Atan2(cc.y, cc.x) * 180/ Mathf.PI;
            print(degree);
            if(degree >= 45 && degree < 135)
            {
                Debug.Log("forward");
                var vector = this.head.transform.forward * speed;
                vector.y = 0;
                Player.transform.position += vector;
            }
            if (degree >= -135 && degree < -45)
            {
                Debug.Log("backward");
                var vector = -this.head.transform.forward * speed;
                vector.y = 0;
                Player.transform.position += vector;
            }
            if (degree >= -45 && degree < 45)
            {
                Debug.Log("right");
                var vector = this.head.transform.right * speed;
                vector.y = 0;
                Player.transform.position += vector;
            }
            if (degree >= -135 && degree < 135)
            {
                Debug.Log("left");
                var vector = -this.head.transform.right * speed;
                vector.y = 0;
                Player.transform.position += vector;
            }
            //下  
        }

        /*
          if (is_trigging)
                {
                    Vector3 v = head.transform.forward;
                    if(!this.name.Equals("Controller (left)"))
                    {
                        Debug.Log("RIGHT HAND?");
                        v = -v;
                    }

                    this.player.transform.position += new Vector3(v.x / 2.0f, 0, v.z / 2.0f);
                }

      */


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Plane")
        {
            this.speed = -0.1f;
            collision.gameObject.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("collision");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        this.speed = 0.1f;
        collision.gameObject.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("wall");
    }
    //void FixedUpdate()
    //{
    //    //抓取手把輸入  
    //    var device = SteamVR_Controller.Input((int)trackdeObjec.index);
    //    //以下是SDK中複製出来的按键列表  
    //    /* public class ButtonMask 
    //       { 
    //           public const ulong System = (1ul << (int)EVRButtonId.k_EButton_System); // reserved 
    //           public const ulong ApplicationMenu = (1ul << (int)EVRButtonId.k_EButton_ApplicationMenu); 
    //           public const ulong Grip = (1ul << (int)EVRButtonId.k_EButton_Grip); 
    //           public const ulong Axis0 = (1ul << (int)EVRButtonId.k_EButton_Axis0); 
    //           public const ulong Axis1 = (1ul << (int)EVRButtonId.k_EButton_Axis1); 
    //           public const ulong Axis2 = (1ul << (int)EVRButtonId.k_EButton_Axis2); 
    //           public const ulong Axis3 = (1ul << (int)EVRButtonId.k_EButton_Axis3); 
    //           public const ulong Axis4 = (1ul << (int)EVRButtonId.k_EButton_Axis4); 
    //           public const ulong Touchpad = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Touchpad); 
    //           public const ulong Trigger = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Trigger); 
    //       } 
    //       */

    //    //同樣是三種按鍵方式 
    //    if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        //右手震動  
    //        //拉弓類似操作應該就是按住trigger（扳機）gettouch時持續調用震動方法模擬弓弦繃緊的感覺。
    //        var deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
    //        SteamVR_Controller.Input(deviceIndex2).TriggerHapticPulse(500);
    //        Debug.Log("按了trigger扳機鍵");
    //    }
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        Debug.Log("按下了trigger扳機鍵");
    //    }
    //    if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        //左手震动  
    //        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
    //        SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(3000);

    //        //右手震动  
    //        var deviceIndex1 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
    //        SteamVR_Controller.Input(deviceIndex1).TriggerHapticPulse(3000);

    //        Debug.Log("鬆開了trigger扳機鍵");
    //    }

    //    //這三種也能檢測到 後面不做贅述  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        Debug.Log("用press按下了 trigger 扳機鍵");
    //    }
    //    if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        Debug.Log("用press按了 trigger 扳機鍵");
    //    }
    //    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    //    {
    //        Debug.Log("用press鬆開了 trigger 扳機鍵");
    //    }

    //    // system鍵 圓盤下面那個鍵   
    //    // reserved 為Steam系統保留,用來調出Steam系統菜單 因此貌似自己加的功能沒啥用
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.System))
    //    {
    //        Debug.Log("按下了 system 系統按鈕/Steam");
    //    }
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.System))
    //    {
    //        Debug.Log("用press按下了 System 系統按鈕/Steam");
    //    }

    //    //Application Menu鍵 帶菜單標誌的那個按鍵(在方向圓盤上面)
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        Debug.Log("按下了 Application Menu 菜單鍵");
    //    }
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        Debug.Log("用press按下了 Application Menu 菜單鍵");
    //    }

    //    //Grip鍵 兩側的鍵 （vive僱傭兵遊戲中的換彈鍵），每個手柄左右各一功能相同，同一手柄兩個鍵是一個鍵。
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
    //    {
    //        Debug.Log("按下了Grip");
    //    }
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
    //    {
    //        Debug.Log("用press按下了Grip");
    //    }

    //    //Axis0键 與圓盤有交互  
    //    //触摸触发  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis0))
    //    {
    //        Debug.Log("按下了 Axis0 方向 ");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
    //    {
    //        Debug.Log("用press按下了 Axis0 方向");
    //    }

    //    //Axis1鍵 目前未發現按鍵位置  
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis1))
    //    {
    //        Debug.Log("按下了 Axis1");
    //    }
    //    //按動觸發 
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis1))
    //    {
    //        Debug.Log("用press按下了 Axis1");
    //    }

    //    //Axis2鍵 目前未發現按鍵位置  
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis2))
    //    {
    //        Debug.Log("按下了 Axis2");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis2))
    //    {
    //        Debug.Log("用press按下了Axis2");
    //    }

    //    //Axis3鍵 目前未發現按鍵位置  
    //    //觸摸觸發   
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis3))
    //    {
    //        Debug.Log("按下了Axis3");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis3))
    //    {
    //        Debug.Log("用press按下了 Axis3");
    //    }

    //    //Axis4鍵 目前未發現按鍵位置  
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis4))
    //    {
    //        Debug.Log("按下了Axis4");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis4))
    //    {
    //        Debug.Log("用press按下了 Axis4");
    //    }

    //    //方向圓盤：  
    //    //這裡開始區分了press檢測與touch檢測的不同之處，圓盤可以觸摸，顧名思義，touch檢測的是觸摸，press檢測的是按動
    //    //鍵與圓盤有交互與圓盤有關
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis0))
    //    {
    //        Debug.Log("按下了Axis0 方向 ");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
    //    {
    //        Debug.Log("用press按下了 Axis0 方向");
    //    }

    //    //Axis1鍵 目前未發現按鍵位置   
    //    //觸摸觸發 
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis1))
    //    {
    //        Debug.Log("按下了 Axis1 ");
    //    }
    //    //按動觸發   
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis1))
    //    {
    //        Debug.Log("用press按下了 Axis1 ");
    //    }

    //    //Axis2鍵 目前未發現按鍵位置  
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis2))
    //    {
    //        Debug.Log("按下了 Axis2 ");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis2))
    //    {
    //        Debug.Log("用press按下了 Axis2 ");
    //    }

    //    //Axis3鍵 目前未發現按鍵位置   
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis3))
    //    {
    //        Debug.Log("按下了 Axis3 ");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis3))
    //    {
    //        Debug.Log("用press按下了 Axis3 ");
    //    }

    //    //Axis4鍵 目前未發現按鍵位置   
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Axis4))
    //    {
    //        Debug.Log("按下了 Axis4 ");
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Axis4))
    //    {
    //        Debug.Log("用press按下了 Axis4 ");
    //    }


    //    //Touchpad鍵 圓盤交互  
    //    //觸摸觸發  
    //    if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
    //    {
    //        Debug.Log("按下了 Touchpad ");

    //        //方法返回一個坐標 接觸圓盤位置
    //        Vector2 cc = device.GetAxis();
    //        Debug.Log(cc);
    //        //例子：圓盤分成上下左右  
    //        float jiaodu = VectorAngle(new Vector2(1, 0), cc);
    //        Debug.Log(jiaodu);
    //        //下  
    //        if (jiaodu > 45 && jiaodu < 135)
    //        {
    //            Debug.Log("下");
    //        }
    //        //上  
    //        if (jiaodu < -45 && jiaodu > -135)
    //        {
    //            Debug.Log("上");
    //        }
    //        //左  
    //        if ((jiaodu < 180 && jiaodu > 135) || (jiaodu < -135 && jiaodu > -180))
    //        {
    //            Debug.Log("左");
    //        }
    //        //右  
    //        if ((jiaodu > 0 && jiaodu < 45) || (jiaodu > -45 && jiaodu < 0))
    //        {
    //            Debug.Log("右");
    //        }
    //    }
    //    //按動觸發  
    //    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
    //    {
    //        Debug.Log("用press按下了 Touchpad ");
    //    }

    //}

    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
