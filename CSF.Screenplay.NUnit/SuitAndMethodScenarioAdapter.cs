﻿using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace CSF.Screenplay.NUnit
{
  public class SuitAndMethodScenarioAdapter : IScenarioAdapter
  {
    readonly ITest featureSuite;
    readonly IMethodInfo scenarioMethod;

    /// <summary>
    /// Gets the name of the scenario.
    /// </summary>
    /// <value>The name of the scenario.</value>
    public string ScenarioName
    {
      get {
        var method = scenarioMethod.MethodInfo;

        if(method == null)
          return null;

        return GetDescription(method);
      }
    }

    /// <summary>
    /// Gets the scenario identifier.
    /// </summary>
    /// <value>The scenario identifier.</value>
    public string ScenarioId => $"{FeatureId}.{scenarioMethod.Name}";

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    /// <value>The name of the feature.</value>
    public string FeatureName
    {
      get {
        var fixtureType = scenarioMethod.MethodInfo.DeclaringType;

        if(fixtureType == null)
          return null;

        return GetDescription(fixtureType);
      }
    }

    /// <summary>
    /// Gets the feature identifier.
    /// </summary>
    /// <value>The feature identifier.</value>
    public string FeatureId => featureSuite.FullName;

    string GetDescription(MemberInfo member)
    {
      var attrib = member.GetCustomAttribute<DescriptionAttribute>();

      if(attrib == null)
        return member.Name;

      var prop = attrib.Properties.Get("Description");
      if(prop == null)
        return member.Name;

      return prop.ToString();
    }

    public SuitAndMethodScenarioAdapter(ITest featureSuite, IMethodInfo scenarioMethod)
    {
      if(scenarioMethod == null)
        throw new ArgumentNullException(nameof(scenarioMethod));
      if(featureSuite == null)
        throw new ArgumentNullException(nameof(featureSuite));
      
      this.scenarioMethod = scenarioMethod;
      this.featureSuite = featureSuite;
    }
  }
}
