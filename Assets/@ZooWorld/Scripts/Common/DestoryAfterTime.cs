using System.Collections;
using UnityEngine;

namespace ZooWorld.Scripts.Common
{
    public class DestoryAfterTime : MonoBehaviour
    {
        public float time = 2f;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

    }
}
