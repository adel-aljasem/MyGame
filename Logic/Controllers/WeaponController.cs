﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdilGame.Components;
using AdilGame.Logic.inventory;
using AdilGame.Logic.Weapons;
using AdilGame.Network;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdilGame.Logic.Controllers
{
    public class WeaponController : BaseController
    {
        public int PlayerId { get; set; }
        public Weapon EquipedWeapon { get; set; }
        public Inventory inventory { get; set; }
        public bool CanFire = true;
        //public Player PlayerComingData { get; set; } = new Player();
        //public Player PlayerGoingData { get; set; } = new Player();

        internal override void Awake()
        {
            base.Awake();
            EquipedWeapon = gameObject.AddComponent<Bow>();
            EquipedWeapon.Collider.IsDynamic = true;
            EquipedWeapon.FireCooldownDuration = 1;
        }

        public void ChangeEquipedWeaponFromServer(WeaponData weapon)
        {
            if (weapon != null)
            {
                EquipedWeapon?.gameObject.RemoveComponent<Weapon>();
                switch (weapon.weaponType)
                {
                    case WeaponTypeEnum.bow:
                        EquipedWeapon = gameObject.AddComponent<Bow>();
                        break;

                    case WeaponTypeEnum.sword: EquipedWeapon = gameObject.AddComponent<Sword>(); break;

                }
                EquipedWeapon.Damage = weapon.Damage;
                EquipedWeapon.BulletSpeed = weapon.BulletSpeed;
                EquipedWeapon.Level = weapon.Level;
                EquipedWeapon.FireCooldownDuration = weapon.FireCooldownDuration;
            }
        }

        public async Task ChangeEquipWeapon()
        {
            var keyboardState = Keyboard.GetState();
            if (PlayerId == PlayerNetworkManager.Instance.playerId)
            {
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    EquipedWeapon = gameObject.AddComponent<Bow>();
                }

                WeaponData weaponData = new WeaponData();
                weaponData.PlusHealth = EquipedWeapon.PlusHealth;
                weaponData.Damage = EquipedWeapon.Damage;
                weaponData.Level = EquipedWeapon.Level;
                weaponData.FireCooldownDuration = EquipedWeapon.FireCooldownDuration;
               await WeaponNetworkManager.instance.ChangeEquipWeapon(weaponData);

            }

        }

        public async Task AddWeapon(Weapon weapon)
        {

            await WeaponNetworkManager.instance.AddWeapon(new WeaponData { PlayerId = PlayerId,weaponType = EquipedWeapon.WeaponTypeenum,BulletSpeed = EquipedWeapon.BulletSpeed,Damage = EquipedWeapon.Damage,
            FireCooldownDuration = EquipedWeapon.FireCooldownDuration,Level = EquipedWeapon.Level,LifeTime = EquipedWeapon.LifeTime,PlusHealth = EquipedWeapon.PlusHealth});
            inventory.AddItem(weapon);
        }


        public async Task FireWeapon()
        {
            var currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);

            if (currentMouseState.LeftButton == ButtonState.Pressed && CanFire)
            {
                EquipedWeapon.weaponState = WeaponState.Attacking;
                EquipedWeapon.Fire();

            }
            else
            {
                EquipedWeapon.weaponState = WeaponState.None;
            }
        }

        public void FlipCharacterBasedOnMousePosition(float MouseX, float mouseY, float characterX)
        {
            bool shouldFaceLeft = MouseX < characterX;

            gameObject.GetComponent<Weapon>()?.FlipWeaponBasedOnMouse(shouldFaceLeft);
            gameObject.GetComponent<Weapon>()?.UpdateWeaponRotation(MouseX, mouseY);
        }
        internal override void Update(GameTime gameTime)
        {
            PlayerGoingData = gameObject.GetComponent<PlayerController>()?.PlayerGoingData;
            PlayerComingData = gameObject.GetComponent<PlayerController>()?.PlayerComingData;
            PlayerGoingData.weaponData = new WeaponData { BulletSpeed = 10 };
            FlipCharacterBasedOnMousePosition(PlayerComingData.MouseData.MouseX,PlayerComingData.MouseData.MouseY,gameObject.Transform.Position.X);
            FireWeapon();
        }
        internal override void NetworkUpdate(GameTime gameTime)
        {
            base.NetworkUpdate(gameTime);

        }

        public async Task UpdateWeaponPosition(WeaponData weaponData)
        {

        }

    }
}