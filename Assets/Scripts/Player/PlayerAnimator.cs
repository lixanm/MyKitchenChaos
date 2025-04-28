using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator isWalking;
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;


    private void Awake()
    {
        isWalking = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        isWalking.SetBool(IS_WALKING, player.IsWalking());
    }
}
