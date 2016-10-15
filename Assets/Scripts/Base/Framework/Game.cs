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

        public Router Router = new Router();

        private bool hasInitialized;

        private readonly WaitForSeconds HEART_BEAT_DELAY = new WaitForSeconds(1f);

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            OnInit();
            hasInitialized = true;
            StartCoroutine(SlowUpdate());
        }
        void OnDestroy()
        {
            StopCoroutine(SlowUpdate());
            OnDispose();
            instance = null;
        }

        void Update()
        {
            if (!hasInitialized) return;
            OnUpdate();
        }
        IEnumerator SlowUpdate()
        {
            while(true)
            {
                if (!hasInitialized) continue;
                OnHeartBeat();
                yield return HEART_BEAT_DELAY;
            }
        }
        
        public abstract void OnInit();
        public abstract void OnUpdate();
        public abstract void OnHeartBeat();
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