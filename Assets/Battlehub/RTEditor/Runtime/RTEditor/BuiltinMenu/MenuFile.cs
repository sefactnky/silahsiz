﻿using Battlehub.RTCommon;
using Battlehub.UIControls.MenuControl;
using UnityEngine;

namespace Battlehub.RTEditor
{
    [MenuDefinition(order:-90)]
    public class MenuFile : MonoBehaviour
    {
        private IRuntimeEditor Editor
        {
            get { return IOC.Resolve<IRuntimeEditor>(); }
        }

        [MenuCommand("MenuFile/New Scene", "RTE_NewScene", priority: 10)]
        public void NewScene()
        {
            Editor.NewScene();
        }

        [MenuCommand("MenuFile/Save Scene", "RTE_Save", priority: 20)]
        public void SaveScene()
        {
            Editor.SaveScene();
        }

        [MenuCommand("MenuFile/Save Scene As...", "RTE_Dialog_SaveAs", priority: 30)]
        public void SaveSceneAs()
        {
            Editor.SaveSceneAs();
        }

        [MenuCommand("MenuFile/Import Assets", "RTE_Dialog_Import", priority:40)]
        public void ImportAssets()
        {
            Editor.CreateOrActivateWindow(BuiltInWindowNames.SelectAssetLibrary);
        }


        #if !UNITY_WEBGL
        [MenuCommand("MenuFile/Import From File", "RTE_Dialog_ImportFile", priority:50)]
        #endif
        public void ImportFromFile()
        {
            Editor.CreateOrActivateWindow(BuiltInWindowNames.ImportFile);
        }

        [MenuCommand("MenuFile/Manage Projects", "RTE_Dialog_OpenProject", priority:60)]
        public void ManageProjects()
        {
            Editor.CreateOrActivateWindow(BuiltInWindowNames.OpenProject);
        }

        [MenuCommand("MenuFile/Close", "", priority:70)]
        public void Close()
        {
            Editor.Close();
        }
    }
}


