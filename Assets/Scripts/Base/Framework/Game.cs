using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace Base
{
    public class Game  
    {
        public Router Router;
        
        public virtual void Start()
        {
            Router = new Router();
        }

        public virtual void Update()
        {
        }

        public virtual void HeartBeat()
        {
        }

        public virtual void End()
        {
        }

        #region Stage Management

        public Stage CurrentStage { get; private set; }
        public StageEnum LoadingStageEnum { get; private set; }

        public StageEnum CurrentStageType
        {
            get { return CurrentStage.Type; }
        }

        #endregion
    }
}