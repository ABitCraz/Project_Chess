// ******************************************************************
//       /\ /|       @file       FILENAME
//       \ V/        @brief      
//       | "")       @author     topkang
//       /  |                    
//      /  \\        @Modified   DATE
//    *(__\_\        @Copyright  Copyright (c) YEAR, TOPGAMING
// ******************************************************************

using Photon.Pun;
using UnityEngine;

namespace UnityTemplateProjects.Tools
{
    public class SingletonPunCallbacks<T> : MonoBehaviourPunCallbacks where T : SingletonPunCallbacks<T>
    {
        private static T instance;
 
        public static T GetInstance()
        {
            return instance;
        }

        protected virtual void Awake()
        {
            if(instance !=null )
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
            }
        }

        public static bool IsInitialized
        {
            get { return instance != null; }
        }

        protected void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}