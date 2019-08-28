using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ex) public int code;//1로그인 2로그아웃 3회원가입
//스레드//디자인 패턴//인터페이스
[Serializable]
public class Code
{
    public int code;
    public Code(int codeVal = 0){
        this.code = codeVal;
    }
}
[Serializable]
public class Login : Code {
    public string userID;
    public string userPass;
    public Login() : base(1) {}
}
[Serializable]
public class Logout : Code
{
    public Logout() : base(2) {}
}
[Serializable]
public class Num {
    public int a;
    public int b;
    public string c;
    public float d;
}
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Num num = new Num();
        num.a = 1;
        num.b = 2;
        num.c = "this is c";
        num.d = 4.5f;
        string json = JsonUtility.ToJson(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
