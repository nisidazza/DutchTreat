using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Principal;

namespace DutchTreatXUnitTests.FakeContext
{
    class ConfigureFakeIdentity : IContextConfigAction
    {
        string _username;

        public ConfigureFakeIdentity(string username)
        {
            _username = username;
        }

        public void Config(DefaultHttpContext fakeContext)
        {
            var fakeIdentity = new GenericIdentity(_username);
            var principal = new GenericPrincipal(fakeIdentity, null);
            fakeContext.User = principal;
        }
    }
}