using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Interfaces
{
    public interface INetowrk : IMovement , IStatus
    {
        public event Action<List<object>> OnConnectEvent;
        public event Action<object> OnUpdateEvent;
        public event Action<object> OnDisconnectEvent;
        public void Connect(object c);
        
    }
}
