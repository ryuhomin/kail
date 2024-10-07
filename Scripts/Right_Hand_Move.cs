using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right_Hand_Move : MonoBehaviour
{
    Animator anime;
    private void Start()
    {
        anime = GetComponent<Animator>();
    }
    void Update()
    {
        float fPlayerMov_X = Input.GetAxis("Horizontal");
        float fPlayerMov_Y = Input.GetAxis("Vertical");

        if (fPlayerMov_X != 0 || fPlayerMov_Y != 0)
        {
            anime.SetBool("isWalk", true);
        }
        else anime.SetBool("isWalk", false);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anime.SetBool("isRun", true);
        }
        else anime.SetBool("isRun", false);

        if(Input.GetMouseButton(0)) // 공격 우클
        {
            anime.SetBool("isAttack", true);
        }
        else anime.SetBool("isAttack", false);

        if (Input.GetKey(KeyCode.F))
        {
            anime.SetBool("isShit", true);
        }
        else anime.SetBool("isShit", false);

    }
}
