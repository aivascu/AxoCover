﻿using AxoCover.Models.Commands;
using AxoCover.Models.Extensions;
using AxoCover.Models.TestCaseProcessors;
using Microsoft.Practices.Unity;
using System.Reflection;

namespace AxoCover.Models
{
  public static class ContainerProvider
  {
    public static UnityContainer Container;

    public static void Initialize()
    {
      if (Container != null)
      {
        return;
      }

      Assembly.LoadFrom(AdapterExtensions.GetTestPlatformPath());

      Container = new UnityContainer();
      RegisterTypes();
    }

    private static void RegisterTypes()
    {
      Container.RegisterType<ITestCaseProcessor, XUnitTestCaseProcessor>("xUnit");
      Container.RegisterType<ITestCaseProcessor, NUnitTestCaseProcessor>("NUnit");
      Container.RegisterType<ITestProvider, TestProvider>(new ContainerControlledLifetimeManager());
      Container.RegisterType<IEditorContext, EditorContext>(new ContainerControlledLifetimeManager());
      Container.RegisterType<ITestRunner, AxoTestRunner>("axo");
      Container.RegisterType<ITestRunner, MultiplexedTestRunner>(new ContainerControlledLifetimeManager());
      Container.RegisterType<ICoverageProvider, CoverageProvider>(new ContainerControlledLifetimeManager());
      Container.RegisterType<IResultProvider, ResultProvider>(new ContainerControlledLifetimeManager());
      Container.RegisterType<IStorageController, StorageController>(new ContainerControlledLifetimeManager());
      Container.RegisterType<IReportProvider, ReportProvider>(new ContainerControlledLifetimeManager());
      Container.RegisterType<ITelemetryManager, HockeyClient>(new ContainerControlledLifetimeManager());
      Container.RegisterInstance(new SelectTestCommand());
      Container.RegisterInstance(new JumpToTestCommand());
      Container.RegisterInstance(new DebugTestCommand());
    }
  }
}
