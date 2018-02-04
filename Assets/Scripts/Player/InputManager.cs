using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CharacterBehaviour player;
    public PauseManager pause;
    public AudioSource pauseAudio;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
        pause = GetComponent<PauseManager>();
        pauseAudio = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        // Leer para pausar el juego
        InputPause();
        if (!pause.pause)
        {
            // movimiento del player
            InputAxis();
            // salto del player
            InputJump();
            // alternar entre caminar y correr para el player
            //InputRun();
            // alternar entre agachado y no agachado
            InputCrouch();
            // dash
            InputDash();
            // ataques
            InputAttack();
            InputAttack2();
            // god mode
            InputGodMode();
        }
    }

    void InputPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("Pause");
            pause.Pause();
            pauseAudio.Play();
        }
    }
    void InputAxis()
    {
        Vector2 axis = Vector2.zero;
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");
        player.SetAxis(axis);
    }
    void InputJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            player.JumpStart();
        }
    }
    //void InputRun()
    //{
    //    if(Input.GetButtonDown("Run"))
    //    {
    //        Debug.Log("Run");
    //        player.isRunning = true;
    //    }
    //    if(Input.GetButtonUp("Run"))
    //    {
    //        player.isRunning = false;
    //        Debug.Log("Walk");
    //    }
    //}
    void InputCrouch()
    {
        if(Input.GetButton("Crouch"))
        {
            Debug.Log("Crouch");
            player.Crouch();
        }
    }
    void InputDash()
    {
        if(Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            player.Dash();
        }
    }
    void InputAttack()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("Melee Attack");
            player.Attack();
        }
    }
    void InputAttack2()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("Ranged Attack");
            player.Attack2();
        }
    }
    void InputGodMode()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            player.GodMode();
            if (player.state == CharacterBehaviour.State.GodMode) Debug.Log("God Mode On");
            else if (player.state == CharacterBehaviour.State.Default) Debug.Log("God Mode Off");
        }
    }
    void InputDirectAccess()
    {

    }
}