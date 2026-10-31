using System;
using BepInEx;
using UnityEngine;

namespace GOIPracticeMod
{
    [BepInPlugin("com.practice.savestate", "Getting Over It Practice & Savestate Mod", "1.0.0")]
    public class SavestatePlugin : BaseUnityPlugin
    {
        private SaveState savedState = null;
        private string hudBannerText = "";
        private float hudBannerTimer = 0f;
        private Color hudBannerColor = Color.green;
        private bool showHud = true;
        private GUIStyle bannerStyle;
        private GUIStyle helpStyle;

        void Awake()
        {
            Logger.LogInfo("=================================================");
            Logger.LogInfo(" GOI Practice & Savestate Mod Initialized!       ");
            Logger.LogInfo(" Keys: [F5] Save Checkpoint | [F6] Teleport Back ");
            Logger.LogInfo("=================================================");
        }

        void Update()
        {
            if (hudBannerTimer > 0f)
            {
                hudBannerTimer -= Time.deltaTime;
            }

            // Key [F5] - Save Checkpoint
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveCheckpoint();
            }

            // Key [F6] - Load / Teleport Checkpoint
            if (Input.GetKeyDown(KeyCode.F6))
            {
                LoadCheckpoint();
            }

            // Key [F7] - Toggle on-screen HUD
            if (Input.GetKeyDown(KeyCode.F7))
            {
                showHud = !showHud;
                ShowBanner(showHud ? "HUD: Enabled" : "HUD: Disabled", Color.cyan, 1.5f);
            }
        }

        private void SaveCheckpoint()
        {
            Saviour saviour = FindObjectOfType<Saviour>();
            if (saviour != null)
            {
                savedState = saviour.Save();
                float altitude = savedState.playerPos.y;
                ShowBanner(string.Format("CHECKPOINT SAVED! (Altitude: {0:F1}m)", altitude), Color.green, 3.0f);
                Logger.LogInfo(string.Format("Checkpoint saved at ({0:F2}, {1:F2})", savedState.playerPos.x, savedState.playerPos.y));
            }
            else
            {
                ShowBanner("Failed to save: Saviour instance not found in scene.", Color.red, 2.5f);
            }
        }

        private void LoadCheckpoint()
        {
            if (savedState == null)
            {
                ShowBanner("NO CHECKPOINT SAVED YET! Press [F5] first to set one.", Color.yellow, 3.0f);
                return;
            }

            Saviour saviour = FindObjectOfType<Saviour>();
            if (saviour != null)
            {
                saviour.Load(savedState);

                // Zero out any residual fall velocity on rigidbodies so Diogenes lands smoothly
                Rigidbody2D[] rbs = FindObjectsOfType<Rigidbody2D>();
                if (rbs != null)
                {
                    for (int i = 0; i < rbs.Length; i++)
                    {
                        if (rbs[i] != null)
                        {
                            rbs[i].velocity = Vector2.zero;
                            rbs[i].angularVelocity = 0f;
                        }
                    }
                }

                ShowBanner("TELEPORTED TO CHECKPOINT!", Color.cyan, 2.5f);
                Logger.LogInfo("Teleported player back to saved checkpoint.");
            }
            else
            {
                ShowBanner("Failed to teleport: Saviour instance not found.", Color.red, 2.5f);
            }
        }

        private void ShowBanner(string text, Color color, float duration)
        {
            hudBannerText = text;
            hudBannerColor = color;
            hudBannerTimer = duration;
        }

        void OnGUI()
        {
            if (!showHud) return;

            if (bannerStyle == null)
            {
                bannerStyle = new GUIStyle(GUI.skin.box);
                bannerStyle.fontSize = 20;
                bannerStyle.fontStyle = FontStyle.Bold;
                bannerStyle.alignment = TextAnchor.MiddleCenter;

                helpStyle = new GUIStyle(GUI.skin.label);
                helpStyle.fontSize = 14;
                helpStyle.fontStyle = FontStyle.Bold;
                helpStyle.normal.textColor = new Color(1f, 1f, 1f, 0.85f);
            }

            // Top-left controls reminder
            GUI.Label(new Rect(15, 12, 550, 25), "[F5] Save Checkpoint  |  [F6] Teleport Back  |  [F7] Toggle HUD", helpStyle);

            // Center notification banner when saving/loading
            if (hudBannerTimer > 0f && !string.IsNullOrEmpty(hudBannerText))
            {
                bannerStyle.normal.textColor = hudBannerColor;
                float width = 520;
                float height = 50;
                float x = (Screen.width - width) / 2f;
                float y = 50;
                GUI.Box(new Rect(x, y, width, height), hudBannerText, bannerStyle);
            }
        }
    }
}
