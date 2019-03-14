using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Ch.ittouch.CastleHuman
{

    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        #endregion

        #region Private Fields

        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }   
        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (other.gameObject.tag == "ammo")
            {
                return;
            }
            Health -= 0.1f;
        }

    }
}
