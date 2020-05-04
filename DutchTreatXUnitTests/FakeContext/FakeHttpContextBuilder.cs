using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;

namespace DutchTreatXUnitTests.FakeContext
{
    public class FakeHttpContextBuilder
    {
        List<IContextConfigAction> _actions;
        DefaultHttpContext _httpContext = new DefaultHttpContext();

        public FakeHttpContextBuilder()
        {
            _actions = new List<IContextConfigAction>();
        }

        public void Append(IContextConfigAction action)
        {
            _actions.Add(action);
        }

        public void Build(Controller controller)
        {
            foreach(var action in _actions)
            {
                action.Config(_httpContext);
            }
            controller.ControllerContext.HttpContext = _httpContext;
        }
    }
}
