using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DutchTreatXUnitTests.FakeContext
{
    public interface IContextConfigAction 
    {
        void Config(DefaultHttpContext fakeContext);
    }
}
