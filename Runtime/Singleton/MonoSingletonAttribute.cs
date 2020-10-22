namespace dev2k {
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class MonoSingletonAttribute : Attribute {
        private string m_AbsolutePath;

        public MonoSingletonAttribute(string relativePath) {
            m_AbsolutePath = relativePath;
        }

        public string AbsolutePath {
            get { return m_AbsolutePath; }
        }
    }
}