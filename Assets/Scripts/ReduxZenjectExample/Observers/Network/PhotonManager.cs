// using Zenject;
// using System;
// using ExitGames.Client.Photon;
// using System.Collections.Generic;
// using Redux;

// namespace Reduxity.Example.Zenject {
// 	public class PhotonManager : IInitializable, IComponent, IPunCallbacks {

// 		readonly App app_;
// 		readonly Settings settings_;
//         readonly PunCloudRequestCreator.ActionCreator actionCreator_;
// 		readonly Dispatcher dispatch_;

//         public PhotonManager(
// 			App app,
// 			Settings settings,
//             PunCloudRequestCreator.ActionCreator actionCreator

// 		) {
// 			app_ = app;
// 			settings_ = settings;
//             actionCreator_ = actionCreator;
// 			dispatch_ = app_.Store.Dispatch;
// 		}

// 		public void Initialize() {
// 		}






 










//         /// <summary>
//         /// Called on all scripts on a GameObject (and children) that have been Instantiated using PhotonNetwork.Instantiate.
//         /// </summary>
//         /// <remarks>
//         /// PhotonMessageInfo parameter provides info about who created the object and when (based off PhotonNetworking.time).
//         /// </remarks>
//         public void OnPhotonInstantiate(PhotonMessageInfo info) {
//             // TODO: create zenject factory?
//         }










//         /// <summary>
//         /// Called when the server sent the response to a FindFriends request and updated PhotonNetwork.Friends.
//         /// </summary>
//         /// <remarks>
//         /// The friends list is available as PhotonNetwork.Friends, listing name, online state and
//         /// the room a user is in (if any).
//         /// </remarks>
//         public void OnUpdatedFriendList()
//         {
//         }



//         /// <summary>
//         /// Called by PUN when the response to a WebRPC is available. See PhotonNetwork.WebRPC.
//         /// </summary>
//         /// <remarks>
//         /// Important: The response.ReturnCode is 0 if Photon was able to reach your web-service.
//         /// The content of the response is what your web-service sent. You can create a WebResponse instance from it.
//         /// Example: WebRpcResponse webResponse = new WebRpcResponse(operationResponse);
//         ///
//         /// Please note: Class OperationResponse is in a namespace which needs to be "used":
//         /// using ExitGames.Client.Photon;  // includes OperationResponse (and other classes)
//         ///
//         /// The OperationResponse.ReturnCode by Photon is:<pre>
//         ///  0 for "OK"
//         /// -3 for "Web-Service not configured" (see Dashboard / WebHooks)
//         /// -5 for "Web-Service does now have RPC path/name" (at least for Azure)</pre>
//         /// </remarks>
//         public void OnWebRpcResponse(OperationResponse response)
//         {
//         }

//         /// <summary>
//         /// Called when another player requests ownership of a PhotonView from you (the current owner).
//         /// </summary>
//         /// <remarks>
//         /// The parameter viewAndPlayer contains:
//         ///
//         /// PhotonView view = viewAndPlayer[0] as PhotonView;
//         ///
//         /// PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;
//         /// </remarks>
//         /// <param name="viewAndPlayer">The PhotonView is viewAndPlayer[0] and the requesting player is viewAndPlayer[1].</param>
//         public void OnOwnershipRequest(object[] viewAndPlayer)
//         {
//         }


// 		[Serializable]
// 		/// <summary>
// 		/// </summary>
// 		public class Settings {
// 		}
// 	}
// }

