namespace dev2k {
    using UnityEngine;

    public static class ObjectExtension {
        public static T Instantiate<T>(this T selfObj) where T : Object {
            return Object.Instantiate(selfObj);
        }

        public static T Name<T>(this T selfObj, string name) where T : Object {
            selfObj.name = name;
            return selfObj;
        }

        public static void DestroySelf<T>(this T selfObj) where T : Object {
            Object.Destroy(selfObj);
        }

        public static T DestroySelfGracefully<T>(this T selfObj) where T : Object {
            if (selfObj) {
                Object.Destroy(selfObj);
            }

            return selfObj;
        }

        public static T As<T>(this Object selfObj) where T : Object {
            return selfObj as T;
        }
    }
}