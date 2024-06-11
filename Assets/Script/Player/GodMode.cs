using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class GodMode : Singleton<GodMode>
{
    float inputTime = 8.01f;
    int inputCount;
    bool inputThisFrame;
    GameObject player;

    public bool EnableGodMode;
    private void Start()
    {
        
    }

    private void Update()
    {
        inputThisFrame = false;

        if (inputTime >= 8f && inputCount == 0)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Start Input Code");
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
            }
        }

        if (inputCount > 0)
        {
            inputTime -= Time.deltaTime;
        }

        if (inputCount == 1 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 2 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }

            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 3 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }

            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 4 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 5 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 6 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 7 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 8 && inputThisFrame == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputCount++;
                inputThisFrame = true;
                Debug.Log("Correct Input: " + inputCount);
                return;
            }
            if (Input.anyKeyDown)
            {
                EndInput();
                inputThisFrame = true;
            }
        }

        if (inputCount == 9 && inputThisFrame == false)
        {
            EnableGodMode = !EnableGodMode;
            if (EnableGodMode)
            {
                Debug.LogWarning("GOD MOD: ON");
            }
            else
            {
                Debug.LogWarning("GOD MOD: OFF");
            }
            EndInput();
        }

        if (EnableGodMode)
        {
            player = GameObject.Find("Player");
            if (player != null)
            {
               var getPlayer = player.GetComponent<PlayerMovement>();
                getPlayer.CheckGodModeOn(true);
            }
        }
        else
        {
            player = GameObject.Find("Player");
            if (player != null)
            {
                var getPlayer = player.GetComponent<PlayerMovement>();
                getPlayer.CheckGodModeOn(false);
            }
        }

        if (inputTime <= 0f)
        {
            EndInput();
        }
    }

    void EndInput()
    {
        Debug.Log("Input Ended");
        inputTime = 8.01f;
        inputCount = 0;
    }
}
