﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using NUnit.Framework;

namespace Octostache.Tests
{
    [TestFixture]
    public abstract class BaseFixture
    {
        [SetUp]
        public void Setup()
        {
            //The TemplateParser Cache is retained between tests. A little hackery to clear it.
            var parser = typeof(VariableDictionary).Assembly.GetType("Octostache.Templates.TemplateParser");
            var cache = (MemoryCache)parser.GetField("Cache", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            foreach (var item in cache)
            {
                cache.Remove(item.Key);
            }
        }

        protected string Evaluate(string template, IDictionary<string, string> variables, bool haltOnError = true)
        {
            var dictionary = new VariableDictionary();
            foreach (var pair in variables)
            {
                dictionary[pair.Key] = pair.Value;
            }
            string error;
            return dictionary.Evaluate(template, out error, haltOnError);
        }

        protected VariableDictionary ParseVariables(string variableDefinitions)
        {
            var variables = new VariableDictionary();

            var items = variableDefinitions.Split(';');
            foreach (var item in items)
            {
                var pair = item.Split('=');
                var key = pair.First();
                var value = pair.Last();
                variables[key] = value;
            }

            return variables;
        }
    }
}
