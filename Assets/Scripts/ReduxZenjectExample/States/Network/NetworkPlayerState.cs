using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class NetworkPlayerState : IState {

		/// <summary>This player's actorID</summary>
		public int ID {
			get { return this.actorID; }
		}

		/// <summary>Identifier of this player in current networkPlayer.</summary>
		private int actorID = -1;

		private string nameField = "";

		/// <summary>Nickname of this player.</summary>
		/// <remarks>Set the PhotonNetwork.playerName to make the name synchronized in a networkPlayer.</remarks>
		public string NickName 
		{
			get
			{
				return this.nameField;
			}
			set
			{
				if (!IsLocal)
				{
					Debug.LogError("Error: Cannot change the name of a remote player!");
					return;
				}
				if (string.IsNullOrEmpty(value) || value.Equals(this.nameField))
				{
					return;
				}

				this.nameField = value;
				PhotonNetwork.playerName = value;   // this will sync the local player's name in a networkPlayer
			}
		}

		/// <summary>UserId of the player, available when the networkPlayer got created with RoomOptions.PublishUserId = true.</summary>
		/// <remarks>Useful for PhotonNetwork.FindFriends and blocking slots in a networkPlayer for expected players (e.g. in PhotonNetwork.CreateRoom).</remarks>
		public string UserId { get; internal set; }

		/// <summary>Only one player is controlled by each client. Others are not local.</summary>
		public readonly bool IsLocal = false;

		/// <summary>
		/// True if this player is the Master Client of the current networkPlayer.
		/// </summary>
		/// <remarks>
		/// See also: PhotonNetwork.masterClient.
		/// </remarks>
		public bool IsMasterClient
		{
			get { return (PhotonNetwork.networkingPeer.mMasterClientId == this.ID); }
		}

		/// <summary>Players might be inactive in a networkPlayer when PlayerTTL for a networkPlayer is > 0. If true, the player is not getting events from this networkPlayer (now) but can return later.</summary>
		public bool IsInactive { get; set; }    // needed for rejoins

		/// <summary>Read-only cache for custom properties of player. Set via PhotonPlayer.SetCustomProperties.</summary>
		/// <remarks>
		/// Don't modify the content of this Hashtable. Use SetCustomProperties and the
		/// properties of this class to modify values. When you use those, the client will
		/// sync values with the server.
		/// </remarks>
		/// <see cref="SetCustomProperties"/>
		public Hashtable CustomProperties { get; internal set; }

		/// <summary>Creates a Hashtable with all properties (custom and "well known" ones).</summary>
		/// <remarks>If used more often, this should be cached.</remarks>
		public Hashtable AllProperties
		{
			get
			{
				Hashtable allProps = new Hashtable();
				allProps.Merge(this.CustomProperties);
				allProps[ActorProperties.PlayerName] = this.NickName;
				return allProps;
			}
		}

		/// <summary>Can be used to store a reference that's useful to know "by player".</summary>
		/// <remarks>Example: Set a player's character as Tag by assigning the GameObject on Instantiate.</remarks>
		public object TagObject;
    }

    public class NetworkPlayerStateInitializer : IStateInitializer, IInitializable {
        readonly NetworkPlayerState networkPlayerState_;

        public NetworkPlayerStateInitializer(NetworkPlayerState networkPlayerState) {
            networkPlayerState_ = networkPlayerState;
        }

        public void Initialize() {
        }
    }
}

