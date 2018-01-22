using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests
{
  public class AutoMoqDataAttribute : AutoDataAttribute
  {
    public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
  }
}
