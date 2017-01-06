// using Zenject;
// using System;
// using System.Collections.Generic;
// using Redux;
// using UniRx;
// using UnityEngine;
// using PhotonRx;

// namespace Reduxity.Example.Zenject {
// 	public class PhotonCloudObserver : MonoBehaviour, IInitializable {

//         [Inject]
//         public void Construct() {
// 		}

// 		public void Initialize() {
// 		}

//         public IConnectableObservable<bool> ConnectToPhotonCloudAsObservable() {
//             return Observable.Merge(
//                 this.OnConnectedToPhotonAsObservable().Select(_ => true),
//                 this.OnConnectionFailAsObservable().Select(_ => false),
//                 this.OnFailedToConnectToPhotonAsObservable().Select(_ => false)
//             )
//                 .FirstOrDefault() // OnCompletedを発火させるため
//                 .PublishLast();   // PhotonNetwork.Connectを呼び出すより前にストリームを稼働させるため
//         }

//         public IConnectableObservable<bool> DisconnectFromPhotonAsObservable() {
// 			return this.OnDisconnectedFromPhotonAsObservable()
// 				.Select(_ => false)
// 				.PublishLast();
//         }
// 	}
// }