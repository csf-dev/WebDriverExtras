using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests
{
  public class HasValuesAttribute : CustomizeAttribute
  {
    readonly int howMany;

    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
      if(parameter?.ParameterType != typeof(Dictionary<string,string>))
        return null;

      return new HasValuesCustomisation(howMany);
    }

    public HasValuesAttribute(int howMany = 3)
    {
      if(howMany < 0)
        throw new ArgumentOutOfRangeException(nameof(howMany), "Cannot fill with less than zero items");
      this.howMany = howMany;
    }

    class HasValuesCustomisation : ICustomization
    {
      readonly int howMany;

      public void Customize(IFixture fixture)
      {
        fixture.Customize<Dictionary<string,string>>(c => {
          return c
            .FromFactory(() => new Dictionary<string,string>())
            .OmitAutoProperties()
            .Do(x => {
              for(int i = 0; i < howMany; i++)
                x.Add(fixture.Create<string>(), fixture.Create<string>());
            });
        });
      }

      public HasValuesCustomisation(int howMany)
      {
        this.howMany = howMany;
      }
    }
  }

}
