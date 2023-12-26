using AdilGame.Logic.Controllers;
using AdilGame.Network.Data;
using Microsoft.AspNetCore.SignalR.Client;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System.Threading.Tasks;

namespace AdilGame.Network
{
    public class WeaponNetworkManager
    {
        public static WeaponNetworkManager instance { get; set; } = new WeaponNetworkManager();
        public WeaponNetworkManager()
        {

            Core.Instance.NetworkSystem.hubConnection.On<WeaponData>("OnChangeEquipedWeapon", (NewWeapon) =>
            {
                foreach (GameObject Playercontroller in Core.Instance.GameObjectSystem.GetAllGameObjects())
                {
                    if (Playercontroller?.GetComponent<PlayerController>()?.PlayerComingData.Id == NewWeapon.PlayerId)
                    {
                       var playerCont = Playercontroller.GetComponent<PlayerController>();
                        if(playerCont.gameObject.GetComponents<WeaponController>() != null)
                        {
                            var weaponContol = Playercontroller.GetComponent<WeaponController>();

                            weaponContol.ChangeEquipedWeaponFromServer(NewWeapon);
                        }
                    }
                }

            });
        }



        public async Task ChangeEquipWeapon(WeaponData weaponData)
        {
            Core.Instance.NetworkSystem.hubConnection.SendAsync("ChangeWeaponEquiped", weaponData);
        }
        public async Task AddWeapon(WeaponData weaponData)
        {
            Core.Instance.NetworkSystem.hubConnection.SendAsync("AddWeapon", weaponData);
        }

    }
}
