using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


namespace Base
{
    public abstract class Game : MonoBehaviour
    {
        public static Game instance;
        public static Game Instance
        {
            get
            {
                return instance as Game;
            }
        }

        protected Router router;

        protected bool hasInitialized;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            router = Router.Instance;

            OnInit();
            hasInitialized = true;
        }
        void OnDestroy()
        {
            OnDispose();
            instance = null;
        }

        void Update()
        {
            if (!hasInitialized) return;
            OnUpdate(Time.deltaTime);
        }
        
        public abstract void OnInit();
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnDispose();

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