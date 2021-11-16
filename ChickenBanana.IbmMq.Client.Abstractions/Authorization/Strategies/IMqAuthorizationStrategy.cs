using System.Collections;

namespace ChickenBanana.IbmMq.Client
{
    public interface IMqAuthorizationStrategy
    {
        public void Apply(Hashtable options);
    }
}
