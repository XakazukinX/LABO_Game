using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {


    private GameObject _mainCameraObject;
    private Camera _maincamera;

    private Vector3 newAngle;
    private Vector3 lastMousePosition;
    [SerializeField] private Vector2 rotationSpeed;
    [SerializeField] private float MoveSpeed;

    [SerializeField] private Animator CharacterAnimator;



 	// Use this for initialization
	void Start () 
    {
        //カメラの取得
        //カメラはキャラごとに存在しているのでFindでオブジェクトを検索する。
        _mainCameraObject = GameObject.Find("Main Camera");
        _maincamera = _mainCameraObject.GetComponent<Camera>();
	}


    // Update is called once per frame
    void Update()
    {
        
        // 左クリックした時
        if (Input.GetMouseButtonDown(0))
        {
            // カメラの角度を変数"newAngle"に格納
            newAngle = gameObject.transform.localEulerAngles;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.mousePosition;
        }
        // 左ドラッグしている間
        else if (Input.GetMouseButton(0))
        {

            Vector3 mousePos = Input.mousePosition;
            _maincamera.WorldToViewportPoint(mousePos);
            CharacterMoving(_maincamera.ScreenToViewportPoint(mousePos));

            // Y軸の回転：マウスドラッグ方向に視点回転
            // マウスの水平移動値に変数"rotationSpeed"を掛ける
            //（クリック時の座標とマウス座標の現在値の差分値）
            newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
            // X軸の回転：マウスドラッグ方向に視点回転
            // マウスの垂直移動値に変数"rotationSpeed"を掛ける
            //（クリック時の座標とマウス座標の現在値の差分値）
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
            // "newAngle"の角度をカメラ角度に格納
            gameObject.transform.localEulerAngles = newAngle;
            // マウス座標を変数"lastMousePosition"に格納
            lastMousePosition = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            CharacterAnimator.Play("Idle", 0, 0);
            CharacterAnimator.SetBool("Front", false);
            CharacterAnimator.SetBool("Left", false);
            CharacterAnimator.SetBool("Right", false);

        }
    }

	// Update is called once per frame

    void CharacterMoving(Vector3 ClickPos)
    {

        //クリックした場所を受け取って画面の上下左右を判定
        if (ClickPos.y >= ClickPos.x && ClickPos.y >= -ClickPos.x+1)
        {
            gameObject.transform.position += new Vector3(0, 0, MoveSpeed);
            CharacterAnimator.SetBool("Left", false);
            CharacterAnimator.SetBool("Right", false);
            CharacterAnimator.SetBool("Front", true);
        }
        if (ClickPos.y > ClickPos.x && ClickPos.y < -ClickPos.x + 1)
        {
            gameObject.transform.position -= new Vector3(MoveSpeed, 0, 0);
            CharacterAnimator.SetBool("Front", false);
            CharacterAnimator.SetBool("Right", false);
            CharacterAnimator.SetBool("Left", true);
        }
        if (ClickPos.y < ClickPos.x && ClickPos.y < -ClickPos.x + 1)
        {
            gameObject.transform.position -= new Vector3(0, 0, MoveSpeed);

        }
        if (ClickPos.y < ClickPos.x && ClickPos.y > -ClickPos.x + 1)
        {
            gameObject.transform.position += new Vector3(MoveSpeed, 0, 0);
            CharacterAnimator.SetBool("Front", false);
            CharacterAnimator.SetBool("Left", false);
            CharacterAnimator.SetBool("Right", true);

        }
    }

}
