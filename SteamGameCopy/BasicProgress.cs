using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGameCopy
{
    class BasicProgress<T> : IProgress<T>
    {
        private readonly Action<T> _handler;

        public BasicProgress(Action<T> handler)
        {
            _handler = handler;
        }

        void IProgress<T>.Report(T value)
        {
            _handler(value);
        }
    }
}
