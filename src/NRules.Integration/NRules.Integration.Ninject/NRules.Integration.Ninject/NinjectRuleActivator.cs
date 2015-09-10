﻿using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Syntax;
using NRules.Fluent;
using NRules.Fluent.Dsl;

namespace NRules.Integration.Ninject
{
    /// <summary>
    /// Rule activator that uses Ninject DI container.
    /// </summary>
    public class NinjectRuleActivator : IRuleActivator
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectRuleActivator(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public IEnumerable<Rule> Activate(Type type)
        {
            var rules = _resolutionRoot.GetAll(type);
            bool resolved = false;
            foreach (Rule rule in rules)
            {
                resolved = true;
                yield return rule;
            }
            if (!resolved) yield return ActivateDefault(type);
        }

        private static Rule ActivateDefault(Type type)
        {
            return (Rule)Activator.CreateInstance(type);
        }
    }
}
