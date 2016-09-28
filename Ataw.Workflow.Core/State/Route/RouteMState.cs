﻿namespace Ataw.Workflow.Core
{
    public class RouteMState : MState
    {
        public static readonly State Instance = new RouteMState();

        private RouteMState()
        {
        }

        public override string ToString()
        {
            return "路由步骤的错误状态";
        }
    }
}
