using UnityEngine;

namespace LSTools
{
    // initial version by @ProGM via https://gist.github.com/ProGM/9cb9ae1f7c8c2a4bd3873e4df14a6687
    public abstract class AStringListAttribute : PropertyAttribute
    {
        public AStringListAttribute(params string[] list)
        {
            List = list;
        }

        public string[] List
        {
            get;
            private set;
        }

#if UNITY_EDITOR
        public abstract string[] GetList();
#endif
    }
}