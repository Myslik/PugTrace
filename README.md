# PugTrace

[![Build status](https://ci.appveyor.com/api/projects/status/ehis2hf6kbd1p9m9/branch/master?svg=true)](https://ci.appveyor.com/project/Myslik/pugtrace/branch/master)
[![LicenseMIT](https://img.shields.io/badge/license-MIT-green.svg)](http://opensource.org/licenses/MIT)

Application for easy tracing on .NET platform. Inspired by [elmah](https://code.google.com/p/elmah/) and [Hangfire](http://hangfire.io/).

![PugTrace Dashboard](http://s29.postimg.org/mn5oj37bb/Dashboard.png)

Installation
-------------

Hangfire is available as a NuGet package. So, you can install it using the NuGet Package Console window:

```
PM> Install-Package PugTrace.SqlServer
```

After install, update your existing [OWIN Startup](http://www.asp.net/aspnet/overview/owin-and-katana/owin-startup-class-detection) file with the following lines of code. If you do not have this class in your project or don't know what is it, please read the [Quick start](http://docs.hangfire.io/en/latest/quickstart.html) guide to learn about how to install Hangfire.

```csharp
using PugTrace;
using PugTrace.SqlServer;

public void Configuration(IAppBuilder app)
{
    GlobalConfiguration.Configuration.UseSqlServerStorage("<connection string or its name>");
    app.UsePugTraceDashboard();
}
```

Then you need to configure `System.Diagnostics` to use PugTrace listener.

```xml
<system.diagnostics>
  <sharedListeners>
    <add name="sqldatabase" 
         type="PugTrace.SqlServer.SqlServerTraceListener, PugTrace.SqlServer"
         initializeData="<connection string or its name>"
         applicationName="<application name>" />
  </sharedListeners>
  <sources>
    <source name="Application" switchValue="All">
      <listeners>
        <clear />
        <add name="sqldatabase" />
      </listeners>
    </source>
  </sources>
</system.diagnostics>
```

Example
-------

You can then log your traces like this.

```csharp
TraceSource source = new TraceSource("Application");
source.TraceInformation("Hello world!");
source.Flush();
```
