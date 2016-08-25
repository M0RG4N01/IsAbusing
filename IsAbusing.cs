﻿using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API.Collections;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;
using Steamworks;
using Rocket.Unturned.Chat;
using Rocket.Core.Logging;

namespace IsAbusing
{
    public class IsAbusing : RocketPlugin<IsAbusingConfiguration>
    {
        public static IsAbusing Instance;

        public static UnturnedPlayer murderer3;

        protected override void Load()
        {
            Instance = this;
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += onplayerdeath;
        }

        protected override void Unload()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath -= onplayerdeath;
        }

        private void FixedUpdate()
        {

        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList() {
                };
            }
        }

        private void onplayerdeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            murderer3 = UnturnedPlayer.FromCSteamID(murderer);
            if (Configuration.Instance.ShowInChat == true)
            {
                try {
                        if (murderer3.GodMode == true)
                        {
                            UnturnedChat.Say(player.CharacterName + " Died by a player in godmode ABUSER: " + murderer3.CharacterName, UnturnedChat.GetColorFromName(Configuration.Instance.Color, Color.green));
                        }
                        else
                        {
                            Logger.Log(player.CharacterName + " Died by a player not in godmode");
                        }
                    }
                    catch (Exception e)
                    {
                        if (Configuration.Instance.debug == true)
                        {
                            Logger.LogException(e);
                        }
                    }
                }
            else
            {
                Logger.Log("Chat is disabled to show messages.");
            }
        }
    }
}