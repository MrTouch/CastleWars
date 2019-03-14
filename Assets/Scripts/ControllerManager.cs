using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerManager : MonoBehaviour
{
    public SteamVR_ActionSet actionSetEnable;

    public SteamVR_Action_Boolean MenuButton;
    public SteamVR_Action_Boolean TouchPad;
    public SteamVR_Action_Vector2 TouchPos;
    private Hand hand;
    public Hand otherHand;

    private bool MenuActive = false;


    public GameObject menuL;
    public GameObject menuR;
    public GameObject LeftHand;
    public GameObject RightHand;
    private GameObject activeMenu;

    public GameObject[] menusL;
    public GameObject[] menusR;

    public GameObject[] objectPrefabs;
    private GameObject selectedPrefab;
    private GameObject objectToSpawn;

    private bool objectGeneratedSincePress = false;

    // Use this for initialization
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
        Debug.Log(hand);
        actionSetEnable.Activate();
        MenuButton.AddOnStateUpListener(generateObjectUp, hand.handType);
        MenuButton.AddOnStateUpListener(generateObjectUp, otherHand.handType);
    }

    private void generateObjectUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("up");
        objectGeneratedSincePress = false;
    }
    private void Update()
    {
        bool leftHandTouched = TouchPad.GetState(SteamVR_Input_Sources.LeftHand);
        bool rightHandTouched = TouchPad.GetState(SteamVR_Input_Sources.RightHand);

        if (MenuButton.GetState(SteamVR_Input_Sources.Any))
        {
            if (!objectGeneratedSincePress)
            {
                objectGeneratedSincePress = true;
                Debug.Log("generate Object");
                objectToSpawn = Instantiate(selectedPrefab, transform.position, transform.rotation);
            }
            //objectToSpawn = Instantiate(selectedPrefab, transform.position, transform.rotation);
        }

        Vector2 menuPosition = TouchPos.GetAxis(SteamVR_Input_Sources.Any);
        Vector2 notTouching = new Vector2(0f, 0f);
        if (menuPosition == notTouching)
        {
            if (MenuActive == true)
            {
                MenuActive = !MenuActive;
                menuL.SetActive(MenuActive);
                menuR.SetActive(MenuActive);
                for (int i = 0; i < menusL.Length; i++)
                {
                    menusL[i].SetActive(false);
                    menusR[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log(menuPosition);
            if (MenuActive == false)
            {
                MenuActive = !MenuActive;
                //menu.SetActive(MenuActive);
                if (leftHandTouched)
                {
                    menuL.SetActive(MenuActive);
                    Debug.Log("Left hand");
                }
                else if (rightHandTouched)
                {
                    menuR.SetActive(MenuActive);
                    Debug.Log("r hand");
                }
            }
            touchSelect(menuPosition, leftHandTouched, rightHandTouched);
        }

    }
    private void touchSelect(Vector2 menuPosition, bool leftHandTouched, bool rightHandTouched)
    {
        double segmentsize = Math.PI / 4;
        double phi = 0;

        //calculate Phi
        if (menuPosition.x > 0)
        {
            phi = Math.Atan(menuPosition.y / menuPosition.x);
        }
        else if (menuPosition.x < 0 && menuPosition.y >= 0)
        {
            phi = Math.Atan(menuPosition.y / menuPosition.x) + Math.PI;
        }
        else if (menuPosition.x < 0 && menuPosition.y < 0)
        {
            phi = Math.Atan(menuPosition.y / menuPosition.x) - Math.PI;
        }
        else if (menuPosition.x == 0 && menuPosition.y > 0)
        {
            phi = Math.PI / 2;
        }
        else if (menuPosition.x == 0 && menuPosition.y < 0)
        {
            phi = -1 * Math.PI / 2;
        }

        //check in which segment phi is.
        //Debug.Log(phi);
        //Debug.Log(segmentsize);
        if (phi > 0 && phi < segmentsize)
        {
            if (rightHandTouched)
            {
                menusL[0].SetActive(true);
                activeMenu = menusL[0];
            }
            else if (rightHandTouched)
            {
                menusR[0].SetActive(true);
                activeMenu = menusR[0];
            }
            selectedPrefab = objectPrefabs[0];
        }
        else if (phi > segmentsize && phi < segmentsize * 2)
        {
            if (leftHandTouched)
            {
                menusL[1].SetActive(true);
                activeMenu = menusL[1];
            }
            else if (rightHandTouched)
            {
                menusR[1].SetActive(true);
                activeMenu = menusR[1];
            }
            selectedPrefab = objectPrefabs[0];
        }
        else if (phi > segmentsize * 2 && phi < segmentsize * 3)
        {
            if (leftHandTouched)
            {
                menusL[2].SetActive(true);
                activeMenu = menusL[2];
            }
            else if (rightHandTouched)
            {
                menusR[2].SetActive(true);
                activeMenu = menusR[2];
            }
            selectedPrefab = objectPrefabs[0];

        }
        else if (phi > segmentsize * 3 && phi < segmentsize * 4)
        {
            if (leftHandTouched)
            {
                menusL[3].SetActive(true);
                activeMenu = menusL[3];
            }
            else if (rightHandTouched)
            {
                menusR[3].SetActive(true);
                activeMenu = menusR[3];
            }
            selectedPrefab = objectPrefabs[0];
        }
        else if (phi < 0 && phi > -1 * segmentsize)
        {
            if (leftHandTouched)
            {
                menusL[7].SetActive(true);
                activeMenu = menusL[7];
            }
            else if (rightHandTouched)
            {
                menusR[7].SetActive(true);
                activeMenu = menusR[7];
            }
            selectedPrefab = objectPrefabs[0];

        }
        else if (phi < (-1 * segmentsize) && phi > (segmentsize * -2))
        {
            if (leftHandTouched)
            {
                menusL[6].SetActive(true);
                activeMenu = menusL[6];
            }
            else if (rightHandTouched)
            {
                menusR[6].SetActive(true);
                activeMenu = menusR[6];
            }
            selectedPrefab = objectPrefabs[0];
        }
        else if (phi < segmentsize * -2 && phi > segmentsize * -3)
        {
            if (leftHandTouched)
            {
                menusL[5].SetActive(true);
                activeMenu = menusL[5];
            }
            else if (rightHandTouched)
            {
                menusR[5].SetActive(true);
                activeMenu = menusR[5];
            }
            selectedPrefab = objectPrefabs[0];

        }
        else if (phi < segmentsize * -3 && phi > segmentsize * -4)
        {
            if (leftHandTouched)
            {
                menusL[4].SetActive(true);
                activeMenu = menusL[4];
            }
            else if (rightHandTouched)
            {
                menusR[4].SetActive(true);
                activeMenu = menusR[4];
            }
            selectedPrefab = objectPrefabs[0];
        }

        for (int i = 0; i < menusL.Length; i++)
        {
            if (activeMenu != menusL[i])
            {
                menusL[i].SetActive(false);
            }
            if (activeMenu != menusR[i])
            {
                menusR[i].SetActive(false);
            }
            selectedPrefab = objectPrefabs[0];
        }
    }
}

    /*
    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (MenuButton == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No open Menu action assigned");
            return;
        }

        MenuButton.AddOnChangeListener(OnOpenMenuActionChange, hand.handType);
        TouchPad.AddOnChangeListener(OnTrackPadTouch, hand.handType);
        TouchPos.AddOnChangeListener(OnTouchPosChange, hand.handType);
    }

    private void OnDisable()
    {
        if (MenuButton != null)
            MenuButton.RemoveOnChangeListener(OnOpenMenuActionChange, hand.handType);
    }

    private void OnOpenMenuActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            OpenMenu();
        }
    }
    private void OnTrackPadTouch(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            TouchPadTouch();
        }
    }

    private void OnTouchPosChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue)
        {
            TouchPosChange();
        }
    }

    private void TouchPosChange()
    {
        throw new NotImplementedException();
    }

    private void TouchPadTouch()
    {
        throw new NotImplementedException();
    }

    public void OpenMenu()
    {
        Debug.Log("openmenu");
        menu.SetActive(true);
    }
    */
