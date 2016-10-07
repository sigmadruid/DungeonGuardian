using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections.Generic;

namespace Base
{
    public class StageManager : BaseManager
    {
        private Dictionary<StageEnum, Stage> stageDic = new Dictionary<StageEnum, Stage>();

        public override void Init()
        {
            base.Init();
            SceneManager.sceneLoaded += OnStageLoaded;
            SceneManager.sceneUnloaded += OnStageUnloaded;
        }

        public override void Dispose()
        {
            base.Dispose();
            SceneManager.sceneLoaded -= OnStageLoaded;
            SceneManager.sceneUnloaded -= OnStageUnloaded;
        }

        public void AddStage(Stage stage)
        {
            if (!stageDic.ContainsKey(stage.Type))
            {
                stageDic[stage.Type] = stage;
            }
        }

        private void OnStageLoaded(Scene scene, LoadSceneMode mode)
        {
            StageEnum stageEnum = ParseToStageEnum(scene.name);
            if (stageDic.ContainsKey(stageEnum))
            {
                Stage stage = stageDic[stageEnum];
                stage.Enter();
            }
            else
            {
                BaseLogger.LogError("Stage {0} doesn't exist!", stageEnum);
            }
        }
        private void OnStageUnloaded(Scene scene)
        {
            StageEnum stageEnum = ParseToStageEnum(scene.name);
            if (stageDic.ContainsKey(stageEnum))
            {
                Stage stage = stageDic[stageEnum];
                stage.Exit();
            }
            else
            {
                BaseLogger.LogError("Stage {0} doesn't exist!", stageEnum);
            }
        }

        private StageEnum ParseToStageEnum(string stageName)
        {
            StageEnum stageEnum = (StageEnum)Enum.Parse(typeof(StageEnum), stageName);
            return stageEnum;
        }
    }
}

