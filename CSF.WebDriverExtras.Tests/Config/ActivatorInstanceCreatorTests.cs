using System;
using CSF.WebDriverExtras.Config;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.Config
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ActivatorInstanceCreatorTests
  {
    [Test,AutoMoqData]
    public void CreateInstance_can_create_instance_of_a_type_with_a_parameterless_constructor(ActivatorInstanceCreator sut)
    {
      // Act
      var result = sut.CreateInstance(typeof(MySampleType));

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void CreateInstance_creates_instance_of_correct_type(ActivatorInstanceCreator sut)
    {
      // Act
      var result = sut.CreateInstance(typeof(MySampleType));

      // Assert
      Assert.That(result, Is.InstanceOf<MySampleType>());
    }

    class MySampleType
    {
      public string AProperty { get; set; }
    }
  }
}
