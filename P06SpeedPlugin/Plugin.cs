using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using System.Reflection;

namespace P06SpeedPlugin
{
    public class Plugin : MelonMod
    {
        private PlayerBase player;
        private bool isPlaying;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);

            if (GameManager.Instance.GameState == GameManager.State.Playing) 
            {
                isPlaying = true;
            } else if (GameManager.Instance.GameState == GameManager.State.Menu || GameManager.Instance.GameState == GameManager.State.Result)
            {
                isPlaying = false;
            }
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            if (!isPlaying) return;

            if (player == null) { player = GameObject.FindObjectOfType<PlayerBase>(); }

            if (player == null) return;

            Type t = player.GetType();
            FieldInfo field = t.GetField("MaximumSpeed", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.GetProperty);

            field.SetValue(player, 50);
        }
    }
}
